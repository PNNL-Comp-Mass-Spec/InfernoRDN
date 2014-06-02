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
# Filter data based on a cutoff for missing values


filterMissing <- function(Data,cutoff=20)
{
    cutoff <- cutoff/100
    allmissIdx <- apply(is.na(Data),1,sum) == dim(Data)[2]
    missingData <- Data[allmissIdx,,drop=FALSE]
    okData <- Data[!allmissIdx,]
    index <- apply(okData, 1, function(x,threshold) ((sum(is.na(x))/length(x)) > threshold),
                threshold=cutoff)
    out <- okData[!index, ,drop=FALSE]
    return(out)
}
