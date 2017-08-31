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

LabKeySpectralCounts <- function(fileList, folder, PepProph=0.95)
{
    columnNames <- character(0)
    Z1 <- integer(0)

    for (i in 1:length(fileList))
    {
        #browser()
        filename <- strsplit((strsplit(fileList[i]," ")[[1]][1]),"/")[[1]][2]
        myfilters<- makeFilter(c("DatasetName","CONTAINS",filename),
                            c("PeptideProphet","GREATER_THAN_OR_EQUAL",PepProph))

        X1 <- labkey.selectRows(baseUrl="http://proteomics.tgen.org/labkey/",
              folderPath=folder, schemaName="ms2",queryName="PeptidesWithFileNames",
              colFilter=myfilters)

        if (!is.null(X1))
        {
              #browser()
              #Z2 <- as.matrix(table(X1[,"Peptide"]))
              Z2 <- as.matrix(table(X1[,"Protein"]))
              if (i == 1)
              {
                  columnNames <- filename
                  Z1 <- Z2
                  colnames(Z1) <- columnNames
              }else
              {
                  columnNames <- c(columnNames, filename)
                  Z1 <- merge(Z1,Z2,by="row.names",all.y=T,all.x=T)
                  row.names(Z1) <- Z1[,1]
                  Z1 <- Z1[,-1]
                  colnames(Z1) <- columnNames
              }
        }
    }
    Z1 <- as.matrix(Z1)
    rowNames <- row.names(Z1)
    rowNames <- unlist(lapply(rowNames,splitID))
    row.names(Z1) <- rowNames
    #return(Z1)
    return(list(eset=Z1, rows=dim(Z1)[1]))
}


splitID <- function(cur.str)
{    
    # Splits string into tokens according to a regular expression.
    str.tokens <- unlist(strsplit(x=cur.str,split="\\|"));
    if (str.tokens[1] == "gi")
        rowID <- paste(str.tokens[1], "|", str.tokens[2], sep="")
    else if (grepl("IPI", str.tokens[1]))
            if (!is.na(unlist(strsplit(x=str.tokens[1], split=":"))[2]))
                rowID <- unlist(strsplit(x=str.tokens[1], split=":"))[2]
            else
                rowID <- str.tokens[1]
    else
        rowID <- cur.str
        
    return(rowID)
}


#createMSMSdt.ObsCount <- function(fileList, dataFolder,
createMSMSdt.SpectralCount <- function(fileList, dataFolder,
                         XcRank=1,
                         XCorr1Th=1.5,
                         XCorr2Th=1.5,
                         XCorr3Th=1.5,
                         XCorrOTh=1.5,
                         DelCn2Th=0.1,
                         TrypState='111')
{
    columnNames <- character(0)
    Z1 <- integer(0)

    for (i in 1:length(fileList))
    {
        browser()
        currFile <- paste(dataFolder, "/", fileList[i], "_syn.txt", sep="")
        X1 <- try(read.csv(currFile, header=T, sep="\t"), silent=TRUE)
        if (!is.null(X1))
        {
            ## Filters ####
            # Charge States
            idx1 <- ((X1[,4] == 1) + (X1[,6] >= XCorr1Th)) == 2
            idx2 <- ((X1[,4] == 2) + (X1[,6] >= XCorr2Th)) == 2
            idx3 <- ((X1[,4] == 3) + (X1[,6] >= XCorr3Th)) == 2
            idx4 <- ((X1[,4] > 3) + (X1[,6] >= XCorrOTh)) == 2
            idxCS <- ((idx1 + idx2 + idx3 + idx4) > 0)

            idxRank <- (X1[,14] <= XcRank) # XCorr rank
            idxDelCn2 <- (X1[,12] >= DelCn2Th) # DelCn threshold

            #Tryptic state
            idxTrNone <- (X1[,19] == 0)
            idxTrPartial <- (X1[,19] == 1)
            idxTrFully <- (X1[,19] == 2)

            switch (TrypState,
                '111' = { idx <- (idxCS + idxRank + idxDelCn2) == 3 },
                '110' = { idxtmp <- (idxTrNone + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        },
                '100' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrNone) == 4 },
                '010' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrPartial) == 4 },
                '001' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrFully) == 4 },
                '011' = { idxtmp <- (idxTrFully + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        }
            )

            ###############
            if (length(idx) > 1)
            {
                Y1 <- as.vector(X1[idx,9])
                Z2 <- as.matrix(table(Y1))
                if (i == 1)
                {
                    columnNames <- fileList[1]
                    Z1 <- Z2
                    colnames(Z1) <- columnNames
                }else
                {
                    columnNames <- c(columnNames, fileList[i])
                    Z1 <- merge(Z1,Z2,by="row.names",all.y=T,all.x=T)
                    row.names(Z1) <- Z1[,1]
                    Z1 <- Z1[,-1]
                    colnames(Z1) <- columnNames
                }
            }
        }
    }
    Z1 <- as.matrix(Z1)
    return(list(eset=Z1, rows=dim(Z1)[1]))
}
#-------------------------------------------------------------------
createMSMSdt.SpectralCount.1 <- function(fileList, dataFolder,
                         XcRank=1,
                         XCorr1Th=1.5,
                         XCorr2Th=1.5,
                         XCorr3Th=1.5,
                         XCorrOTh=1.5,
                         DelCn2Th=0.1,
                         TrypState='111')
{
    columnNames <- character(0)
    Z1 <- integer(0)

    for (i in 1:length(fileList))
    {
        #browser()
        currFile <- paste(dataFolder, "/", fileList[i], "_syn.txt", sep="")
        X1 <- try(read.csv(currFile, header=T, sep="\t"), silent=TRUE)
        if (!is.null(X1))
        {
            ## Filters ####
            # Charge States
            idx1 <- ((X1[,4] == 1) + (X1[,6] >= XCorr1Th)) == 2
            idx2 <- ((X1[,4] == 2) + (X1[,6] >= XCorr2Th)) == 2
            idx3 <- ((X1[,4] == 3) + (X1[,6] >= XCorr3Th)) == 2
            idx4 <- ((X1[,4] > 3) + (X1[,6] >= XCorrOTh)) == 2
            idxCS <- ((idx1 + idx2 + idx3 + idx4) > 0)

            idxRank <- (X1[,14] <= XcRank) # XCorr rank
            idxDelCn2 <- (X1[,12] >= DelCn2Th) # DelCn threshold

            #Tryptic state
            idxTrNone <- (X1[,19] == 0)
            idxTrPartial <- (X1[,19] == 1)
            idxTrFully <- (X1[,19] == 2)

            switch (TrypState,
                '111' = { idx <- (idxCS + idxRank + idxDelCn2) == 3 },
                '110' = { idxtmp <- (idxTrNone + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        },
                '100' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrNone) == 4 },
                '010' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrPartial) == 4 },
                '001' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrFully) == 4 },
                '011' = { idxtmp <- (idxTrFully + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        }
            )

            ###############
            if (length(idx) > 1)
            {
                Y1 <- as.vector(X1[idx,11])
                Z2 <- as.matrix(table(Y1))
                if (i == 1)
                {
                    columnNames <- fileList[1]
                    Z1 <- Z2
                    colnames(Z1) <- columnNames
                }else
                {
                    columnNames <- c(columnNames, fileList[i])
                    Z1 <- merge(Z1,Z2,by="row.names",all.y=T,all.x=T)
                    row.names(Z1) <- Z1[,1]
                    Z1 <- Z1[,-1]
                    colnames(Z1) <- columnNames
                }
            }
        }
    }
    Z1 <- as.matrix(Z1)
    return(list(eset=Z1, rows=dim(Z1)[1]))
}



