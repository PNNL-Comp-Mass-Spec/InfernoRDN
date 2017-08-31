# Written by Ashoka D. Polpitiya
# for the Translational Genomics Research Institute (TGen, Phoenix, AZ)
# Copyright 2010, Translational Genomics Research Institute
# E-mail: ashoka@tgen.org
#         proteomics@pnnl.gov
# Website: https://github.com/PNNL-Comp-Mass-Spec/InfernoRDN
# -------------------------------------------------------------------------
#
# Licensed under the Apache License, Version 2.0; you may not use this file except
# in compliance with the License.  You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
#
# -------------------------------------------------------------------------

# Written by Tom Taverner
# for the Department of Energy (PNNL, Richland, WA)
# Copyright 2009, Battelle Memorial Institute
# E-mail: thomas.taverner@pnl.gov
# Website: http://omics.pnl.gov/software
#
# Reference: Karpievich, 2009, DOI 10.1093/bioinformatics/btp362
# https://www.ncbi.nlm.nih.gov/pubmed/?term=19535538
# -------------------------------------------------------------------------
#

DoTamuQ <- function(Data, FixedEffects, Factors=factors)
{

# estimate data parameters
grp.x <- apply(Data, 1, mean, na.rm=T)
grp.y <- rowSums(is.na(Data))/ncol(Data)

grp.x <<- grp.x
grp.y <<- grp.y

my.df <- na.omit(data.frame(grp.x, grp.y))

# get parameter for pi
ss.beta <- getInitial(grp.y ~ SSfpl(grp.x, 1, 0,  median(grp.x, na.rm=T), 1), data = my.df)
ss.beta <<- ss.beta
my.pi <<- ss.beta[2]

 # do single imputation
 # return group effects and p-values


# our treatment factors
allFactors <- c(FixedEffects)
treatment <- factors[allFactors,]
treatment <<- treatment
n.u.treatment <- length(unique(treatment))

# match to protein
all.proteins <- unique(ProtInfo[,2])
all.proteins <- all.proteins[order(all.proteins)]

impute.coef <- array(NA, c(length(all.proteins), n.u.treatment-1))
impute.pval <- array(NA, c(length(all.proteins), n.u.treatment-1))
rownames(impute.pval) <- all.proteins
colnames(impute.pval) <- unique(treatment)[-1]
Y_imputed <- NULL
for (i in 1:length(all.proteins)){
#for (i in 8){
  prot <- all.proteins[i]
  pmid.matches <- ProtInfo[ProtInfo[,2]==prot,1]
  idx.prot <- which(rownames(Data) %in% pmid.matches)
  Y_raw <- rbind(Data[idx.prot,])
  rownames(Y_raw) <- rownames(Data)[idx.prot]

  if (nrow(Y_raw) == 0) next
#  try({
#print("I")
#print(prot)
#print(Y_raw)
    Y_raw <- filter.y(Y_raw)
#print(Y_raw)
#print("nrow")
#print(nrow(Y_raw))
    if (nrow(Y_raw) == 0) next
    my.coefs <- get_coefs(Y_raw)
Y_imputed <- rbind(Y_imputed, my.coefs$y.impute)
#print("HERE")
#print(i)
#print(my.coefs)
    impute.coef[i,] <- my.coefs$coef
    impute.pval[i,] <- my.coefs$p.val

#  }, silent=F)

}
colnames(Y_imputed) <- colnames(Data)
library(qvalue)
impute.qval <- array(NA, dim(impute.pval))
colnames(impute.qval) <- paste("(q)", colnames(impute.pval), sep="")
rownames(impute.qval) <- rownames(impute.pval)
for(j in 1:ncol(impute.pval)){
ii <- !is.na(impute.pval[,j])
impute.qval[ii,j] <- qvalue(as.vector(na.omit(impute.pval[,j])))$qvalue
}


return.arr <- array(NA, c(length(all.proteins), 3*ncol(impute.coef)))
colnames(return.arr) <- as.vector(outer(c("", "(p)", "(q)"), colnames(impute.pval), F=paste, sep=""))
rownames(return.arr) <- rownames(impute.pval)

for(i in 1:ncol(impute.coef)){
return.arr[,3*(i-1)+1] <- impute.coef[,i]
return.arr[,3*(i-1)+2] <- impute.pval[,i]
return.arr[,3*(i-1)+3] <- impute.qval[,i]
}
return.arr <- return.arr[rowSums(!is.na(return.arr)) > 0,]
return(list(pvals = return.arr, y.impute=Y_imputed))
}


