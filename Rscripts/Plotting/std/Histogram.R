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

# Plot the histograms to either a JPEG or PNG file
plot_hist <- function(data,
                      ncols = 2,
                      cells="Sturges",
                      file="deleteme.png",
                      bkground="white",
                      colF="#ffc38a",
                      colB="#5FAE27",
                      addRug=TRUE,
                      stamp=NULL,
                      ...)
{
    # Plot histograms, the distribution profile and the reference profile

    #png(filename=file,width=1152,height=864,pointsize=12,
    #        bg=bkground,res=600)
    #require(Cairo)
    #CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

    if (ncols == 0) ncols <- 1
    m <- ceiling((NCOL(data))/ncols)
    #par(mfrow=c(m,ncols), cex=.5, mex=.5,mar=c(6,7,4,4))
    par(mfrow=c(m,ncols), cex=.6, mex=.6, oma=c(5, 2, 2, 2), mar=c(4,5,5,1))

    tryCatch(
    {
        for (i in 1:NCOL(data))
        {
          if (NCOL(data) == 1)
          {
            par(mfrow=c(1,1))
            xx <- data
            htitle = "Distribution"
            if (is.numeric(cells))
            {
                partitions <- seq(min(xx,na.rm=TRUE)-.001,
                                  max(xx,na.rm=TRUE)+.001, len=(cells+1))
            }
            else
                partitions <- cells
          }
          else
          {
            xx <- data[,i]
            htitle = colnames(data)[i]
            if (is.numeric(cells))
            {
                partitions <- seq(min(xx,na.rm=TRUE)-.001,
                                  max(xx,na.rm=TRUE)+.001, len=(cells+1))
            }
            else
                partitions <- cells
          }

          h <- hist(xx, breaks=partitions,plot=FALSE)
          d <- density(xx,na.rm=TRUE)
          ylim1 <- c(0, max(h$intensities,d$y))

          #curve(dnorm(x, mean=mean(xx,na.rm=T), sd=sd(xx,na.rm=T)),
          #        add=TRUE, lty=2, col="red")
          #  curve(dnorm(x, mean=0, sd=sd(xx,na.rm=T)), add=TRUE, lty=2,
          #        col="blue")
          plot(function(y) dnorm(y, mean(xx, na.rm=T), sd(xx, na.rm=T)),
              from=min(xx, na.rm=T), to=max(xx, na.rm=T),
              main = htitle,
              col="white",xlab="",ylab="Probability")
          hist(xx, breaks=partitions, prob=TRUE,
                col=colF, border=colB,xpd=TRUE,ylim=ylim1,add=TRUE)
          plot(function(y) dnorm(y, 0, sd(xx, na.rm=T)),
              from=min(xx, na.rm=T), to=max(xx, na.rm=T),
              col="blue", add=TRUE, lty = "dashed")
          plot(function(y) dnorm(y, mean(xx, na.rm=T), sd(xx, na.rm=T)),
              from=min(xx, na.rm=T), to=max(xx, na.rm=T),
              col="red",add=TRUE)
          if (addRug)
            rug(xx,col=colB)
          box()
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
      #dev.off()
      cat("done\n");
    }) # tryCatch()
}
