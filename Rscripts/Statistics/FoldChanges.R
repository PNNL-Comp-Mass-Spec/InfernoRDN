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
# Fold change calculations

calcFoldChanges <- function(Data, Factor, fVal1, fVal2, logScale=TRUE)
{
    #browser()
    fold.change <- integer(0)

    idx1 <- which(Factor == fVal1)
    idx2 <- which(Factor == fVal2)
    set.1 <- Data[,idx1,drop=FALSE] # extract data for fVal1
    set.2 <- Data[,idx2,drop=FALSE] # extract data for fVal1
    set1.N <- rowSums(!is.na(set.1), na.rm=TRUE)
    set2.N <- rowSums(!is.na(set.2), na.rm=TRUE)

    set.1 <- rowMeans(set.1, na.rm=TRUE)
    set.2 <- rowMeans(set.2, na.rm=TRUE)
    if (logScale)
      fold.change <- set.1 - set.2
    else
    {
      fold.change <- set.1 / set.2
      fold.change.down <- -1/fold.change
      fold.change[fold.change < 1 & !is.na(fold.change)] <-
          fold.change.down[fold.change < 1 & !is.na(fold.change)]
    }
    fold.change.abs <- abs(fold.change)
    out <- cbind(set.1, set1.N, set.2, set2.N, fold.change, fold.change.abs)
    colnames(out) <- c(fVal1, paste(fVal1,"(#non-missing)",sep=""), fVal2,
                    paste(fVal2,"(#non-missing)",sep=""), "Fold Change",
                    "Absolute Fold Change")
    return (out)
}
