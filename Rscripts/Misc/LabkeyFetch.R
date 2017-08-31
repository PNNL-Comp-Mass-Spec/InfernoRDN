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

LabkeyFetch <- function(datasetname, urlname, folder, schema="ms2", query="DatasetNames")
{
    require(Rlabkey)
    filefilter <- makeFilter(c("DatasetName","CONTAINS",datasetname))
    fileNames <- labkey.selectRows(baseUrl=urlname,
        folderPath=folder, schemaName=schema,queryName=query, colFilter=filefilter)
    fileNames <- as.matrix(fileNames)
    rownames(fileNames) <- 1:dim(fileNames)[1]
    return(fileNames)
}

LabKeyProjects <- function()
{
    require(Rlabkey)
    Projects <- lsProjects(baseUrl="http://proteomics.tgen.org/labkey/")
    Projects <- as.vector(sapply(Projects, function(x) gsub("/","",x)))
    return(Projects[!Projects%in%c("home","Shared")])
}