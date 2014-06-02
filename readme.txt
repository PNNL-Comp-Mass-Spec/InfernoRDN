DAnTE - Data Analysis Tool Extension

This package contains latest release of DAnTE.
DAnTE can perform various downstream analyses on large scale datasets from proteomics and microarrays.

Among many features of DAnTE are:
* A set of diagnostic plots (Histograms, boxplots, correlation plots, qq-plots, peptide-protein rollup plots, MA plots, PCA plots, etc).
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

Dependencies:

DAnTE depends on the following:
1. Windows XP with .NET 2.0 framework (http://www.microsoft.com/downloads/)
2. R Statistical Environment (http://www.r-project.org/)
3. statconnDCOM server (http://rcom.univie.ac.at/)

DAnTE uses the following R packages (from http://cran.r-project.org/):
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

DAnTE also uses PeptideFileExtractorConsole application written by Matthew Monroe and
PeptideFileExtractor and FileConcatenator DLLs written by Ken Auberry, for Sequest output file processing.

_______________________________________________________________________________
Developed by Ashoka Polpitiya for the US Department of Energy
PNNL, Richland, WA, USA.
Copyright 2007, Battelle Memorial Institute.  All Rights Reserved.

E-mail: ashoka.polpitiya@pnl.gov
Website: http://omics.pnl.gov/software/DAnTE.php

--------------------------------------------------------
License Agreement

DAnTE is licensed under the Apache License, Version 2.0; you may not use this file except in compliance with the License.  
You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0

All publications that utilize this software should provide the citation below and appropriate acknowledgement to PNNL and the OMICS.PNL.GOV website. 
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

