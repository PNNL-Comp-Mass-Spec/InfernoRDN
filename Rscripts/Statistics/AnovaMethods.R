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
# ANOVA

DoAnova <- function(Data, FixedEffects,
                    RandomEffects, thres=3,
                    interact=FALSE,
                    unbalanced = TRUE,
                    useREML=TRUE,
                    Factors=factors)
{
    require(qvalue)
    require(car)
    require(nlme)

    Data <- Data[order(as.numeric(rownames(Data))),]
    allFactors <- c(FixedEffects,RandomEffects)
    splitIdx <- splitForAnova(Data, Factors[allFactors,], thres=thres)
    Data.good <- Data[splitIdx$Good,,drop=FALSE]
    Data.bad <- Data[splitIdx$Bad,,drop=FALSE]
    if (dim(Data.bad)[1]==1)
      Data.bad <- rbind(Data.bad, Data.bad)
        
    #test for the length of p-value vector 
    tmp <- anovaPvals(runif(dim(Data.good)[2]), FixedEffects, 
	 	                    RandomEffects, Factors=factors, interact=interact, 
	 	                    unbalanced=unbalanced, test=TRUE, useREML=useREML) 
    pNames <- names(tmp) 
	 	 
    anovaresults <- t(apply(Data.good, 1, anovaPvals, FixedEffects,
                    RandomEffects, Factors=factors, interact=interact,
                    unbalanced=unbalanced, Np=length(tmp),
                    test=FALSE, useREML=useREML))

    if (dim(anovaresults)[1] < dim(anovaresults)[2])
        anovaresults <- t(anovaresults)
    colnames(anovaresults)<-pNames

    if (is.matrix(anovaresults))
    {
        out <- anovaresults
        outColNames <- pNames
        for (i in 1:length(pNames))
        {
            idx <- !is.na(anovaresults[,i])
            qval <- rep(NA, length(idx))
            #browser()
            tryCatch(
            {
                qval.tmp <- (qvalue(anovaresults[idx,i]))$qvalues
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
                outColNames <- c(outColNames, paste(pNames[i],"(q)",sep=""))
            }) # tryCatch()
        }
        colnames(out) <- outColNames
    }

    return(list(pvals=out, miss=Data.bad, allused=(dim(Data.bad)[1]==0)))
}

#--------------------------------------------------------------
anovaPvals <- function(x, fEff, rEff,
                   Factors=factors,
                   interact=FALSE,
                   unbalanced=TRUE,
                   Np=2,
                   test=FALSE,
                   useREML=TRUE)
{
    allF <- c(fEff,rEff)
    X <- data.frame(t(Factors[allF, , drop=FALSE]), x)

    for (i in 1:(dim(X)[2]-1))
    {
        names(X)[i] <- allF[i]
    }

    lhs <- fEff[1]
    if (length(fEff) > 1)
    {
        for (i in 2:length(fEff))
        {
            if (interact)
                lhs <- paste(lhs, "*", fEff[i])
            else
                lhs <- paste(lhs, "+", fEff[i])
        }
    }
    lm.Formula <- as.formula(paste('x~', lhs))
    modelF <- lm(lm.Formula, X)
    
    if (useREML)
        Method <- "REML"
    else
        Method <- "ML"
    
    if (!is.null(rEff))
    {
        rEffects <- paste("~1|", rEff[1], sep="")
        if (length(rEff) > 1)
        {
            for (i in 2:length(rEff))
            {
                rEffects <- paste(rEffects, ", ~1|", rEff[i])
            }
            rEffects <- paste("random=list(", rEffects, ")", sep="")
        }
        else
            rEffects <- paste("random=",rEffects, sep="")

        #modelR <- lme(eval(parse(text=lm.Formula)), data=X, eval(parse(text=rEffects)))
        modelR <- lme(lm.Formula, data=X, eval(parse(text=rEffects)),
                      method=Method, na.action = na.omit)

        options(warn = -1)
        if (unbalanced)
            anova.result <- try(anova(modelR, type="marginal"),silent=TRUE)
        else
            anova.result <- try(anova(modelR),silent=TRUE)

        options(warn = 0)
        
        if(inherits(anova.result, "try-error"))
        {
            return(rep(NA, Np))
        }
        else
        {
            #anova.result <- anova(modelR)
            pvals <- anova.result$"p-value"
            names(pvals)<-rownames(anova.result)

            if (names(pvals)[1] == "(Intercept)")
                    pvals <- pvals[-1]
            idx <- length(names(pvals))
            if (names(pvals)[idx] == "Residuals")
                    pvals <- pvals[-idx]

            pvals[is.nan(pvals)] <- NA
            if (test)
                return(pvals)
            else
            {
                tmp <- rep(NA, Np)
                tmp[1:length(pvals)] <- pvals
                return(tmp)
            }
        }
    }
    else  # No random effects
    {
        if (unbalanced)
            anova.result <- try(Anova(modelF, type="III"),silent=TRUE)
        else
            anova.result <- try(Anova(modelF),silent=TRUE)

        if(inherits(anova.result, "try-error"))
        {
            return(rep(NA, Np))
        }
        else
        {
            pvals <- anova.result["Pr(>F)"][[1]]
            names(pvals)<-rownames(anova.result)

            if (names(pvals)[1] == "(Intercept)")
                    pvals <- pvals[-1]
            idx <- length(names(pvals))
            if (names(pvals)[idx] == "Residuals")
                    pvals <- pvals[-idx]

            if (test)
                return(pvals)
            else
            {
                tmp <- rep(NA, Np)
                tmp[1:length(pvals)] <- pvals
                return(tmp)
            }
        }
    }
}

