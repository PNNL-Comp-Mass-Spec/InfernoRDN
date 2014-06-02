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

plotProts.raw <- function(pdata=NULL,
                          Data=Eset,
                          file="deleteme.png",
                          bkground="white",
                          IPI="IPI:IPI00013508.3",
                          datalabels=TRUE,
                          has.legend=TRUE)
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=IMGwidth,height=IMGheight,pointsize=FNTsize,bg=bkground,res=600)

    require(ggplot2)
    #Data <- Eset
    tryCatch(
    {
        if (datalabels)
          xlabels <- colnames(Data)
        else
          xlabels <- 1:dim(Data)[2]
        protIPI <- ProtInfo[,2]
        MassTags <- ProtInfo[,1]
        pidx <- which(protIPI==IPI)
        data_idx <- is.element(row.names(Data),MassTags[pidx])
        currProtData <- Data[data_idx,]
        #x <- 1:dim(Data)[2]
        
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
        
        if (length(pidx) == 1) { peps <- "Peptide"; } 
        else { peps <- "Peptides"; }
        
        total.plot <- ggplot( data = total.df,
                aes( x = sample, y = Prot, colour = factor(peptide) ) ) +
            geom_line() +
            geom_point() +
            scale_x_continuous(breaks=1:NCOL(currProtData),labels=xlabels) +
            scale_y_continuous("Raw Data") +
            scale_colour_hue(breaks=1:NROW(currProtData),labels=peptide.names,
                name="Peptide",l=59,c=150) +
            opts(
                title = paste(IPI,"\n",dim(currProtData)[1],peps,sep=" "),
                axis.text.x=theme_text(angle=90, hjust=1,col="grey30"),
                axis.title.x=theme_blank(),
                panel.background = theme_rect(colour=NA,fill="grey90")
            );
        
        if (!has.legend) {
          total.plot <- total.plot + opts(legend.position="none");
        }
            
        total.plot
        #par(mfrow=c(2,1))
#        if (length(pidx) == 1)
#        {
#            par(mar=c(10,4,4,2))
#            plot(currProtData,type="o",pch=19,main=IPI,ylab="Raw Data",
#                col="blue", xlab="", xaxt="n")
#            axis(1,at=1:length(xlabels),labels=xlabels, las = 2)
#            mtext("1 Peptide", 3)
#            foo <- currProtData ~ x
#            lines(foo, model.frame(foo), lty=3, col='blue', cex.axis=0.7)
#            grid()
#        }
#        else
#        {
#            par(mar=c(10,4,4,2))
#            matplot(t(currProtData),type="o",main=IPI,ylab="Raw Data",
#                xlab="", xaxt="n")
#            axis(1,at=1:length(xlabels),labels=xlabels, las = 2, cex.axis=0.7)
#            grid()
#            mtext(paste(dim(currProtData)[1],"Peptides",sep=" "), 3)
#        }
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
