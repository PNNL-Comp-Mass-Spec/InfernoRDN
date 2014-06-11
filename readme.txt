InfernoRDN can perform various downstream analyses on large scale datasets 
from proteomics and microarrays.

Among many features of InfernoRDN are:
* A set of diagnostic plots (Histograms, boxplots, correlation plots, 
  qq-plots, peptide-protein rollup plots, MA plots, PCA plots, etc).
* Log transforming.
* Rolling up to proteins (3 methods are available).
* LOESS normalization
* Linear Regression Normalization
* Mean Centering
* Median Absolute Deviation (MAD) Adjustment across datasets
* Quantile Normalization
* Principal Component Analysis
* Partial Least Squares Analysis
* ANOVA (multi-way, unbalanced, random effects)
* Heatmaps with Hierarchical and K-means cluster options

InfernoRDN uses R.NET (http://rdotnet.codeplex.com/) to communicate with R. 
InfernoRDN is an updated version of Inferno (which used StatconnDCOM)
InfernoRDN supersedes all previous DAnTE (Data Analysis Tool Extension), DanteR, and Inferno versions.


== Installation ==

1) Download and install the latest version of R 3.x from 
   http://cran.r-project.org/bin/windows/base/

2) Run the installer, InfernoRDNSetup.exe

3) Start InfernoRDN using the Start Menu or desktop shortcut

4) The InfernoRDN splash screen will appear and status messages will be shown
	- If a Dialog box appears asking "Would you like to use a personal library"
	  you should answer Yes to that question, and Yes to the next question regarding
	  the folder to use for the personal library
	- Following this, several Bioconductor packages will be downloaded

5) Test loading a data file	
	- Choose File, Open, Expression File
	- Navigate to "C:\Program Files (x86)\PNNL\InfernoRDN"
	  (or C:\Program Files\PNNL\InfernoRDN)
	- Select SampleInput4DAnTE.csv and click Open
	- Choose column Mass_Tag_ID then click the ">>" button to the left (and just below) "Unique Row ID"
	- Enable checkbox "Protein ID"
	- Select column MinOfOrf then click the ">>" button to the left (and just below) "Protein ID" 
	- Select data columns P10A through P19B then click the ">>" button to the left (and below) "Data Columns" 
	- Click OK

6) Test the plotting
	- Choose Plot, Correlation
	- Enable checkbox Toggle All, then click OK


== Dependencies ==

InfernoRDN depends on the following:
1) Windows 7 (or newer) with the .NET 4.0 framework 
	* http://www.microsoft.com/en-us/download/details.aspx?id=17718
2) R Statistical Environment, version 2.8 or newer
	* http://cran.r-project.org/bin/windows/base/

InfernoRDN uses the following R packages (from http://cran.r-project.org/):
* amap: Another Multidimensional Analysis Package
* car: Companion to Applied Regression
* nlme: Linear and Nonlinear Mixed Effects Models
* outliers: Tests for outliers
* fpc: Fixed point clusters, clusterwise regression and discriminant plots
* pls: Partial Least Squares Regression (PLSR) and Principal Component Regression (PCR)
* MASS: Main Package of Venables and Ripley's MASS
* impute: Imputation for microarray data
* qvalue: Q-value estimation for false discovery rate control
* e1071: Misc Functions of the Department of Statistics (e1071), TU Wien
* gplots: Various R programming tools for plotting data
* ellipse: Functions for drawing ellipses and ellipse-like confidence regions
* plotrix: Various plotting functions
* scatterplot3d: 3D Scatter Plot
* colorspace: Colorspace Manipulation

_______________________________________________________________________________
Developed by Ashoka Polpitiya for the US Department of Energy and TGen
Includes contributions from Gary Kiebel and Matthew Monroe at PNNL
PNNL, Richland, WA, USA.
TGen, Phoenix, AZ, USA.

Copyright 2007, 2014, Battelle Memorial Institute.  All Rights Reserved.
Copyright 2010, Translational Genomics Research Institute.  All Rights Reserved.

E-mail: ashoka@tgen.org or matthew.monroe@pnnl.gov
Website: http://omics.pnl.gov/software/InfernoRDN
_______________________________________________________________________________


== License Agreement ==

InfernoRDN is licensed under the Apache License, Version 2.0; you may not use this file except in compliance with the License.  
You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0

All publications that result from the use of this software should include the following acknowledgment statement: 

Portions of this research were supported by NIH, DOE, and Pacific Northwest National Laboratory (PNNL), 
in addition to the Center for Proteomics, Translational Genomics Research Institute (TGEN).

However, if the software is extended or modified, then any subsequent publications should include a more extensive statement, 
using this text or a similar variant: 
 
Portions of this research were supported by the National Institute of General Medical Sciences (NIGMS, Large Scale 
Collaborative Research Grants U54 GM-62119-02), the NIH National Center for Research Resources (RR18522), and the 
National Institute of Allergy and Infectious Diseases NIH/DHHS (through interagency agreement Y1-AI-4894-01). 
Work was performed at Pacific Northwest National Laboratory (PNNL) in the Environmental Molecular Sciences Laboratory, 
a national scientific user facility sponsored by the U.S. Department of Energy (DOE) Office of Biological and 
Environmental Research. PNNL is operated by Battelle for the DOE under contract DE-AC05-76RLO-1830.

Polpitiya AD, Qian WJ, Jaitly N, Petyuk VA, Adkins JN, Camp DG 2nd, Anderson GA, Smith RD., DAnTE: a statistical tool for 
quantitative analysis of -omics data. Bioinformatics. 2008 Jul 1;24(13):1556-8. (PMID: 18453552)

Notice: This computer software was prepared by Battelle Memorial Institute, hereinafter the Contractor, under Contract 
No. DE-AC05-76RL0 1830 with the Department of Energy (DOE).  All rights in the computer software are reserved by DOE on 
behalf of the United States Government and the Contractor as provided in the Contract.  NEITHER THE GOVERNMENT NOR THE 
CONTRACTOR MAKES ANY WARRANTY, EXPRESS OR IMPLIED, OR ASSUMES ANY LIABILITY FOR THE USE OF THIS SOFTWARE. 
This notice including this sentence must appear on any copies of this computer software.


== R.NET License ==

R.NET is Copyright (c) 2010, RecycleBin
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted 
provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions 
  and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
  and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