#--------------------------------------------------------------
splitForAnova <- function(Data,Factors,thres=3)
{
    anovaIdx <- integer(0)
    anovaIdxNon <- integer(0)
    allIdx <- 1:dim(Data)[1]

    if (is.matrix(Factors))
    {
        N <- dim(Factors)[1]  # how many factors?
    }
    else
        N <- 1
    for (k in 1:N)
    {
        if (N > 1)
            currFac <- Factors[k,]
        else
            currFac <- Factors

        splitIdx <- splitmissing.factor(Data, currFac, thres=thres)
        if (k == 1)
            anovaIdx <- splitIdx$good
        else
            anovaIdx <- intersect(anovaIdx, splitIdx$good)
    }
    anovaIdxNon <- allIdx[!(allIdx %in% anovaIdx)]
    out <- list(Good=anovaIdx,Bad=anovaIdxNon)
    return(out)
}

#--------------------------------------------------------------
factor.values <- function(factors)
{
    out <- list()
    for (i in 1:dim(factors)[1])
    {
        out[[rownames(factors)[i]]] <- as.matrix(factors[i,!duplicated(t(factors[i,]))])
    }
    return(out)
}

#--------------------------------------------------------------
splitmissing.factor <- function(Data, Factor, thres=3)
{
    anovaIdx <- integer(0)
    anovaIdxNon <- integer(0)
    allIdx <- 1:dim(Data)[1]
    Nreps <- unique(as.vector(t(Factor))) # Factor Levels
    for (i in 1:length(Nreps)) # for each unique factor level
    {
        idx <- which(Factor == Nreps[i])
        dataset <- Data[,idx,drop=FALSE]
        if (length(idx) > 1) # multiple columns
        {
            splitIdx <- splitmissing.fLevel(dataset, thres=thres)
            if (i == 1)
                anovaIdx <- splitIdx$good
            else
                anovaIdx <- intersect(anovaIdx, splitIdx$good)
        }
    }
    badIdx <- allIdx[!(allIdx %in% anovaIdx)]
    return(list(good=anovaIdx, bad=badIdx))
}

#--------------------------------------------------------------
splitmissing.fLevel <- function(Data,thres=3)
{
    allIdx <- 1:dim(Data)[1]
    validIdx <- rowSums(!is.na(Data)) >= thres
    #validIdx <- apply(!is.na(Data),1,sum) >= thres

    goodIdx <- unique(which(validIdx))
    badIdx <- allIdx[!(allIdx %in% goodIdx)]
    return(list(good=goodIdx, bad=badIdx))
}



