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

plot_qq <- function(data,
                      reference = "Normal",
                      wshape = 2,
                      wscale = 1,
                      degfree = 4,
                      exprate=1,
                      ncols = 2,
                      file="deleteme.png",
                      bkground="white",
                      colF="#ffc38a",
                      colB="#5FAE27",
                      colL="#FF0000",
                      ...)
{
    # Plot histograms, the distribution profile and the reference profile

    #png(filename=file,width=1152,height=864,pointsize=12,
    #        bg=bkground,res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)

    if (ncols == 0) ncols <- 1
    m <- ceiling((NCOL(data))/ncols)
    par(mfrow=c(m,ncols), cex=.6, mex=.6, oma=c(5, 2, 2, 2), mar=c(4,5,5,1))

    tryCatch(
    {
        for (i in 1:NCOL(data))
        {
          if (NCOL(data) == 1)
          {
            par(mfrow=c(1,1))
            xx <- data
            qtitle = "Normal Q-Q Plot"
          }
          else
          {
            xx <- data[,i]
            qtitle = colnames(data)[i]
          }

          #qqnorm(xx, col=colB, bg=colF, pch=21, main=qtitle)
          #qqline(xx, col = colL)
          qqplot.1(xx, reference = reference, shape = wshape,
                scale = wscale, def = degfree, exprate=exprate,
                colb=colB, bgrnd=colF,
                psize=21, main=qtitle)
          qqline.1(xx, reference = reference, colL = colL, shape = wshape,
                scale = wscale, def = degfree, exprate=exprate)
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
      par(mfrow=c(1,1),pch=1)
      dev.off()
      cat("done\n");
    }) # tryCatch()
}

#-------------------------------------------------------------------
## Generic qqplots for any distribution
## (Normal, Exponential, Student and Weibull)
qqplot.1 <- function (data, position = 0.5, reference = "Normal",
                shape=2, scale=1, def = 4, exprate = 1,
                main='QQ plot', colb=colB, bgrnd=colF, psize=21, ...)
{
    data <- data[!is.na(data)]
    n <- length(data)
    plot.points <- ppoints(n, position)
    xpoints <- switch(reference,
      Normal = qnorm(plot.points, ...),
      Exponential = qexp(plot.points, rate=exprate, ...),
      Student = qt(plot.points, df=def, ...),
      Weibull = qweibull(plot.points, shape, scale, ...)
    )
    plot(xpoints, sort(data), col = colb, bg = bgrnd, pch = 21,
        xlab = paste("Theoretical Quantiles - ", reference),
        ylab = "Sample Quantiles", main = main)
}

#-------------------------------------------------------------------
## qqline.1 is based on qqline with additional, optional arguments.
qqline.1 <- function(x, reference = "Normal", exprate = 1,
              shape=2, scale=1, def = 4, colL='red', ...) {
  x <- x[!is.na(x)]
  plot.points <- c(0.25, 0.75)
  data.quartiles <- quantile(x, plot.points, na.rm = T)
  norm.quartiles <- switch(reference,
      Normal = qnorm(plot.points, ...),
      Exponential = qexp(plot.points, rate=exprate, ...),
      Student = qt(plot.points, df=def, ...),
      Weibull = qweibull(plot.points, shape, scale, ...)
  )
  b <- (data.quartiles[2] - data.quartiles[1]) /
    (norm.quartiles[2] - norm.quartiles[1])
  a <- data.quartiles[1] - norm.quartiles[1] * b
  abline(a, b, col=colL, ...)
  ans <- as.vector(c(a, b))
  names(ans) <- c("intercept", "slope")
  invisible(ans)
}
