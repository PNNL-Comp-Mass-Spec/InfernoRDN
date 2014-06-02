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

plot2Dmat <- function(x,
                      file="deleteme.png",
                      bkground="white",
                      show.vals=TRUE,
                      corRange=c(0,1),
                      cMap="BlueWhiteRed",
                      customColors=c("#FF0000","#00FF00", "#0000FF"),
                      stamp=NULL,
                      ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)

  tryCatch(
  {
      Xcor <- cor(x,method="pearson",use="pairwise.complete.obs")
      Xcor <- signif(Xcor,digits=3)
      Xcor <- abs(Xcor)

      #browser()
      switch (cMap,
        BlackBody = { redRange <- c(0,1,1,1); greenRange <- c(0,0,1,1)
                      blueRange <- c(0,0,0,1) },
        GreenRed = { redRange <- c(0,0,1); greenRange <- c(1,0,0)
                     blueRange <- c(0,0,0) },
        Heat = { redRange <- c(1,1,1); greenRange <- c(0,1,1)
                 blueRange <- c(0,0,1) },
        BlueWhiteRed =  { redRange <- c(0,1,1); greenRange <- c(0,1,0)
                          blueRange <- c(1,1,0) },
        Custom = {
                    require(colorspace)
                    rgbRange <- coords(hex2RGB(customColors))
                    redRange = rgbRange[,1]
                    greenRange = rgbRange[,2]
                    blueRange = rgbRange[,3]
                  },
      )

      dimx <- dim(x)[2]

      library(plotrix)
      par(las=2,cex.axis=.7)
      par(mar=c(3,6,6,2))

      color2D.matplot.1(Xcor,redrange=redRange,greenrange=greenRange,
            bluerange=blueRange,
            show.legend=TRUE, xlab="",ylab="",main="",show.values=show.vals,
            vcex=.7,vcol="black", col.range=corRange)
      axis(3,(1:dimx-0.5),colnames(x))
      axis(2, dimx-(1:dimx-0.5), colnames(x))
      box()
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

#-------------------------------------------------------------

color2D.matplot.1 <- function (x,
                               redrange = c(0, 1),
                               greenrange = c(0, 1),
                               bluerange = c(0,1),
                               col.range=c(0,1),
                               show.legend = FALSE,
                               xlab = "Column",
                               ylab = "Row",
                               do.hex = FALSE,
                               show.values = FALSE,
                               vcol = "white", vcex = 1,
                               draw.axes = FALSE,
                               ...)
{
    if (is.matrix(x) || is.data.frame(x)) {
        xdim <- dim(x)
        if (is.data.frame(x))
            x <- unlist(x)
        else x <- as.vector(x)
        oldpar <- par(no.readonly = TRUE)
        par(xaxs = "i", yaxs = "i")
        plot(c(0, xdim[2]), c(0, xdim[1]), xlab = xlab, ylab = ylab,
            type = "n", axes = FALSE, ...)
        oldpar$usr <- par("usr")
        if (!do.hex) {
            box()
            pos <- 0
        }
        else pos <- -0.3
        if (draw.axes)
        {
            axis(1, at = pretty(0:xdim[2])[-1] - 0.5, labels = pretty(0:xdim[2])[-1],
                    pos = pos)
            yticks <- pretty(0:xdim[1])[-1]
            axis(2, at = xdim[1] - yticks + 0.5, yticks)
        }

        #cellcolors <- color.scale(x, redrange, greenrange, bluerange)

        x.col <- x
        x.col[x.col > col.range[2]] <- col.range[2]
        x.col[x.col < col.range[1]] <- col.range[1]
        colRange <- seq(col.range[1],col.range[2],0.1)
        x.col <- c(colRange, x.col)
        cellcolors <- color.scale(x.col, redrange, greenrange, bluerange)
        cellcolors <- cellcolors[-c(1:length(colRange))]

        if (do.hex) {
            par(xpd = TRUE)
            offset <- 0
            for (row in 1:xdim[1]) {
                for (column in 0:(xdim[2] - 1)) {
                  hexagon(column + offset, xdim[1] - row, col = cellcolors[row +
                    xdim[1] * column])
                  if (show.values)
                    text(column + offset + 0.5, xdim[1] - row +
                      0.5, x[row + column * xdim[1]], col = vcol,
                      cex = vcex)
                }
                offset <- ifelse(offset, 0, 0.5)
            }
            par(xpd = FALSE)
        }
        else {
            rect(sort(rep((1:xdim[2]) - 1, xdim[1])), rep(seq(xdim[1] -
                1, 0, by = -1), xdim[2]), sort(rep(1:xdim[2],
                xdim[1])), rep(seq(xdim[1], 1, by = -1), xdim[2]),
                col = cellcolors, border = FALSE)
            if (show.values)
                text(sort(rep((1:xdim[2]) - 0.5, xdim[1])), rep(seq(xdim[1] -
                  0.5, 0, by = -1), xdim[2]), x, col = vcol,
                  cex = vcex)
        }
        xy <- par("usr")
        plot.din <- par("din")
        plot.pin <- par("pin")
        bottom.gap <- (xy[3] - xy[4]) * (plot.din[2] - plot.pin[2])/(2 *
            plot.pin[2])
        grx1 <- xy[1]
        gry1 <- bottom.gap * 0.4
        grx2 <- xy[1] + (xy[2] - xy[1])/4
        gry2 <- bottom.gap * 0.25
        if (show.legend)
            color.legend(grx1, gry1, grx2, gry2, round(range(x.col[!is.na(x)]),
                2), color.gradient(redrange, greenrange, bluerange,
                nslices = 50))
        par(oldpar)
    }
    else cat("x must be a data frame or matrix\n")
}
