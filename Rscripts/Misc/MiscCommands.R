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

# Log transform
logTransform <- function(Data, logBase=2, bias=0, add=TRUE)
{
    if (add)
        out <- log(Data + bias, base=logBase)
    else
        out <- log(Data * bias, base=logBase)
    #out <- asinh(Data)
    return(out)
}

# Load a file
loadfile <- function(filename,stripwhite=TRUE,header=TRUE,separator){
  Abund <- read.csv(filename,strip.white=stripwhite,header=header,sep=separator,row.names=1)
  Abund[Abund==0]<-NA
  Abund
}

# Send matrix to the App. So, 'NA' should be replaced by '999999'
sendmatrix <- function(x){
  x[is.na(x)] <- 999999
  x <- as.matrix(x)
  return(x)
}

# Get a matrix from the App. Missing values are substituted by 'NA'
getmatrix <- function(x){
  x[x==0] <- NA
  return(as.matrix(x))
}

RVersionOK <- function(major=2, minor=6.0)
{
    rver <- R.Version()
    rver.maj <- as.numeric(rver$major)
    rver.min <- as.numeric(rver$minor)
    out <- (major <= rver.maj && minor <= rver.min)
    return (out)
}

checkPackage <- function(package="gplots"){
  ins <- installed.packages()
  x <- grep(package,rownames(ins))
  out <- (length(x)>0)
  return(out)
}

installPackage <- function(package="gplots",
                    repository="http://lib.stat.cmu.edu/R/CRAN")
{
    if (!checkPackage(package))
      install.packages(package,.libPaths()[1],repository)
}

installPackages <- function(packages,
                        repository="http://lib.stat.cmu.edu/R/CRAN")
{
    for (num in 1:length(packages))
    {
        installPackage(packages[num], repository=repository)
    }
    #cat(print(paste('Packages installed:', packages)))
}

SaveWithProts <- function(Data=Eset, filename=fName)
{
    Data1 <- data.frame(Row_ID=rownames(Data),Data)
    X <- merge(ProtInfo,Data1,by="Row_ID")
    X <- X[order(X[,1]),]
    #return(X)
    write.table(X, file=filename, quote=FALSE, sep=",", col.names=TRUE,
        row.names=FALSE,na="")
}

remove.duplicates <- function(Data)
{
     rowIDs <- rownames(Data)
     Data <- Data[!duplicated(rowIDs),]
     return(Data)
}

remove.emptyProtInfo <- function(Data, checkEset=FALSE)
{
    outData <- Data[!(Data[,1]==""),]
    outData <- outData[!(outData[,2]==""),]
    if (checkEset)
    {
       RN <- rownames(Eset)
       Idx <- outData[,1] %in% RN
       outData <- outData[Idx,]
    }
    return(unique(outData))
}

#-------------------------------------------------------------
customScale <- function(x,y)
{
    xmin <- min(x,na.rm=T)
    xmax <- max(x,na.rm=T)
    ymin <- min(y,na.rm=T)
    ymax <- max(y,na.rm=T)
    y <- (ymax - ymin)/(xmax-xmin) * (x - xmin) + ymin
    return(y)
}




#---------------------------------------------------------------
zscores <- function(x)
{
    mu <- mean(x, na.rm=TRUE)
    sigma <- sd(x, na.rm=TRUE)
    z <- (mu - x)/sigma
    return(z)
}

pvalsNormal <- function(x)
{
    z <- zscores(x)
    return(2*pnorm(-abs(z)))
}
