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

plotProts.raw <- function(pdata=NULL,
                          Data=Eset,
                          file="deleteme.png",
                          bkground="white",
                          IPI="IPI:IPI00009793.1",
                          datalabels=TRUE)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

    #Data <- Eset
    tryCatch(
    {
        if (datalabels)
          xlabels <- colnames(Data)
        else
          xlabels <- 1:dim(Data)[2]
        protIPI <- ProtInfo[,2]
        MassTags <- ProtInfo[,1]
        pidx <- which(protIPI==IPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]
        x <- 1:dim(Data)[2]

        #par(mfrow=c(2,1))
        if (length(pidx) == 1)
        {
            par(mar=c(10,4,4,2))
            plot(currProtData,type="o",pch=19,main=IPI,ylab="Raw Data",
                col="blue", xlab="", xaxt="n")
            axis(1,at=1:length(xlabels),labels=xlabels, las = 2)
            mtext("1 Peptide", 3)
            foo <- currProtData ~ x
            lines(foo, model.frame(foo), lty=3, col='blue', cex.axis=0.7)
            grid()
        }
        else
        {
            par(mar=c(10,4,4,2))
            matplot(t(currProtData),type="o",main=IPI,ylab="Raw Data",
                xlab="", xaxt="n")
            axis(1,at=1:length(xlabels),labels=xlabels, las = 2, cex.axis=0.7)
            grid()
            mtext(paste(dim(currProtData)[1],"Peptides",sep=" "), 3)
        }
    },
    interrupt = function(ex)
    {
      cat("An interrupt was detected.\n");
      print(ex);
    },
    error = function(ex)
    {
      plot(c(1,1),type="n",axes=F,xlab="",ylab="")
      text(1.5,1,paste("Error:", ex),cex=2)
      cat("An error was detected.\n");
      print(ex);
    },
    finally =
    {
      cat("Releasing tempfile...");
      dev.off()
      cat("done\n");
    }) # tryCatch()
}
