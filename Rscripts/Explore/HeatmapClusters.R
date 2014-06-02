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

plotHeatmap <- function(x, rDend=NULL, cDend=NULL,
                            Kmeans=FALSE,
                            fixSeed=FALSE,
                            agglomeration = 1,
                            distance = 6,
                            rowscale=TRUE,
                            file="deleteme.png",
                            cMap="BlackBody",
                            bkground="white",
                            color="wheat2",
                            Factor=1,
                            customColors=c("green", "black", "red"),
                            colRange=NULL,
                            labelscale=1.3,
                            noxlab = FALSE,
                            stamp=NULL,
                            ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
  corners <- par('usr')

  tryCatch(
  {
      if (length(dim(x)) != 2 || !is.numeric(x))
        stop("Data must be a numeric matrix")

      cmap <- colorRampPalette(c("black", "red", "orange","yellow","lightyellow"),
                        space="rgb")(20)
      cmap <- switch (cMap,
            BlackBody = colorRampPalette(
                        c("black", "red", "orange","yellow","lightyellow"),
                        space="rgb")(20),
            GreenRed = colorRampPalette( c("green", "black", "red"),
                       space="rgb")(20),
            Heat = colorRampPalette(c("red", "orange","yellow","lightyellow"),
                        space="rgb")(20),
            BlueWhiteRed =  colorRampPalette( c("blue", "white", "red"),
                       space="rgb")(20),
            Custom = colorRampPalette(customColors, space="rgb")(20),
            colorRampPalette(c("black", "red", "orange","yellow","lightyellow"),
                        space="rgb")(20)
            )

      linkmethod <- switch(as.character(agglomeration),
            "0" = "single",
            "1" = "complete",
            "2" = "average",
            "3" = "mcquitty",
            "4" = "ward",
            "5" = "median",
            "6" = "centroid",
            "complete"  # default case for switch
            )
      distmethod <- switch(as.character(distance),
            "0" = "euclidean",
            "1" = "maximum",
            "2" = "manhattan",
            "3" = "canberra",
            "4" = "binary",
            "5" = "pearson",
            "6" = "correlation",
            "7" = "spearman",
            "8" = "kendall",
            "euclidean"  # default case for switch
            )

      ColSides <- FALSE
      RowSides <- FALSE
      clust_color <- rep(color,dim(x)[1])
      # Factor Colors
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
        ColSides <- TRUE
      }
      # end Factor Colors

      #clustResult <- 0
      if (is.numeric(Kmeans))
      {
        if (Kmeans >= dim(x)[1])
            stop("Too many clusters requested.")
        ############impute####################
        x1 <- imputeData(x, mode="median")
        ######################################
        if (fixSeed)
        {
            set.seed(1234)
            N <- 1
        }
        else
            N <- 10

        require(amap)
        km <- Kmeans(x1, Kmeans, iter.max=100, nstart=N)
        x1 <- cbind(km$cluster, x1)
        #############
        clustResult <- as.matrix(km$cluster)
        colnames(clustResult) <- "Km_Clusters"
        #############

        x2 <- x1[order(x1[,1]),]
        x <- x[order(x1[,1]),]
        # Cluster Colors
        clust_color <- rep(color,dim(x)[1])
        colStep <- Kmeans
        colorRange <- hsv(h = seq(0,1,1/colStep), s=1, v=1)
        for (i in 1:Kmeans)
        {
            idx <- which(x2[,1]==i)
            clust_color[idx] <- colorRange[i]
        }
        RowSides <- TRUE
        rDend = NA
        # end Clust Colors
      }

      OUT <- heatmap.dante(x, color=cmap, Rowv=rDend, Colv=cDend,
                scale= if (rowscale) "row" else "none",
                hclustfun=hcluster,
                distmethod=distmethod,
                linkmethod=linkmethod,
                cexCol=1*labelscale,
                cexRow=0.8*labelscale,
                ColSideColors=if (ColSides) box_color else NA,
                RowSideColors=if (RowSides) clust_color else NA,
                colRange=colRange,
                noxlab = noxlab,
                margin=c(12,10),...)

      #browser()
      if (!is.numeric(Kmeans))
      {
          clustResult <- as.matrix(1:dim(x)[1])
          rownames(clustResult) <- rev(OUT$labRow)
          colnames(clustResult) <- "HC_Order"
      }
      x <- OUT$X
      #browser()
      # Color legend
      Min <- signif(min(x,na.rm=TRUE), digits=2)
      Max <- signif(max(x,na.rm=TRUE), digits=2)
      Mid <- signif((Min+Max)/2, digits=2)
      col.labels <- c(Min, Mid, Max)

      if (is.null(rDend) && !is.null(cDend))
      { xa <- .94; xb <- .96 }
      else
      { xa <- .87; xb <- .89 }
      color.legend(xa,.3,xb,.7, col.labels, cmap, align="rb", cex=0.8,gradient="y")
      # end color legend
      text(1,1,"a",col="white") # :-)) ????

      if (!is.null(stamp))
            mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
                  " (", stamp, ")", sep=""),col=1,cex=.6,line=3, side=1, adj=1)
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
    print(ex)
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

  return(clustResult)
}

