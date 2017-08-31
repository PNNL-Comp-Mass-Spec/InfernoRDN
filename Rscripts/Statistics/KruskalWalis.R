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
# Kruskal-Walis

KWPvals <- function(x, fEff, Factors=factors)
{
    X <- data.frame(t(Factors[fEff, , drop=FALSE]), x)
    #browser()
    for (i in 1:(dim(X)[2]-1))
    {
        names(X)[i] <- fEff[i]
    }
    lhs <- fEff[1]
    Formula <- as.formula(paste('x~', lhs))
    nonpara.result <- try(kruskal.test(Formula, na.action=na.omit, data=X),
                          silent=TRUE)
    if(inherits(nonpara.result, "try-error"))
    {
        return(rep(NA, Np))
    }
    else
    {
        pvals <- nonpara.result["p.value"]
        return(pvals[[1]])
    }
}