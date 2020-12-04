import React from 'react';
import classnames from 'classnames';
import Layout from '@theme/Layout';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import useBaseUrl from '@docusaurus/useBaseUrl';
import styles from './styles.module.css';

const largeImgFeature = [
  {
    imageUrl: 'img/abstract_overview.svg',
  },
  {
    title: <>Improve Sensitivity and Specificity of Peak Calling, and Identify Consensus Regions</>,
    description: (
      <>
        MSPC uses combined evidence from replicated experiments to evaluate
        peak calling output, rescuing peaks, and reduce false positives.
        It takes any number of replicates as input and improves sensitivity
        and specificity of peak calling on each, and identifies consensus
        regions between the input samples.
      </>
    ),
    //link: 'docs/quick_start'
  },
];


const repTypes = [
  {
    title: <>Accounts for the Differences Between Biological and Technical Replicates</>,
    //imageUrl: 'img/...',
    description: (
      <>
        A higher degree of similarity is expected between technical replicates
        compared to biological replicates, where the differences between biological
        replicates represent true biological variability while the differences
        between technical replicates are predominantly artifactual bindings or
        signal noise.
        MSPC accounts for such characteristics when evaluating binding sites.
      </>
    ),
  },
  {
    imageUrl: 'img/tech_bio_ref_diff_process.svg',
  },
];

// Replicated experiments can improve the sensitivity and 
// specificity of peak callers by providing more evidence 
// to differentiate between artifactual bindings and weak,
// but true binding sites.Therefore, a common practice is
// to replicate(biologically or technically) experiments.
// Due to the sequencing costs, the number of replicates 
// were limited to two; however, with the drop in sequencing 
// costs, a growing number of experiments are producing 
// more than two replicates.


const features = [
  {
    title: <>Supports Any Number of Replicates</>,
    description: (
      <>
        Designed ground-up to efficiently process any number of replicated experiments.
      </>
    ),
    //link: 'docs/quick_start'
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
];

function WholeRowFeature({imageUrl, title, description}) {
  const imgUrl = useBaseUrl(imageUrl);
  return (
    <div className={classnames('col col--6', styles.feature)}>
      {imgUrl && (
        <div className="text--center">
          <img className={styles.largeFeatureImage} src={imgUrl} alt={title} />
        </div>
      )}
      <h3>{title}</h3>
      <p>{description}</p>
    </div>
  );
}

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
          <img src="logo/logo_w_txt_banner.svg" alt="logo" height="40%" width="40%"/>
          <div className={styles.buttons, styles.quickstartButton}>
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
              <div className={classnames("row", "single-feature-row")}>
                {largeImgFeature.map((props, idx) => (
                  <WholeRowFeature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}

        {repTypes && repTypes.length > 0 && (
          <section className={styles.features, styles.featuresAlt}>
            <div className="container">
              <div className={classnames("row", "single-feature-row")}>
                {repTypes.map((props, idx) => (
                  <WholeRowFeature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}

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
      </main>
    </Layout>
  );
}

export default Home;
