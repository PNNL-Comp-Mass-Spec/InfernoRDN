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
# Central Tendancy Adjustment

MeanCenter.Sub <- function(Data, Mean=TRUE, centerZero=TRUE)
{
    ndata <- Data

    # Compute the mean or median of each column in matrix Data, storing in vector Center
    if (Mean)
        Center <- apply(Data,2,mean,na.rm=TRUE)
    else
        Center <- apply(Data,2,median,na.rm=TRUE)

    # Transform vector Center into a matrix
    centerM <- matrix(Center,nrow=dim(Data)[1],ncol=dim(Data)[2], byrow=TRUE)
                        
    if (centerZero)
    {
        # For data in each column, subtract the
        # column mean or median from the values, 
        # centering at zero
        ndata <- Data - centerM
    }
    else
    {
        # For data in each column, subtract the
        # column mean or median from the values, 
        # centering at the maximum mean or median
        newAverage <- max(Center,na.rm=TRUE)
        ndata <- Data - centerM + newAverage
    }
    return(ndata)
}

######################## Mean center the data ###############
MeanCenter.Div <- function(Data, Mean=TRUE, centerZero=TRUE)
{
    ndata <- Data

    # Compute the mean or median of each column in matrix Data, storing in vector Center
    if (Mean)
        Center <- apply(Data,2,mean,na.rm=TRUE)
    else
        Center <- apply(Data,2,median,na.rm=TRUE)

    # Transform vector Center into a matrix
    centerM <- matrix(Center,nrow=dim(Data)[1],ncol=dim(Data)[2], byrow=TRUE)
    
    if (centerZero)
    {
        # For data in each column, divide the
        # values by the column mean or median, 
        # centering at zero
        ndata <- Data / centerM
    }
    else
    {
        # For data in each column, divide the
        # values by the column mean or median, 
        # centering at the maximum mean or median
        newAverage <- max(Center,na.rm=TRUE)
        ndata <- (Data / centerM) * newAverage
    }
    
    return(ndata)
}
