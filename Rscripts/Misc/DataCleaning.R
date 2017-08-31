# Written by Ashoka D. Polpitiya
# for the Department of Energy (PNNL, Richland, WA)
# Copyright 2007, Battelle Memorial Institute
# E-mail: proteomics@pnnl.gov
# Website: http://omics.pnl.gov/software
# -------------------------------------------------------------------------
#
# Licensed under the Apache License, Version 2.0; you may not use this file except
# in compliance with the License.  You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
#
# Duplicate handling when data loading
# -------------------------------------------------------------------------

# Remove the duplicate rows.
cleanmatrix <- function(x){
  x <- x[!apply(is.na(x),1,all),]
  rowIDs <- rownames(x)

  # Remove empty MT rows
  rowIDs.1 <- rowIDs[rowIDs != ""]
  x <- x[rowIDs %in% rowIDs.1,]

  #x <- unique(x)

  # Treat duplicates
  rowIDs <- rownames(x)
  rowIDdup <- rowIDs[duplicated(rowIDs)]

  if (length(rowIDdup) > 0)  # has duplicates
  {
      dupIdx <- rowIDs %in% rowIDdup
      uData <- x[!dupIdx,] # unique data portion
      dData <- x[dupIdx,]  # duplicates
      dupRowNames <- rownames(dData)
      urowIds <- unique(dupRowNames)
      dupData <- numeric(0)
      for (i in 1:length(urowIds))
      {
        dupDataSubset <- dData[dupRowNames %in% urowIds[i],]
        dupDataSubset <- unique(dupDataSubset) # identical rows get omitted
        dupData <- rbind(dupData, apply(dupDataSubset, 2, sum, na.rm=TRUE))
        dupData[dupData == 0] <- NA
      }
      rownames(dupData) <- urowIds
      outData <- rbind(uData, dupData)
      outData <- outData[order(as.numeric(rownames(outData))),]
  }
  else
    outData <- x
  return(outData)
}

#--------------------------------------------------------------------
# Remove the duplicate rows.
cleanmatrix.1 <- function(x){
  rowIDs <- rownames(x)

  # Remove empty MT rows
  rowIDs <- rowIDs[rowIDs != ""]
  x <- x[rowIDs,]

  # Treat duplicates
  rowIDs <- rownames(x)
  rowIDdup <- rowIDs[duplicated(rowIDs)]
  if (length(rowIDdup) > 0)  # has duplicates
  {
      dupIdx <- rowIDs %in% rowIDdup
      uData <- x[!dupIdx,] # unique data portion
      dData <- x[dupIdx,]  # duplicates
      N <- dim(dData)[1]
      dData.1 <- dData[1:floor(N/2),] # 1st half
      dData.2 <- dData[(floor(N/2) + 1):N,] # 2nd half
      browser()
      dData.res <- dData.1 + dData.2  # add them up
      outData <- rbind(uData, dData.res)
      outData <- outData[order(as.numeric(rownames(outData))),]
  }
  else
    outData <- x
  return(outData)
}