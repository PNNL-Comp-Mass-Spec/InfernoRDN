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
# Arrange Data columns

arrangeColumns <- function(numvars, newOrder, allvars=ls(envir=.GlobalEnv))
{
    print("Changing order....")
    for (i in 1:length(numvars))
    {
        currVar <- numvars[i]
        textExp <- paste(currVar, "<-", currVar, "[,", newOrder, "]")
        eval(parse(text=textExp), envir=.GlobalEnv)
    }
    newProtOrder <- c(1, eval(parse(text=newOrder)) + 1)
    print(newProtOrder)
    if ("pScaled1" %in% allvars)
    {
        pScaled1$pData <- pScaled1$pData[, newProtOrder]
        assign("pScaled1", pScaled1, envir=.GlobalEnv)
    }

    if ("pScaled2" %in% allvars)
    {
        pScaled1$pData <- pScaled1$pData[, newProtOrder]
        assign("pScaled2", pScaled2, envir=.GlobalEnv)
    }

    if ("qrollupP" %in% allvars)
    {
        qrollupP <- qrollupP[, newProtOrder]
        assign("qrollupP", qrollupP, envir=.GlobalEnv)
    }
}
