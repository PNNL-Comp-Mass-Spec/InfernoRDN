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
################ Quantile Normalization ###################
quantileN <- function (Data,method="median")
{
    Data <- Data[complete.cases(Data),]
    sortedData <- apply(Data, 2, sort, na.last=TRUE)
    rowMedians <- apply(sortedData, 1, method, na.rm=TRUE)
    dataRanks <- c(apply(Data, 2, rank, na.last = TRUE)) # ... and concatenate
    normedData <- array(approx(1:nrow(Data), rowMedians, dataRanks)$y,
                 dim(Data), dimnames(Data))
    normedData <- normedData[order(as.numeric(rownames(normedData))),]
    return(normedData)
}

IsCompleteData <- function(Data)
# Quantile Normalization works only with complete data
{
    return(sum(complete.cases(Data)) > 50)
}
############################################################