#------------------------------------------------------------------------------
heatmap.dante <- function (x, Rowv = NULL, Colv = if (symm) "Rowv" else NULL,
    distmethod = "euclidean", hclustfun = hcluster, linkmethod="complete",
    reorderfun = function(d, w) reorder(d, w), add.expr, symm = FALSE, 
    revC = identical(Colv, "Rowv"), scale = c("row", "column", "none"), na.rm = TRUE,
    margins = c(5, 5), ColSideColors, RowSideColors, cexRow = 0.2 +
        1/log10(nr), cexCol = 0.2 + 1/log10(nc), labRow = NULL,
    labCol = NULL, main = NULL, xlab = NULL, ylab = NULL, keep.dendro = FALSE,
    color = heat.colors(20), colRange=NULL, noxlab = FALSE,
    verbose = getOption("verbose"), ...)
{
    require(plotrix)
    require(amap)
    
    scale <- if (symm && missing(scale))
        "none"
    else match.arg(scale)
    if (length(di <- dim(x)) != 2 || !is.numeric(x))
        stop("'x' must be a numeric matrix")
    nr <- di[1]
    nc <- di[2]
    if (nr <= 1 || nc <= 1)
        stop("'x' must have at least 2 rows and 2 columns")
    if (!is.numeric(margins) || length(margins) != 2)
        stop("'margins' must be a numeric vector of length 2")

    ############impute####################
    y <- matrix(NA, nr, nc)
    y[is.na(x)] <- 1   # NA's are stored
    x <- imputeData(x, mode="median")
    ######################################

    doRdend <- !identical(Rowv, NA)
    doCdend <- !identical(Colv, NA)
    if (is.null(Rowv))
        Rowv <- rowMeans(x, na.rm = na.rm)
    if (is.null(Colv))
        Colv <- colMeans(x, na.rm = na.rm)
    if (doRdend) {
        if (inherits(Rowv, "dendrogram"))
            ddr <- Rowv
        else {
            hcr <- hclustfun(x, method=distmethod, link=linkmethod) # Find the row dendrogram
            ddr <- as.dendrogram(hcr)
            if (!is.logical(Rowv) || Rowv)
                ddr <- reorderfun(ddr, Rowv)
        }
        if (nr != length(rowInd <- order.dendrogram(ddr)))
            stop("row dendrogram ordering gave index of wrong length")
    }
    else rowInd <- 1L:nr
    if (doCdend) {
        if (inherits(Colv, "dendrogram"))
            ddc <- Colv
        else if (identical(Colv, "Rowv")) {
            if (nr != nc)
                stop("Colv = \"Rowv\" but nrow(x) != ncol(x)")
            ddc <- ddr
        }
        else {
            hcc <- hclustfun(t(x), method=distmethod, link="complete") # Find the column dendrogram
            ddc <- as.dendrogram(hcc)
            if (!is.logical(Colv) || Colv)
                ddc <- reorderfun(ddc, Colv)
        }
        if (nc != length(colInd <- order.dendrogram(ddc)))
            stop("column dendrogram ordering gave index of wrong length")
    }
    else colInd <- 1L:nc

    x <- x[rowInd, colInd] # Here are the row and column indices
    y <- y[rowInd, colInd] ###############################

    labRow <- if (is.null(labRow))
        if (is.null(rownames(x)))
            (1L:nr)[rowInd]
        else rownames(x)
    else labRow[rowInd]
    labCol <- if (is.null(labCol))
        if (is.null(colnames(x)))
            (1L:nc)[colInd]
        else colnames(x)
    else labCol[colInd]
    
    if (scale == "row") {
        x <- sweep(x, 1, rowMeans(x, na.rm = na.rm), check.margin = FALSE)
        sx <- apply(x, 1, sd, na.rm = na.rm)
        x <- sweep(x, 1, sx, "/", check.margin = FALSE)
    }
    else if (scale == "column") {
        x <- sweep(x, 2, colMeans(x, na.rm = na.rm), check.margin = FALSE)
        sx <- apply(x, 2, sd, na.rm = na.rm)
        x <- sweep(x, 2, sx, "/", check.margin = FALSE)
    }
    lmat <- rbind(c(NA, 3), 2:1)
    lwid <- c(if (doRdend) 1 else 0.05, 4)
    lhei <- c((if (doCdend) 1 else 0.05) + if (!is.null(main)) 0.2 else 0,
        4)
    if (!is.na(ColSideColors)) {
        if (!is.character(ColSideColors) || length(ColSideColors) !=
            nc)
            stop("'ColSideColors' must be a character vector of length ncol(x)")
        lmat <- rbind(lmat[1, ] + 1, c(NA, 1), lmat[2, ] + 1)
        lhei <- c(lhei[1], 0.2, lhei[2])
    }
    if (!is.na(RowSideColors)) {
        if (!is.character(RowSideColors) || length(RowSideColors) !=
            nr)
            stop("'RowSideColors' must be a character vector of length nrow(x)")
        lmat <- cbind(lmat[, 1] + 1, c(rep(NA, nrow(lmat) - 1),
            1), lmat[, 2] + 1)
        lwid <- c(lwid[1], 0.2, lwid[2])
    }
    lmat[is.na(lmat)] <- 0
    if (verbose) {
        cat("layout: widths = ", lwid, ", heights = ", lhei,
            "; lmat=\n")
        print(lmat)
    }
    op <- par(no.readonly = TRUE)
    on.exit(par(op))
    layout(lmat, widths = lwid, heights = lhei, respect = TRUE)
    if (!is.na(RowSideColors)) {
        par(mar = c(margins[1], 0, 0, 0.5))
        image(rbind(1L:nr), col = RowSideColors[rowInd], axes = FALSE)
    }
    if (!is.na(ColSideColors)) {
        par(mar = c(0.5, 0, 0, margins[2]))
        image(cbind(1L:nc), col = ColSideColors[colInd], axes = FALSE)
    }
    par(mar = c(margins[1], 0, 0, margins[2]))
    if (!symm || scale != "none")
    {
        x <- t(x)
        y <- t(y)
    }
    if (revC) {
        iy <- nr:1
        ddr <- rev(ddr)
        x <- x[, iy]
        y <- y[, iy]
    }
    else iy <- 1L:nr
    ###### color range
    if (!is.null(colRange))
    {
      scaleMin <- colRange[1]
      scaleMax <- colRange[2]
      x[x > scaleMax] <- scaleMax
      x[x < scaleMin] <- scaleMin
    }
    ######
    image(1L:nc, 1L:nr, x, xlim = 0.5 + c(0, nc), col = color, ylim = 0.5 +
        c(0, nr), axes = FALSE, xlab = "", ylab = "", ...)
    ############################Plot the missing in grey#####################
    image(1L:nc, 1L:nr, y, xlim = 0.5 + c(0, nc), col = "grey", ylim = 0.5 +
        c(0, nr), axes = FALSE, xlab = "", ylab = "", add = TRUE, ...)
    #########################################################################
    axis(1, 1L:nc, labels = labCol, las = 2, line = -0.5, tick = 0,
        cex.axis = cexCol)
    if (!is.null(xlab))
        mtext(xlab, side = 1, line = margins[1] - 1.25)
    if (!noxlab)
        axis(4, iy, labels = labRow, las = 2, line = -0.5, tick = 0,
            cex.axis = cexRow)
    if (!is.null(ylab))
        mtext(ylab, side = 4, line = margins[2] - 1.25)
    if (!missing(add.expr))
        eval(substitute(add.expr))
    par(mar = c(margins[1], 0, 0, 0))
    if (doRdend)
        plot(ddr, horiz = TRUE, axes = FALSE, yaxs = "i", leaflab = "none")
    else frame()
    par(mar = c(0, 0, if (!is.null(main)) 1 else 0, margins[2]))
    if (doCdend)
        plot(ddc, axes = FALSE, xaxs = "i", leaflab = "none")
    else if (!is.null(main))
        frame()
    if (!is.null(main))
        title(main, cex.main = 1.5 * op[["cex.main"]])

    invisible(list(rowInd = rowInd, colInd = colInd, 
            labRow = labRow, labCol = labCol,
            X=x,
            Rowv = if (keep.dendro && doRdend) ddr, 
            Colv = if (keep.dendro && doCdend) ddc))
}
