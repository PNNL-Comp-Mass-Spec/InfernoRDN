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

plotScatterCorr <- function(x,
                            file = "deleteme.png",
                            bkground = "white",
                            dHIST = TRUE,
                            regL = FALSE,
                            showOverlap = TRUE,
                            showloess = TRUE,
                            stamp = NULL,
                            ...)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)
    par(oma=c(3.4, 2, 2, 2), mar=c(5,4,4,1))

  tryCatch(
  {
      if (dHIST)
      {
        if (regL)
          pairs(x, lower.panel=panel.myfitline,upper.panel=panel.cor,
              diag.panel=panel.hist, overlap=showOverlap, showloess=showloess)
        else
          pairs(x, lower.panel=panel.plane,upper.panel=panel.cor,
              diag.panel=panel.hist, overlap=showOverlap, showloess=showloess)
      }
      else
      {
        if (regL)
          pairs(x, lower.panel=panel.myfitline, upper.panel=panel.cor, 
              overlap=showOverlap, showloess=showloess)
        else
          pairs(x, lower.panel=panel.plane, upper.panel=panel.cor, 
              overlap=showOverlap, showloess=showloess)
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
#-----------------------------------------------------------------

panel.hist <- function(x, ...)
{
    usr <- par("usr"); on.exit(par(usr))
    par(usr = c(usr[1:2], 0, 1.5) )
    h <- hist(x, breaks=40, plot = FALSE)
    breaks <- h$breaks; nB <- length(breaks)
    y <- h$counts; y <- y/max(y)
    rect(breaks[-nB], 0, breaks[-1], y, col="wheat", ...)
}

panel.cor <- function(x, y, digits=2, prefix="", overlap, cex.cor, ...)
{
    usr <- par("usr"); on.exit(par(usr))
    par(usr = c(0, 1, 0, 1))
    r <- (cor(x, y,use="pairwise.complete.obs")) # Calculate correlation pairwise ignoring missing
    txt <- format(c(r, 0.123456789), digits=digits)[1]
    txt <- paste(prefix, txt, sep="")
    if(missing(cex.cor)) cex <- 0.8/strwidth(txt)
    text(0.5, 0.5, txt, cex = cex * r)
    
    if(missing(overlap)) overlap <- FALSE
    if(overlap)
    {
        numpairs <- sum(!is.na(x+y))
        txt2 <- paste(prefix, "(", numpairs, ")", sep="")
        text(0.8, 0.9, txt2, cex = .9)
    }
}


panel.myfitline <- function(x, y, showloess, ...)
{
    usr <- par("usr")
    # Plot missing
    miss <- is.na(x+y)
    x1 <- x[miss]
    y1 <- y[miss]
    x1[is.na(x1)]<-min(x,na.rm=TRUE)
    y1[is.na(y1)]<-min(y,na.rm=TRUE)
    points(x1,y1, ...)
    ########
    points(x, y, ...)
    abline(a=0, b=1,col="red")
    #res<-panel.smooth(x,y, ...)
    #reg <- coef(lm(y ~ x))
    #abline(coef=reg,untf=F,col="blue")
    if(missing(showloess)) showloess <- FALSE
    if(showloess)
    {
        ok <- is.finite(x) & is.finite(y)
        if (any(ok)) 
            lines(stats::lowess(x[ok], y[ok], f = 2/3, iter = 3), 
                col = "cyan3", ...)
    }
}

panel.plane <- function(x, y, ...)
{
    usr <- par("usr")
    # Plot missing
    miss <- is.na(x+y)
    x1 <- x[miss]
    y1 <- y[miss]
    x1[is.na(x1)]<-min(x,na.rm=TRUE)
    y1[is.na(y1)]<-min(y,na.rm=TRUE)
    points(x1,y1, ...)
    ######
    points(x, y, ...)
    #abline(a=0, b=1,col="red")
    #res<-panel.smooth(x,y, ...)
    #reg <- coef(lm(y ~ x))
    #abline(coef=reg,untf=F,col="blue")
}
