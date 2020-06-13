import React from 'react';
import classnames from 'classnames';
import Layout from '@theme/Layout';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import useBaseUrl from '@docusaurus/useBaseUrl';
import styles from './styles.module.css';

const features = [
  {
    //title: <>Easy to Use</>,
    //imageUrl: 'img/...',
    description: (
      <>
        Rescue weak ChIP-seq peaks without introducing false-positives.
      </>
    ),
  },
];

const features2 = [
  {
    title: <>Any Number of Replicates</>,
    description: (
      <>
        Designed ground-up to support any number of replicates
      </>
      )
  },
  {
    title: <>Bulk and Single-cell</>,
    description: (
      <>
        MSPC can be applied to data at both bulk and single-cell resoloutions
      </>
    )
  },
  {
    title: <>Reliable and Performant</>,
    description: (
      <>
        Reliable, with 98% code-coverage, and performant that seamlessly scales 
        to hundreds of thousands of input on a standard personal computer 
      </>
    )
  },
]

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
      <header className={classnames('hero hero--primary', styles.heroBanner)}>
        <div className="container">
          <h1 className="hero__title">{siteConfig.title}</h1>
          <p className="hero__subtitle">{siteConfig.tagline}</p>
          <div className={styles.buttons}>
            <Link
              className={classnames(
                'button button--outline button--secondary button--lg',
                /*styles.neonButton,*/
                styles.getStarted,
              )}
              to={useBaseUrl('docs/quick_start')}>
              Quick Start
            </Link>
          </div>
        </div>
      </header>
      <main>
        {features && features.length > 0 && (
          <section className={styles.features}>
            <div className="container">
              <div className="row">
                {features.map((props, idx) => (
                  <Feature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}

        {features2 && features2.length > 0 && (
          <section className={styles.features, styles.featuresAlt}>
            <div className="container">
              <div className="row">
                {features2.map((props, idx) => (
                  <Feature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}
      </main>
    </Layout>
  );
}

export default Home;
