# Written by Jared A. Kirschner
# for the Translation Genomics Research Institute (TGen, Phoenix, AZ)
# Copyright 2010, TGen
# E-mail: jkirschner@tgen.org
#         proteomics@pnnl.gov
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

PlotRows <- function(x, idx, file="deleteme.png", bkground="white")
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

    require(ggplot2)
    
    tryCatch(
    {
      xlabels <- colnames(x)
      # idx <- rev(idx)

      plot.labels <- rownames(x[idx,,drop=FALSE])
      X <- x[idx,];
      
      data <- matrix(nrow = (NCOL(X)*NROW(X)), ncol = 3);
      
      low.it <- 1;
      upp.it <- NCOL(X);
      
      for (i in 1:NROW(X)) {
      
        # Column 1: data.  Column 2: protein.  Column 3: sample.
        data[low.it:upp.it,] =
          cbind(
            X[i,],
            i,
            1:NCOL(X)
          );
      
        low.it <- upp.it + 1;
        upp.it <- upp.it + NCOL(X);
      }
      
      data <- as.data.frame(data);
      colnames(data) <- c("myData", "peptide", "sample");

      total.plot <- ggplot(data=data, aes( x=sample,y=myData,
            colour=factor(peptide) ), na.rm=TRUE) +
          geom_line() +
          geom_point() +
          scale_colour_hue(l=59,c=150,
            breaks=1:NROW(X),labels=plot.labels,name="Peptide") +
          scale_x_continuous(breaks=1:NCOL(X),labels=xlabels) +
          opts(
            axis.text.x=theme_text(angle=90,hjust=1,col="grey30"),
            axis.title.x = theme_blank(),
            axis.title.y = theme_blank()#,
#            plot.background = theme_rect(colour = "black")#,
#            panel.background = theme_rect(colour=NA,fill="grey90")#,
#            panel.grid.minor = theme_line(colour="white"),
#            panel.grid.major = theme_line(colour="grey50")
          );
      
      total.plot

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
