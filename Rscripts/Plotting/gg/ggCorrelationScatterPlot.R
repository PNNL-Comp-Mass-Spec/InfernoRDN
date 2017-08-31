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

plotScatterCorr <- function(x,
                            file = "deleteme.png",
                            bkground = "white",
                            dHIST = TRUE,
                            regL = TRUE,
                            showOverlap = TRUE,
                            showloess = TRUE,
                            stamp = NULL,
                            ...)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)
    par(oma=c(3.4, 2, 2, 2), mar=c(5,4,4,1))

  tryCatch(
  {
      grid.newpage()
      pushViewport(viewport(layout=grid.layout(NCOL(x),NCOL(x)) ) );
      
      if (dHIST) {
        # Generates a list of ggplot objects containing all histograms
        hist.graphs <- gg_plot_hist_called(x,cells=40);
        
        for (i in 1:length(hist.graphs)) {
             print(hist.graphs[[i]],vp=vp.layout(i,i));
        }
      } else {
        
        title.graphs <- title.plot.generator(x);
        
        for (i in 1:length(title.graphs)) {
             print(title.graphs[[i]],vp=vp.layout(i,i));
        }
        
      }
      
      for (i in 1:NCOL(x)) {
          j = i+1;
          
          while (j <= NCOL(x)) {
            
            print(
                panel.corr(x[,i],x[,j],showloess, regL),
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
#-----------------------------------------------------------------

panel.corr <- function(x, y, showloess, regL, overlap, digits=2,prefix="") {

  miss <- is.na(x+y);
  
  x1 <- x[miss];
  y1 <- y[miss];
  
  x1[is.na(x1)]<-min(x,na.rm=TRUE);
  y1[is.na(y1)]<-min(y,na.rm=TRUE);
  
  main.df <- data.frame(cbind(x,y));
  colnames(main.df) <- c("X","Y");
  
  small.df <- data.frame(cbind(x1,y1));
  colnames(small.df) <- c("X1","Y1");
  
  ### Title generation
  r <- (cor(x, y,use="pairwise.complete.obs")); # Calculate correlation pairwise ignoring missing
  txt <- format(c(r, 0.123456789), digits=digits)[1];
  txt <- paste(prefix, txt, sep="");
  
  if(missing(overlap)) overlap <- TRUE
    if(overlap)
    {
        numpairs <- sum(!is.na(x+y));
        txt2 <- paste(" ", prefix, "(", numpairs, ")", sep="");
        
    } else {txt2 <- "";}
    
    my.title <- paste(txt, txt2);
  ### Title generation ENDS
  
  main.plot <- ggplot(data=main.df,aes(x=X,y=Y),na.rm=TRUE) + 
                geom_point(data=main.df,aes(alpha=.15)) +
                geom_point(data=small.df,aes(x=X1,y=Y1,alpha=.15)) + 
                scale_x_continuous("") +
                scale_y_continuous("") +
                opts(legend.position="none",title=my.title,
                  plot.margin = unit(c(.25,.25,-.25,-.25), "lines"),
                  plot.background = theme_rect(colour = "black"),
                  axis.title.x = theme_blank(),
                  axis.title.y = theme_blank(),
                  axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                    vjust=.4),
                  axis.text.x = theme_text(size=8,colour="grey40",
                    vjust=.7),
                  plot.title = theme_text(size=10,colour="black",face="bold")
                  );
                
  if (regL) {
    main.plot <- main.plot +
                geom_abline(intercept = 0, slope = 1, col="blue", size=1); 
  }
  
  if (showloess) {
    ok <- is.finite(main.df$X) & is.finite(main.df$Y);
    if (any(ok)) {
      adj.main.df <- main.df[ok,];
      
      main.plot <- main.plot +
                stat_smooth(data=adj.main.df, method="loess",size=1,
                  colour="red",fill="red");
    }
  }

  main.plot

}

gg_plot_hist_called <- function(data,
                      cells="sturges",
                      file="deleteme.png",
                      bkground="white",
                      colF="grey60",
                      colB="black",
                      addRug=FALSE,
                      stamp=NULL,
                      ...)
{
    require(ggplot2);

    plot.list = vector("list",NCOL(data));
    xlim1 = c(min(data,na.rm=TRUE)-1,max(data,na.rm=TRUE)+1);
    
    partitions = vector("list",NCOL(data));
    all.graphs = vector("list",NCOL(data));
   
    max.length = 0; # Will store maximum number of non-NA pts. in data

    ylim1 = c(0,0); ymax = 0;
    
    for (i in 1:NCOL(data)) {
   
            if (NCOL(data) == 1) {current.data = data;}
            else {current.data = data[,i];}
            
            current.data = current.data[!is.na(current.data)];
            
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
            
            temp.ymax <- max(h$counts)

            if (temp.ymax > ymax) { ymax = temp.ymax; }
    }
    
    ylim1[2] = ymax;
          
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
#
#            STDev.zero.centered <- function(x) dnorm(
#              x, mean = 0, sd = sd(current.data));
#            STDev.mean.centered <- function(x) dnorm(
#              x, mean = mean(current.data), sd = sd(current.data));

            myHistogram = base.plot +
              geom_histogram(breaks=partitions[[i]],colour=colB,fill=colF,
                  na.rm=TRUE,right=FALSE) +
#              stat_function(fun = STDev.zero.centered,
#                colour = "blue", linetype = "dashed", size=1) +   
#              stat_function(fun = STDev.mean.centered,
#                colour = "red", size=1) +   
              scale_x_continuous("",limits=xlim1,expand=c(0,0)) +
              scale_y_continuous("",limits=ylim1,expand=c(0,0)) +
              opts(title=htitle,
                  plot.margin = unit(c(.25,.25,-.25,-.25), "lines"),
                  plot.background = theme_rect(colour = "black"),
                  axis.title.x = theme_blank(),
                  axis.title.y = theme_blank(),
                  axis.text.y = theme_text(size=8,colour="grey40",hjust=.82,
                    vjust=.4),
                  axis.text.x = theme_text(size=8,colour="grey40",
                    vjust=.7),
                  plot.title = theme_text(size=10,colour="black",face="bold")
                  );

            if (addRug) {myHistogram <- myHistogram + geom_rug();}
            
            all.graphs[[i]] <- myHistogram;                
                 
      }
      
#      if (stamp!=NULL) {
#          mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
#                  " (", stamp, ")", sep=""),col=1,cex=.6,line=2, side=1,
#                  adj=1, outer=T)
#      }

        all.graphs
          
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
            plot.background = theme_rect(colour = "black"),
            panel.grid.major = theme_blank(),
            panel.grid.minor = theme_blank(),
            axis.line = theme_blank(),
            axis.text.x = theme_blank(),
            axis.text.y = theme_blank(),
            axis.title.x = theme_blank(),
            axis.title.y = theme_blank(),
            axis.ticks = theme_blank(),
            strip.background = theme_blank(),
            panel.background = theme_blank(),
            plot.margin = unit(c(.25,.25,-1,-1), "lines")
          );
      }
    
    all.graphs

}
