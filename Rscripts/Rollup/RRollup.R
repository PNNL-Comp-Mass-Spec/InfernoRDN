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
#############################################################
# Peptide rollup functions (reference based) used in DAnTE  #
#                                                           #
# By Ashoka D. Polpitiya                                    #
#############################################################

RRollup.proteins <- function(Data, ProtInfo, minPresence=50, Mode="median",
                        minOverlap=3, oneHitWonders=TRUE, outfolder="C:/",
                        plotflag=FALSE, gpvalue=0.05, gminPCount=5, center=TRUE)
#
# Ashoka Polpitiya - July 2007
#
{
    ProtInfo <- unique(ProtInfo)
    minCountPerP <- 2
    minPresence <- minPresence/100
    protIPI <- ProtInfo[,2]
    MassTags <- ProtInfo[,1]
    uProtCounts <- table(protIPI)
    sigIPI <- names(uProtCounts[uProtCounts >= minCountPerP])
    restIPI <- names(uProtCounts[(uProtCounts < minCountPerP) & (uProtCounts > 0)])

    threshold <- round(dim(Data)[2] * minPresence)

    scaledData <- rep(numeric(0),dim(Data)[2])
    orData <- rep(numeric(0),dim(Data)[2])
    protData <- rep(numeric(0),dim(Data)[2]+2)
    singlePepProtData <- rep(numeric(0),dim(Data)[2]+2)

    proteinNames <- "empty"
    oneHitProtNames <- "empty"
    
    k = 1
    for (prot in 1:length(sigIPI))
    {
        pidx <- which(sigIPI[prot]==protIPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]

        if (is.matrix(currProtData))
            if (dim(currProtData)[1] > 1)
            {
                xPresenceCount <- rowSums(!is.na(currProtData), na.rm=TRUE)
                if (max(xPresenceCount, na.rm=TRUE) >= threshold)
                {
                    pData <- protein.Rrollup(currProtData,minOverlap=minOverlap,
                             Mode=Mode, minPs=gminPCount, pvalue=gpvalue, center=center)
                    scaledData <- rbind(scaledData,pData$sData)
                    orData <- rbind(orData,pData$orData)
                    protData <- rbind(protData,pData$pData)
                    proteinNames[k] <- sigIPI[prot]

                    if (plotflag)
                    {
                        outfile = paste(outfolder,k,".png",sep="")
                        plotCurrProt.RefRup(currProtData, pData, file=outfile,IPI=sigIPI[prot])
                    }
                    k = k + 1
                }
            }
    }
    rownames(protData) <- proteinNames
    scaledData <- remove.duplicates(scaledData)
    orData <- remove.duplicates(orData)
    
    if (oneHitWonders)
    {
        k = 1
        for (prot in 1:length(restIPI))
        {
            pidx <- which(restIPI[prot]==protIPI)
            data_idx <- is.element(row.names(Data),MassTags[pidx[1]])
            currProtData <- Data[data_idx,]
            xPresenceCount <- sum(!is.na(currProtData))
            if (xPresenceCount >= threshold)
            {
                singlePepProt <- c(PepCount=1, Score=1, currProtData)
                singlePepProtData <- rbind(singlePepProtData,singlePepProt)
                oneHitProtNames[k] <- restIPI[prot]
                k = k + 1
            }
        }
        rownames(singlePepProtData) <- oneHitProtNames
        if (center)
        {
            singlePepProtData1 <- singlePepProtData[,-c(1,2)]
            scaledSinglePepProtData <- t(scale(t(singlePepProtData1),center=T,scale=F))
            singlePepProtData <- cbind(singlePepProtData[,c(1,2)],scaledSinglePepProtData)
        }
        outProtData <- rbind(protData,singlePepProtData)
    }
    else
        outProtData <- protData
    
    out <- list(sData=scaledData, orData=orData, pData=outProtData)
    return(out)
}

