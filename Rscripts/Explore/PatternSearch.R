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

patternSearch <- function(Data, patterns)
{
    corrTable <- numeric(0)
    columnames <- numeric(0)
    Data <- Data[complete.cases(Data),]
    for (i in 1:dim(patterns)[2])
    {
        # Get correlations:
        corrVals <- cor(t(Data), patterns[,i], method="kendall",
                    use="pairwise.complete.obs")
        corrTable <- cbind(corrTable, corrVals)
        columnames <- c(columnames, paste("Pattern", i, sep="_"))
    } 
    colnames(corrTable) <- columnames
    return (corrTable)    
}







