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
# To make things faster, divide Data in to N chunks and
# do wknnimpute on each
KNNWsplitImpute <- function(Data, k=10, N=1000)
{
    retval <- rep(numeric(0),dim(Data)[2])
    T <- round((dim(Data)[1])/N)
    for (i in 1:T)
    {
        if (N*(i+1) > dim(Data)[1])
            Last <- dim(Data)[1]
        else
            Last <- N*i
        range1 <- (N*i-(N-1)):Last
        #browser()
        tmpData <- Data[range1,]
        tmpData <- KNNWimpute(tmpData, k=k)
        retval <- rbind(retval,tmpData)
    }
    return(retval)
}

#--------------------------------------------------------------------
# This is the core routine to do wknnimpute
## KNNimpute algorithm as described in Troyanskaya 2001.
## Missing values will be replaced by averging over the k-nearest neighbours
## Same as knn, but values averaged are weighted by the distance to the neighbours
KNNWimpute <- function(Data, k=10)
{
    datatmp <- Data
    for(i in 1:dim(Data)[[1]]) {
        if (any(is.na(Data[i,]))){
            dt1 <- matrix(Data[i,],nrow = dim(Data)[[1]],ncol = dim(Data)[[2]],byrow =TRUE)
            pept.dist   <- sqrt(t(apply((dt1 - datatmp)^2,1,sumifobs, na.rm =TRUE)))
            w <- sort(pept.dist)[2:(1+k)]
            W <- 1/w
            W <- W/sum(W, na.rm = TRUE)
            Data[i, is.na(Data[i,])] <- apply(as.matrix(W*Data[order(pept.dist)[2:(1+k)],is.na(Data[i,])]), 2, sum, na.rm =  TRUE)
        }
    }
    return(Data)
}


#--------------------------------------------------------------------
# This fixes a bug where peptides with no matching value would be defaulted to zeo
# Thanks to Jonathon O'Brien for both finding the bug and providing the fix 
sumifobs<-function(vec)
{
  n.obs<-sum(!(is.na(vec)))
  total<-sum(vec,na.rm=T)
  if (n.obs==0){total<-NA}
  return(total)
}