#------------------------------------------------------------------
protein.Rrollup <- function(currSel, minOverlap=3, Mode="median",
                        minPs=5, pvalue=0.05, center=TRUE)
{
    pepCounts <- rowSums(!is.na(currSel)) # presence of a peptide sample-wise
    pepMaxCounts <- which(pepCounts == max(pepCounts)) # maximally present ones
    if (length(pepMaxCounts) > 1)
    {
        possibleRefs <- currSel[pepMaxCounts,] # multiple refrence peptides?
        totalAbundances <- rowSums(possibleRefs, na.rm=TRUE) # pick the one with highest overall abundance
        refs <- which(totalAbundances == max(totalAbundances,na.rm=TRUE))
        reference <- pepMaxCounts[refs[1]]
    }
    else
    {
        reference <- pepMaxCounts[1]
    }

    currSelAdj <- t(t(currSel) - as.vector(t(currSel[reference,]))) #get ratios
    overlapCount <- rowSums(!is.na(currSelAdj)) # count the number of non-missing
    overlapMedians <- apply(currSelAdj,1,median,na.rm=T) # median ratios for each peptide
    overlapMedians[which(overlapCount < minOverlap)] <- 0 # get rid of medians for sparse peptides
    currSel <- currSel - overlapMedians # adjust the originals with median ratios

    orCurrSel <- remove.outliers(currSel, minPs=minPs, pvalue=pvalue)

    if (center)
    {
        scaled <- t(scale(t(currSel),center=T,scale=F))
        scaled.or <- t(scale(t(orCurrSel),center=T,scale=F))
    }
    else
    {
        scaled <- currSel
        scaled.or <- orCurrSel
    }

    if (Mode=="median")
        proteinVal <- apply(scaled.or, 2, median, na.rm=T) # rollup to proteins as medians
    else
        proteinVal <- apply(scaled.or, 2, mean, na.rm=T)

    rollupScore <- rollup.score(currSel, proteinVal, "pearson")

    proteinVal <- c(PepCount=dim(scaled.or)[1], QPRo=rollupScore, proteinVal) # append the column with peptide counts
    out <- list(sData=scaled, orData=scaled.or, pData=proteinVal)
    return(out)
}

#-----------------------------------------------------------------------------
remove.outliers <- function(Data, minPs=5, pvalue=0.05)
# internal function used by normalize.proteins()
# Calculates the protein abundances after removing outliers
# Depends on package "outliers"
{
    library(outliers)
    ColNames = colnames(Data)
    xPeptideCount <- colSums(!is.na(Data))
    for (i in 1:dim(Data)[2])
    {
        if (xPeptideCount[i] >= minPs)
        {
            repeat
            {
                grubbs <- grubbs.test(Data[,i]) # Grubb's test
                if ( (grubbs$p.value < pvalue) && (!is.nan(grubbs$statistic[2])) &&
                        (grubbs$statistic[2] != 0) ) # pass the p-value cutoff
                {
                    Data[,i] <- rm.outlier.1(Data[,i],fill=TRUE,median=TRUE)
                    # fill the outlier with the median value
                }
                else { break }
            }
        }
    }
    return(Data)
}


##############################################################################
plotCurrProt.RefRup <- function(currData,pdata,file="deleteme.png",
                            bkground="transparent",
                            IPI="IPI:IPI00009793.1")
# pdata : output from protein rollup (scale.proteins)
{
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
    #png(filename=file,width=1024,height=768,pointsize=12,bg=bkground,
    #        res=600)
    tryCatch(
    {
    scaledData <- pdata$sData
    protData <- pdata$pData
    orData <- pdata$orData

    mainT <- paste(IPI, "(QPRo=", protData[2], ")", sep=" ")
    
    par(mfrow=c(3,1))
    
    matplot(t(currData),type="b",main=mainT,ylab="Raw Data")
    matplot(t(scaledData),type="b",ylab="Scaled Data")
    matplot(t(orData),type="b",ylab="Scaled and Outlier removed",
            xlab=paste(dim(currData)[1],"Peptides",sep=" "))
    lines(protData[-c(1,2)],type="l",lwd=2)
    },
    interrupt = function(ex)
    {
      cat("An interrupt was detected.\n");
      print(ex);
    },
    error = function(ex)
    {
      cat("An error was detected.\n");
      print(ex);
    },
    finally =
    {
      cat("Releasing tempfile...");
      dev.off()
      cat("done\n");
    }) # tryCatch()
}
#------------------------------------------------------------------

