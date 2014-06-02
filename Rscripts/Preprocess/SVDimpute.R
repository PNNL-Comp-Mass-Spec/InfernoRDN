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
#############################################################
## SVDimpute algorithm as described in Troyanskaya 2001.
## Initially all missing values are replaced with 0.
## Then select the 'nPcs' i.e., the most significant "Eigenpeptides".
## Eigenpeptides are the eigenvectors when peptides are considered as samples.
## Missing values are estimated by regressing the target peptide against
## the 'nPcs' most significant Eigenpeptides.
SVDimpute <- function(Data, nPcs=5, thresholdSVD=0.01,
                maxSteps=100, verbose=interactive())
{
  Matrix <- as.matrix(Data)

  if (nPcs > ncol(Matrix))
   stop("more components than matrix columns selected, exiting")

  Ye <- Matrix
  missing <- is.na(Matrix)
  temp <- apply(missing, 2, sum)
  missIx <- which(temp != 0)

  ## Initially set estimates to 0
  Ye[missing] <- 0

  ## Now do the regression
  count <- 0
  error <- Inf

  while ( (error > thresholdSVD) && (count < maxSteps) ) {
    res         <- prcomp(t(Ye), center = FALSE, scale = FALSE, retx = TRUE)
    loadings    <- res$rotation[,1:nPcs, drop = FALSE]
    sDev        <- res$sdev

    ## Estimate missing values as a linear combination of the eigenvectors
    ## The optimal solution is found by regression against the k eigengenes
    for (index in missIx) {
      target <- Ye[!missing[,index],index, drop = FALSE]
      Apart <- loadings[!missing[,index], , drop = FALSE]
      Bpart <- loadings[missing[,index], , drop = FALSE]
      X <- ginv(Apart) %*% target
      estimate <- Bpart %*% X

      Ye[missing[,index], index] <- estimate
    }

    count <- count + 1
    if (count > 5) {
      error <- sqrt(sum( (YeOld - Ye)^2 ) / sum(YeOld^2))
      if (verbose) { cat("change in estimate: ", error, "\n") }
    }
    YeOld <- Ye
  }

  Data <- Matrix
  Data[missing] <- Ye[missing]
  return(Data)
} ## End of SVDimpute