# Written by Ashoka D. Polpitiya
# for the Translational Genomics Research Institute (TGen, Phoenix, AZ)
# Copyright 2010, Translational Genomics Research Institute
# E-mail: ashoka@tgen.org
# Website: http://inferno4proteomics.googlecode.com
# -------------------------------------------------------------------------
#
# Licensed under the Apache License, Version 2.0; you may not use this file except
# in compliance with the License.  You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
#
# -------------------------------------------------------------------------
######################### Global intensity normalization #####

########################### MAD ##################################
adjustMAD <- function(Data,
                       Factor = 1,
                       meanadjust=FALSE)
{
    if (length(Factor) == 1)
        Data <- MADdata(Data, meanadjust=meanadjust)
    else
    {
        Nreps <- unique(as.vector(t(Factor)))
        for (i in 1:length(Nreps)) # for each unique factor level
        {
            idx <- which(Factor == Nreps[i])
            dataset <- Data[,idx] # extract data for factor i
            if (length(idx) > 1) # MAD
            {
                madData <- MADdata(dataset, meanadjust=meanadjust)
                Data[,idx] <- madData
            }
        }
    }
    return(Data)
}

#-----------------------------------------------------------------

MADdata <- function(Data, meanadjust=FALSE)
{
    if (meanadjust)
    {
        colmeans <- colMeans(Data, na.rm=TRUE)
        Data <- Data - colmeans
    }
    colMad <- apply(Data,2,"mad",na.rm=TRUE)
    GMmad <- exp(mean(log(colMad),na.rm=TRUE)) # geometric mean

    for (i in 1:dim(Data)[2])
    {
        Data[,i] <- Data[,i] / (mad(Data[,i],na.rm=TRUE)/GMmad)
    }
    if (meanadjust)
        return(Data + colmeans)
    else
        return(Data)
}
