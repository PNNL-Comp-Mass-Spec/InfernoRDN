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

dataBoxPlots <- function(x,
                         file="deleteme.png",
                         Factor=NULL,
                         outliers=TRUE,
                         color="black",
                         bkground="grey60",
                         labelscale=0.8,
                         boxwidth=1,
                         showcount=TRUE,
                         stamp=NULL,
                         ...)
{
  #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
  #          res=600)
  require(Cairo)
  CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

  require(ggplot2);

  tryCatch(
  {
      
      if (length(Factor) == dim(x)[2]) {
      
          data = c(numeric(0), numeric(0), numeric(0));
          #data = c(numeric(0), numeric(0), character(0));  
       
          uF = unique(Factor);
          len.uF = length(uF);
          num.F = vector("numeric",length(Factor));
          
          for (i in 1:len.uF) {
          
             index <- which(uF[i] == Factor);
             num.F[index] = i; 
          
          }
       
          for (i in 1:NCOL(x)) {
          
            data <- rbind(data,cbind(x[,i],i,num.F[i]));
          
          }
          
          myColnames = c("myData","Sample","myFactor");  
      
      } else {  # if Factor == NULL or there is an error

          data <- c(numeric(0), numeric(0));
          
          for (i in 1:NCOL(x)) {
          
            data <- rbind(data,cbind(x[,i],i));

          }
          
          myColnames = c("myData","Sample");
            
     } 
       
       data <- as.data.frame(data);
       colnames(data) <- myColnames;

       myBoxPlot <- ggplot(data = data, aes(x = factor(Sample), y = myData),
          na.rm=TRUE);
       
       grid.newpage()
       pushViewport(viewport(layout=grid.layout(1,1) ) )
       
       if (length(Factor) == dim(x)[2]) {
         
         total.plot <- myBoxPlot +
            geom_boxplot(aes(fill=factor(myFactor)),outlier.colour="NA") +
            scale_fill_hue(breaks=1:length(uF),labels=uF,name="Factor");
          
       } else {
         
         total.plot <- myBoxPlot +
            geom_boxplot(fill=bkground,colour=color,outlier.colour="NA");
       
       }

       total.plot <- total.plot + 
          scale_x_discrete("",breaks=seq(1:NCOL(x)),labels=colnames(x)) +
          scale_y_continuous("") +
          opts(axis.text.x=theme_text(angle=-90, hjust=0,col="grey30"));
       
       cur.stats <- boxplot.stats(x[,i]);
           box.height <- cur.stats$stats[4];
       
       adj.data <- data;
       lower.limit <- 1;
       upper.limit <- dim(x)[1];
       accessor <- vector(length=dim(data)[1]);
       
       for (i in 1:NCOL(x)) {
           
           cur.stats <- boxplot.stats(x[,i]);
           box.height <- cur.stats$stats[4];
           
           if (showcount) { 
           total.plot <- total.plot + annotate(geom = "text",
              x = i, y = box.height, vjust = -.35, size = 3, 
              label = as.character(length(x[!is.na(x[,i]),i])) );
           }
              
          accessor[lower.limit:upper.limit] <- 
              (data$myData[lower.limit:upper.limit] > cur.stats$stats[5] | 
              data$myData[lower.limit:upper.limit] < cur.stats$stats[1]) &
              !is.na(data$myData[lower.limit:upper.limit]);
          
          lower.limit <- upper.limit + 1;
          upper.limit <- upper.limit + dim(x)[1];
       }
       
       adj.data <- adj.data[accessor,];
       
       total.plot <- total.plot +
                     geom_point(data=adj.data,
                        aes(x = factor(Sample), y = myData), na.rm=TRUE,
                        alpha=.5) +
                     opts(plot.margin = unit(c(.75,.75,-.25,-.25), "lines"));
       
       #grob.plot <- ggplotGrob(total.plot);
       
       print(total.plot,vp=vp.layout(1,1));
       
       #grid.gedit(gPath("axis_h", "axis.text.x.text"), grep=TRUE, rot=90,
       #   hjust=1,vjust=.5);
          
       #ggaxis(at=seq(1:NCOL(x)), labels=c(1:NCOL(x)), position="top");
            
#      if (showcount)
#      {
#        axis(side=3, at=1:dim(x)[2], labels=colSums(!is.na(x)), tick=FALSE,
#            cex.axis=.7, las=2)
#      }
#      if (!is.null(stamp))
#        mtext(paste("DAnTE : ", format(Sys.time(), "%m-%d-%Y %I:%M%p"),
#                  " (", stamp, ")", sep=""),col=1,cex=.6,line=2, side=1,
#                  adj=1, outer=T)
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
