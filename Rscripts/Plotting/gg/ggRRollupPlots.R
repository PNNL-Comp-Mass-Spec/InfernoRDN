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

# Plot RRollup data
plotRefRUpData <- function(pdata,
                           Data=Eset,
                           file="deleteme.png",
                           bkground="white",
                           IPI="IPI:IPI00013508.3",
                           datalabels=TRUE)
# pdata : output from protein rollup (scale.proteins)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)
    
    tryCatch(
    {
        #Data <- Eset
        if (datalabels)
          xlabels <- colnames(Data)
        else
          xlabels <- 1:dim(Data)[2]
        protIPI <- ProtInfo[,2]
        MassTags <- ProtInfo[,1]
        Prots <- pdata$pData
        pidx <- which(protIPI==IPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]
        data_idx <- is.element(row.names(pdata$sData),MassTags[pidx])
        currSData <- pdata$sData[data_idx,]
        data_idx <- is.element(row.names(pdata$orData),MassTags[pidx])
        currORData <- pdata$orData[data_idx,]
        Pdata <- Prots[rownames(Prots)==IPI,]
        Score <- format(Pdata[2], digits=4)
        Pdata <- Pdata[-c(1,2)] 

        peptide.names <- rownames(currProtData);

        total.df <- matrix(nrow=(NROW(currProtData)*NCOL(currProtData)),ncol=5);
        
        low.it <- 1;
        upp.it <- NCOL(currProtData);
             
        for (i in 1:NROW(currProtData)) {

          # Column 1: Peptide
          # Column 2: Sample  
          # Column 3: currProtData
          # Column 4: currSData
          # Column 5: currORData 
          total.df[low.it:upp.it,] <- cbind(
            i, 
            1:NCOL(currProtData),
            currProtData[i,],
            currSData[i,],
            currORData[i,]
            );  
          
          low.it <- upp.it + 1;
          upp.it <- upp.it + NCOL(currProtData);  
        }
        
        total.df <- as.data.frame(total.df);
        colnames(total.df) <- c("peptide","sample","Prot","S","OR")

        if (length(pidx) == 1)
        {
            grid.newpage()
            pushViewport(viewport(layout=
                grid.layout(nrow=1,ncol=2,widths = unit(c(2,0.275),"null"))
            ));
            
            base.plot <- 
              ggplot(data=total.df,
                aes(x=sample,y=S,colour=factor(peptide)),na.rm=TRUE) +
              scale_colour_hue(breaks=1:NROW(currProtData),labels=peptide.names,
                name="Peptide",l=59,c=150);
                  
            main.graphs <- base.plot +   
              geom_point(aes(y=Prot)) +
              geom_line(aes(y=Prot)) +
              scale_x_continuous("",breaks=1:NCOL(currProtData),labels=xlabels) +
              scale_y_continuous("Raw Data") +
              opts(
                title=paste(IPI, "(QPRo=", Score, ")\n",
                    dim(currProtData)[1],"Peptides", sep=" "),
                legend.position = "none",
                plot.margin = unit(c(.2,.2,-.1,-.1), "lines"),
                axis.text.x=theme_text(angle=90, hjust=1,col="grey30")
              );
              
              legend <- base.plot + 
                geom_point(aes(y=Prot)) +
                opts(keep = "legend_box");    
          
              print(main.graphs,vp=vp.layout(1,1));
          
              print(legend,vp=vp.layout(1,2));
            
        }
        else
        {

        grid.newpage()
        pushViewport(viewport(layout=
          grid.layout(nrow=3,ncol=2,widths = unit(c(2,0.275),"null"), 
              heights = unit(c(3.5,2.75,4.25),"null"))
            ));    
        
          main.graphs <- vector("list",3);
          
          base.plot <- 
              ggplot(data=total.df,
                aes(x=sample,y=S,colour=factor(peptide)),na.rm=TRUE) +
              scale_colour_hue(breaks=1:NROW(currProtData),labels=peptide.names,
                name="Peptide",l=59,c=150);
                  
          main.graphs[[1]] <- base.plot +   
              geom_point(aes(y=Prot)) +
              geom_line(aes(y=Prot)) +
              scale_y_continuous("Raw Data") +
              opts(
                title=paste(IPI, "(QPRo=", Score, ")\n",
                    dim(currProtData)[1],"Peptides", sep=" "),
                axis.title.x = theme_blank(),
                legend.position = "none",
                plot.margin = unit(c(.2,.2,-.1,-.1), "lines"),
                axis.title.y = theme_text(size=10,angle=90,colour="grey25"),
                axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                    vjust=.4),
                axis.text.x = theme_text(size=8,colour="grey40",
                  vjust=.7)
                #plot.background = theme_rect(colour = "black")
              );
          main.graphs[[2]] <- base.plot +   
              geom_point(aes(y=S)) +
              geom_line(aes(y=S)) +
              scale_y_continuous("Scaled Data") +
              opts(
                axis.title.x = theme_blank(),
                plot.margin = unit(c(.2,.2,-.1,-.1), "lines"),
                legend.position = "none",
                axis.title.y = theme_text(size=10,angle=90,colour="grey25"),
                axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                    vjust=.4),
                axis.text.x = theme_text(size=8,colour="grey40",
                  vjust=.7)
                #plot.background = theme_rect(colour = "black")
              );
          main.graphs[[3]] <- base.plot + 
              geom_point(aes(y=OR)) +
              geom_line(aes(y=OR)) +
              scale_x_continuous("",breaks=1:NCOL(currProtData),labels=xlabels) +
              scale_y_continuous("Scaled and Outlier Removed") +
              opts(
                axis.text.x=theme_text(angle=90,hjust=1,col="grey30"),
                axis.title.x = theme_blank(),
                legend.position = "none",
                plot.margin = unit(c(.2,.2,-.1,.1), "lines"),
                axis.title.y = theme_text(size=10,angle=90,colour="grey25",
                    vjust=.7),
                axis.text.y = theme_text(size=8,colour="grey40",hjust=1,
                    vjust=.4),
                axis.text.x = theme_text(size=8,colour="grey40",
                    vjust=.7)#,
                #plot.background = theme_rect(colour = "black")
              );
              
          legend <- base.plot + 
              geom_point(aes(y=Prot)) +
              geom_line(aes(y=Prot)) +
              opts(
                legend.text=theme_text(size=10),
                legend.key.size = unit(1.2, "lines"),
                keep = "legend_box"
              );    
          
          for (i in 1:length(main.graphs)) {
            print(main.graphs[[i]],vp=vp.layout(i,1));
          }
          
          print(legend,vp=vp.layout(1:3,2));
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
      dev.off()
      cat("done\n");
    }) # tryCatch()
}
