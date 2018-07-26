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
# Peptide Qrollup functions used in DAnTE            #
#                                                    #
# By Ashoka D. Polpitiya                             #
######################################################

QRollup.proteins <- function(Data, ProtInfo, minPresence=50, Top=33, topN=0,
                            Mean=FALSE, oneHitWonders=TRUE)
#
# Ashoka Polpitiya - June 2007
#
{
    ProtInfo <- unique(ProtInfo)
    minCountPerP <- 2
    minPresence <- minPresence/100
    Top <- Top/100
    protIPI <- ProtInfo[,2]
    MassTags <- ProtInfo[,1]
    uProtCounts <- table(protIPI)
    sigIPI <- names(uProtCounts[uProtCounts >= minCountPerP])
    restIPI <- names(uProtCounts[(uProtCounts < minCountPerP) & (uProtCounts > 0)])
    threshold <- round(dim(Data)[2] * minPresence)

    protData <- rep(numeric(0),dim(Data)[2]+1)
    singlePepProtData <- rep(numeric(0),dim(Data)[2]+1)

    proteinNames <- "empty"
    oneHitProtNames <- "empty"
    
    k = 1
    for (prot in 1:length(sigIPI))
    {
        #print(sigIPI[prot])
        pidx <- which(sigIPI[prot]==protIPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]
        if (is.matrix(currProtData))
            if (dim(currProtData)[1] > 1)
            {
                xPresenceCount <- rowSums(!is.na(currProtData))
                if (max(xPresenceCount, na.rm=TRUE) >= threshold)
                {
                    if (topN > 0)
                      pData <- protein.Trollup(currProtData, topN=topN, Mean=Mean)
                    else
                      pData <- protein.Qrollup(currProtData, top=Top, Mean=Mean)
                    protData <- rbind(protData,pData)
                    proteinNames[k] <- sigIPI[prot]
                    k = k + 1
                }
            }
    }
    rownames(protData) <- proteinNames

    if (oneHitWonders)
    {
        k = 1
        for (protIndex in 1:length(restIPI))
        {
            pidx <- which(restIPI[protIndex]==protIPI)
            data_idx <- is.element(row.names(Data),MassTags[pidx[1]])
            currProtData <- Data[data_idx,]
            xPresenceCount <- sum(!is.na(currProtData))
            if (xPresenceCount >= threshold)
            {
                singlePepProt <- c(PepCount=1,currProtData)
                singlePepProtData <- rbind(singlePepProtData,singlePepProt)
                oneHitProtNames[k] <- restIPI[protIndex]
                k = k + 1
            }
        }
        rownames(singlePepProtData) <- oneHitProtNames
        outProtData <- rbind(protData,singlePepProtData)
    }
    else
        outProtData <- protData

    return(outProtData)
}

#------------------------------------------------------------------
qrollup <- function(x, top, Mean)
{
    peps <- x
    N <- length(peps)
    peps <- peps[!is.na(peps)]
    peps <- peps[order(peps, decreasing=TRUE)]
    N <- ceiling(top*N)
    peps <- peps[1:N]
    
    if (Mean)
        proteinValue <- mean(peps,na.rm=T)
    else
        proteinValue <- median(peps,na.rm=T) # median may be better    
}

#------------------------------------------------------------------
protein.Qrollup <- function(Data, top=.33, Mean=FALSE)
{
    ColNames = colnames(Data)
    #proteinValue <- matrix(0,1,dim(Data)[2])
    proteinValue <- apply(Data, 2, qrollup, top, Mean)
    
    rollupScore <- rollup.score(Data, proteinValue, "pearson")
    
    proteinVal <- c(dim(Data)[1], rollupScore, proteinValue)
    
    names(proteinVal) <- c("PepCount", "QPRo", ColNames)
    return(proteinVal)
}

