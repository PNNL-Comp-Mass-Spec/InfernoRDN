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
######################################################
# Peptide scaling and rollup functions used in DAnTE #
#                                                    #
# By Ashoka D. Polpitiya                             #
######################################################

ZRollup.proteins <- function(Data, ProtInfo, minPresence=50, Mode="median",
                           minCountPerP=4, gpvalue=0.05, gminPCount=5,
                           plotflag=FALSE,outfolder="C:/",oneHitWonders=TRUE)
# This function scales peptides, remove outliers and find the protein
# abundance as the median of the peptide abundances.
#
# Inputs:
# ------
# Data :
#
# minPresence : how many non-NA's are allowed.
# minCountPerP : minimum number of peptides per protein.
# gpvalue : pvalue cutoff for the Grubbs outlier test.
# gminPCount : minimum peptides required for the outlier test.
#
# Depends on:
# ----------
# "outliers" package
# internal functions written below: scale.data(),
#   protein.Zrollup(), rm.outlier.1(), outlier.1()
#
# Output:
# ------
# A list: sData - Scaled data
#         orData - Outliers removed scaled data
#         pData - Protein abundances
#
# Ashoka Polpitiya - June 2007
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
    olRmData <- rep(numeric(0),dim(Data)[2])
    protData <- rep(numeric(0),dim(Data)[2]+1)
    singlePepProtData <- rep(numeric(0),dim(Data)[2]+1)

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
                xPresenceCount <- rowSums(!is.na(currProtData))
                if (max(xPresenceCount, na.rm=TRUE) >= threshold)
                {
                    #browser()
                    sData <- scale.data(currProtData)
                    pData <- protein.Zrollup(sData, Mode=Mode, minPs=gminPCount,
                                pvalue=gpvalue)
                    scaledData <- rbind(scaledData,sData)
                    olRmData <- rbind(olRmData,pData$orData)
                    protData <- rbind(protData,pData$proteinVals)

                    proteinNames[k] <- sigIPI[prot]

                    if (plotflag)
                    {
                        outfile = paste(outfolder,k,".png",sep="")
                        plotCurrProt.2(currProtData,sData,pData,
                                file=outfile,IPI=sigIPI[prot])
                    }
                    k = k + 1
                }
            }
    }
    rownames(protData) <- proteinNames

    if ((oneHitWonders) && (length(restIPI) > 0))
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
                singlePepProt <- c(PepCount=1,currProtData)
                #browser()
                singlePepProtData <- rbind(singlePepProtData,singlePepProt)
                oneHitProtNames[k] <- restIPI[prot]
                k = k + 1
            }
        }
        rownames(singlePepProtData) <- oneHitProtNames
        outProtData <- rbind(protData,singlePepProtData)
    }
    else
        outProtData <- protData

    scaledData <- remove.duplicates(scaledData)
    olRmData <- remove.duplicates(olRmData)
    out <- list(sData=scaledData, orData=olRmData, pData=outProtData)
    return(out)
}

#------------------------------------------------------------------
scale.data <- function(Data)
# internal function used by normalize.proteins()
{
    med <- apply(Data, 1, median, na.rm=T)
    Data <- Data - med #kronecker(matrix(1, 1, dim(Data)[2]), med)
    Data <- apply(Data, 1, function(y)y/sd(y,na.rm=T)) # divide by SDev
    return(t(Data))
}

#------------------------------------------------------------------
protein.Zrollup <- function(Data, Mode="median", minPs=5, pvalue=0.005)
# internal function used by normalize.proteins()
# Calculates the protein abundances after removing outliers
# Depends on package "outliers"
{
    library(outliers)
    
    ColNames = colnames(Data)
    proteinValue <- matrix(0,1,dim(Data)[2])
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
        if (Mode=="median")
            proteinValue[i] <- median(Data[,i],na.rm=T) # median may be better
        else
            proteinValue[i] <- mean(Data[,i],na.rm=T)
    }
    rollupScore <- rollup.score(Data, proteinValue, "pearson")
    proteinVal <- c(dim(Data)[1], rollupScore, proteinValue)
    names(proteinVal) <- c("PepCount", "QPRo", ColNames)
    out = list(orData=Data,proteinVals=proteinVal)
    # orData : Outlier Removed Data
    return(out)
}

#------------------------------------------------------------------
remove.duplicates <- function(Data)
{
     rowIDs <- rownames(Data)
     rowIDs <- rowIDs[rowIDs != ""]
     Data <- Data[!duplicated(rowIDs),]
     return(Data)
}

##############################################################################
plotCurrProt.2 <- function(currData,sData,pdata,file="deleteme.png",
                            bkground="transparent",
                            IPI="IPI:IPI00009793.1")
# pdata : output from protein rollup (scale.proteins)
{
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
    #png(filename=file,width=1024,height=768,pointsize=12,bg=bkground,
    #        res=600)
    
    mainT <- paste(IPI, "(QPRo=", pdata$proteinVals[2], ")", sep=" ")
    
    par(mfrow=c(3,1))
    tryCatch(
    {
        matplot(t(currData),type="b",main=mainT,ylab="Raw Data")
        matplot(t(sData),type="b",ylab="Scaled Data")
        matplot(t(pdata$orData),type="b",ylab="Scaled and Outlier removed",
                xlab=paste(dim(currData)[1],"Peptides",sep=" "))
        lines(pdata$proteinVals[-c(1,2)],type="l",lwd=2)
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


###############################################################################
# modified R outier functions from "outlier" package to handle missing
rm.outlier.1 <- function (x, fill = FALSE, median = FALSE,
                         opposite = FALSE,
                         na.rm = TRUE)
{
    if (is.matrix(x))
        apply(x, 2, rm.outlier.1, fill = fill, median = median,
            opposite = opposite, na.rm = na.rm)
    else if (is.data.frame(x))
        as.data.frame(sapply(x, rm.outlier.1, fill = fill, median = median,
            opposite = opposite, na.rm = na.rm))
    else {
        res <- x
        if (!fill)
            res[-which(x == outlier.1(x, opposite))]
        else {
            if (median)
                res[which(x == outlier.1(x, opposite))] <- median(x[-which(x ==
                  outlier.1(x, opposite))], na.rm = na.rm)
            else res[which(x == outlier.1(x, opposite))] <- mean(x[-which(x ==
                outlier.1(x, opposite))], na.rm = na.rm)
            res
        }
    }
}

# modified R outier functions to handle missing
outlier.1 <- function (x, opposite = FALSE, logical = FALSE, na.rm = TRUE)
{
    if (is.matrix(x))
        apply(x, 2, outlier.1, opposite = opposite, logical = logical)
    else if (is.data.frame(x))
        sapply(x, outlier.1, opposite = opposite, logical = logical)
    else {
        if (xor(((max(x, na.rm = na.rm) - mean(x, na.rm = na.rm)) <
            (mean(x, na.rm = na.rm) - min(x, na.rm = na.rm))), opposite)) {
            if (!logical)
                min(x, na.rm = na.rm)
            else x == min(x, na.rm = na.rm)
        }
        else {
            if (!logical)
                max(x, na.rm = na.rm)
            else x == max(x, na.rm = na.rm)
        }
    }
}
###############################################################################
