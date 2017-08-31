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
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)

  tryCatch(
  {
      grid.newpage()
      pushViewport(viewport(layout=grid.layout(NCOL(x),NCOL(x)) ) );
        
      title.plots <- title.plot.generator(x);
        
      for (i in 1:length(title.plots)) {
          print(title.plots[[i]],vp=vp.layout(i,i));
      }

      for (i in 1:NCOL(x)) {
          j = i+1;
          
          while (j <= NCOL(x)) {
            
            print(
                panel.MA(x[,i],x[,j],dcolor=dCol,lcolor=lCol),
                vp=vp.layout(j,i)
                );

            j <- j+1;
            
          }
          
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
      text(1.5,1,paste("Error:", ex),cex=1)
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

panel.MA <- function(x, y, dcolor="green", lcolor="red",
                     sp=0.2, ...) # single pairs plot
{

    Mean <- (x + y)/2;
    Diff <- x - y;
    
    MA.df <- data.frame(Mean,Diff);
    colnames(MA.df) <- c("mean","diff");
    
    MA.plot <- ggplot(data=MA.df,aes(x=mean,y=diff),na.rm=TRUE) +
        geom_point(alpha=.25, color="black", size=1.3) + geom_point(alpha=.25, color=dcolor) + 
        stat_smooth(method="loess",family="gaussian",span=sp,colour=lcolor,
          size=1,fill=lcolor) +
        geom_abline(intercept=0,slope=0,colour="blue",size=1) +
        scale_x_continuous("") +
        scale_y_continuous("") +
        opts(
           plot.background = theme_rect(colour = "black"),
           plot.margin = unit(c(.4,.3,-.35,-.35), "lines"),
           axis.title.x = theme_blank(),
           axis.title.y = theme_blank(),
           axis.text.y = theme_text(size=8,colour="grey40",hjust=.82,
              vjust=.4),
           axis.text.x = theme_text(size=8,colour="grey40",
              vjust=.7)
        );
        
    MA.plot
}

title.plot.generator <- function(x) {

    all.graphs = vector("list",NCOL(x));
    
    for (i in 1:NCOL(x)) {
    
      htitle <- colnames(x)[i];
      
      my.df <- data.frame(0,0,htitle);
      colnames(my.df) <- c("x","y","title");
      
      base.plot <- ggplot(data=my.df);

      all.graphs[[i]] <- base.plot +
          geom_text(aes(x=x,y=y,label=title)) +
          theme_bw() +
          opts(
            plot.background = theme_rect(colour = "grey40", fill = "grey40"),
            panel.grid.major = theme_blank(),
            panel.grid.minor = theme_blank(),
            axis.line = theme_blank(),
            axis.text.x = theme_blank(),
            axis.text.y = theme_blank(),
            axis.title.x = theme_blank(),
            axis.title.y = theme_blank(),
            axis.ticks = theme_blank(),
            strip.background = theme_blank(),
            plot.margin = unit(c(.2,.13,-1.1,-1.1), "lines")
          );
      }
    
    all.graphs

}