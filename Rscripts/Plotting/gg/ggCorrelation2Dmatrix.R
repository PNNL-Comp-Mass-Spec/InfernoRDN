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

plot2Dmat <- function(x,
                      file="deleteme.png",
                      bkground="white",
                      show.vals=TRUE,
                      corRange=c(0,1),
                      cMap="BlueWhiteRed",
                      customColors=c("#FF0000","#00FF00", "#0000FF"),
                      stamp=NULL,
                      isDiag=TRUE,
                      ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)
  require(ggplot2)

  tryCatch(
  {
      Xcor <- cor(x,method="pearson",use="pairwise.complete.obs")
      Xcor <- signif(Xcor,digits=3)
      Xcor <- abs(Xcor)

      if (isDiag) {
            Xcor[!lower.tri(Xcor,diag=TRUE)] <- NA;
      }

      #browser()
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
      
      txt.col <- "black";      
      txt.col <- switch (cMap,
            BlackBody = "darkcyan",
            GreenRed = "white",
            Heat = "black",
            BlueWhiteRed = "black",
            Custom = "white"
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

      Xcor.df <- as.data.frame(Xcor.df[!is.na(Xcor.df$corr),]);


        base.corrMatrix <- ggplot(data=Xcor.df, aes(x=factor(x),y=factor(y)),
                        na.rm=TRUE);
      
      if (cMap!="Default" && length(cmap)>1) {
        c.scale <- scale_fill_gradientn(colour = cmap,limits=corRange,
          name = "Correlation"); }
      else {
        c.scale <- scale_fill_gradient(limits=corRange, name = "Correlation"); }
        
      #c.scale$legend <- FALSE;
                        
      corrMatrix <- base.corrMatrix + 
                  geom_tile(aes(fill=corr),colour="white",na.rm=TRUE) +
                  scale_x_discrete("",breaks=seq(1:NCOL(Xcor)),
                      labels=colnames(Xcor),expand=c(0,0)) +
                  scale_y_discrete("",breaks=seq(1:NCOL(Xcor)),
                      labels=rev(colnames(Xcor)),expand=c(0,0)) +
                  c.scale +
                  theme_bw() +
                  opts(
                      axis.text.x=theme_text(angle=-90, hjust=0,col="grey30"),
                      axis.text.y=theme_text(col="grey30",hjust=1),
                      plot.margin = unit(c(.75,.25,-.5,-.5), "lines")
                      );
                      
      if (show.vals) {
          corrMatrix <- corrMatrix + 
              geom_text(aes(label=round(corr,3)),size=3,colour=txt.col);
      }
     
      grid.newpage()
      pushViewport(viewport(layout=grid.layout(1,1) ) )
      
      print(corrMatrix,vp=vp.layout(1,1));
      
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