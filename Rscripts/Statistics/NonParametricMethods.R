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
# Non-parametric methods

DoNonPara <- function(Data, FixedEffects,
                      thres=3,
                      Factors=factors,
                      testType=c("KW","Wilcox"))
{
    require(qvalue)

    Data <- Data[order(as.numeric(rownames(Data))),]
    splitIdx <- splitForAnova(Data, Factors[FixedEffects,], thres=thres)
    Data.good <- Data[splitIdx$Good,,drop=FALSE]
    Data.bad <- Data[splitIdx$Bad,,drop=FALSE]
    if (dim(Data.bad)[1]==1)
      Data.bad <- rbind(Data.bad, Data.bad)

    npresults <- switch(match.arg(testType),
        KW = t(apply(Data.good, 1, KWPvals, FixedEffects,
                                      Factors=factors)),
        Wilcox = t(apply(Data.good, 1, WilcoxPvals, FixedEffects,
                                      Factors=factors)),
        t(apply(Data.good, 1, KWPvals, FixedEffects,
                                      Factors=factors))
    )

    if (dim(npresults)[1] < dim(npresults)[2])
        npresults <- t(npresults)

    if (is.matrix(npresults))
    {
        out <- npresults
        idx <- !is.na(npresults)
        qval <- rep(NA, length(idx))
        tryCatch(
        {
            qval.tmp <- (qvalue(npresults[idx]))$qvalues
            qval[idx] <- qval.tmp
        },
        interrupt = function(ex)
        {
            cat("An interrupt was detected.\n");
            print(ex);
        },
        error = function(ex)
        {
            cat("An error was detected.\n");
            print(ex);
        },
        finally =
        {
            out <- cbind(out,qval)
            outColNames <- c("p-value(Non Para.)", "q-value")
        }) # tryCatch()
        colnames(out) <- outColNames
    }

    return(list(pvals=out, miss=Data.bad, allused=(dim(Data.bad)[1]==0)))
}