############################################################################
createMSMSdt.old <- function(fileList, dataFolder,
                         XcRank=1,
                         XCorr1Th=1.5,
                         XCorr2Th=1.5,
                         XCorr3Th=1.5,
                         XCorrOTh=1.5,
                         DelCn2Th=0.1,
                         TrypState='111')
{
    columnNames <- character(0)
    Z1 <- integer(0)

    for (i in 1:length(fileList))
    {
        #browser()
        currFile <- paste(dataFolder, "/", fileList[i], "_syn.txt", sep="")
        X1 <- try(read.table(currFile, header=T), silent=TRUE)
        if (!is.null(X1))
        {
            ## Filters ####
            # Charge States
            idx1 <- (X1[,4] == 1)
            idx2 <- (X1[,4] == 2)
            idx3 <- (X1[,4] == 3)
            idx4 <- (X1[,4] > 3)
            idxCS <- ((idx1 + idx2 + idx3 + idx4) > 0)

            idxRank <- (X1[,14] <= XcRank) # XCorr rank
            idxDelCn2 <- (X1[,12] >= DelCn2Th) # DelCn threshold

            #Tryptic state
            idxTrNone <- (X1[,19] == 0)
            idxTrPartial <- (X1[,19] == 1)
            idxTrFully <- (X1[,19] == 2)

            switch (TrypState,
                '111' = { idx <- (idxCS + idxRank + idxDelCn2) == 3 },
                '110' = { idxtmp <- (idxTrNone + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        },
                '100' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrNone) == 4 },
                '010' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrPartial) == 4 },
                '001' = { idx <- (idxCS + idxRank + idxDelCn2 + idxTrFully) == 4 },
                '011' = { idxtmp <- (idxTrFully + idxTrPartial) == 1
                          idx <- (idxCS + idxRank + idxDelCn2 + idxtmp) == 4
                        }
            )

            ###############
            if (length(idx) > 1)
            {
                Y1 <- as.vector(X1[idx,9])
                Z2 <- as.matrix(table(Y1))
                if (i == 1)
                {
                    columnNames <- fileList[1]
                    Z1 <- Z2
                    colnames(Z1) <- columnNames
                }else
                {
                    columnNames <- c(columnNames, fileList[i])
                    Z1 <- merge(Z1,Z2,by="row.names",all.y=T,all.x=T)
                    row.names(Z1) <- Z1[,1]
                    Z1 <- Z1[,-1]
                    colnames(Z1) <- columnNames
                }
            }
        }
    }
    Z1 <- as.matrix(Z1)
    return(list(eset=Z1, rows=dim(Z1)[1]))
}
