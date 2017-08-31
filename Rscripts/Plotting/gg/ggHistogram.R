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

# Plot the histograms to either a JPEG or PNG file
plot_hist <- function(data,
                      ncols = 1,
                      cells="sturges",
                      file="deleteme.png",
                      bkground="white",
                      colF="#ffc38a",
                      colB="#5FAE27",
                      addRug=TRUE,
                      stamp=NULL,
                      blend.rug=TRUE,
                      ...)
{
# ADD BLEND RUG FLAG!

    require(ggplot2);

    # Plot histograms, the distribution profile and the reference profile

    # png(filename=file,width=1152,height=864,pointsize=12,
    #        bg=bkground,res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,
      pointsize=FNTsize,bg=bkground,res=600);

    tryCatch(
    {
    
    plot.list = vector("list",NCOL(data));
    xlim1 = c(min(data,na.rm=TRUE)-1,max(data,na.rm=TRUE)+1);
    
    partitions = vector("list",NCOL(data));
   
    max.length = 0; # Will store maximum number of non-NA pts. in data

    ylim1 = c(0,0); ymax = 0;
    
    for (i in 1:NCOL(data)) {
   
            if (NCOL(data) == 1) {current.data = data;}
            else {current.data = data[,i];}
            
            if (is.numeric(cells)){
              partitions[[i]] <- seq(min(current.data)-0.001,max(current.data)+0.001,
                  len=(cells+1));
              # cells (int) controls the number of evenly distributed bins
              
              h <- hist(current.data, breaks=partitions[[i]],plot=FALSE,right=FALSE)
            }
            else {
              if (NCOL(data) == 1) { current.data = data; }
              else { current.data = data[,i]; }
            
              current.data = current.data[!is.na(current.data)];

              h <- hist(current.data, breaks=cells,plot=FALSE,right=FALSE)
              partitions[[i]] = h$breaks;
              
            }
            
            temp.ymax <- max(h$intensities)

            if (temp.ymax > ymax) { ymax = temp.ymax; }
    }
    
    ylim1[2] = ymax;
    
    nrow = NULL; # Default, can be changed
      
    n <- NCOL(data);
      
    if(is.null(nrow) & is.null(ncols)) { nrow = floor(n/2); ncols = ceiling(n/nrow)}
    if(is.null(nrow)) { nrow = ceiling(n/ncols)}
    if(is.null(ncols)) { ncols = ceiling(n/nrow)}

    ## NOTE see n2mfrow in grDevices for possible alternative
    grid.newpage()
    pushViewport(viewport(layout=grid.layout(nrow,ncols) ) )
    
    row.iterator = 0;
    column.iterator = 1;
      
    for (i in 1:NCOL(data)) {
            
            if (NCOL(data) == 1)
            {
              htitle = "Distribution"; # automatically sets title
              current.data = data;
            } 
            else { 
              htitle = colnames(data)[i]; 
              current.data = data[,i];
            }
            
            current.data = current.data[!is.na(current.data)];
            
            data = as.data.frame(data);
            base.plot = ggplot(data=data);

            if (NCOL(data) > 1) {
              base.plot <- eval(parse(text=paste("ggplot(data=data,",
                "aes(x=",colnames(data)[i],"));",sep="")))
            } else {
              base.plot <- eval(parse(text=paste("ggplot(data=data,",
                "aes(x=data));",sep="")))
            }

            STDev.zero.centered <- function(x) dnorm(
              x, mean = 0, sd = sd(current.data));
            STDev.mean.centered <- function(x) dnorm(
              x, mean = mean(current.data), sd = sd(current.data));

            myHistogram = base.plot +
              geom_histogram(breaks=partitions[[i]],colour=colB,fill=colF,
                  aes(y=..density..),na.rm=TRUE,right=FALSE) +
              stat_function(fun = STDev.zero.centered,
                colour = "blue", linetype = "dashed", size=1) +   
              stat_function(fun = STDev.mean.centered,
                colour = "red", size=1) +   
              scale_x_continuous("",limits=xlim1,extend=c(0,0)) +
              scale_y_continuous("Probability",limits=ylim1,extend=c(0,0)) +
              opts(title=htitle,
                  axis.title.x = theme_blank(),
                  axis.title.y = theme_text(size=10,angle=90,colour="grey25"),
                  axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                    vjust=.4),
                  axis.text.x = theme_text(size=8,colour="grey40",
                    vjust=.7),
                  plot.margin = unit(c(0,.35,0,0), "lines"),
                  plot.background = theme_rect(colour = "NA",fill=bkground),
                  plot.title = theme_text(size=10,colour="black",face="bold")
                  );

            if (addRug) {
              if (blend.rug) {myHistogram <- myHistogram + geom_rug(alpha=.25);}
              else {myHistogram <- myHistogram + geom_rug();}
            }

            if (row.iterator >= nrow) {
                row.iterator = 1;
                column.iterator = column.iterator + 1;
              }
            else {
                row.iterator = row.iterator + 1;
              }
             
            print(myHistogram,vp=vp.layout(row.iterator,column.iterator));                  
                 
      }
      
#      if (stamp!=NULL) {
#          mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
#                  " (", stamp, ")", sep=""),col=1,cex=.6,line=2, side=1,
#                  adj=1, outer=T)
#      }
          
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
      # Either an integer specifying a symbol or a single character to be
      # used as the default in plotting points.

      dev.off()
      # dev.off shuts down the specified (by default the current) device.
      cat("done\n");
    }) # tryCatch()
}
