== Updating Functions in .RData Files ==

Use the following steps to update the details of one or more functions 
in the .RData files in the Rscripts folder.  Note that 
Inferno_ggplots.RData is historical and is no longer used.

1) Start RStudio
* If previous code was loaded, use "Close All" on the File menu

2) On the Session menu, choose "Load Workspace"
* Open Inferno_stdplots.RData or Inferno.RData

3) The Environment pane lists the available functions, for example plot_hist or QRollup.proteins

4) To view the function body for a function go to the console tab, then use View()
* View(plot_hist)
* View(QRollup.proteins)

5) Optionally open an existing .R file using "Open file" from the "File" menu

6) After editing the function, you can update the copy stored in the 
.RData file by adding the function name and <- like this:

plot_hist <-
  function(data,
           ncols = 2)
  {
     etc.
  }


Next, click "Source" (for Source the Active Document) and a new 
command will run in the console to update the .RData file.

source('~/Projects/_CommunityApplications/InfernoRDN/Rscripts/Rollup/QRollUp.R')


7) If you add or remove parameters from the function header, you can 
verify that the changes were stored when you "sourced" the script 
by hovering your mouse over the function definition column in the 
Environment tab and examining the function parameters listed.

Changes may also be verified by typing the function name in the console tab (without parentheses):

plot_hist


8) When done updating functions, choose Session, Save Workspace As ...
and overwrite the original .RData file that you opened.  This runs command

save.image("~/Projects/_CommunityApplications/InfernoRDN/Rscripts/Inferno.RData")


== Loading All InfernoRDN Scripts Into RStudio ==

Use the following steps to load every InfernoRDN script into RStudio
* Start RStudio
* Under the "Session" menu choose "Load workspace".
** Select a .dnt file saved from InfernoRDN
* Use "Load Workspace" two more times
** Load Inferno.RData 
** Load Inferno_stdplots.RData

