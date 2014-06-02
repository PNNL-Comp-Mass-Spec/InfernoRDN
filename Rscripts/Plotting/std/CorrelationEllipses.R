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

plotEllipseCorr <- function(x,
                            file="deleteme.png",
                            color="green",
                            bkground="white",
                            labelscale=0.8,
                            stamp=NULL,...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)
  par(oma=c(3.4, 2, 2, 2), mar=c(5,4,4,1))

  tryCatch(
  {
      Xcor <- cor(x,method="pearson",use="pairwise.complete.obs")

      if (checkPackage(package="ellipse"))
      {
        library(ellipse)
        plotcorr(Xcor,col=color,cex.lab=labelscale);
      }
      else
      {
        plot(c(1,1),type="n",axes=F,xlab="",ylab="")
        text(1.5,1,"Error: A required package is missing!",cex=1)
      }
      if (!is.null(stamp))
            mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
                  " (", stamp, ")", sep=""),col=1,cex=.6,line=2, side=1,
                  adj=1, outer=T)
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
    par(mfrow=c(1,1),pch=1)
    dev.off()
    cat("done\n");
  }) # tryCatch()
}
