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


const P1 = (props) => (
  <div className={pubsStyles.container}>
    <header>
      <h4>{props.title}</h4>
    </header>
    <div className={pubsStyles.content}>
      <div className={classnames(pubsStyles.citations, "__dimensions_badge_embed__")} data-doi={props.doi} data-style="small_circle" />
      <div className={classnames(pubsStyles.referenceCounter, "altmetric-embed")} data-badge-popover="top" data-badge-type="donut" data-condensed="true" data-hide-no-mentions="true" data-doi={props.doi}/>
      <a className={pubsStyles.publicationLink} target="_pub" href={props.link}>{props.linkdescription}</a>
    </div>
  </div>
  );

function Feature({imageUrl, title, description}) {
  const imgUrl = useBaseUrl(imageUrl);
  return (
    <div className={classnames('col col--4', styles.feature)}>
      {imgUrl && (
        <div className="text--center">
          <img className={styles.featureImage} src={imgUrl} alt={title} />
        </div>
      )}
      <h3>{title}</h3>
      <p>{description}</p>
    </div>
  );
}

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
          <h1 className="hero__title">{siteConfig.title}</h1>
          <p className="hero__subtitle">{siteConfig.tagline}</p>
          <div className={styles.buttons}>
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
          <P1
            title="Jalili, V., Matteucci, M., Masseroli, M., & Morelli, M. J. (2015). Using combined evidence from replicates to evaluate ChIP-seq peaks. Bioinformatics, 31(17), 2761-2769."
            doi="10.1093/bioinformatics/btv293"
            link="https://academic.oup.com/bioinformatics/article/31/17/2761/183989"
            linkdescription="Web" />

          <P1
            title="Jalili, V., Matteucci, M., Morelli, M. J., & Masseroli, M. (2016). MuSERA: multiple sample enriched region assessment. Briefings in bioinformatics, 18(3), 367-381."
            doi="10.1093/bib/bbw029"
            link="https://academic.oup.com/bib/article-abstract/18/3/367/2562755"
            linkdescription="Web" />
        </div>
      </main>
    </Layout>
  );
}

export default Home;
