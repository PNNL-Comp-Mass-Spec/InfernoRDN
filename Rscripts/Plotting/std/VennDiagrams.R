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

PlotVenn <- function(x1, x2, x3=NULL,
                listNames=c('A','B','C'),
                file="deleteme.png",
                bkground="white", Factor=1, Data=NULL)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
    
    tryCatch(
    {
      if (Factor != 1)
      {
        Z1 <- integer(0)
        Nreps <- unique(as.vector(t(Factor)))
        for (i in 1:length(Nreps)) # for each unique Factor value
        {
            idx <- which(Factor == Nreps[i])
            dataset <- Data[,idx,drop=FALSE] # extract data for sample i with all the replicates
    
            if (length(idx) > 1) # Process
                Z2 <- rowSums(dataset, na.rm=TRUE)
            else
                Z2 <- dataset
            Z1 <- cbind(Z1, Z2)
        }# for
        colnames(Z1) <- Nreps
        #browser()
        set1 <- Z1[,which(Nreps==x1),drop=FALSE]
        list1 <- names(set1[!is.na(set1),])
        set2 <- Z1[,which(Nreps==x2),drop=FALSE]
        list2 <- names(set2[!is.na(set2),])
        if (!is.null(x3)){
          set3 <- Z1[,which(Nreps==x3),drop=FALSE]
          list3 <- names(set3[!is.na(set3),])
        }
        idlist <- c(list1, list2, if (!is.null(x3)) list3)
        groups <- c(rep(listNames[1], length(list1)), rep(listNames[2], length(list2)),
                    if (!is.null(x3)) rep(listNames[3], length(list3)))
      }
      else {
        idlist <- c(rownames(x1), rownames(x2), if (!is.null(x3)) rownames(x3))
        groups <- c(rep(listNames[1], dim(x1)[1]), rep(listNames[2], dim(x2)[1]),
                    if (!is.null(x3)) rep(listNames[3], dim(x3)[1]))
      }
      Data <- data.frame(id=idlist, grp=groups)
      venn.1(Data$id, Data$grp, main="")
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

#------------- Functions for Plotting Venn diagrams  --------------
venn.1 <- function (id, category, cutoff = 1, duplicates = FALSE, tab,
    main)
{
    if (missing(tab)) {
        tab <- incidence.table.1(as.character(id), category, cutoff = cutoff,
            duplicates = duplicates)
        Nreps <- unique(category)
        nA <- sum(category == Nreps[1])
        nB <- sum(category == Nreps[2])
        nC <- ifelse((length(Nreps)==3),sum(category == Nreps[3]),NA)
        if (missing(main))
            main <- paste("Count of", deparse(substitute(id)),
                "by", deparse(substitute(category)))
    }
    else if (missing(main))
        main <- paste("Venn diagram of", deparse(substitute(tab)))

    index <- tab %*% 2^(1:ncol(tab) - 1)
    itab <- table(index)
    save <- par(pty = "s", mar = c(1, 0, 1, 0) * par("mar"))
    on.exit(par(save))
    if (ncol(tab) == 2) {
        plot(1, 1, xlim = c(-1.3, 2.3), ylim = c(-1.8, 1.8),
            bty = "n", axes = FALSE, type = "n", xlab = "", ylab = "",
            main = main)
        if (!is.na(zero <- itab[as.character(0)]))
            title(sub = paste(zero, "not shown"))
        cx <- c(0, 1.1)
        cy <- c(0, 0)
        mx <- mean(cx)
        my <- mean(cy)
        colr <- c(rgb(122,103,238,128,max=255),rgb(255,64,64,128,max=255))
#        rotx <- cos(seq(0,2*pi,.01))
#        roty <- sin(seq(0,2*pi,.01))
#        polygon(rotx+cx[1], roty+cy[1], col=colr[1], border=NA)
#        polygon(rotx+cx[2], roty+cy[2], col=colr[2], border=NA)
        symbols(cx, cy, circles = rep(1, 2), inches = FALSE,
            #add = TRUE, fg=c(1,2),lwd=4)
            add = TRUE, fg=NA, bg=colr, lwd=4)
        text(c(mx + 2 * (cx[1] - mx), mx + 2 * (cx[2] - mx)),
            c(my + 2 * (cy[1] - my), my + 2 * (cy[2] - my)),
            itab[as.character(c(1, 2))], col=c("blue","red"), cex=1.5)
        text(mx, my, itab["3"], col='black', cex=1.5)
        text(c((mx + 3 * (cx[1] - mx) + cx[1])/2, (mx + 3 * (cx[2] -
            mx) + cx[2])/2), c((my + 3 * (cy[1] - my) + cy[1] -
            1.8)/2, (my + 3 * (cy[2] - my) + cy[2] - 1.8)/2),
            pos = c(2, 4), colnames(tab), col=c("blue","red"), cex=2)
        vSummary <- c(paste(Nreps[1], "=", nA),paste(Nreps[2], "=", nB))
        legend("topleft", vSummary, text.col=c("blue","red"), box.lty=0)
    }
    else if (ncol(tab) == 3) {
        plot(1, 1, xlim = c(-1.5, 2.6), ylim = c(-1.5, 2.6),
            bty = "n", axes = FALSE, type = "n", xlab = "", ylab = "",
            main = main)
        if (!is.na(zero <- itab[as.character(0)]))
            mtext(paste(zero, "not shown"), side = 1)
        cx <- c(0, 1.1, 0.55)
        cy <- c(0, 0, 1.1 * sqrt(3)/2)
        mx <- mean(cx)
        my <- mean(cy)
        colr <- c(rgb(122,103,238,128,max=255),rgb(255,64,64,128,max=255),
                  rgb(60,179,113,128,max=255))
        symbols(cx, cy, circles = rep(1, 3), inches = FALSE,
            #add = TRUE, fg=c(1,2,3),lwd=4)
            add = TRUE, fg=NA, bg=colr, lwd=4)
        text(c(mx + 2 * (cx[3] - mx), mx + 2 * (cx[1] - mx),
            mx + 2 * (cx[2] - mx)), c(my + 2 * (cy[3] - my),
            my + 2 * (cy[1] - my), my + 2 * (cy[2] - my)), itab[as.character(c(1,
            2, 4))], col=c("darkgreen","blue","red"), cex=1.5)
        text(c(mx + (cx[1] + cx[3] - 2 * mx), mx + (cx[2] + cx[3] -
            2 * mx), mx + (cx[2] + cx[1] - 2 * mx)), c(my + (cy[1] +
            cy[3] - 2 * my), my + (cy[2] + cy[3] - 2 * my), my +
            (cy[2] + cy[1] - 2 * my)), itab[as.character(c(3,
            5, 6))], col='purple', cex=1.5)
        text(mx, my, itab["7"], col='black', cex=1.5)
        text(c(mx + 2.6 * (cx[3] - mx), (mx + 3 * (cx[1] - mx) +
            cx[1])/2, (mx + 3 * (cx[2] - mx) + cx[2])/2), c(my +
            2.6 * (cy[3] - my), (my + 3 * (cy[1] - my) + cy[1] -
            1.2)/2, (my + 3 * (cy[2] - my) + cy[2] - 1.2)/2),
            pos = c(3, 2, 4), colnames(tab), col=c("darkgreen","blue","red"), cex=2)
        vSummary <- c(paste(Nreps[1], "=", nA), paste(Nreps[2], "=", nB),
                      paste(Nreps[3], "=", nC))
        legend("topleft", vSummary, text.col=c("darkgreen","blue","red"), box.lty=0)
    }
    else stop("Can only Venn 2 or 3 categories")
}

incidence.table.1 <- function (id, category, names = NULL, cutoff = 1, duplicates = FALSE)
{
    if (!duplicates) {
        tab <- table(as.character(id), category)
        tab >= cutoff
    }
    else {
        tab <- table(as.character(id), category)
        result <- matrix(FALSE, length(id), ncol(tab))
        for (i in 1:ncol(tab)) result[, i] <- tab[as.character(id),
            i] >= cutoff
        rownames(result) <- as.character(names)
        colnames(result) <- colnames(tab)
        result
    }
}

