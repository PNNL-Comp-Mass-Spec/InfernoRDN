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
# Filter based on ANOVA results

filterAnova <- function(pvals, Data, thres, column,
                        smode=c("LT","GT"))
{
    out <- numeric(0)
    nodata <- TRUE
    idx <- switch(match.arg(smode),
        LT = rownames(pvals[pvals[,column] < thres,,drop=FALSE]), #less than
        GT = rownames(pvals[pvals[,column] > thres,,drop=FALSE]), #greater than
        rownames(pvals[pvals[,column] < thres,,drop=FALSE])
    )
    dRows <- rownames(Data)
    found <- sum(idx %in% dRows)
    if (found)
    {
        out <- Data[idx,]
        nodata=(dim(out)[1]==0)
    }
    return(list(Filtered=out, NoData=nodata, error=(found==0)))
}