filter.y <- function(Y_raw){
  threshold <- 1
  Y_raw <- rbind(Y_raw)
#print("FILTER")
#print(Y_raw)
  n.peptide <- nrow(Y_raw)
  y <- as.vector(t(Y_raw))
  y.cen <- y
#print(y.cen)
  n.treatment <- length(treatment)
  n.u.treatment <- length(unique(treatment))
  peptide <-rep(rep(1:n.peptide, each=n.treatment))
  # missing. peptide array
  m.missing <- array(NA, c(n.peptide, n.u.treatment))
colnames(m.missing) <- unique(treatment)
  for(i in 1:n.peptide) for(j in 1:n.u.treatment) m.missing[i,j] <- sum(!is.na(y.cen[peptide==i & treatment==unique(treatment)[j]]))

  missing.min <- apply(m.missing, 1, min)
  rn <- row.names(Y_raw)
  ii <- missing.min > threshold
  Y_raw <- rbind(Y_raw[ii,])
  row.names(Y_raw) <- rn[ii]

  return(Y_raw)
}

get_coefs <- function(Y_raw, plot.opt=F){
  # this has a lot of side effects
  #filter.y()
  n.peptide <- nrow(Y_raw)
  n.peptide <<- nrow(Y_raw)

#print("n.peptide is")
#print(n.peptide)
  y <- as.vector(t(Y_raw))
  y.cen <- y
#print(y.cen)
  n.peptide <- nrow(Y_raw)
#print(y.cen)
  n.treatment <- length(treatment)
  n.u.treatment <- length(unique(treatment))
  peptide <-rep(rep(1:n.peptide, each=n.treatment))
  if (n.peptide < 1){
    return(list(n = 0, p.val = NULL, coef = NULL))
  }
  missing <- array(NA, c(n.peptide, n.u.treatment))
colnames(missing) <- unique(treatment)
  for(i in 1:n.peptide) for(j in 1:n.u.treatment) missing[i,j] <- sum(!is.na(y.cen[peptide==i & treatment==unique(treatment)[j]]))
  missing.min <<- apply(missing, 1, min)
  peptides.missing <- rowSums(is.na(Y_raw))

  n <<- length(y)
  c.guess <<- min(y.cen, na.rm=T)

  f.treatment <- factor(rep(treatment, n.peptide))
  f.treatment <<- f.treatment

  na.set <- is.na(y.cen)
  if(n.peptide == 1){
    fit.1 <- lm(y.cen ~ f.treatment -1)
    p <- ncol(model.matrix(~f.treatment - 1))
  } else {
    fit.1 <- lm(y.cen ~ factor(peptide) + f.treatment-1)
    p <- ncol(model.matrix(~factor(peptide) + f.treatment - 1))
  }

  if(n.peptide==1){
    y.predict <<- model.matrix(~f.treatment - 1)%*% fit.1$coef
  } else {
    y.predict <<- model.matrix(~factor(peptide) + f.treatment - 1)%*% fit.1$coef
  }
#print("here2")
  m <- n.peptide
  ii <- (1:n)[is.na(y.cen)]
  if (m != 1){
    X  <- model.matrix(~factor(peptide) + f.treatment -1)
    n_obs <- as.numeric(table(factor(peptide)))
  } else {
    X <- model.matrix(~f.treatment)
  }
  if(length(ii) > 0){
    y.c <- y[-ii]
    X.c <- X[-ii,]
  } else {
    y.c <- y
    X.c <- X
  }

  seen.beta <- rep(NA, ncol(X))
  for(i in 1:ncol(X)) seen.beta[i] <- sum(!is.na(y.cen)[which(X[,i]==1)])>0
  #seen.beta <- seen.beta[-(1:m)]

  if(length(ii) == 0){
a <- list(n=n.peptide, coef=coef(fit.1)[-(1:m)], p.val = summary(fit.1)$coefficients[,4][-(1:m)], seen.beta = seen.beta)
    return(a)
  }

  my.beta = drop(solve(t(X.c) %*% X.c) %*% t(X.c) %*% y.c)
  y.hat = drop(X.c %*% my.beta)
  ee <- y.c - y.hat

#print("here3")
  # variance calculation code
  k <- length(table(treatment))
  # code for estimating delta
   if(n.peptide == 1) {
      delta.hat <- var(ee) * (n - 1) / (n - p)
      delta.hat <- sqrt(1/delta.hat)
      dd <- rep(delta.hat, n)
    } else {
	
      X.pep <- model.matrix(~ factor(peptide))
      ee.ss <- drop((ee ^ 2) %*% (X.pep[-ii,]))
      if (length(ii) == 0) ee.ss <- drop((ee ^ 2) %*% (X.pep))
      delta.hat <- ee.ss / (n_obs - (k - 1) - k / n)
      delta.hat <- sqrt(1 / delta.hat)
      dd <- delta.hat[as.numeric(peptide)]
    }

  # single imputation code
  # replace variance with vars from most-complete peptide
  # yuliya's method: make variance proportional to number missing
  peptides.missing[peptides.missing==0] <- 0.9
  # delta.hat is 1/sd
  dd <- dd/(peptides.missing[peptide])

  zeta <- dd*(c.guess - y.predict)
  prob.cen <- pnorm(zeta)/(my.pi + (1-my.pi)*pnorm(zeta))
  choose.cen <- runif(n) < prob.cen
  set.cen <- is.na(y.cen) &choose.cen
  set.mar <- is.na(y.cen) &!choose.cen

  kappa <- my.pi + (1 - my.pi)*dnorm(zeta)

  # information calculations
  I_beta <- t(X) %*% diag(as.vector(dd^2*(1 - kappa*(1 + my.Psi.dash(zeta, my.pi))))) %*% X
  I_GRP <- I_beta[rev(rev(1:p)[1:(k - 1)]), rev(rev(1:p)[1:(k - 1)]), drop = F]
  det(I_GRP)

  if(is.na(sum(set.cen))) {
    print("NA in set.cen")
    return(0)
  }

  sigma <- 1/dd
  y.impute <- y.cen
  if(sum(set.cen) > 0){
    y.impute[set.cen] <- rnorm.trunc(sum(set.cen), y.predict[set.cen], sigma[set.cen], hi=rep(c.guess, n)[set.cen])
}
  y.impute[set.mar] <- rnorm(n, y.predict, sigma)[set.mar]

  if (n.peptide == 1){
    fit.1 <- lm(y ~ f.treatment)
    fit.2 <- lm(y.impute ~ f.treatment)
  } else {
    fit.1 <- lm(y ~ factor(peptide) + f.treatment -1)
    fit.2 <- lm(y.impute ~ factor(peptide) + f.treatment -1)
  }
# which coefficients in beta are undetermined?

y.impute.return <- t(array(y.impute, rev(dim(Y_raw))))
row.names(y.impute.return) <- row.names(Y_raw)

m <- n.peptide
a <- list(n=n.peptide, coef=coef(fit.2)[-(1:m)], p.val = summary(fit.2)$coefficients[,4][-(1:m)], I_GRP=det(I_GRP), sigma=sigma, seen.beta = seen.beta, y.impute.return=y.impute.return)
if(plot.opt) plot.more.stuff()
#print("here4")
return(a)
}

