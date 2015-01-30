Use the following steps to update the details of one or more functions 
in the .RData files in the Rscripts folder.  Note that 
Inferno_ggplots.RData is historical and is no longer used.

1) Start RStudio

2) Choose Session, Load Workspace, and open Inferno_stdplots.RData or 
   Inferno.RData, depending on your needs

3) The Environment pane lists the available functions, for example plot_hist

4) To view the function body for function plot_hist, go to the console tab, 
   type "plot_hist", then press Enter

5) Now copy the function body and paste in the R Script tab 
   (optionally use File, New File, R Script)
   Example code to copy/paste
       function(data,
                ncols = 2,
          etc.
       }

6) After editing the function, you can update the copy stored in the 
   .RData file by adding the function name and <- like this:
       plot_hist <-
       function(data,
                ncols = 2,
          etc.
       }

   Next, click "Source" (for Source the Active Document) and a new 
   command will run in the console to update the .RData file.

7) If you add or remove parameters from the function header, you can 
   verify that the changes were stored when you "sourced" the script 
   by hovering your mouse over the function definition column in the 
   Environment tab and examining the function parameters listed.

   Changes may also be verified by typing the function name in the console tab
   again:
      plot_hist
   
8) When done updating functions, choose Session, Save Workspace As ...
   and overwrite the original .RData file that you opened
