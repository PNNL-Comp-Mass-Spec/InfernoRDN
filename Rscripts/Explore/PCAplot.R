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

plotPCA <- function(Data,
                    Factor=1,
                    PCs=c(1,2),
                    file="deleteme.png",
                    bkground="white",
                    Lines=TRUE,
                    Persp=TRUE,
                    biplotting=FALSE,
                    Labels=TRUE,
                    scree=FALSE,
                    biplotL=FALSE,
                    Arrows=TRUE,
                    Type = c('PCA','PLS'),
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
        if (dim(Data)[2] < 3)
        {
            plot(c(1,1),type="n",axes=F,xlab="",ylab="")
            text(1.3,1,"Too few datasets!",cex=1.2)
        }
        else if (dim(na.omit(Data))[1] < 4)
        {
            plot(c(1,1),type="n",axes=F,xlab="",ylab="")
            text(1.3,1,"Too many missing values.\n Or too few features.",cex=1.2)
        }
        else
        {
            txtlabels <- colnames(Data)
            Dim <- length(PCs)
            pca_color <- rep("blue",dim(Data)[2])

            Factor <- Factor[is.element(names(Factor),colnames(Data))]
            if (length(Factor) == dim(Data)[2])
            {
                uF <- unique(Factor)
                colStep <- length(uF)
                #colorRange <- rainbow(colStep)
                colorRange <- hsv(h = seq(0,1,1/colStep), s=.9, v=.9)
                #colorRange <- hsv(h = seq(0,1,1/colStep), s=1, v=1)
                for (i in 1:length(uF))
                {
                    idx <- which(uF[i]==Factor)
                    pca_color[idx] <- colorRange[i]
                }
            }
            switch(match.arg(Type),
                PCA = {
                        completeData <- Data[complete.cases(Data),]
                        pepLabels <- rownames(completeData)
                        try(Object <- prcomp(t(completeData),scale=TRUE,retx=TRUE),
                            silent=TRUE)
                        if (!exists("Object"))
                            Object <- prcomp(t(completeData),scale=FALSE,retx=TRUE)
                        rot <- Object$rotation
                        x <- Object$x
                        #x <- Object$rotation
                        #rot <- Object$x
                        eigens <- (Object$sdev)^2
                        percentVar <- signif(eigens[PCs]/sum(eigens)*100,digits=3)
                        percentCumVar <- signif(sum(eigens[PCs])/sum(eigens)*100,digits=3)
                        mainLabel = paste("PCA Plot (", percentCumVar, "%)", sep="")
                        print('__________PCA Variation___________________________________')
                        print(summary(Object))
                        print('__________________________________________________________')
                        screep <- summary(Object)
                        screep <- screep$importance[2,]
                      },
                PLS = {
                        require(pls)
                        plsF <- plsFactors(Factor)
                        completeData <- Data[complete.cases(Data),]
                        pepLabels <- rownames(completeData)
                        plsData <- list(y=plsF, x=t(completeData))
                        Object <- plsr(y~x, data = plsData, x=TRUE, y=TRUE,
                                         na.action=na.exclude)
                        x <- Object$scores
                        rot <- Object$loadings
                        percentVar <- signif(Object$Xvar/Object$Xtotvar * 100, digits=3)
                        percentCumVar <- signif(sum(percentVar[PCs]), digits=3)
                        mainLabel = paste("PLS Plot (", percentCumVar, "%)", sep="")
                        screep <- percentVar
                        print('___________PLS Variation__________________________________')
                        print(signif(Object$Xvar/Object$Xtotvar * 100, digits=4))
                        print('__________________________________________________________')
                      }
            ) #switch
            #pvals <- apply(rot, 2, pvalsNormal)
            pvals <- apply(abs(rot), 2, function(x) 1 - ecdf(x)(x))
            rownames(pvals) <- rownames(rot)

            Xlabel <- paste("PC",PCs[1]," (",percentVar[1],"%)",sep="")
            Ylabel <- paste("PC",PCs[2]," (",percentVar[2],"%)",sep="")
            Zlabel <- paste("PC",PCs[3]," (",percentVar[3],"%)",sep="")

            if (biplotting)
            {
                biplot.dante(Object, Rownames=pepLabels,
                        Colnames=txtlabels, Type=Type, PCs=PCs, Labels=biplotL,
                        col.obj=pca_color, Arrows=Arrows, perspective=Persp)
                if (length(Factor) == dim(Data)[2])
                    legend("topleft",uF,col=colorRange[1:length(uF)],pch=19,
                        bg='transparent', inset = .02)
                if (scree)
                {
                    par(new = TRUE, fig = c(.84, .94, .8, .9), mar = c(0,0,0,0))
                    barplot(screep,main="",cex.axis=.6,cex.names=.6,axisnames=F)
                }
            }
            else
            {
                if (Dim == 2)
                {
                    plot(x[,PCs], cex=2, col=pca_color, pch=19,
                        xlab=Xlabel,ylab=Ylabel,main=mainLabel)
                    if (Labels)
                        text(x[,PCs],txtlabels,cex=.6,pos=4) # text labels
                    grid()
                }
                else
                {
                    if (Persp)
                    {
                        scatter.3D(x[,PCs],col=pca_color,pch=19,cex=2,
                            xlab=Xlabel,
                            ylab=Ylabel,
                            zlab=Zlabel,
                            main=mainLabel,
                            Lines=Lines,
                            txtLabels=txtlabels, Labels=Labels)
                    }
                    else
                    {
                        require(scatterplot3d)
                        Type="p"
                        if (Lines)
                            Type="h"
                        rR <- scatterplot3d(x[,PCs[1]],
                            x[,PCs[2]],x[,PCs[3]],
                            color=pca_color,cex.symbols=2,pch=19, xlab=Xlabel,
                            ylab=Ylabel,zlab=Zlabel,
                            box=FALSE,grid=TRUE,main=mainLabel,
                            angle=35,type=Type)
                        if (Labels)
                        {
                            text(rR$xyz.convert(x[,PCs]),txtlabels,cex=.6,pos=4) #labels
                        }
                    }
                }

                if (length(Factor) == dim(Data)[2])
                    legend("topleft",uF,col=colorRange[1:length(uF)],pch=19,
                        bg='transparent', inset = .02)
                if (scree)
                {
                    par(new = TRUE, fig = c(.84, .94, .8, .9), mar = c(0,0,0,0))
                    barplot(screep,main="",cex.axis=.6,cex.names=.6,axisnames=F)
                }
            }
            if (!is.null(stamp))
                mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
                    " (", stamp, ")", sep=""),col=1,cex=.6,line=2, side=1,
                    adj=1, outer=T)
            #return(list(X=rot, P=pvals, Mode=Type))
            invisible(list(X=pvals, P=pvals, Mode=Type))
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
      text(1.5,1,paste("Error:", ex),cex=.9)
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

