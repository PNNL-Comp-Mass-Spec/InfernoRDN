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

plot_qq <- function(data,
                      reference = "Normal",
                      wshape = 2,
                      wscale = 1,
                      degfree = 4,
                      exprate=1,
                      ncols = 2,
                      file="deleteme.png",
                      bkground="white",
                      colF="#ffc38a",
                      colB="#5FAE27",
                      colL="#FF0000",
                      ...)
{
    # Add alpha input argument?

    #png(filename=file,width=1152,height=864,pointsize=12,
    #        bg=bkground,res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground)

    if (ncols <= 0) ncols <- 1 

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

    tryCatch(
    {
        for (i in 1:NCOL(data))
        {
          if (NCOL(data) == 1)
          {
            current.data <- data
            qtitle = "Normal Q-Q Plot"
          }
          else
          {
            current.data <- data[,i]
            qtitle = colnames(data)[i]
          }
          
          current.data = current.data[!is.na(current.data)];
            
          data = as.data.frame(data);
          
          if (NCOL(data) > 1) {
              base.plot <- eval(parse(text=paste("ggplot(data=data,",
                "aes(sample=",colnames(data)[i],"));",sep="")))
          } else {
              base.plot <- eval(parse(text=paste("ggplot(data=data,",
                "aes(sample=data));",sep="")))
          }

          # Q-Q reg calcs
          current.data <- current.data[!is.na(current.data)]
          n <- length(current.data)
          plot.points <- c(0.25, 0.75)

          data.quartiles <- quantile(current.data, plot.points, na.rm = TRUE)
          norm.quartiles <- switch(reference,
              Normal = qnorm(plot.points,...),
              Exponential = qexp(plot.points, rate=exprate,...),
              Student = qt(plot.points, df=degfree,...),
              Weibull = qweibull(plot.points,shape=wshape,scale=wscale,...)
          )
          b <- (data.quartiles[2] - data.quartiles[1]) /
                (norm.quartiles[2] - norm.quartiles[1]);
          a <- data.quartiles[1] - norm.quartiles[1] * b;
          # Q-Q reg calcs end
                    
          myQQplot <- base.plot + 
                      scale_y_continuous("Sample Quantiles") +
                      opts(
                        title=qtitle,
                        axis.title.y = theme_text(size=10,angle=90,colour="grey25"),
                        axis.title.x = theme_text(size=10,colour="grey25"),
                        axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                          vjust=.4),
                        axis.text.x = theme_text(size=8,colour="grey40",
                            vjust=.7),
                        plot.margin = unit(c(.1,.35,.1,0), "lines"),
                        plot.title = theme_text(size=10,colour="black",face="bold")
                      );
                             
          myQQplot <- myQQplot + switch(reference,
            Normal = stat_qq(aes(dt=qnorm),dparams = list(scale=wscale, df=degfree,
              rate=exprate,shape=wshape),na.rm=TRUE,alpha=.2),
            Exponential = stat_qq(aes(dt=qexp),dparams = list(scale=wscale, df=degfree,
              rate=exprate,shape=wshape),na.rm=TRUE,alpha=.2),
            Student = stat_qq(aes(dt=qt),dparams = list(scale=wscale, df=degfree,
              rate=exprate,shape=wshape),na.rm=TRUE,alpha=.2),
            Weibull = stat_qq(aes(dt=qweibull),dparams = list(scale=wscale, df=degfree,
              rate=exprate,shape=wshape),na.rm=TRUE,alpha=.2)
            ) + switch(reference,
             Normal = scale_x_continuous("Theoretical Quantiles - Normal"),
             Exponential = scale_x_continuous("Theoretical Quantiles - Exponential"),
             Student = scale_x_continuous("Theoretical Quantiles - Student"),
             Weibull = scale_x_continuous("Theoretical Quantiles - Weibull")
            ) +  stat_abline(intercept = a, slope = b, colour="red", size=1);

          if (row.iterator >= nrow) {
                row.iterator = 1;
                column.iterator = column.iterator + 1;
              }
            else {
                row.iterator = row.iterator + 1;
              }
             
            print(myQQplot,vp=vp.layout(row.iterator,column.iterator));
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

#-------------------------------------------------------------------
## Generic qqplots for any distribution
## (Normal, Exponential, Student and Weibull)
#qqplot.1 <- function (data, position = 0.5, reference = "Normal",
#                shape=2, scale=1, def = 4, exprate = 1,
#                main='QQ plot', colb=colB, bgrnd=colF, psize=21, ...)
#{
#    data <- data[!is.na(data)]
#    n <- length(data)
#    plot.points <- ppoints(n, position)
#    xpoints <- switch(reference,
#      Normal = qnorm(plot.points, ...),
#      Exponential = qexp(plot.points, rate=exprate, ...),
#      Student = qt(plot.points, df=def, ...),
#      Weibull = qweibull(plot.points, shape, scale, ...)
#    )
#    plot(xpoints, sort(data), col = colb, bg = bgrnd, pch = 21,
#        xlab = paste("Theoretical Quantiles - ", reference),
#        ylab = "Sample Quantiles", main = main)
#}

#-------------------------------------------------------------------
## qqline.1 is based on qqline with additional, optional arguments.
#qqline.1 <- function(x, reference = "Normal", exprate = 1,
#              shape=2, scale=1, def = 4, colL='red', ...) {
#  x <- x[!is.na(x)]
#  plot.points <- c(0.25, 0.75)
#  data.quartiles <- quantile(x, plot.points, na.rm = T)
#  norm.quartiles <- switch(reference,
#      Normal = qnorm(plot.points, ...),
#      Exponential = qexp(plot.points, rate=exprate, ...),
#      Student = qt(plot.points, df=def, ...),
#      Weibull = qweibull(plot.points, shape, scale, ...)
#  )
#  b <- (data.quartiles[2] - data.quartiles[1]) /
#    (norm.quartiles[2] - norm.quartiles[1])
#  a <- data.quartiles[1] - norm.quartiles[1] * b
#  abline(a, b, col=colL, ...)
#  ans <- as.vector(c(a, b))
#  names(ans) <- c("intercept", "slope")
#  invisible(ans)
#}
