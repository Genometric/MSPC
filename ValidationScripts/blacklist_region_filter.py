import os

input_path = "..."
blacklist_regions = "..."

for root, dirs, files in os.walk(input_path):
    for experiment_folder in dirs:
        os.rename(os.path.join(root, experiment_folder), os.path.join(root, experiment_folder.replace("--", "")))

for root, dirs, files in os.walk(input_path):
    for experiment_folder in dirs:
        for assay in os.listdir(os.path.join(root, experiment_folder)):
            if assay.endswith("-rep1.bed") or assay.endswith("-rep2.bed"):
                os.system("bedtools intersect -a {} -b {} -v > {}".format(
                    os.path.join(root, experiment_folder, assay),
                    blacklist_regions,
                    os.path.join(root,
                                 experiment_folder,
                                 assay.replace("-rep1.bed", "-filtered-rep1.bed").replace("-rep2.bed", "-filtered-rep2.bed"))
                ))