#------------------------------------------------------------------
protein.Trollup <- function(currSel, topN=3, Mean=FALSE)
{
    N <- dim(currSel)[1]
    totalAbundances <- rowSums(currSel, na.rm=TRUE)
    totalAbundances <- totalAbundances[order(totalAbundances, decreasing=TRUE)]
    if (topN <= length(totalAbundances))
      currSel <- currSel[names(totalAbundances[1:topN]),]

    if (is.null(dim(currSel)[1]))
        proteinVal <- currSel
    else
    {
        if (Mean)
            proteinVal <- apply(currSel, 2, mean, na.rm=TRUE)
        else
            proteinVal <- apply(currSel, 2, median, na.rm=TRUE)
    }

    rollupScore <- rollup.score(currSel, proteinVal, "pearson")

    proteinVal <- c(PepCount=N, Score=rollupScore, proteinVal) # append the column with peptide counts
    return(proteinVal)
}

#------------------------------------------------------------------
#protein.Qrollup <- function(Data, top=.33, Mean=FALSE)
#{
#    ColNames = colnames(Data)
#    proteinValue <- matrix(0,1,dim(Data)[2])
#    for (i in 1:dim(Data)[2])
#    {
#        peps <- Data[,i]
#        N <- length(peps)
#        peps <- peps[!is.na(peps)]
#        peps <- peps[order(peps, decreasing=TRUE)]
#        N <- ceiling(top*N)
#        peps <- peps[1:N]
#        
#        if (Mean)
#            proteinValue[i] <- mean(peps,na.rm=T)
#        else
#            proteinValue[i] <- median(peps,na.rm=T) # median may be better
#    }
#    proteinVal <- c(dim(Data)[1],proteinValue)
#    names(proteinVal) <- c("PepCount", ColNames)
#    return(proteinVal)
#}

#----------------------------------------------------------------------
#protein.Qrollup.2 <- function(Data, top=.33, Mean=FALSE)
#{
#    ColNames = colnames(Data)
#    proteinValue <- matrix(0,1,dim(Data)[2])
#    for (i in 1:dim(Data)[2])
#    {
#        peps <- Data[,i]
#        baseline <- abs(min(peps,na.rm=TRUE)) + 1 # -ve values can skew results
#        peps <- peps + baseline
#        thres <- (1-top)*max(peps,na.rm=TRUE)
#        peps <- peps[peps>thres]
#            peps <- peps - baseline # Switch back to originals
#        if (Mean)
#            proteinValue[i] <- mean(peps,na.rm=T)
#        else
#            proteinValue[i] <- median(peps,na.rm=T) # median may be better
#    }
#    proteinVal <- c(dim(Data)[1],proteinValue)
#    names(proteinVal) <- c("PepCount", ColNames)
#    return(proteinVal)
#}
#
##------------------------------------------------------------------
#protein.Qrollup.1 <- function(Data, top=.33, Mean=FALSE)   # not used
#{
#    ColNames = colnames(Data)
#    proteinValue <- matrix(0,1,dim(Data)[2])
#    for (i in 1:dim(Data)[2])
#    {
#        peps <- Data[,i]
#        
#        if (sum(is.na(peps)) == length(peps))
#            proteinValue[i] <- NA
#        else
#        {
#            positive <- (sum(peps>0,na.rm=TRUE) > sum(peps<0,na.rm=TRUE))
#            negative <- (sum(peps>0,na.rm=TRUE) < sum(peps<0,na.rm=TRUE))
#            if (positive)
#            {
#                thres <- top*max(peps,na.rm=TRUE)
#                peps <- peps[peps > thres]
#            }
#            if (negative)
#            {
#                thres <- top*min(peps,na.rm=TRUE)
#                peps <- peps[peps < thres]
#            }
#            if (Mean)
#                proteinValue[i] <- mean(peps,na.rm=T)
#            else
#                proteinValue[i] <- median(peps,na.rm=T) # median may be better
#        }
#    }
#    
#    colnames(proteinValue) <- ColNames
#    return(proteinValue)
#}

