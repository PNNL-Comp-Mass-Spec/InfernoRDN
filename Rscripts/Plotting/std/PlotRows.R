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

PlotRows <- function(x, idx, file="deleteme.png", bkground="white")
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
    corners <- par('usr')
    par(mar=c(10,4,4,2))

    #browser()
    tryCatch(
    {
      xlabels <- colnames(x)
      idx <- rev(idx)
      plot.labels <- rownames(x[idx,,drop=FALSE])
      X <- x[idx,]

      if (length(idx) == 1)
      {
        plot(x[idx,],type="o",pch=19,main=plot.labels,
              ylab="", xlab="", col="blue", xaxt="n")
        axis(1,at=1:length(xlabels),labels=xlabels, las = 2, cex.axis=.7)
        grid()
      }
      else
      {
          X <- X[rowSums(!is.na(X)) > 1,,drop=FALSE]
          plot.labels <- rownames(X)
          require(Hmisc)
          N <- dim(X)[1]
          curves <- vector('list', N)
          matplot(t(X),type="n",main="",ylab="",
              xlab="", xaxt="n")
          for(i in 1:N) {
            x <- 1:dim(X)[2]
            y <- X[i,]
            lines(x,y, type='o', col=i, pch=19)
            curves[[i]] <- list(x=x,y=y)
          }
          labcurve(curves, plot.labels, tilt=FALSE, type='l', col=1:N, cex=.7)
          axis(1,at=1:length(xlabels),labels=xlabels, las = 2, cex.axis=.7)
          grid()
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
    text(1.5,1,paste("Error:", ex),cex=.7)
    cat("An error was detected.\n");
    print(ex);
  },
  finally =
  {
    cat("Releasing tempfile...");
    par(mfrow=c(1,1),pch=1)
    dev.off()
    cat("done\n");
  }) # tryCatch()
}
