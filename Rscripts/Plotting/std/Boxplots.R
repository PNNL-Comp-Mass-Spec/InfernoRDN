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

dataBoxPlots <- function(x,
                         file="deleteme.png",
                         Factor=1,
                         outliers=TRUE,
                         color="wheat2",
                         bkground="white",
                         labelscale=0.8,
                         boxwidth=1,
                         showcount=TRUE,
                         stamp=NULL,
                         ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

  par(oma=c(3.4, 2, 2, 2), mar=c(5,4,4,1))

  tryCatch(
  {
      box_color <- rep(color,dim(x)[2])
      
      Factor <- Factor[is.element(names(Factor),colnames(x))]
      if (length(Factor) == dim(x)[2])
      {
        uF <- unique(Factor)
        colStep <- length(uF)
        colorRange <- hsv(h = seq(0,1,1/colStep), s=1, v=1)
        for (i in 1:length(uF))
        {
            idx <- which(uF[i]==Factor)
            box_color[idx] <- colorRange[i]
        }
      }
      par(omd=c(0,1,0.1,1))
      boxplot(data.frame(x),outline=outliers,notch=T,las=2,
          boxwex=boxwidth,col=box_color,cex.axis=labelscale,...)
      if (length(Factor) == dim(x)[2])
        legend("topleft",uF,col=colorRange[1:length(uF)],pch=19,
            bg='transparent')
      if (showcount)
      {
        axis(side=3, at=1:dim(x)[2], labels=colSums(!is.na(x)), tick=FALSE,
            cex.axis=.7, las=2)
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