#-------------------------------------------------------------

plsFactors <- function(factors)
{
    plsF <- rep(numeric(0),length(factors))
    uF <- unique(factors)
    ulevels <- seq(from=0, to=length(uF)-1, by=1)
    for (i in 1:length(uF))
    {
        idx <- which(uF[i] == factors)
        plsF[idx] <- ulevels[i]
    }
    return(plsF)
}

#-------------------------------------------------------------------------
scatter.3D <- function(x,xlim=NULL,ylim=NULL,zlim=NULL,
                       col=par("col"),
                       pch=par("pch"), cex=par("cex"),
                       main=NULL, Lines=TRUE,
                       xlab=NULL, ylab=NULL, zlab=NULL,
                       txtLabels=NULL, Labels=FALSE, ...)
{
   x1 <- x

   if(is.matrix(x))
   {
     z <- x[,3]
     y <- x[,2]
     x <- x[,1]
     x1[,3] <- rep(min(z,na.rm=T),length(x1[,3])) # z1
   }
   if(is.matrix(x1))
   {
     z1 <- x1[,3]
     y1 <- x1[,2]
     x1 <- x1[,1]
   }
   if(missing(zlim))
   {
     z.grid <- matrix(range(z),2,2)
   }
   else
   {
     z.grid <- matrix(zlim,2,2)
   }

   if(missing(xlim)){ xlim <- range(x) }
   if(missing(ylim)){ ylim <- range(y) }

   persp(xlim, ylim, z.grid, col = NA, border=NA,
         xlab=xlab, ylab=ylab, zlab=zlab,
         main=main, theta=35, phi=25, d=2,
         ticktype = "detailed",...) -> res
   #----------------------------------
   trans3d <- function(x,y,z, pmat)
   {
     tr <- cbind(x,y,z,1) %*% pmat
     list(x = tr[,1]/tr[,4], y= tr[,2]/tr[,4])
   }
   #----------------------------------
   out <- trans3d(x,y,z,pmat=res)
   out1 <- trans3d(x1,y1,z1,pmat=res)
   if (Lines)
   {
      for(i in 1:length(out$x))
      {
        lines(c(out$x[i],out1$x[i]),c(out$y[i],out1$y[i]), col="gray", ...)
      }
   }
   points(out, col=col, pch=pch, cex=cex, ...)
   if (Labels)
      text(out,txtLabels,cex=.6,pos=4) # text labels

   return(invisible(out))
}

#-----------------------------------------------------------------------

