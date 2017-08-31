# Written by Ashoka D. Polpitiya
# for the Translational Genomics Research Institute (TGen, Phoenix, AZ)
# Copyright 2010, Translational Genomics Research Institute
# E-mail: ashoka@tgen.org
#         proteomics@pnnl.gov
# Website: https://github.com/PNNL-Comp-Mass-Spec/InfernoRDN
# -------------------------------------------------------------------------
#
# Licensed under the Apache License, Version 2.0; you may not use this file except
# in compliance with the License.  You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
#
# -------------------------------------------------------------------------

# Do the Loess normalization on replicates
loess_normalize <- function(Data, replicates, span=0.2, plotflag=FALSE,
                            reference=1,folder="C:/")
{
  Nreps <- unique(as.vector(t(replicates)))
  
  for (i in 1:length(Nreps)) # for each unique sample
  {
    idx <- which(replicates == Nreps[i])
    dataset <- Data[,idx] # extract data for sample i with all the replicates

    if (length(idx) > 1) # do LOESS
    {
      fittedData <- doLOESSreplicates(dataset, sp=span, plotflag,
                                    folder=folder, reference=reference)
      Data[,idx] <- fittedData
    }
  }# for
  return(Data)
}# function

#------------------------------------------------------------------
doLOESSreplicates <- function(dataset, sp=0.2, plotflag=FALSE,
                                folder="C:/", reference=1)
{
  fittedData <- dataset
  header <- colnames(dataset)

  if (reference == 1) # pick the first dataset
  {
    print("LOESS: First set")
    refset <- 1
    indexSet <- dataset[,refset]
    set2normalize <- 1:dim(dataset)[2]
    set2normalize <- set2normalize[set2normalize != refset]
    idxSetName <- header[refset]
  }
  if (reference == 2) # median
  {
    print("LOESS: Median")
    indexSet <- apply(dataset,1,"median",na.rm=TRUE)
    set2normalize <- 1:dim(dataset)[2]
    idxSetName <- "Median"
  }
  if (reference == 3) # least missing
  {
    print("LOESS: Least missing")
    xx <- 1:dim(dataset)[2]
    missTotal <- colSums(is.na(dataset))
    refset <- xx[missTotal==min(missTotal,na.rm=TRUE)]
    refset <- refset[1]
    indexSet <- dataset[,refset]
    set2normalize <- 1:dim(dataset)[2]
    set2normalize <- set2normalize[set2normalize != refset]
    idxSetName <- header[refset]
  }

  for (cycle in set2normalize)
  {
    #browser()
    matchSet <- dataset[,cycle]
    Mean <- (indexSet + matchSet)/2
    Diff <- indexSet - matchSet
    #LOESS<-loess(Diff~Mean, family="gaussian", span=sp) # based on positions both numeric
    LOESS<-loess(Diff~Mean, family="symmetric", span=sp) # based on positions both numeric
    FIT <- LOESS$fit

    # handle missing values
    positionsbothnumeric <- !is.na(matchSet+indexSet)
    positionsbothnumeric[is.na(positionsbothnumeric)] <- FALSE
    missingindex <- is.na(indexSet) # missing in indexSet
    missing <- matchSet[missingindex] # corresponding entries in match Set
    nas <- array(NA,dim=length(missing))
    presentInMatchSet <- missing[!is.na(missing)] # present in matchSet but missing in indexSet
    doPredict = (length(presentInMatchSet) > 0)
    if (doPredict)
    {
        predict(LOESS,presentInMatchSet) -> nas[!is.na(missing)] # estimate them
    }
    fittedMatchSet <- matchSet
    fittedMatchSet[positionsbothnumeric] <- matchSet[positionsbothnumeric] + FIT
    if (doPredict)
    {
        fittedMatchSet[missingindex] <- matchSet[missingindex] + nas
    }
    else
        fittedMatchSet[missingindex] <- matchSet[missingindex]

    fittedMean <- (indexSet + fittedMatchSet)/2
    fittedDiff <- indexSet - fittedMatchSet

    fittedData[,cycle] <- fittedMatchSet

    if (plotflag)
    {
      #browser()
      pic1<-paste(folder, header[cycle],"_Loess.png",sep="")
      pic2<-paste(folder, header[cycle],"_LoessFitted.png",sep="")
      png(filename=pic1, width=1024, height=768, pointsize=12, bg="white", units="px")
      #require(Cairo)
      #CairoPNG(filename=pic1,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg="white",res=600)
      plot(Mean,Diff, pch=21,bg="green", xlab="MEAN (A)",
        ylab="DIFF (M)", main=paste("M-A Plot (",idxSetName," - ",
        header[cycle],")",sep=""))
      points(na.omit(Mean),LOESS$fit,col=2,pch=20)
      abline(h=0, col=1, lwd=1)
      dev.off()

      png(filename=pic2, width=1024, height=768, pointsize=12, bg="white", units="px")
      plot(fittedMean,fittedDiff, pch=21,bg="purple", xlab="MEAN (A)",
        ylab="DIFF (M)", main=paste("M-A Plot after Loess (",
        idxSetName," - ", header[cycle],")",sep=""))
      abline(h=0, col=1)
      dev.off()
    } # if
  } # for
  return(fittedData)
}
