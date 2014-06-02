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
# Plot QRollup data
plotScaleData.QRup <- function(pdata,
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

    #Data <- Eset
    tryCatch(
    {
        if (datalabels)
          xlabels <- colnames(Data)
        else
          xlabels <- 1:dim(Data)[2]
        protIPI <- ProtInfo[,2]
        MassTags <- ProtInfo[,1]
        #Prots <- pdata[,-c(1,2)]
        pidx <- which(protIPI==IPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]

        Pdata <- pdata[rownames(pdata)==IPI,]
        Score <- format(Pdata[2], digits=4)
        Pdata <- Pdata[-c(1,2)]

        peptide.names <- rownames(currProtData);

        total.df <- matrix(nrow=(NROW(currProtData)*NCOL(currProtData)),ncol=3);
        
        low.it <- 1;
        upp.it <- NCOL(currProtData);
             
        for (i in 1:NROW(currProtData)) {

          # Column 1: Peptide
          # Column 2: Sample  
          # Column 3: currProtData
          total.df[low.it:upp.it,] <- cbind(
            i, 
            1:NCOL(currProtData),
            currProtData[i,]
            );  
          
          low.it <- upp.it + 1;
          upp.it <- upp.it + NCOL(currProtData);  
        }
        
        total.df <- as.data.frame(total.df);
        colnames(total.df) <- c("peptide","sample","Prot")

        grid.newpage()
        pushViewport(viewport(layout=
          grid.layout(nrow=1,ncol=2,widths = unit(c(2,0.275),"null"))
        ));
        
        if (dim(currProtData)[1] == 1) {peps <- "Peptide";}
        else {peps <- "Peptides";}
          
          base.plot <- 
              ggplot(data=total.df,
                aes(x=sample,y=S,colour=factor(peptide)),na.rm=TRUE) +
              scale_colour_hue(breaks=1:NROW(currProtData),labels=peptide.names,
                name="Peptide",l=59,c=150);
                  
          main.graph <- base.plot +   
              geom_point(aes(y=Prot)) +
              geom_line(aes(y=Prot)) +
              scale_x_continuous("",breaks=1:NCOL(currProtData),labels=xlabels) +
              scale_y_continuous("Raw Data") +
              opts(
                title=paste(IPI, "(QPRo=", Score, ")\n",
                    dim(currProtData)[1],peps, sep=" "),
                legend.position = "none",
                plot.margin = unit(c(.2,.2,-.1,.4), "lines"),
                axis.text.x=theme_text(angle=90, hjust=1,col="grey30")
              );
              
          legend <- base.plot + 
              geom_point(aes(y=Prot)) +
              geom_line(aes(y=Prot)) +
              opts(
                legend.text=theme_text(size=10),
                legend.key.size = unit(1.2, "lines"),
                keep = "legend_box"
              );    

          print(main.graph,vp=vp.layout(1,1));
          print(legend,vp=vp.layout(1,2));
        
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