biplot.dante <- function (object,
                       Rownames,
                       Colnames,
                       Type = c('PCA','PLS'),
                       PCs=c(1,2),
                       box=FALSE,
                       Labels=TRUE,
                       col.obj=2,
                       col.var=1,
                       cex=.6,
                       Arrows=TRUE,
                       perspective=FALSE, ... )
{
    n.values <- length(PCs)
    switch(match.arg(Type),
        PCA = {
            scores <- object$x[,PCs]
            rots <- object$rotation[,PCs]
            eigens <- (object$sdev)^2
            percentVar <- signif(eigens[PCs]/sum(eigens)*100,digits=3)
            percentCumVar <- signif(sum(eigens[PCs])/sum(eigens)*100,digits=3)
            mainBiLabel = paste("PCA Bi-Plot (", percentCumVar, "%)", sep="")
            n <- NROW(object$x)
            lam <- object$sdev[PCs] * 2
            scores <- t(t(scores)/lam)
            rots <- t(t(rots) * lam)
            #scores <- customScale(scores,rots)
        },
        PLS = {
            scores <- object$scores[,PCs]
            rots <- object$loadings[,PCs]
            scores <- customScale(scores,rots)
            percentVar <- signif(object$Xvar/object$Xtotvar * 100, digits=3)
            percentCumVar <- signif(sum(percentVar[PCs]), digits=3)
            mainBiLabel = paste("PLS Bi-Plot (", percentCumVar, "%)", sep="")
        })

    Xlabel = paste("PC",PCs[1]," (",percentVar[1],"%)",sep="")
    Ylabel = paste("PC",PCs[2]," (",percentVar[2],"%)",sep="")
    Zlabel = paste("PC",PCs[3]," (",percentVar[3],"%)",sep="")

    Vars <- rbind(scores, rots, rep(0, n.values)) # scores = samples, rots = features

    if (length(col.obj) == 1)
        col.obj = rep(col.obj,nrow(scores))

    if(n.values == 2) {
      plot(Vars, xlab=Xlabel, ylab=Ylabel, main=mainBiLabel,
           type = if(Labels) 'n' else 'p',
           col=col.var, pch = 20) # samples + features
      points(scores,col=col.obj, pch = 19, type='p') # variables
      grid()

      if(Labels)
        text(x=rots[,1], y=rots[,2], labels = Rownames,
           cex=cex, col=col.var, pos=4)
      if (Arrows)
        arrows(x0=0, y0=0, x1=rots[,1], y1=rots[,2], length=0.1, angle=20,
               col=col.var)
      text(x=scores[,1], y=scores[,2], labels=Colnames,
             cex=cex, col=col.obj, pos=4)
    }
    if(n.values == 3) {
        if (perspective)
        {
            x1 <- Vars[,1]
            y1 <- Vars[,2]
            z1 <- Vars[,3]
            xlim <- range(x1)
            ylim <- range(y1)
            z.grid <- matrix(range(z1),2,2)
            persp(xlim, ylim, z.grid, col = NA, border=NA,
                xlab=Xlabel, ylab=Ylabel, zlab=Zlabel,
                main=mainBiLabel, theta=35, phi=25, d=2,
                ticktype = "detailed") -> res
            out <- trans3d(scores[,1],scores[,2],scores[,3],pmat=res)
            out1 <- trans3d(rots[,1],rots[,2],rots[,3],pmat=res)
            out0 <- trans3d(0,0,0,pmat=res)
            if(Labels)
                text(out1, labels=Rownames, col=1, cex=cex, pos=4)
            else
                points(out1, col=col.var, pch=20, cex=cex)
            points(out, col=col.obj, pch=19, cex=1.3)
            if (Arrows)
            {
                for(i in 1:length(out1$x))
                {
                    lines(c(out1$x[i],out0$x[1]),c(out1$y[i],out0$y[1]),
                          col=col.var)
                }
            }
            text(out,Colnames,cex=.6,col=col.obj, pos=4)
        }
        else
        {
            require(scatterplot3d)
            graph = scatterplot3d(Vars,
                                  type = if(Labels) 'n' else 'p',
                                  xlab = Xlabel,
                                  ylab = Ylabel,
                                  zlab = Zlabel,
                                  main = mainBiLabel,
                                  grid = TRUE, box = box,
                                  cex.symbols = cex, color = col.var, pch = 20)
            graph$points3d(scores[,1], scores[,2], scores[,3],
                           pch = 19, type='p', col=col.obj, cex = 1.3)
            if(Labels)
               text(graph$xyz.convert(rots), labels=Rownames,
                    col=col.var, cex=cex, pos=4)

            if (Arrows)
            {
                for(i in 1:nrow(rots))
                {
                    graph$points3d(c(0, rots[i,1]), c(0, rots[i,2]),
                             c(0, rots[i,3]), type='l', col=col.var)
                }
            }
            text(graph$xyz.convert(scores), labels=Colnames, col=col.obj,
                 cex=cex, pos=4)
        }
    }
}
