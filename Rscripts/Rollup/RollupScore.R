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
#############################################################
rollup.score <- function(currPepSel, currProtSel, method)
{
    N1 <- dim(currPepSel)[1]
    N2 <- dim(currPepSel)[2]
    pepCorr <- rep(numeric(0),N1)
    ws <- rep(numeric(0),N1)

    for(i in 1:N1)#finds correlation values between each peptide profile and calculated protein profile
    {
        ws[i] <- sum(!is.na(currPepSel[i,]))/N2
        if(method=="pearson")
            pepCorr[i] <- cor(as.vector(currPepSel[i,]), as.vector(currProtSel), use="pairwise.complete.obs")
        else if(method=="kendall")
            pepCorr[i] <- cor(as.vector(currPepSel[i,]), as.vector(currProtSel), use="pairwise.complete.obs",
                          method="kendall")
        else
            pepCorr[i] <- cor(as.vector(currPepSel[i,]), as.vector(currProtSel), use="pairwise.complete.obs",
                          method="spearman")
    }
    #meanCorr <- mean(pepCorr, na.rm=TRUE) #mean correlation value for each protein
    meanCorr <- weighted.mean(pepCorr, ws, na.rm=TRUE) #mean correlation value for each protein
    Penalty1 <- 1-1/N1
    #Penalty2 <- sum(!is.na(currPepSel))/(N1*N2)
    #Score <- meanCorr * Penalty1 * Penalty2
    Score <- meanCorr * Penalty1
    
    out <- Score
    return(out)
}