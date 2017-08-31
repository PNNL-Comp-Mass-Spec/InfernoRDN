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

MApairs <- function(x,
                    file="deleteme.png",
                    dCol="green",
                    lCol="red",
                    bkground="white",
                    stamp=NULL,
                    ...)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

    par(oma=c(3.4, 2, 2, 2), mar=c(5,4,4,1))

    tryCatch(
    {
        if (dim(x)[2]==2)  # Only one plot
        {
            SingleMAplot(x[,1],x[,2],dcolor=dCol,lcolor=lCol)
        }
        else
        {
            pairs(x, lower.panel=panel.ma,upper.panel=NULL,col.axis="transparent",
                dcolor=dCol,lcolor=lCol)
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
      text(1.5,1,paste("Error:", ex),cex=1)
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
#-----------------------------------------------------------------

panel.ma <- function(x, y, dcolor="green", lcolor="red",
                     sp=0.2, ...) # multiple pairs plot
{
    usr <- par("usr"); on.exit(par(usr))

    Mean <- (x + y)/2
    Diff <- x - y
    LOESS<-loess(Diff~Mean, family="gaussian", span=sp)
    FIT <- LOESS$fit
    #browser()
    par(usr = c(range(Mean,na.rm=T), range(Diff,na.rm=T)))
    points(Mean,Diff, pch=21,bg=dcolor, xlab="MEAN (A)",
        ylab="DIFF (M)",...)
    points(na.omit(Mean),LOESS$fit,col=lcolor,pch=20,...)
    abline(h=0, col=1, lwd=1)
}

#-----------------------------------------------------------------
SingleMAplot <- function(x, y, dcolor="green",lcolor="red",
                     sp=0.2,...)  # used when there is a single plot
{
    usr <- par("usr"); on.exit(par(usr))

    Mean <- (x + y)/2
    Diff <- x - y
    LOESS<-loess(Diff~Mean, family="gaussian", span=sp)
    FIT <- LOESS$fit
    #browser()
    par(usr = c(range(Mean,na.rm=T), range(Diff,na.rm=T)))
    plot(Mean,Diff, pch=21,bg=dcolor, xlab="MEAN (A)",
        ylab="DIFF (M)",...)
    points(na.omit(Mean),LOESS$fit,col=lcolor,pch=20,...)
    abline(h=0, col=1, lwd=1)
}