# truncated normal
rnorm.trunc <- function (n, mu, sigma, lo=-Inf, hi=Inf){
  p.lo <- pnorm (lo, mu, sigma)
  p.hi <- pnorm (hi, mu, sigma)
  u <- runif (n, p.lo, p.hi)
  return (qnorm (u, mu, sigma))
}


# plotting routines
# this one plots the curve fit of SSfpl


PlotTamuQ <- function(file="deleteme.png")
{
    #png(filename=file,width=1152,height=864,pointsize=12,bg=bkground,
    #        res=600)
    require(Cairo)
    CairoPNG(filename=file,width=600,height=400, res=600)

    #Data <- Eset
    tryCatch(
    {
plot(grp.x, grp.y, pch=20, cex=0.5, main="4-parameter logistic fit to missing data", xlab="Mean peptide log-intensity", ylab = "Proportion missing")
curve(SSfpl(x, ss.beta[1], ss.beta[2], ss.beta[3], ss.beta[4]), add=T, col="red", lwd=2)
text(min(grp.x) + 0.85*diff(range(grp.x)), my.pi + 0.05, substitute(pi == x,  list(x=round(ss.beta[2], 3))), cex=2)

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


#plot.stuff <- function(){
#plot(grp.x, grp.y, pch=20, cex=0.5, main="4-parameter logistic fit to missing data", xlab="Mean peptide log-intensity", ylab = #"Proportion missing")
#curve(SSfpl(x, beta[1], beta[2], beta[3], beta[4]), add=T, col="red", lwd=2)
#text(min(grp.x) + 0.85*diff(range(grp.x)), my.pi + 0.05, substitute(pi == x,  list(x=round(beta[2], 3))), cex=2)
#}

# this one plots censored and MAR data
plot.more.stuff <- function(){
ylim=range(y.cen, na.rm=T)+c(-1, 1)
x.off <- 0.5*(1:n.u.treatment)/n.u.treatment

plot(peptide + x.off[f.treatment], y.cen, xlab="Peptide", ylab = "Intensity", ylim=ylim)
points(peptide + x.off[f.treatment], y.cen, pch=20, col=rainbow(n.u.treatment)[f.treatment])
points((peptide + x.off[f.treatment])[set.cen], y.impute[set.cen], col="red", pch=3)
points((peptide + x.off[f.treatment])[set.mar], y.impute[set.mar], col="green", pch=3)
abline(h = c.guess, lty=2)

title(expression(phantom("Real, ")* "censored," * phantom(" MAR ANOVA intensities")), col.main="red")
title(expression(phantom("Real, censored, ")* "MAR" * phantom(" ANOVA intensities")), col.main="green")
title(expression("Real, " * phantom("censored, MAR")* " ANOVA intensities"), col.main="black")

}

################################################################
# Calculates Psi and its derivative
################################################################
my.Psi = function(x, my.pi){
exp(log(1-my.pi)  + dnorm(x, 0, 1, log=T) - log(my.pi + (1 - my.pi) * pnorm(x, 0, 1) ))
}

################################################################
my.Psi.dash = function(x, my.pi){
-my.Psi(x, my.pi) * (x + my.Psi(x, my.pi))
}

phi = function(x){dnorm(x)}


# Ordering routine
# We compute the most-informative of all our single peptides.
#Y_all <- read.table("C:/Documents and Settings/d3x487/Desktop/Alan Dabney/s1/protein1.txt")

get_cutoff_informative_peptides <- function(){
best.ind <<- NULL
grp.info.best <<- NULL

while(length(setdiff(1:nrow(Y_all), best.ind)) > 0){
my.sigma <<- rep(NA, nrow(Y_all))
grp.info <<- rep(NA, nrow(Y_all))

for(i in setdiff(1:nrow(Y_all), best.ind)){
  Y_raw <<- Y_all[c(best.ind, i),-1]
  treatment <<- rep(c(0, 1), each=10)
  n.peptide <<- nrow(Y_raw)
  y <<- as.vector(t(Y_raw))
  y.cen <<- y
  n.treatment <<- length(treatment)
  n.u.treatment <<- length(unique(treatment))
  peptide <<-rep(rep(1:n.peptide, each=n.treatment))
  f.treatment <- factor(rep(treatment, n.peptide))

  m.missing <<- array(NA, c(n.peptide, n.u.treatment))
  colnames(m.missing) <<- unique(treatment)
  for(k in 1:n.peptide) for(j in 1:n.u.treatment) m.missing[k,j] <<-   sum(!is.na(y.cen[peptide==k & treatment==unique(treatment)[j]]))

  missing.min <<- apply(m.missing, 1, min)
  peptides.missing <<- n.treatment-apply(m.missing, 1, sum)
  gc <<- get_coefs()
  my.sigma[i] <<- gc$sigma[1]
  grp.info[i] <<- gc$I_GRP
}
#my.sigma
best.ind <<- c(best.ind, which(grp.info == max(grp.info, na.rm=T)))
grp.info.best <<- c(grp.info.best, max(grp.info, na.rm=T))
#best.ind
#Y_all[best.ind,]
#gr1 <- grp.info/max(grp.info, na.rm=T)
#par(mfrow=c(1,2))
#plot(rowSums(!is.na(Y_all)), gr1)
#plot(rowSums(!is.na(Y_all)), my.sigma)
}
b.i <<- best.ind[grp.info.best/max(grp.info.best) < 0.80]
return(b.i)
}
# filter the most informative 80% of peptides
#informative.ind <- get_cutoff_informative_peptides()

# impute the coefficient from this

#  Y_raw <- Y_all[informative.ind,-1]
#  treatment <- rep(c(0, 1), each=10)
#  n.peptide <<- nrow(Y_raw)
#  y <<- as.vector(t(Y_raw))
#  y.cen <<- y
#  n.treatment <<- length(treatment)
#  n.u.treatment <<- length(unique(treatment))
#  peptide <<-rep(rep(1:n.peptide, each=n.treatment))
#  f.treatment <- factor(rep(treatment, n.peptide))#
#
#  m.missing <- array(NA, c(n.peptide, n.u.treatment))
#  colnames(m.missing) <- unique(treatment)
#  for(k in 1:n.peptide) for(j in 1:n.u.treatment) m.missing[k,j] <-   sum(!is.na(y.cen[peptide==k & #treatment==unique(treatment)[j]]))##
#
#  missing.min <<- apply(m.missing, 1, min)
#  peptides.missing <<- n.treatment-apply(m.missing, 1, sum)
#  gc <- get_coefs()


