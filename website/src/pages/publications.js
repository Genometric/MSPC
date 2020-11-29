import React from 'react';
import classnames from 'classnames';
import Layout from '@theme/Layout';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import useBaseUrl from '@docusaurus/useBaseUrl';
import styles from './styles.module.css';

// TODO: maybe better to pub this css in the src/css/ folder 
// and import all styles globally. 
import pubsStyles from './pubs.module.css';
import Head from '@docusaurus/Head';



const Publication = (props) => (
  <div className={pubsStyles.container}>
    <header className={pubsStyles.biblio}>
      <span className={pubsStyles.authors}>{props.authors} </span>
      <span className={pubsStyles.year}>({props.year}). </span>
      <span className={pubsStyles.title}>{props.title}. </span>
      <span className={pubsStyles.journal}>{props.journal}, </span>
      <span className={pubsStyles.volume}>{props.volume}</span>
      <span className={pubsStyles.issue}>({props.issue}), </span>
      <span className={pubsStyles.pages}>{props.pages}. </span>
      <a href={props.link}>{props.doi} (open-access)</a>
    </header>
    <div className={pubsStyles.content}>
      <div className={classnames(pubsStyles.citations, "__dimensions_badge_embed__")} data-doi={props.doi} data-style="small_circle" />
      <div className={classnames(pubsStyles.referenceCounter, "altmetric-embed")} data-badge-popover="top" data-badge-type="donut" data-condensed="true" data-hide-no-mentions="true" data-doi={props.doi}/>
    </div>
  </div>
);

function Home() {
  const context = useDocusaurusContext();
  const {siteConfig = {}} = context;
  return (
    <Layout
      title={`${siteConfig.title}`}
      description="Using combined evidence from replicates to evaluate ChIP-seq peaks">
      <Head>
        <script src="https://badge.dimensions.ai/badge.js" async="true"></script>
        <script src="https://d1bxh8uas1mnw7.cloudfront.net/assets/embed.js" async="true"></script>
      </Head>
      <header className={classnames('hero hero--primary', styles.heroBanner)}>
        <div className="container">
          <img src="logo/logo_w_txt_banner.svg" alt="logo" height="40%" width="40%"/>
          <div className={styles.buttons, styles.quickstartButton}>
            <Link
              className={classnames(
                'button button--outline button--secondary button--lg',
                styles.getStarted,
              )}
              to={useBaseUrl('docs/quick_start')}>
              Quick Start
            </Link>
          </div>
        </div>
      </header>
      <main>
        <div className={pubsStyles.publications}>
          <Publication
            authors="Jalili, V., Matteucci, M., Masseroli, M., & Morelli, M. J."
            title="Using combined evidence from replicates to evaluate ChIP-seq peaks"
            year="2015"
            journal="Bioinformatics"
            volume="31"
            issue="17"
            pages="2761-2769"
            doi="10.1093/bioinformatics/btv293"
            link="https://academic.oup.com/bioinformatics/article/31/17/2761/183989" />

          <Publication
            authors="Jalili, V., Matteucci, M., Morelli, M. J., & Masseroli, M."
            title="MuSERA: multiple sample enriched region assessment"
            year="2016"
            journal="Briefings in bioinformatics"
            volume="18"
            issue="3"
            pages="367-381"
            doi="10.1093/bib/bbw029"
            link="https://academic.oup.com/bib/article-abstract/18/3/367/2562755" />
        </div>
      </main>
    </Layout>
  );
}

export default Home;
