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
# Central Tendancy Adjustment

MeanCenter.Sub <- function(Data, Mean=TRUE, centerZero=TRUE)
{
    ndata <- Data

    if (Mean)
        Center <- apply(Data,2,mean,na.rm=TRUE)
    else
        Center <- apply(Data,2,median,na.rm=TRUE)

    centerM <- matrix(Center,nrow=dim(Data)[1],ncol=dim(Data)[2],
                        byrow=TRUE)
    if (centerZero)
        ndata <- Data - centerM
    else
    {
        newAverage <- max(Center,na.rm=TRUE)
        ndata <- Data - centerM + newAverage
    }
    return(ndata)
}

######################## Mean center the data ###############
MeanCenter.Div <- function(Data, Mean=TRUE, centerZero=TRUE)
{
    ndata <- Data

    if (Mean)
        Center <- apply(Data,2,mean,na.rm=TRUE)
    else
        Center <- apply(Data,2,median,na.rm=TRUE)

    centerM <- matrix(Center,nrow=dim(Data)[1],ncol=dim(Data)[2],
                        byrow=TRUE)
    if (centerZero)
        ndata <- Data / centerM
    else
    {
        newAverage <- max(Center,na.rm=TRUE)
        ndata <- (Data / centerM) * newAverage
    }
    return(ndata)
}
