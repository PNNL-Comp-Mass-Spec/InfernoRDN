# Written by Jared A. Kirschner
# for the Translation Genomics Research Institute (TGen, Phoenix, AZ)
# Copyright 2010, TGen
# E-mail: jkirschner@tgen.org
#
# Inspired by code written by Ashoka D. Polpitiya
# for the Department of Energy (PNNL, Richland, WA)
# R plotting functions used in DAnTE
# -------------------------------------------------------------------------
# Licensed under the Apache License, Version 2.0; you may not use this file except
# in compliance with the License.  You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
#
# Implements ggplot2 package plotting functions used in Inferno.
# -------------------------------------------------------------------------

plotHeatmapCorr <- function(x,
                            file="deleteme.png",
                            cMap="Default",
                            corRange=c(0,1),
                            bkground="white",
                            customColors=c("#FF0000","#00FF00", "#0000FF"),
                            labelscale=1,
                            stamp=NULL,
                            ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)
  #par(oma=c(3.4, 2, 2, 2), mar=c(25,25,4,1))

  require(ggplot2);

  tryCatch(
  {
      Xcor <- cor(x,method="pearson",use="pairwise.complete.obs")
      Xcor <- abs(Xcor)

      ##### Correction for correlation range selection #########
      Xcor[Xcor > corRange[2]] <- corRange[2]
      Xcor[Xcor < corRange[1]] <- corRange[1]
      Xcor[1,1] <- corRange[1] # to make sure the lower and upper limits are ..
      Xcor[2,2] <- corRange[2] # represented in the image. These will be
                               # replaced in heatmap.corr with 1
      ##########################################################
      #Xcor <- mirror.df(Xcor);
      
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
            Custom = colorRampPalette(customColors, space="rgb")(20)
            )
      
      uF <- dim(Xcor)[1]; # unique factors
      #horizontal.index = 1:uF;
      #vertical.index = 1:uF;
      
      Xcor.df <- matrix(nrow=uF^2,ncol=3);
          
      Xcor.df <- as.data.frame(Xcor.df);
      colnames(Xcor.df) = c("x","y","corr");

      for (i in 1:uF) {
        Xcor.df$x[((i-1)*uF+1):(i*uF)] <- i;
        Xcor.df$y[((i-1)*uF+1):(i*uF)] <- uF:1;
        Xcor.df$corr[((i-1)*uF+1):(i*uF)] <- Xcor[,i];
        Xcor.df$corr[((i-1)*uF+i)] <- 1.0; # force same sample to be
          # correlated  
      }      

      ###################
      base.heatMap <- ggplot(data=Xcor.df, aes(x=factor(x),y=factor(y)),
                        na.rm=TRUE);
      
      if (cMap!="Default" && length(cmap)>1)
          { c.scale <- scale_fill_gradientn(colour = cmap,limits=corRange); }
      else { c.scale <- scale_fill_gradient(limits=corRange); }
        
      c.scale$legend <- FALSE;
                        
      heatMap <- base.heatMap + 
                  geom_tile(aes(fill=corr),colour="white") +
                  scale_x_discrete("",breaks=seq(1:NCOL(Xcor)),
                      labels=colnames(Xcor),expand=c(0,0)) +
                  scale_y_discrete("",breaks=seq(1:NCOL(Xcor)),
                      labels=rev(colnames(Xcor)),expand=c(0,0)) +
                  c.scale +
                  opts(
                      axis.text.x=theme_text(angle=-90, hjust=0,col="grey30"),
                      axis.text.y=theme_text(col="grey30",hjust=1),
                      plot.margin = unit(c(0,1,-.5,-.5), "lines")
                  );
      ###################
      base.colorHist <- ggplot(data=Xcor.df, aes(x=corr));
      
      if (cMap!="Default" && length(cmap)>1) {
        man.breaks = seq(from=corRange[1],to=corRange[2],
            length.out=length(cmap)+1);
      } else {
        man.breaks=seq(from=corRange[1],to=corRange[2],length.out=31);
      }
      colorHist.graph <- stat_bin(fill="NA",colour="cyan",geom="histogram",
            breaks=man.breaks,right=FALSE,fill="NA");
      
      h <- hist(Xcor.df$corr, breaks=man.breaks, plot=FALSE,right=FALSE)
      ymax <- max(h$counts);
      
      color.data = as.data.frame(cbind(man.breaks-(man.breaks[2]-man.breaks[1])/2,
        ymax/2,ymax));
      colnames(color.data) <- c("x","y","ymax");
      
      color.ref <- geom_tile(data=color.data,aes(x=x,y=y,fill=x,height=ymax));
      
      colorHist <- base.colorHist + 
            color.ref +
            c.scale +
            colorHist.graph +
            scale_x_continuous("Value",expand=c(0,0),limits=corRange) +
            scale_y_continuous("",expand=c(0,0)) +
            opts(
                legend.position="none",
                title="Color Key and Histogram",
                plot.title = theme_text(size=10,colour="black",face="bold"),
                axis.title.x = theme_text(size=8,colour="grey15",vjust=.85),
                axis.text.y = theme_text(size=8,colour="grey40",hjust=.82,
                    vjust=.4),
                axis.text.x = theme_text(size=8,colour="grey40",
                    vjust=.7),
                plot.margin = unit(c(.5,.25,.25,0), "lines")
            );        
      
      ###################
      grid.newpage()
      pushViewport(viewport(layout=grid.layout(10,10) ) )
      
      #print(heatMap,vp=vp.layout(4:10,1:10));
      #print(colorHist,vp=vp.layout(1:3,1:4));
      print(heatMap,vp=vp.layout(3:10,1:10));
      print(colorHist,vp=vp.layout(1:2,1:2));

      #if (!is.null(stamp))
      #      mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
      #            " (", stamp, ")", sep=""),col=1,cex=.6,line=3, side=1, adj=1)
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

mirror.df <- function(x)
{
  xx <- rev(x)
  dim(xx) <- dim(x)
  rownames(xx) <- rev(rownames(x))
  colnames(xx) <- colnames(x)
  xx
} 
