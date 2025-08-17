---
license: cc-by-nc-4.0
tags:
- feature-extraction
- sentence-similarity
- mteb
- sentence-transformers
language:
  - multilingual
  - af
  - am
  - ar
  - as
  - az
  - be
  - bg
  - bn
  - br
  - bs
  - ca
  - cs
  - cy
  - da
  - de
  - el
  - en
  - eo
  - es
  - et
  - eu
  - fa
  - fi
  - fr
  - fy
  - ga
  - gd
  - gl
  - gu
  - ha
  - he
  - hi
  - hr
  - hu
  - hy
  - id
  - is
  - it
  - ja
  - jv
  - ka
  - kk
  - km
  - kn
  - ko
  - ku
  - ky
  - la
  - lo
  - lt
  - lv
  - mg
  - mk
  - ml
  - mn
  - mr
  - ms
  - my
  - ne
  - nl
  - no
  - om
  - or
  - pa
  - pl
  - ps
  - pt
  - ro
  - ru
  - sa
  - sd
  - si
  - sk
  - sl
  - so
  - sq
  - sr
  - su
  - sv
  - sw
  - ta
  - te
  - th
  - tl
  - tr
  - ug
  - uk
  - ur
  - uz
  - vi
  - xh
  - yi
  - zh
inference: false
library_name: transformers
model-index:
- name: jina-embeddings-v3
  results:
  - dataset:
      config: default
      name: MTEB AFQMC (default)
      revision: b44c3b011063adb25877c13823db83bb193913c4
      split: validation
      type: C-MTEB/AFQMC
    metrics:
    - type: cosine_pearson
      value: 41.74237700998808
    - type: cosine_spearman
      value: 43.4726782647566
    - type: euclidean_pearson
      value: 42.244585459479964
    - type: euclidean_spearman
      value: 43.525070045169606
    - type: main_score
      value: 43.4726782647566
    - type: manhattan_pearson
      value: 42.04616728224863
    - type: manhattan_spearman
      value: 43.308828270754645
    - type: pearson
      value: 41.74237700998808
    - type: spearman
      value: 43.4726782647566
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB ArguAna-PL (default)
      revision: 63fc86750af76253e8c760fc9e534bbf24d260a2
      split: test
      type: clarin-knext/arguana-pl
    metrics:
    - type: main_score
      value: 50.117999999999995
    - type: map_at_1
      value: 24.253
    - type: map_at_10
      value: 40.725
    - type: map_at_100
      value: 41.699999999999996
    - type: map_at_1000
      value: 41.707
    - type: map_at_20
      value: 41.467999999999996
    - type: map_at_3
      value: 35.467
    - type: map_at_5
      value: 38.291
    - type: mrr_at_1
      value: 24.751066856330013
    - type: mrr_at_10
      value: 40.91063808169072
    - type: mrr_at_100
      value: 41.885497923928675
    - type: mrr_at_1000
      value: 41.89301098419842
    - type: mrr_at_20
      value: 41.653552355442514
    - type: mrr_at_3
      value: 35.656709340919775
    - type: mrr_at_5
      value: 38.466097676623946
    - type: nauc_map_at_1000_diff1
      value: 7.503000359807567
    - type: nauc_map_at_1000_max
      value: -11.030405164830546
    - type: nauc_map_at_1000_std
      value: -8.902792782585117
    - type: nauc_map_at_100_diff1
      value: 7.509899249593199
    - type: nauc_map_at_100_max
      value: -11.023581259404406
    - type: nauc_map_at_100_std
      value: -8.892241185067272
    - type: nauc_map_at_10_diff1
      value: 7.24369711881512
    - type: nauc_map_at_10_max
      value: -10.810000200433278
    - type: nauc_map_at_10_std
      value: -8.987230542165776
    - type: nauc_map_at_1_diff1
      value: 11.37175831832417
    - type: nauc_map_at_1_max
      value: -13.315221903223055
    - type: nauc_map_at_1_std
      value: -9.398199605510275
    - type: nauc_map_at_20_diff1
      value: 7.477364530860648
    - type: nauc_map_at_20_max
      value: -10.901251218105566
    - type: nauc_map_at_20_std
      value: -8.868148116405925
    - type: nauc_map_at_3_diff1
      value: 6.555548802174882
    - type: nauc_map_at_3_max
      value: -12.247274800542934
    - type: nauc_map_at_3_std
      value: -9.879475250984811
    - type: nauc_map_at_5_diff1
      value: 7.426588563355882
    - type: nauc_map_at_5_max
      value: -11.347695686001805
    - type: nauc_map_at_5_std
      value: -9.34441892203972
    - type: nauc_mrr_at_1000_diff1
      value: 5.99737552143614
    - type: nauc_mrr_at_1000_max
      value: -11.327205136505727
    - type: nauc_mrr_at_1000_std
      value: -8.791079115519503
    - type: nauc_mrr_at_100_diff1
      value: 6.004622525255784
    - type: nauc_mrr_at_100_max
      value: -11.320336759899723
    - type: nauc_mrr_at_100_std
      value: -8.780602249831777
    - type: nauc_mrr_at_10_diff1
      value: 5.783623516930227
    - type: nauc_mrr_at_10_max
      value: -11.095971693467078
    - type: nauc_mrr_at_10_std
      value: -8.877242032013582
    - type: nauc_mrr_at_1_diff1
      value: 9.694937537703797
    - type: nauc_mrr_at_1_max
      value: -12.531905083727912
    - type: nauc_mrr_at_1_std
      value: -8.903992940100146
    - type: nauc_mrr_at_20_diff1
      value: 5.984841206233873
    - type: nauc_mrr_at_20_max
      value: -11.195236951048969
    - type: nauc_mrr_at_20_std
      value: -8.757266039186018
    - type: nauc_mrr_at_3_diff1
      value: 5.114333824261379
    - type: nauc_mrr_at_3_max
      value: -12.64809799843464
    - type: nauc_mrr_at_3_std
      value: -9.791146138025184
    - type: nauc_mrr_at_5_diff1
      value: 5.88941606224512
    - type: nauc_mrr_at_5_max
      value: -11.763903418071918
    - type: nauc_mrr_at_5_std
      value: -9.279175712709446
    - type: nauc_ndcg_at_1000_diff1
      value: 7.076950652226086
    - type: nauc_ndcg_at_1000_max
      value: -10.386482092087371
    - type: nauc_ndcg_at_1000_std
      value: -8.309190917074046
    - type: nauc_ndcg_at_100_diff1
      value: 7.2329220284865245
    - type: nauc_ndcg_at_100_max
      value: -10.208048403220337
    - type: nauc_ndcg_at_100_std
      value: -7.997975874274613
    - type: nauc_ndcg_at_10_diff1
      value: 6.065391100006953
    - type: nauc_ndcg_at_10_max
      value: -9.046164377601153
    - type: nauc_ndcg_at_10_std
      value: -8.34724889697153
    - type: nauc_ndcg_at_1_diff1
      value: 11.37175831832417
    - type: nauc_ndcg_at_1_max
      value: -13.315221903223055
    - type: nauc_ndcg_at_1_std
      value: -9.398199605510275
    - type: nauc_ndcg_at_20_diff1
      value: 6.949389989202601
    - type: nauc_ndcg_at_20_max
      value: -9.35740451760307
    - type: nauc_ndcg_at_20_std
      value: -7.761295171828212
    - type: nauc_ndcg_at_3_diff1
      value: 5.051471796151364
    - type: nauc_ndcg_at_3_max
      value: -12.158763333711653
    - type: nauc_ndcg_at_3_std
      value: -10.078902544421926
    - type: nauc_ndcg_at_5_diff1
      value: 6.527454512611454
    - type: nauc_ndcg_at_5_max
      value: -10.525118233848586
    - type: nauc_ndcg_at_5_std
      value: -9.120055125584031
    - type: nauc_precision_at_1000_diff1
      value: -10.6495668199151
    - type: nauc_precision_at_1000_max
      value: 12.070656425217841
    - type: nauc_precision_at_1000_std
      value: 55.844551709649004
    - type: nauc_precision_at_100_diff1
      value: 19.206967129266285
    - type: nauc_precision_at_100_max
      value: 16.296851020813456
    - type: nauc_precision_at_100_std
      value: 45.60378984257811
    - type: nauc_precision_at_10_diff1
      value: 0.6490335354304879
    - type: nauc_precision_at_10_max
      value: 0.5757198255366447
    - type: nauc_precision_at_10_std
      value: -4.875847131691451
    - type: nauc_precision_at_1_diff1
      value: 11.37175831832417
    - type: nauc_precision_at_1_max
      value: -13.315221903223055
    - type: nauc_precision_at_1_std
      value: -9.398199605510275
    - type: nauc_precision_at_20_diff1
      value: 4.899369866929203
    - type: nauc_precision_at_20_max
      value: 5.988537297189552
    - type: nauc_precision_at_20_std
      value: 4.830900387582837
    - type: nauc_precision_at_3_diff1
      value: 0.8791156910997744
    - type: nauc_precision_at_3_max
      value: -11.983373635905993
    - type: nauc_precision_at_3_std
      value: -10.646185111581257
    - type: nauc_precision_at_5_diff1
      value: 3.9314486166548432
    - type: nauc_precision_at_5_max
      value: -7.798591396895839
    - type: nauc_precision_at_5_std
      value: -8.293043407234125
    - type: nauc_recall_at_1000_diff1
      value: -10.649566819918673
    - type: nauc_recall_at_1000_max
      value: 12.070656425214647
    - type: nauc_recall_at_1000_std
      value: 55.84455170965023
    - type: nauc_recall_at_100_diff1
      value: 19.206967129265127
    - type: nauc_recall_at_100_max
      value: 16.296851020813722
    - type: nauc_recall_at_100_std
      value: 45.60378984257728
    - type: nauc_recall_at_10_diff1
      value: 0.6490335354304176
    - type: nauc_recall_at_10_max
      value: 0.5757198255366095
    - type: nauc_recall_at_10_std
      value: -4.875847131691468
    - type: nauc_recall_at_1_diff1
      value: 11.37175831832417
    - type: nauc_recall_at_1_max
      value: -13.315221903223055
    - type: nauc_recall_at_1_std
      value: -9.398199605510275
    - type: nauc_recall_at_20_diff1
      value: 4.899369866929402
    - type: nauc_recall_at_20_max
      value: 5.98853729718968
    - type: nauc_recall_at_20_std
      value: 4.830900387582967
    - type: nauc_recall_at_3_diff1
      value: 0.8791156910997652
    - type: nauc_recall_at_3_max
      value: -11.983373635905997
    - type: nauc_recall_at_3_std
      value: -10.64618511158124
    - type: nauc_recall_at_5_diff1
      value: 3.9314486166548472
    - type: nauc_recall_at_5_max
      value: -7.7985913968958585
    - type: nauc_recall_at_5_std
      value: -8.293043407234132
    - type: ndcg_at_1
      value: 24.253
    - type: ndcg_at_10
      value: 50.117999999999995
    - type: ndcg_at_100
      value: 54.291999999999994
    - type: ndcg_at_1000
      value: 54.44799999999999
    - type: ndcg_at_20
      value: 52.771
    - type: ndcg_at_3
      value: 39.296
    - type: ndcg_at_5
      value: 44.373000000000005
    - type: precision_at_1
      value: 24.253
    - type: precision_at_10
      value: 8.016
    - type: precision_at_100
      value: 0.984
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.527
    - type: precision_at_3
      value: 16.808999999999997
    - type: precision_at_5
      value: 12.546
    - type: recall_at_1
      value: 24.253
    - type: recall_at_10
      value: 80.156
    - type: recall_at_100
      value: 98.43499999999999
    - type: recall_at_1000
      value: 99.57300000000001
    - type: recall_at_20
      value: 90.54100000000001
    - type: recall_at_3
      value: 50.427
    - type: recall_at_5
      value: 62.731
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB DBPedia-PL (default)
      revision: 76afe41d9af165cc40999fcaa92312b8b012064a
      split: test
      type: clarin-knext/dbpedia-pl
    metrics:
    - type: main_score
      value: 34.827000000000005
    - type: map_at_1
      value: 7.049999999999999
    - type: map_at_10
      value: 14.982999999999999
    - type: map_at_100
      value: 20.816000000000003
    - type: map_at_1000
      value: 22.33
    - type: map_at_20
      value: 17.272000000000002
    - type: map_at_3
      value: 10.661
    - type: map_at_5
      value: 12.498
    - type: mrr_at_1
      value: 57.25
    - type: mrr_at_10
      value: 65.81934523809524
    - type: mrr_at_100
      value: 66.2564203928212
    - type: mrr_at_1000
      value: 66.27993662923856
    - type: mrr_at_20
      value: 66.0732139130649
    - type: mrr_at_3
      value: 64.08333333333333
    - type: mrr_at_5
      value: 65.27083333333333
    - type: nauc_map_at_1000_diff1
      value: 16.41780871174038
    - type: nauc_map_at_1000_max
      value: 30.193946325654654
    - type: nauc_map_at_1000_std
      value: 31.46095497039037
    - type: nauc_map_at_100_diff1
      value: 18.57903165498531
    - type: nauc_map_at_100_max
      value: 29.541476938623262
    - type: nauc_map_at_100_std
      value: 28.228604103301052
    - type: nauc_map_at_10_diff1
      value: 24.109434489748946
    - type: nauc_map_at_10_max
      value: 21.475954208048968
    - type: nauc_map_at_10_std
      value: 9.964464537806988
    - type: nauc_map_at_1_diff1
      value: 38.67437644802124
    - type: nauc_map_at_1_max
      value: 14.52136658726491
    - type: nauc_map_at_1_std
      value: -2.8981666782088755
    - type: nauc_map_at_20_diff1
      value: 21.42547228801935
    - type: nauc_map_at_20_max
      value: 25.04510402960458
    - type: nauc_map_at_20_std
      value: 16.533079346431155
    - type: nauc_map_at_3_diff1
      value: 26.63648858245477
    - type: nauc_map_at_3_max
      value: 13.632235789780415
    - type: nauc_map_at_3_std
      value: -0.40129174577700716
    - type: nauc_map_at_5_diff1
      value: 24.513861031197933
    - type: nauc_map_at_5_max
      value: 16.599888813946688
    - type: nauc_map_at_5_std
      value: 3.4448514739556346
    - type: nauc_mrr_at_1000_diff1
      value: 36.57353464537154
    - type: nauc_mrr_at_1000_max
      value: 55.34763483979515
    - type: nauc_mrr_at_1000_std
      value: 40.3722796438533
    - type: nauc_mrr_at_100_diff1
      value: 36.555989566513134
    - type: nauc_mrr_at_100_max
      value: 55.347805216808396
    - type: nauc_mrr_at_100_std
      value: 40.38465945075711
    - type: nauc_mrr_at_10_diff1
      value: 36.771572999261984
    - type: nauc_mrr_at_10_max
      value: 55.41239897909165
    - type: nauc_mrr_at_10_std
      value: 40.52058934624793
    - type: nauc_mrr_at_1_diff1
      value: 38.2472828531032
    - type: nauc_mrr_at_1_max
      value: 51.528473828685705
    - type: nauc_mrr_at_1_std
      value: 33.03676467942882
    - type: nauc_mrr_at_20_diff1
      value: 36.642602571889036
    - type: nauc_mrr_at_20_max
      value: 55.3763342076553
    - type: nauc_mrr_at_20_std
      value: 40.41520090500838
    - type: nauc_mrr_at_3_diff1
      value: 36.79451847426628
    - type: nauc_mrr_at_3_max
      value: 54.59778581826193
    - type: nauc_mrr_at_3_std
      value: 39.48392075873095
    - type: nauc_mrr_at_5_diff1
      value: 36.92150807529304
    - type: nauc_mrr_at_5_max
      value: 55.03553978718272
    - type: nauc_mrr_at_5_std
      value: 40.20147745489917
    - type: nauc_ndcg_at_1000_diff1
      value: 21.843092744321268
    - type: nauc_ndcg_at_1000_max
      value: 44.93275990394279
    - type: nauc_ndcg_at_1000_std
      value: 47.09186225236347
    - type: nauc_ndcg_at_100_diff1
      value: 25.180282568979095
    - type: nauc_ndcg_at_100_max
      value: 41.737709709508394
    - type: nauc_ndcg_at_100_std
      value: 38.80950644139446
    - type: nauc_ndcg_at_10_diff1
      value: 24.108368037214046
    - type: nauc_ndcg_at_10_max
      value: 41.29298370689967
    - type: nauc_ndcg_at_10_std
      value: 35.06450769738732
    - type: nauc_ndcg_at_1_diff1
      value: 35.51010679525079
    - type: nauc_ndcg_at_1_max
      value: 42.40790024212412
    - type: nauc_ndcg_at_1_std
      value: 26.696412036243157
    - type: nauc_ndcg_at_20_diff1
      value: 23.909989673256195
    - type: nauc_ndcg_at_20_max
      value: 39.78444647091927
    - type: nauc_ndcg_at_20_std
      value: 33.39544470364529
    - type: nauc_ndcg_at_3_diff1
      value: 22.50484297956035
    - type: nauc_ndcg_at_3_max
      value: 39.14551926034168
    - type: nauc_ndcg_at_3_std
      value: 30.330135925392014
    - type: nauc_ndcg_at_5_diff1
      value: 21.7798872028265
    - type: nauc_ndcg_at_5_max
      value: 40.23856975248015
    - type: nauc_ndcg_at_5_std
      value: 32.438381067440396
    - type: nauc_precision_at_1000_diff1
      value: -21.62692442272279
    - type: nauc_precision_at_1000_max
      value: 0.9689046974430882
    - type: nauc_precision_at_1000_std
      value: 18.54001058230465
    - type: nauc_precision_at_100_diff1
      value: -10.132258779856192
    - type: nauc_precision_at_100_max
      value: 23.74516110444681
    - type: nauc_precision_at_100_std
      value: 47.03416663319965
    - type: nauc_precision_at_10_diff1
      value: 1.543656509571949
    - type: nauc_precision_at_10_max
      value: 36.98864812757555
    - type: nauc_precision_at_10_std
      value: 46.56427199077426
    - type: nauc_precision_at_1_diff1
      value: 38.2472828531032
    - type: nauc_precision_at_1_max
      value: 51.528473828685705
    - type: nauc_precision_at_1_std
      value: 33.03676467942882
    - type: nauc_precision_at_20_diff1
      value: -4.612864872734335
    - type: nauc_precision_at_20_max
      value: 34.03565449182125
    - type: nauc_precision_at_20_std
      value: 48.880727648349534
    - type: nauc_precision_at_3_diff1
      value: 6.360850444467829
    - type: nauc_precision_at_3_max
      value: 36.25816942368427
    - type: nauc_precision_at_3_std
      value: 34.48882647419187
    - type: nauc_precision_at_5_diff1
      value: 2.6445596936740037
    - type: nauc_precision_at_5_max
      value: 37.174463388899056
    - type: nauc_precision_at_5_std
      value: 40.25254370626113
    - type: nauc_recall_at_1000_diff1
      value: 13.041227176748077
    - type: nauc_recall_at_1000_max
      value: 39.722336427072094
    - type: nauc_recall_at_1000_std
      value: 52.04032890059214
    - type: nauc_recall_at_100_diff1
      value: 18.286096899139153
    - type: nauc_recall_at_100_max
      value: 34.072389201930314
    - type: nauc_recall_at_100_std
      value: 37.73637623416653
    - type: nauc_recall_at_10_diff1
      value: 22.35560419280504
    - type: nauc_recall_at_10_max
      value: 19.727247199595197
    - type: nauc_recall_at_10_std
      value: 8.58498575109203
    - type: nauc_recall_at_1_diff1
      value: 38.67437644802124
    - type: nauc_recall_at_1_max
      value: 14.52136658726491
    - type: nauc_recall_at_1_std
      value: -2.8981666782088755
    - type: nauc_recall_at_20_diff1
      value: 19.026320886902916
    - type: nauc_recall_at_20_max
      value: 22.753562309469867
    - type: nauc_recall_at_20_std
      value: 14.89994263882445
    - type: nauc_recall_at_3_diff1
      value: 23.428129702129684
    - type: nauc_recall_at_3_max
      value: 10.549153954790542
    - type: nauc_recall_at_3_std
      value: -1.7590608997055206
    - type: nauc_recall_at_5_diff1
      value: 21.27448645803921
    - type: nauc_recall_at_5_max
      value: 13.620279707461677
    - type: nauc_recall_at_5_std
      value: 2.0577962208292675
    - type: ndcg_at_1
      value: 46.75
    - type: ndcg_at_10
      value: 34.827000000000005
    - type: ndcg_at_100
      value: 38.157999999999994
    - type: ndcg_at_1000
      value: 44.816
    - type: ndcg_at_20
      value: 34.152
    - type: ndcg_at_3
      value: 39.009
    - type: ndcg_at_5
      value: 36.826
    - type: precision_at_1
      value: 57.25
    - type: precision_at_10
      value: 27.575
    - type: precision_at_100
      value: 8.84
    - type: precision_at_1000
      value: 1.949
    - type: precision_at_20
      value: 20.724999999999998
    - type: precision_at_3
      value: 41.167
    - type: precision_at_5
      value: 35.199999999999996
    - type: recall_at_1
      value: 7.049999999999999
    - type: recall_at_10
      value: 19.817999999999998
    - type: recall_at_100
      value: 42.559999999999995
    - type: recall_at_1000
      value: 63.744
    - type: recall_at_20
      value: 25.968000000000004
    - type: recall_at_3
      value: 11.959
    - type: recall_at_5
      value: 14.939
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB FiQA-PL (default)
      revision: 2e535829717f8bf9dc829b7f911cc5bbd4e6608e
      split: test
      type: clarin-knext/fiqa-pl
    metrics:
    - type: main_score
      value: 38.828
    - type: map_at_1
      value: 19.126
    - type: map_at_10
      value: 31.002000000000002
    - type: map_at_100
      value: 32.736
    - type: map_at_1000
      value: 32.933
    - type: map_at_20
      value: 31.894
    - type: map_at_3
      value: 26.583000000000002
    - type: map_at_5
      value: 28.904000000000003
    - type: mrr_at_1
      value: 37.808641975308646
    - type: mrr_at_10
      value: 46.36745541838134
    - type: mrr_at_100
      value: 47.14140915794908
    - type: mrr_at_1000
      value: 47.190701435388846
    - type: mrr_at_20
      value: 46.81387776440309
    - type: mrr_at_3
      value: 43.750000000000014
    - type: mrr_at_5
      value: 45.23919753086418
    - type: nauc_map_at_1000_diff1
      value: 38.5532285881503
    - type: nauc_map_at_1000_max
      value: 34.44383884813453
    - type: nauc_map_at_1000_std
      value: -1.3963497949476722
    - type: nauc_map_at_100_diff1
      value: 38.49292464176943
    - type: nauc_map_at_100_max
      value: 34.33752755618645
    - type: nauc_map_at_100_std
      value: -1.4794032905848582
    - type: nauc_map_at_10_diff1
      value: 38.26061536370962
    - type: nauc_map_at_10_max
      value: 33.16977912721411
    - type: nauc_map_at_10_std
      value: -2.3853370604730393
    - type: nauc_map_at_1_diff1
      value: 46.288767289528344
    - type: nauc_map_at_1_max
      value: 25.67706785013364
    - type: nauc_map_at_1_std
      value: -6.989769609924645
    - type: nauc_map_at_20_diff1
      value: 38.507270129330685
    - type: nauc_map_at_20_max
      value: 33.70963328055982
    - type: nauc_map_at_20_std
      value: -1.9835510011554272
    - type: nauc_map_at_3_diff1
      value: 39.81061518646884
    - type: nauc_map_at_3_max
      value: 30.101186374147748
    - type: nauc_map_at_3_std
      value: -4.027120247237715
    - type: nauc_map_at_5_diff1
      value: 38.55602589746512
    - type: nauc_map_at_5_max
      value: 31.515174267015983
    - type: nauc_map_at_5_std
      value: -3.4064239358570303
    - type: nauc_mrr_at_1000_diff1
      value: 45.030514454725726
    - type: nauc_mrr_at_1000_max
      value: 43.878919881666164
    - type: nauc_mrr_at_1000_std
      value: 2.517594250297626
    - type: nauc_mrr_at_100_diff1
      value: 45.00868212878687
    - type: nauc_mrr_at_100_max
      value: 43.87437011120001
    - type: nauc_mrr_at_100_std
      value: 2.5257874265014966
    - type: nauc_mrr_at_10_diff1
      value: 44.855044606754056
    - type: nauc_mrr_at_10_max
      value: 43.946617058785186
    - type: nauc_mrr_at_10_std
      value: 2.5173751662794044
    - type: nauc_mrr_at_1_diff1
      value: 49.441510997817346
    - type: nauc_mrr_at_1_max
      value: 43.08547383044357
    - type: nauc_mrr_at_1_std
      value: -1.8747770703324347
    - type: nauc_mrr_at_20_diff1
      value: 45.019880416584215
    - type: nauc_mrr_at_20_max
      value: 43.85691473662242
    - type: nauc_mrr_at_20_std
      value: 2.4625487605091303
    - type: nauc_mrr_at_3_diff1
      value: 45.322041658604036
    - type: nauc_mrr_at_3_max
      value: 43.95079293074395
    - type: nauc_mrr_at_3_std
      value: 2.4644274393435737
    - type: nauc_mrr_at_5_diff1
      value: 44.99461837803437
    - type: nauc_mrr_at_5_max
      value: 43.97934275090601
    - type: nauc_mrr_at_5_std
      value: 2.5353091695125096
    - type: nauc_ndcg_at_1000_diff1
      value: 39.38449023275524
    - type: nauc_ndcg_at_1000_max
      value: 39.48382767312788
    - type: nauc_ndcg_at_1000_std
      value: 3.414789408343409
    - type: nauc_ndcg_at_100_diff1
      value: 38.29675861135578
    - type: nauc_ndcg_at_100_max
      value: 38.2674786507297
    - type: nauc_ndcg_at_100_std
      value: 2.7094055381218207
    - type: nauc_ndcg_at_10_diff1
      value: 38.09514955708717
    - type: nauc_ndcg_at_10_max
      value: 36.664923238906525
    - type: nauc_ndcg_at_10_std
      value: 0.6901410544967921
    - type: nauc_ndcg_at_1_diff1
      value: 49.441510997817346
    - type: nauc_ndcg_at_1_max
      value: 43.08547383044357
    - type: nauc_ndcg_at_1_std
      value: -1.8747770703324347
    - type: nauc_ndcg_at_20_diff1
      value: 38.44967736231759
    - type: nauc_ndcg_at_20_max
      value: 36.871179313622584
    - type: nauc_ndcg_at_20_std
      value: 1.157560360065234
    - type: nauc_ndcg_at_3_diff1
      value: 39.02419271805571
    - type: nauc_ndcg_at_3_max
      value: 37.447669442586324
    - type: nauc_ndcg_at_3_std
      value: 0.41502589779297794
    - type: nauc_ndcg_at_5_diff1
      value: 38.10233452742001
    - type: nauc_ndcg_at_5_max
      value: 35.816381905465676
    - type: nauc_ndcg_at_5_std
      value: -0.3704499913387088
    - type: nauc_precision_at_1000_diff1
      value: 2.451267097838658
    - type: nauc_precision_at_1000_max
      value: 29.116394969085306
    - type: nauc_precision_at_1000_std
      value: 14.85900786538363
    - type: nauc_precision_at_100_diff1
      value: 8.10919082251277
    - type: nauc_precision_at_100_max
      value: 36.28388256191417
    - type: nauc_precision_at_100_std
      value: 14.830039904317657
    - type: nauc_precision_at_10_diff1
      value: 15.02446609920477
    - type: nauc_precision_at_10_max
      value: 41.008463775454054
    - type: nauc_precision_at_10_std
      value: 10.431403152334486
    - type: nauc_precision_at_1_diff1
      value: 49.441510997817346
    - type: nauc_precision_at_1_max
      value: 43.08547383044357
    - type: nauc_precision_at_1_std
      value: -1.8747770703324347
    - type: nauc_precision_at_20_diff1
      value: 14.222022201169926
    - type: nauc_precision_at_20_max
      value: 40.10189643835305
    - type: nauc_precision_at_20_std
      value: 12.204443815975527
    - type: nauc_precision_at_3_diff1
      value: 25.41905395341234
    - type: nauc_precision_at_3_max
      value: 41.56133905339819
    - type: nauc_precision_at_3_std
      value: 5.575516915590082
    - type: nauc_precision_at_5_diff1
      value: 20.20081221089351
    - type: nauc_precision_at_5_max
      value: 40.95218555916681
    - type: nauc_precision_at_5_std
      value: 7.2040745500708745
    - type: nauc_recall_at_1000_diff1
      value: 28.021198234033395
    - type: nauc_recall_at_1000_max
      value: 36.165148684597504
    - type: nauc_recall_at_1000_std
      value: 28.28852356008973
    - type: nauc_recall_at_100_diff1
      value: 21.882447802741897
    - type: nauc_recall_at_100_max
      value: 26.979684607567222
    - type: nauc_recall_at_100_std
      value: 9.783658817010082
    - type: nauc_recall_at_10_diff1
      value: 28.493097951178818
    - type: nauc_recall_at_10_max
      value: 29.40937476550134
    - type: nauc_recall_at_10_std
      value: 2.7593763576979353
    - type: nauc_recall_at_1_diff1
      value: 46.288767289528344
    - type: nauc_recall_at_1_max
      value: 25.67706785013364
    - type: nauc_recall_at_1_std
      value: -6.989769609924645
    - type: nauc_recall_at_20_diff1
      value: 27.638381299425234
    - type: nauc_recall_at_20_max
      value: 27.942035836106328
    - type: nauc_recall_at_20_std
      value: 3.489835161380808
    - type: nauc_recall_at_3_diff1
      value: 33.90054781392646
    - type: nauc_recall_at_3_max
      value: 27.778812533030322
    - type: nauc_recall_at_3_std
      value: -0.03054068020022706
    - type: nauc_recall_at_5_diff1
      value: 30.279060732221346
    - type: nauc_recall_at_5_max
      value: 27.49854749597931
    - type: nauc_recall_at_5_std
      value: 0.5434664581939099
    - type: ndcg_at_1
      value: 37.809
    - type: ndcg_at_10
      value: 38.828
    - type: ndcg_at_100
      value: 45.218
    - type: ndcg_at_1000
      value: 48.510999999999996
    - type: ndcg_at_20
      value: 41.11
    - type: ndcg_at_3
      value: 34.466
    - type: ndcg_at_5
      value: 35.843
    - type: precision_at_1
      value: 37.809
    - type: precision_at_10
      value: 11.157
    - type: precision_at_100
      value: 1.762
    - type: precision_at_1000
      value: 0.233
    - type: precision_at_20
      value: 6.497
    - type: precision_at_3
      value: 23.044999999999998
    - type: precision_at_5
      value: 17.284
    - type: recall_at_1
      value: 19.126
    - type: recall_at_10
      value: 46.062
    - type: recall_at_100
      value: 70.22800000000001
    - type: recall_at_1000
      value: 89.803
    - type: recall_at_20
      value: 53.217999999999996
    - type: recall_at_3
      value: 30.847
    - type: recall_at_5
      value: 37.11
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB HotpotQA-PL (default)
      revision: a0bd479ac97b4ccb5bd6ce320c415d0bb4beb907
      split: test
      type: clarin-knext/hotpotqa-pl
    metrics:
    - type: main_score
      value: 60.27
    - type: map_at_1
      value: 35.199000000000005
    - type: map_at_10
      value: 51.369
    - type: map_at_100
      value: 52.212
    - type: map_at_1000
      value: 52.28
    - type: map_at_20
      value: 51.864
    - type: map_at_3
      value: 48.446
    - type: map_at_5
      value: 50.302
    - type: mrr_at_1
      value: 70.39837947332883
    - type: mrr_at_10
      value: 76.8346141067273
    - type: mrr_at_100
      value: 77.10724392048137
    - type: mrr_at_1000
      value: 77.12037412892865
    - type: mrr_at_20
      value: 77.01061532947222
    - type: mrr_at_3
      value: 75.5908170155299
    - type: mrr_at_5
      value: 76.39095205941899
    - type: nauc_map_at_1000_diff1
      value: 24.701387884989117
    - type: nauc_map_at_1000_max
      value: 23.25553235642178
    - type: nauc_map_at_1000_std
      value: 7.1803506915661774
    - type: nauc_map_at_100_diff1
      value: 24.674498622483103
    - type: nauc_map_at_100_max
      value: 23.234948525052175
    - type: nauc_map_at_100_std
      value: 7.168677997105447
    - type: nauc_map_at_10_diff1
      value: 24.676025039755626
    - type: nauc_map_at_10_max
      value: 23.171971872726964
    - type: nauc_map_at_10_std
      value: 6.485610909852058
    - type: nauc_map_at_1_diff1
      value: 68.90178464319715
    - type: nauc_map_at_1_max
      value: 46.05537868917558
    - type: nauc_map_at_1_std
      value: 1.7658552480698708
    - type: nauc_map_at_20_diff1
      value: 24.69297151842494
    - type: nauc_map_at_20_max
      value: 23.213064691673637
    - type: nauc_map_at_20_std
      value: 6.9357946556849
    - type: nauc_map_at_3_diff1
      value: 26.279128947950507
    - type: nauc_map_at_3_max
      value: 23.929537354117922
    - type: nauc_map_at_3_std
      value: 4.625061565714759
    - type: nauc_map_at_5_diff1
      value: 25.04448959482816
    - type: nauc_map_at_5_max
      value: 23.432012857899338
    - type: nauc_map_at_5_std
      value: 5.845744681998008
    - type: nauc_mrr_at_1000_diff1
      value: 66.7503918108276
    - type: nauc_mrr_at_1000_max
      value: 48.42897342336844
    - type: nauc_mrr_at_1000_std
      value: 5.3097517971144415
    - type: nauc_mrr_at_100_diff1
      value: 66.74645215862695
    - type: nauc_mrr_at_100_max
      value: 48.4368663009989
    - type: nauc_mrr_at_100_std
      value: 5.322297898555188
    - type: nauc_mrr_at_10_diff1
      value: 66.69310166180729
    - type: nauc_mrr_at_10_max
      value: 48.475437698330225
    - type: nauc_mrr_at_10_std
      value: 5.258183461631702
    - type: nauc_mrr_at_1_diff1
      value: 68.90178464319715
    - type: nauc_mrr_at_1_max
      value: 46.05537868917558
    - type: nauc_mrr_at_1_std
      value: 1.7658552480698708
    - type: nauc_mrr_at_20_diff1
      value: 66.72000262431975
    - type: nauc_mrr_at_20_max
      value: 48.45593642981319
    - type: nauc_mrr_at_20_std
      value: 5.353665929072101
    - type: nauc_mrr_at_3_diff1
      value: 66.84936676396276
    - type: nauc_mrr_at_3_max
      value: 48.466611276778295
    - type: nauc_mrr_at_3_std
      value: 4.485810398557475
    - type: nauc_mrr_at_5_diff1
      value: 66.62362565394174
    - type: nauc_mrr_at_5_max
      value: 48.456431835482014
    - type: nauc_mrr_at_5_std
      value: 5.08482458391903
    - type: nauc_ndcg_at_1000_diff1
      value: 29.984825173719443
    - type: nauc_ndcg_at_1000_max
      value: 27.289179238639893
    - type: nauc_ndcg_at_1000_std
      value: 10.661480455527526
    - type: nauc_ndcg_at_100_diff1
      value: 29.322074257047877
    - type: nauc_ndcg_at_100_max
      value: 26.850650276220605
    - type: nauc_ndcg_at_100_std
      value: 10.599247982501902
    - type: nauc_ndcg_at_10_diff1
      value: 29.659909113886094
    - type: nauc_ndcg_at_10_max
      value: 26.836139599331005
    - type: nauc_ndcg_at_10_std
      value: 8.12844399452719
    - type: nauc_ndcg_at_1_diff1
      value: 68.90178464319715
    - type: nauc_ndcg_at_1_max
      value: 46.05537868917558
    - type: nauc_ndcg_at_1_std
      value: 1.7658552480698708
    - type: nauc_ndcg_at_20_diff1
      value: 29.510802214854294
    - type: nauc_ndcg_at_20_max
      value: 26.775562637730722
    - type: nauc_ndcg_at_20_std
      value: 9.341342661702363
    - type: nauc_ndcg_at_3_diff1
      value: 32.741885846292966
    - type: nauc_ndcg_at_3_max
      value: 28.44225108761343
    - type: nauc_ndcg_at_3_std
      value: 5.204440768465042
    - type: nauc_ndcg_at_5_diff1
      value: 30.57856348635919
    - type: nauc_ndcg_at_5_max
      value: 27.475007474301698
    - type: nauc_ndcg_at_5_std
      value: 6.961546044312487
    - type: nauc_precision_at_1000_diff1
      value: 0.002113156309413332
    - type: nauc_precision_at_1000_max
      value: 11.198242419541286
    - type: nauc_precision_at_1000_std
      value: 28.69676419166541
    - type: nauc_precision_at_100_diff1
      value: 3.6049575557782627
    - type: nauc_precision_at_100_max
      value: 12.499173524574791
    - type: nauc_precision_at_100_std
      value: 23.3755281004721
    - type: nauc_precision_at_10_diff1
      value: 10.922574784853193
    - type: nauc_precision_at_10_max
      value: 16.23221529562036
    - type: nauc_precision_at_10_std
      value: 12.45014808813857
    - type: nauc_precision_at_1_diff1
      value: 68.90178464319715
    - type: nauc_precision_at_1_max
      value: 46.05537868917558
    - type: nauc_precision_at_1_std
      value: 1.7658552480698708
    - type: nauc_precision_at_20_diff1
      value: 8.840710781302827
    - type: nauc_precision_at_20_max
      value: 14.804644554205524
    - type: nauc_precision_at_20_std
      value: 16.245009770815237
    - type: nauc_precision_at_3_diff1
      value: 19.447291487137573
    - type: nauc_precision_at_3_max
      value: 21.47123471597057
    - type: nauc_precision_at_3_std
      value: 6.441862800128802
    - type: nauc_precision_at_5_diff1
      value: 14.078545719721108
    - type: nauc_precision_at_5_max
      value: 18.468288046016387
    - type: nauc_precision_at_5_std
      value: 9.58650641691393
    - type: nauc_recall_at_1000_diff1
      value: 0.0021131563095336584
    - type: nauc_recall_at_1000_max
      value: 11.198242419541558
    - type: nauc_recall_at_1000_std
      value: 28.6967641916655
    - type: nauc_recall_at_100_diff1
      value: 3.6049575557781393
    - type: nauc_recall_at_100_max
      value: 12.499173524574765
    - type: nauc_recall_at_100_std
      value: 23.375528100472074
    - type: nauc_recall_at_10_diff1
      value: 10.922574784853168
    - type: nauc_recall_at_10_max
      value: 16.2322152956203
    - type: nauc_recall_at_10_std
      value: 12.450148088138535
    - type: nauc_recall_at_1_diff1
      value: 68.90178464319715
    - type: nauc_recall_at_1_max
      value: 46.05537868917558
    - type: nauc_recall_at_1_std
      value: 1.7658552480698708
    - type: nauc_recall_at_20_diff1
      value: 8.840710781302905
    - type: nauc_recall_at_20_max
      value: 14.804644554205515
    - type: nauc_recall_at_20_std
      value: 16.245009770815273
    - type: nauc_recall_at_3_diff1
      value: 19.447291487137498
    - type: nauc_recall_at_3_max
      value: 21.47123471597054
    - type: nauc_recall_at_3_std
      value: 6.441862800128763
    - type: nauc_recall_at_5_diff1
      value: 14.07854571972115
    - type: nauc_recall_at_5_max
      value: 18.468288046016337
    - type: nauc_recall_at_5_std
      value: 9.586506416913904
    - type: ndcg_at_1
      value: 70.39800000000001
    - type: ndcg_at_10
      value: 60.27
    - type: ndcg_at_100
      value: 63.400999999999996
    - type: ndcg_at_1000
      value: 64.847
    - type: ndcg_at_20
      value: 61.571
    - type: ndcg_at_3
      value: 55.875
    - type: ndcg_at_5
      value: 58.36599999999999
    - type: precision_at_1
      value: 70.39800000000001
    - type: precision_at_10
      value: 12.46
    - type: precision_at_100
      value: 1.493
    - type: precision_at_1000
      value: 0.169
    - type: precision_at_20
      value: 6.65
    - type: precision_at_3
      value: 35.062
    - type: precision_at_5
      value: 23.009
    - type: recall_at_1
      value: 35.199000000000005
    - type: recall_at_10
      value: 62.302
    - type: recall_at_100
      value: 74.666
    - type: recall_at_1000
      value: 84.355
    - type: recall_at_20
      value: 66.496
    - type: recall_at_3
      value: 52.593
    - type: recall_at_5
      value: 57.522
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB MSMARCO-PL (default)
      revision: 8634c07806d5cce3a6138e260e59b81760a0a640
      split: test
      type: clarin-knext/msmarco-pl
    metrics:
    - type: main_score
      value: 64.886
    - type: map_at_1
      value: 1.644
    - type: map_at_10
      value: 12.24
    - type: map_at_100
      value: 28.248
    - type: map_at_1000
      value: 33.506
    - type: map_at_20
      value: 17.497
    - type: map_at_3
      value: 4.9399999999999995
    - type: map_at_5
      value: 8.272
    - type: mrr_at_1
      value: 83.72093023255815
    - type: mrr_at_10
      value: 91.08527131782945
    - type: mrr_at_100
      value: 91.08527131782945
    - type: mrr_at_1000
      value: 91.08527131782945
    - type: mrr_at_20
      value: 91.08527131782945
    - type: mrr_at_3
      value: 91.08527131782945
    - type: mrr_at_5
      value: 91.08527131782945
    - type: nauc_map_at_1000_diff1
      value: -36.428271627303424
    - type: nauc_map_at_1000_max
      value: 44.87615127218638
    - type: nauc_map_at_1000_std
      value: 67.92696808824724
    - type: nauc_map_at_100_diff1
      value: -28.11674206786188
    - type: nauc_map_at_100_max
      value: 36.422779766334955
    - type: nauc_map_at_100_std
      value: 49.99876313755116
    - type: nauc_map_at_10_diff1
      value: -5.838593619806058
    - type: nauc_map_at_10_max
      value: 11.026519190509742
    - type: nauc_map_at_10_std
      value: 2.5268752263522045
    - type: nauc_map_at_1_diff1
      value: 17.897907271073016
    - type: nauc_map_at_1_max
      value: 12.229062762540844
    - type: nauc_map_at_1_std
      value: -4.088830895573149
    - type: nauc_map_at_20_diff1
      value: -13.871097716255626
    - type: nauc_map_at_20_max
      value: 19.291271635609533
    - type: nauc_map_at_20_std
      value: 16.745335606507826
    - type: nauc_map_at_3_diff1
      value: 4.425238457033843
    - type: nauc_map_at_3_max
      value: 4.611864744680824
    - type: nauc_map_at_3_std
      value: -8.986916608582863
    - type: nauc_map_at_5_diff1
      value: -6.254849256920095
    - type: nauc_map_at_5_max
      value: 2.729437079919823
    - type: nauc_map_at_5_std
      value: -7.235906279913092
    - type: nauc_mrr_at_1000_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_1000_max
      value: 68.26259125411818
    - type: nauc_mrr_at_1000_std
      value: 56.345086428353575
    - type: nauc_mrr_at_100_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_100_max
      value: 68.26259125411818
    - type: nauc_mrr_at_100_std
      value: 56.345086428353575
    - type: nauc_mrr_at_10_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_10_max
      value: 68.26259125411818
    - type: nauc_mrr_at_10_std
      value: 56.345086428353575
    - type: nauc_mrr_at_1_diff1
      value: 56.55126663944154
    - type: nauc_mrr_at_1_max
      value: 66.37014285522565
    - type: nauc_mrr_at_1_std
      value: 53.2508271389779
    - type: nauc_mrr_at_20_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_20_max
      value: 68.26259125411818
    - type: nauc_mrr_at_20_std
      value: 56.345086428353575
    - type: nauc_mrr_at_3_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_3_max
      value: 68.26259125411818
    - type: nauc_mrr_at_3_std
      value: 56.345086428353575
    - type: nauc_mrr_at_5_diff1
      value: 52.18669104947672
    - type: nauc_mrr_at_5_max
      value: 68.26259125411818
    - type: nauc_mrr_at_5_std
      value: 56.345086428353575
    - type: nauc_ndcg_at_1000_diff1
      value: -19.06422926483731
    - type: nauc_ndcg_at_1000_max
      value: 56.30853514590265
    - type: nauc_ndcg_at_1000_std
      value: 70.30810947505557
    - type: nauc_ndcg_at_100_diff1
      value: -25.72587586459692
    - type: nauc_ndcg_at_100_max
      value: 51.433781241604194
    - type: nauc_ndcg_at_100_std
      value: 68.37678512652792
    - type: nauc_ndcg_at_10_diff1
      value: -23.21198108212602
    - type: nauc_ndcg_at_10_max
      value: 43.5450720846516
    - type: nauc_ndcg_at_10_std
      value: 48.78307907005605
    - type: nauc_ndcg_at_1_diff1
      value: 44.00179301267447
    - type: nauc_ndcg_at_1_max
      value: 48.202370455680395
    - type: nauc_ndcg_at_1_std
      value: 25.69655992704088
    - type: nauc_ndcg_at_20_diff1
      value: -33.88168753446507
    - type: nauc_ndcg_at_20_max
      value: 45.16199742613164
    - type: nauc_ndcg_at_20_std
      value: 61.87098383164902
    - type: nauc_ndcg_at_3_diff1
      value: 11.19174449544048
    - type: nauc_ndcg_at_3_max
      value: 44.34069860560555
    - type: nauc_ndcg_at_3_std
      value: 27.451258369798115
    - type: nauc_ndcg_at_5_diff1
      value: -7.186520929432436
    - type: nauc_ndcg_at_5_max
      value: 43.41869981139378
    - type: nauc_ndcg_at_5_std
      value: 34.89898115995178
    - type: nauc_precision_at_1000_diff1
      value: -34.43998154563451
    - type: nauc_precision_at_1000_max
      value: 29.172655907480372
    - type: nauc_precision_at_1000_std
      value: 65.15824469614837
    - type: nauc_precision_at_100_diff1
      value: -37.82409643259692
    - type: nauc_precision_at_100_max
      value: 38.24986991317909
    - type: nauc_precision_at_100_std
      value: 72.74768183105327
    - type: nauc_precision_at_10_diff1
      value: -32.21556182780535
    - type: nauc_precision_at_10_max
      value: 34.27170432382651
    - type: nauc_precision_at_10_std
      value: 58.358255004394664
    - type: nauc_precision_at_1_diff1
      value: 56.55126663944154
    - type: nauc_precision_at_1_max
      value: 66.37014285522565
    - type: nauc_precision_at_1_std
      value: 53.2508271389779
    - type: nauc_precision_at_20_diff1
      value: -40.18751579026395
    - type: nauc_precision_at_20_max
      value: 33.960783153758896
    - type: nauc_precision_at_20_std
      value: 65.42918390184195
    - type: nauc_precision_at_3_diff1
      value: -7.073870209006578
    - type: nauc_precision_at_3_max
      value: 50.81535269862325
    - type: nauc_precision_at_3_std
      value: 59.248681565955685
    - type: nauc_precision_at_5_diff1
      value: -31.136580596983876
    - type: nauc_precision_at_5_max
      value: 45.88147792380426
    - type: nauc_precision_at_5_std
      value: 67.46814230928243
    - type: nauc_recall_at_1000_diff1
      value: -23.15699999594577
    - type: nauc_recall_at_1000_max
      value: 39.77277799761876
    - type: nauc_recall_at_1000_std
      value: 60.326168012901114
    - type: nauc_recall_at_100_diff1
      value: -21.636664823598498
    - type: nauc_recall_at_100_max
      value: 31.104969346131583
    - type: nauc_recall_at_100_std
      value: 38.811686891592096
    - type: nauc_recall_at_10_diff1
      value: -10.542765625053569
    - type: nauc_recall_at_10_max
      value: 2.043876058107446
    - type: nauc_recall_at_10_std
      value: -5.578449908984766
    - type: nauc_recall_at_1_diff1
      value: 17.897907271073016
    - type: nauc_recall_at_1_max
      value: 12.229062762540844
    - type: nauc_recall_at_1_std
      value: -4.088830895573149
    - type: nauc_recall_at_20_diff1
      value: -15.132909355710103
    - type: nauc_recall_at_20_max
      value: 12.659765287241065
    - type: nauc_recall_at_20_std
      value: 8.277887800815819
    - type: nauc_recall_at_3_diff1
      value: -3.1975017812715016
    - type: nauc_recall_at_3_max
      value: -3.5539857085038538
    - type: nauc_recall_at_3_std
      value: -14.712102851318118
    - type: nauc_recall_at_5_diff1
      value: -14.040507717380743
    - type: nauc_recall_at_5_max
      value: -6.126912150131701
    - type: nauc_recall_at_5_std
      value: -13.821624015640355
    - type: ndcg_at_1
      value: 71.318
    - type: ndcg_at_10
      value: 64.886
    - type: ndcg_at_100
      value: 53.187
    - type: ndcg_at_1000
      value: 59.897999999999996
    - type: ndcg_at_20
      value: 58.96
    - type: ndcg_at_3
      value: 69.736
    - type: ndcg_at_5
      value: 70.14099999999999
    - type: precision_at_1
      value: 83.721
    - type: precision_at_10
      value: 71.163
    - type: precision_at_100
      value: 29.465000000000003
    - type: precision_at_1000
      value: 5.665
    - type: precision_at_20
      value: 57.791000000000004
    - type: precision_at_3
      value: 82.171
    - type: precision_at_5
      value: 81.86
    - type: recall_at_1
      value: 1.644
    - type: recall_at_10
      value: 14.238000000000001
    - type: recall_at_100
      value: 39.831
    - type: recall_at_1000
      value: 64.057
    - type: recall_at_20
      value: 21.021
    - type: recall_at_3
      value: 5.53
    - type: recall_at_5
      value: 9.623
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB NFCorpus-PL (default)
      revision: 9a6f9567fda928260afed2de480d79c98bf0bec0
      split: test
      type: clarin-knext/nfcorpus-pl
    metrics:
    - type: main_score
      value: 31.391000000000002
    - type: map_at_1
      value: 4.163
    - type: map_at_10
      value: 10.744
    - type: map_at_100
      value: 14.038999999999998
    - type: map_at_1000
      value: 15.434999999999999
    - type: map_at_20
      value: 12.16
    - type: map_at_3
      value: 7.614999999999999
    - type: map_at_5
      value: 9.027000000000001
    - type: mrr_at_1
      value: 39.0092879256966
    - type: mrr_at_10
      value: 48.69809327239668
    - type: mrr_at_100
      value: 49.20788148442068
    - type: mrr_at_1000
      value: 49.25509336494706
    - type: mrr_at_20
      value: 48.99606551850896
    - type: mrr_at_3
      value: 46.284829721362236
    - type: mrr_at_5
      value: 47.77089783281735
    - type: nauc_map_at_1000_diff1
      value: 22.75421477116417
    - type: nauc_map_at_1000_max
      value: 49.242283787799046
    - type: nauc_map_at_1000_std
      value: 29.056888272331832
    - type: nauc_map_at_100_diff1
      value: 23.585977398585594
    - type: nauc_map_at_100_max
      value: 48.25845199409498
    - type: nauc_map_at_100_std
      value: 24.944264511223693
    - type: nauc_map_at_10_diff1
      value: 27.386613094780255
    - type: nauc_map_at_10_max
      value: 41.52415346691586
    - type: nauc_map_at_10_std
      value: 12.93872448563755
    - type: nauc_map_at_1_diff1
      value: 46.78688143865053
    - type: nauc_map_at_1_max
      value: 37.20408843995871
    - type: nauc_map_at_1_std
      value: 4.383444959401098
    - type: nauc_map_at_20_diff1
      value: 25.590969047740288
    - type: nauc_map_at_20_max
      value: 44.57109307999418
    - type: nauc_map_at_20_std
      value: 16.45855141821407
    - type: nauc_map_at_3_diff1
      value: 36.30017108362863
    - type: nauc_map_at_3_max
      value: 34.66149613991648
    - type: nauc_map_at_3_std
      value: 5.67985905078467
    - type: nauc_map_at_5_diff1
      value: 31.157644795417223
    - type: nauc_map_at_5_max
      value: 37.274738661636825
    - type: nauc_map_at_5_std
      value: 8.70088872394168
    - type: nauc_mrr_at_1000_diff1
      value: 25.638564218157384
    - type: nauc_mrr_at_1000_max
      value: 57.77788270285353
    - type: nauc_mrr_at_1000_std
      value: 43.507586592911274
    - type: nauc_mrr_at_100_diff1
      value: 25.662002580561584
    - type: nauc_mrr_at_100_max
      value: 57.80578394278584
    - type: nauc_mrr_at_100_std
      value: 43.543905743986635
    - type: nauc_mrr_at_10_diff1
      value: 25.426034796339835
    - type: nauc_mrr_at_10_max
      value: 57.68443186258669
    - type: nauc_mrr_at_10_std
      value: 43.438009108331215
    - type: nauc_mrr_at_1_diff1
      value: 26.073028156311075
    - type: nauc_mrr_at_1_max
      value: 52.11817916720053
    - type: nauc_mrr_at_1_std
      value: 37.41073893153695
    - type: nauc_mrr_at_20_diff1
      value: 25.548645553336147
    - type: nauc_mrr_at_20_max
      value: 57.78552760401915
    - type: nauc_mrr_at_20_std
      value: 43.521687428822325
    - type: nauc_mrr_at_3_diff1
      value: 25.72662577397805
    - type: nauc_mrr_at_3_max
      value: 56.891263536265605
    - type: nauc_mrr_at_3_std
      value: 41.384872305390104
    - type: nauc_mrr_at_5_diff1
      value: 25.552211551655386
    - type: nauc_mrr_at_5_max
      value: 57.976813828353926
    - type: nauc_mrr_at_5_std
      value: 43.504564461855544
    - type: nauc_ndcg_at_1000_diff1
      value: 23.456158044182757
    - type: nauc_ndcg_at_1000_max
      value: 60.05411773552709
    - type: nauc_ndcg_at_1000_std
      value: 47.857510017262584
    - type: nauc_ndcg_at_100_diff1
      value: 19.711635700390772
    - type: nauc_ndcg_at_100_max
      value: 56.178746740470665
    - type: nauc_ndcg_at_100_std
      value: 42.36829180286942
    - type: nauc_ndcg_at_10_diff1
      value: 18.364428967788413
    - type: nauc_ndcg_at_10_max
      value: 54.38372506578223
    - type: nauc_ndcg_at_10_std
      value: 41.75765411340369
    - type: nauc_ndcg_at_1_diff1
      value: 26.571093272640773
    - type: nauc_ndcg_at_1_max
      value: 51.061788341958284
    - type: nauc_ndcg_at_1_std
      value: 36.514987974075986
    - type: nauc_ndcg_at_20_diff1
      value: 18.345487193027697
    - type: nauc_ndcg_at_20_max
      value: 54.62621882656994
    - type: nauc_ndcg_at_20_std
      value: 41.42835554714241
    - type: nauc_ndcg_at_3_diff1
      value: 23.260105658139025
    - type: nauc_ndcg_at_3_max
      value: 52.07747385334546
    - type: nauc_ndcg_at_3_std
      value: 36.91985577837284
    - type: nauc_ndcg_at_5_diff1
      value: 20.40428109665566
    - type: nauc_ndcg_at_5_max
      value: 53.52015347884604
    - type: nauc_ndcg_at_5_std
      value: 39.46008849580017
    - type: nauc_precision_at_1000_diff1
      value: -7.3487344916380035
    - type: nauc_precision_at_1000_max
      value: 16.58045221394852
    - type: nauc_precision_at_1000_std
      value: 38.94030932397075
    - type: nauc_precision_at_100_diff1
      value: -5.257743986683922
    - type: nauc_precision_at_100_max
      value: 34.43071687475306
    - type: nauc_precision_at_100_std
      value: 53.499519170670474
    - type: nauc_precision_at_10_diff1
      value: 2.385136433119139
    - type: nauc_precision_at_10_max
      value: 47.210743878631064
    - type: nauc_precision_at_10_std
      value: 47.22767704186548
    - type: nauc_precision_at_1_diff1
      value: 26.073028156311075
    - type: nauc_precision_at_1_max
      value: 52.11817916720053
    - type: nauc_precision_at_1_std
      value: 37.41073893153695
    - type: nauc_precision_at_20_diff1
      value: -0.3531531127238474
    - type: nauc_precision_at_20_max
      value: 44.78044604856974
    - type: nauc_precision_at_20_std
      value: 49.532804150743615
    - type: nauc_precision_at_3_diff1
      value: 15.350050569991447
    - type: nauc_precision_at_3_max
      value: 51.01572315596549
    - type: nauc_precision_at_3_std
      value: 38.801125728413155
    - type: nauc_precision_at_5_diff1
      value: 9.109003666144694
    - type: nauc_precision_at_5_max
      value: 50.935269774898494
    - type: nauc_precision_at_5_std
      value: 43.323548180559676
    - type: nauc_recall_at_1000_diff1
      value: 16.64743647648886
    - type: nauc_recall_at_1000_max
      value: 38.46012283772285
    - type: nauc_recall_at_1000_std
      value: 36.02016164796441
    - type: nauc_recall_at_100_diff1
      value: 14.005834785186744
    - type: nauc_recall_at_100_max
      value: 37.70026105513647
    - type: nauc_recall_at_100_std
      value: 27.085222642129697
    - type: nauc_recall_at_10_diff1
      value: 21.204106627422632
    - type: nauc_recall_at_10_max
      value: 36.737624881893424
    - type: nauc_recall_at_10_std
      value: 13.755054514272702
    - type: nauc_recall_at_1_diff1
      value: 46.78688143865053
    - type: nauc_recall_at_1_max
      value: 37.20408843995871
    - type: nauc_recall_at_1_std
      value: 4.383444959401098
    - type: nauc_recall_at_20_diff1
      value: 19.740977611421933
    - type: nauc_recall_at_20_max
      value: 39.21908969539783
    - type: nauc_recall_at_20_std
      value: 16.560269670318494
    - type: nauc_recall_at_3_diff1
      value: 32.189359545367815
    - type: nauc_recall_at_3_max
      value: 31.693634445562758
    - type: nauc_recall_at_3_std
      value: 6.246326281543587
    - type: nauc_recall_at_5_diff1
      value: 25.51586860499901
    - type: nauc_recall_at_5_max
      value: 33.15934725342885
    - type: nauc_recall_at_5_std
      value: 9.677778511696705
    - type: ndcg_at_1
      value: 37.307
    - type: ndcg_at_10
      value: 31.391000000000002
    - type: ndcg_at_100
      value: 28.877999999999997
    - type: ndcg_at_1000
      value: 37.16
    - type: ndcg_at_20
      value: 29.314
    - type: ndcg_at_3
      value: 35.405
    - type: ndcg_at_5
      value: 33.922999999999995
    - type: precision_at_1
      value: 39.009
    - type: precision_at_10
      value: 24.52
    - type: precision_at_100
      value: 7.703
    - type: precision_at_1000
      value: 2.04
    - type: precision_at_20
      value: 18.08
    - type: precision_at_3
      value: 34.469
    - type: precision_at_5
      value: 30.712
    - type: recall_at_1
      value: 4.163
    - type: recall_at_10
      value: 15.015999999999998
    - type: recall_at_100
      value: 30.606
    - type: recall_at_1000
      value: 59.606
    - type: recall_at_20
      value: 19.09
    - type: recall_at_3
      value: 9.139
    - type: recall_at_5
      value: 11.477
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB NQ-PL (default)
      revision: f171245712cf85dd4700b06bef18001578d0ca8d
      split: test
      type: clarin-knext/nq-pl
    metrics:
    - type: main_score
      value: 54.017
    - type: map_at_1
      value: 34.193
    - type: map_at_10
      value: 47.497
    - type: map_at_100
      value: 48.441
    - type: map_at_1000
      value: 48.481
    - type: map_at_20
      value: 48.093
    - type: map_at_3
      value: 44.017
    - type: map_at_5
      value: 46.111000000000004
    - type: mrr_at_1
      value: 37.949015063731174
    - type: mrr_at_10
      value: 49.915772315105954
    - type: mrr_at_100
      value: 50.62841255829997
    - type: mrr_at_1000
      value: 50.656773027666745
    - type: mrr_at_20
      value: 50.37785276657083
    - type: mrr_at_3
      value: 46.98725376593267
    - type: mrr_at_5
      value: 48.763035921205066
    - type: nauc_map_at_1000_diff1
      value: 39.5632191792873
    - type: nauc_map_at_1000_max
      value: 37.4728247053629
    - type: nauc_map_at_1000_std
      value: 5.742498414663762
    - type: nauc_map_at_100_diff1
      value: 39.555570352061906
    - type: nauc_map_at_100_max
      value: 37.497880976847334
    - type: nauc_map_at_100_std
      value: 5.7798021019465375
    - type: nauc_map_at_10_diff1
      value: 39.5423723444454
    - type: nauc_map_at_10_max
      value: 37.41661971723365
    - type: nauc_map_at_10_std
      value: 5.2378002164144695
    - type: nauc_map_at_1_diff1
      value: 41.52697034146981
    - type: nauc_map_at_1_max
      value: 28.558995576942863
    - type: nauc_map_at_1_std
      value: 0.13094542859192052
    - type: nauc_map_at_20_diff1
      value: 39.55484628943701
    - type: nauc_map_at_20_max
      value: 37.5247794933719
    - type: nauc_map_at_20_std
      value: 5.702881342279231
    - type: nauc_map_at_3_diff1
      value: 39.949323925425325
    - type: nauc_map_at_3_max
      value: 35.770298168901924
    - type: nauc_map_at_3_std
      value: 2.9127112432479874
    - type: nauc_map_at_5_diff1
      value: 39.768310617004545
    - type: nauc_map_at_5_max
      value: 37.1549191664796
    - type: nauc_map_at_5_std
      value: 4.4681285748269515
    - type: nauc_mrr_at_1000_diff1
      value: 39.14001746706457
    - type: nauc_mrr_at_1000_max
      value: 37.477376518267775
    - type: nauc_mrr_at_1000_std
      value: 6.8088891531621565
    - type: nauc_mrr_at_100_diff1
      value: 39.13054707413684
    - type: nauc_mrr_at_100_max
      value: 37.498126443766274
    - type: nauc_mrr_at_100_std
      value: 6.839411380129971
    - type: nauc_mrr_at_10_diff1
      value: 39.09764730048156
    - type: nauc_mrr_at_10_max
      value: 37.58593798217306
    - type: nauc_mrr_at_10_std
      value: 6.713795164982413
    - type: nauc_mrr_at_1_diff1
      value: 41.581599918664075
    - type: nauc_mrr_at_1_max
      value: 31.500589231378722
    - type: nauc_mrr_at_1_std
      value: 2.059116370339438
    - type: nauc_mrr_at_20_diff1
      value: 39.09011023988447
    - type: nauc_mrr_at_20_max
      value: 37.55856008791344
    - type: nauc_mrr_at_20_std
      value: 6.847165397615844
    - type: nauc_mrr_at_3_diff1
      value: 39.382542043738
    - type: nauc_mrr_at_3_max
      value: 36.49265363659468
    - type: nauc_mrr_at_3_std
      value: 4.759157976438336
    - type: nauc_mrr_at_5_diff1
      value: 39.304826333759976
    - type: nauc_mrr_at_5_max
      value: 37.46326016736024
    - type: nauc_mrr_at_5_std
      value: 6.122608305766621
    - type: nauc_ndcg_at_1000_diff1
      value: 38.568500038453266
    - type: nauc_ndcg_at_1000_max
      value: 39.799710882413166
    - type: nauc_ndcg_at_1000_std
      value: 9.357010223096639
    - type: nauc_ndcg_at_100_diff1
      value: 38.38026091343228
    - type: nauc_ndcg_at_100_max
      value: 40.48398173542486
    - type: nauc_ndcg_at_100_std
      value: 10.373054013302214
    - type: nauc_ndcg_at_10_diff1
      value: 38.27340980909964
    - type: nauc_ndcg_at_10_max
      value: 40.35241649744093
    - type: nauc_ndcg_at_10_std
      value: 8.579139930345168
    - type: nauc_ndcg_at_1_diff1
      value: 41.581599918664075
    - type: nauc_ndcg_at_1_max
      value: 31.500589231378722
    - type: nauc_ndcg_at_1_std
      value: 2.059116370339438
    - type: nauc_ndcg_at_20_diff1
      value: 38.26453028884807
    - type: nauc_ndcg_at_20_max
      value: 40.70517858426641
    - type: nauc_ndcg_at_20_std
      value: 9.987693876137905
    - type: nauc_ndcg_at_3_diff1
      value: 39.2078971733273
    - type: nauc_ndcg_at_3_max
      value: 37.48672195565316
    - type: nauc_ndcg_at_3_std
      value: 4.051464994659221
    - type: nauc_ndcg_at_5_diff1
      value: 38.883693595665285
    - type: nauc_ndcg_at_5_max
      value: 39.763115634437135
    - type: nauc_ndcg_at_5_std
      value: 6.738980451582073
    - type: nauc_precision_at_1000_diff1
      value: -7.223215910619012
    - type: nauc_precision_at_1000_max
      value: 13.075844604892161
    - type: nauc_precision_at_1000_std
      value: 19.864336920890107
    - type: nauc_precision_at_100_diff1
      value: 1.3305994810812418
    - type: nauc_precision_at_100_max
      value: 25.9219108557104
    - type: nauc_precision_at_100_std
      value: 27.5076605928207
    - type: nauc_precision_at_10_diff1
      value: 18.441551484970326
    - type: nauc_precision_at_10_max
      value: 39.85995330437054
    - type: nauc_precision_at_10_std
      value: 20.561269077428914
    - type: nauc_precision_at_1_diff1
      value: 41.581599918664075
    - type: nauc_precision_at_1_max
      value: 31.500589231378722
    - type: nauc_precision_at_1_std
      value: 2.059116370339438
    - type: nauc_precision_at_20_diff1
      value: 12.579593891480531
    - type: nauc_precision_at_20_max
      value: 36.620221830588775
    - type: nauc_precision_at_20_std
      value: 26.40364876775059
    - type: nauc_precision_at_3_diff1
      value: 30.158859294487073
    - type: nauc_precision_at_3_max
      value: 41.168215766389174
    - type: nauc_precision_at_3_std
      value: 9.44345004450809
    - type: nauc_precision_at_5_diff1
      value: 25.438624678672785
    - type: nauc_precision_at_5_max
      value: 42.72802023518524
    - type: nauc_precision_at_5_std
      value: 15.357657388511099
    - type: nauc_recall_at_1000_diff1
      value: 24.987564782718003
    - type: nauc_recall_at_1000_max
      value: 70.508416373353
    - type: nauc_recall_at_1000_std
      value: 69.75092280398808
    - type: nauc_recall_at_100_diff1
      value: 29.504202856421397
    - type: nauc_recall_at_100_max
      value: 63.41356585545318
    - type: nauc_recall_at_100_std
      value: 50.09250954437847
    - type: nauc_recall_at_10_diff1
      value: 32.355776022971774
    - type: nauc_recall_at_10_max
      value: 49.47121901667283
    - type: nauc_recall_at_10_std
      value: 19.418439406631244
    - type: nauc_recall_at_1_diff1
      value: 41.52697034146981
    - type: nauc_recall_at_1_max
      value: 28.558995576942863
    - type: nauc_recall_at_1_std
      value: 0.13094542859192052
    - type: nauc_recall_at_20_diff1
      value: 31.57334731023589
    - type: nauc_recall_at_20_max
      value: 54.06567225197383
    - type: nauc_recall_at_20_std
      value: 29.222029720570468
    - type: nauc_recall_at_3_diff1
      value: 36.45033533275773
    - type: nauc_recall_at_3_max
      value: 40.39529713780803
    - type: nauc_recall_at_3_std
      value: 5.21893897772794
    - type: nauc_recall_at_5_diff1
      value: 35.18471678478859
    - type: nauc_recall_at_5_max
      value: 46.20100816867823
    - type: nauc_recall_at_5_std
      value: 11.94481894633221
    - type: ndcg_at_1
      value: 37.949
    - type: ndcg_at_10
      value: 54.017
    - type: ndcg_at_100
      value: 58.126
    - type: ndcg_at_1000
      value: 59.073
    - type: ndcg_at_20
      value: 55.928
    - type: ndcg_at_3
      value: 47.494
    - type: ndcg_at_5
      value: 50.975
    - type: precision_at_1
      value: 37.949
    - type: precision_at_10
      value: 8.450000000000001
    - type: precision_at_100
      value: 1.083
    - type: precision_at_1000
      value: 0.117
    - type: precision_at_20
      value: 4.689
    - type: precision_at_3
      value: 21.051000000000002
    - type: precision_at_5
      value: 14.664
    - type: recall_at_1
      value: 34.193
    - type: recall_at_10
      value: 71.357
    - type: recall_at_100
      value: 89.434
    - type: recall_at_1000
      value: 96.536
    - type: recall_at_20
      value: 78.363
    - type: recall_at_3
      value: 54.551
    - type: recall_at_5
      value: 62.543000000000006
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB Quora-PL (default)
      revision: 0be27e93455051e531182b85e85e425aba12e9d4
      split: test
      type: clarin-knext/quora-pl
    metrics:
    - type: main_score
      value: 84.114
    - type: map_at_1
      value: 65.848
    - type: map_at_10
      value: 79.85900000000001
    - type: map_at_100
      value: 80.582
    - type: map_at_1000
      value: 80.60300000000001
    - type: map_at_20
      value: 80.321
    - type: map_at_3
      value: 76.741
    - type: map_at_5
      value: 78.72200000000001
    - type: mrr_at_1
      value: 75.97
    - type: mrr_at_10
      value: 83.04630158730119
    - type: mrr_at_100
      value: 83.22785731032968
    - type: mrr_at_1000
      value: 83.23123717623899
    - type: mrr_at_20
      value: 83.17412021320565
    - type: mrr_at_3
      value: 81.83333333333287
    - type: mrr_at_5
      value: 82.61933333333275
    - type: nauc_map_at_1000_diff1
      value: 73.26316553371083
    - type: nauc_map_at_1000_max
      value: 27.92567859085245
    - type: nauc_map_at_1000_std
      value: -47.477909533360446
    - type: nauc_map_at_100_diff1
      value: 73.2690602807223
    - type: nauc_map_at_100_max
      value: 27.915868327849996
    - type: nauc_map_at_100_std
      value: -47.525777766107595
    - type: nauc_map_at_10_diff1
      value: 73.45464428464894
    - type: nauc_map_at_10_max
      value: 27.451611487246296
    - type: nauc_map_at_10_std
      value: -49.35818715843809
    - type: nauc_map_at_1_diff1
      value: 77.29690208952982
    - type: nauc_map_at_1_max
      value: 19.839875762282293
    - type: nauc_map_at_1_std
      value: -45.355684654708284
    - type: nauc_map_at_20_diff1
      value: 73.35102731979796
    - type: nauc_map_at_20_max
      value: 27.741506490134583
    - type: nauc_map_at_20_std
      value: -48.22006207310331
    - type: nauc_map_at_3_diff1
      value: 73.94878241064137
    - type: nauc_map_at_3_max
      value: 24.761321386766728
    - type: nauc_map_at_3_std
      value: -51.20638883618126
    - type: nauc_map_at_5_diff1
      value: 73.66143558047698
    - type: nauc_map_at_5_max
      value: 26.53483405013543
    - type: nauc_map_at_5_std
      value: -50.697541279640056
    - type: nauc_mrr_at_1000_diff1
      value: 73.84632320009759
    - type: nauc_mrr_at_1000_max
      value: 30.50182733610048
    - type: nauc_mrr_at_1000_std
      value: -44.3021647995251
    - type: nauc_mrr_at_100_diff1
      value: 73.84480792662302
    - type: nauc_mrr_at_100_max
      value: 30.50749424571614
    - type: nauc_mrr_at_100_std
      value: -44.29615086388113
    - type: nauc_mrr_at_10_diff1
      value: 73.79442772949346
    - type: nauc_mrr_at_10_max
      value: 30.55724252219984
    - type: nauc_mrr_at_10_std
      value: -44.50997069462057
    - type: nauc_mrr_at_1_diff1
      value: 75.23369827945945
    - type: nauc_mrr_at_1_max
      value: 29.20073967447664
    - type: nauc_mrr_at_1_std
      value: -43.1920147658285
    - type: nauc_mrr_at_20_diff1
      value: 73.82731678072307
    - type: nauc_mrr_at_20_max
      value: 30.566328605497667
    - type: nauc_mrr_at_20_std
      value: -44.24683607643705
    - type: nauc_mrr_at_3_diff1
      value: 73.61997576749954
    - type: nauc_mrr_at_3_max
      value: 30.150393853381917
    - type: nauc_mrr_at_3_std
      value: -44.96847297506626
    - type: nauc_mrr_at_5_diff1
      value: 73.69084310616132
    - type: nauc_mrr_at_5_max
      value: 30.578033703441125
    - type: nauc_mrr_at_5_std
      value: -44.74920746066566
    - type: nauc_ndcg_at_1000_diff1
      value: 72.89349862557452
    - type: nauc_ndcg_at_1000_max
      value: 29.824725190462086
    - type: nauc_ndcg_at_1000_std
      value: -44.96284395063211
    - type: nauc_ndcg_at_100_diff1
      value: 72.85212753715273
    - type: nauc_ndcg_at_100_max
      value: 29.933114207845605
    - type: nauc_ndcg_at_100_std
      value: -44.944225570663754
    - type: nauc_ndcg_at_10_diff1
      value: 72.80576740454528
    - type: nauc_ndcg_at_10_max
      value: 29.16829118320828
    - type: nauc_ndcg_at_10_std
      value: -48.149473740079614
    - type: nauc_ndcg_at_1_diff1
      value: 75.00032534968587
    - type: nauc_ndcg_at_1_max
      value: 29.61849062038547
    - type: nauc_ndcg_at_1_std
      value: -42.560207043864054
    - type: nauc_ndcg_at_20_diff1
      value: 72.88440406302502
    - type: nauc_ndcg_at_20_max
      value: 29.65496676092656
    - type: nauc_ndcg_at_20_std
      value: -46.21238462167732
    - type: nauc_ndcg_at_3_diff1
      value: 72.37916962766987
    - type: nauc_ndcg_at_3_max
      value: 27.125094834547586
    - type: nauc_ndcg_at_3_std
      value: -48.62942991399391
    - type: nauc_ndcg_at_5_diff1
      value: 72.57017330527658
    - type: nauc_ndcg_at_5_max
      value: 28.470485561757254
    - type: nauc_ndcg_at_5_std
      value: -49.07593345591059
    - type: nauc_precision_at_1000_diff1
      value: -41.67915575853946
    - type: nauc_precision_at_1000_max
      value: 1.2012264478568844
    - type: nauc_precision_at_1000_std
      value: 44.723834559400466
    - type: nauc_precision_at_100_diff1
      value: -40.45196679236971
    - type: nauc_precision_at_100_max
      value: 2.3525450401714894
    - type: nauc_precision_at_100_std
      value: 43.7092529413952
    - type: nauc_precision_at_10_diff1
      value: -30.256026923068767
    - type: nauc_precision_at_10_max
      value: 8.313422052132559
    - type: nauc_precision_at_10_std
      value: 25.929372356449694
    - type: nauc_precision_at_1_diff1
      value: 75.00032534968587
    - type: nauc_precision_at_1_max
      value: 29.61849062038547
    - type: nauc_precision_at_1_std
      value: -42.560207043864054
    - type: nauc_precision_at_20_diff1
      value: -35.61971069986584
    - type: nauc_precision_at_20_max
      value: 5.4664303079116765
    - type: nauc_precision_at_20_std
      value: 34.992352471692826
    - type: nauc_precision_at_3_diff1
      value: -5.691231842471157
    - type: nauc_precision_at_3_max
      value: 14.797949087742444
    - type: nauc_precision_at_3_std
      value: -0.1930317395644928
    - type: nauc_precision_at_5_diff1
      value: -20.03913781462645
    - type: nauc_precision_at_5_max
      value: 11.956771408712749
    - type: nauc_precision_at_5_std
      value: 13.179251389859731
    - type: nauc_recall_at_1000_diff1
      value: 64.03509042729674
    - type: nauc_recall_at_1000_max
      value: 40.91691485428493
    - type: nauc_recall_at_1000_std
      value: 16.12968625875372
    - type: nauc_recall_at_100_diff1
      value: 63.83116179628575
    - type: nauc_recall_at_100_max
      value: 43.72908117676382
    - type: nauc_recall_at_100_std
      value: -20.50966716852155
    - type: nauc_recall_at_10_diff1
      value: 66.42071960186394
    - type: nauc_recall_at_10_max
      value: 28.983207818687205
    - type: nauc_recall_at_10_std
      value: -56.61417798753744
    - type: nauc_recall_at_1_diff1
      value: 77.29690208952982
    - type: nauc_recall_at_1_max
      value: 19.839875762282293
    - type: nauc_recall_at_1_std
      value: -45.355684654708284
    - type: nauc_recall_at_20_diff1
      value: 66.32360705219874
    - type: nauc_recall_at_20_max
      value: 33.30698111822631
    - type: nauc_recall_at_20_std
      value: -43.89233781737452
    - type: nauc_recall_at_3_diff1
      value: 69.67029394927077
    - type: nauc_recall_at_3_max
      value: 22.67803039327696
    - type: nauc_recall_at_3_std
      value: -56.43327209861502
    - type: nauc_recall_at_5_diff1
      value: 68.05622143936131
    - type: nauc_recall_at_5_max
      value: 26.67795559040675
    - type: nauc_recall_at_5_std
      value: -58.158231198510954
    - type: ndcg_at_1
      value: 76.08
    - type: ndcg_at_10
      value: 84.114
    - type: ndcg_at_100
      value: 85.784
    - type: ndcg_at_1000
      value: 85.992
    - type: ndcg_at_20
      value: 84.976
    - type: ndcg_at_3
      value: 80.74799999999999
    - type: ndcg_at_5
      value: 82.626
    - type: precision_at_1
      value: 76.08
    - type: precision_at_10
      value: 12.926000000000002
    - type: precision_at_100
      value: 1.509
    - type: precision_at_1000
      value: 0.156
    - type: precision_at_20
      value: 6.912999999999999
    - type: precision_at_3
      value: 35.5
    - type: precision_at_5
      value: 23.541999999999998
    - type: recall_at_1
      value: 65.848
    - type: recall_at_10
      value: 92.611
    - type: recall_at_100
      value: 98.69
    - type: recall_at_1000
      value: 99.83999999999999
    - type: recall_at_20
      value: 95.47200000000001
    - type: recall_at_3
      value: 83.122
    - type: recall_at_5
      value: 88.23
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB SCIDOCS-PL (default)
      revision: 45452b03f05560207ef19149545f168e596c9337
      split: test
      type: clarin-knext/scidocs-pl
    metrics:
    - type: main_score
      value: 15.379999999999999
    - type: map_at_1
      value: 3.6029999999999998
    - type: map_at_10
      value: 8.843
    - type: map_at_100
      value: 10.433
    - type: map_at_1000
      value: 10.689
    - type: map_at_20
      value: 9.597
    - type: map_at_3
      value: 6.363
    - type: map_at_5
      value: 7.603
    - type: mrr_at_1
      value: 17.7
    - type: mrr_at_10
      value: 26.58900793650793
    - type: mrr_at_100
      value: 27.699652322890987
    - type: mrr_at_1000
      value: 27.78065313118353
    - type: mrr_at_20
      value: 27.215020950411816
    - type: mrr_at_3
      value: 23.36666666666668
    - type: mrr_at_5
      value: 25.211666666666666
    - type: nauc_map_at_1000_diff1
      value: 21.92235143827129
    - type: nauc_map_at_1000_max
      value: 37.50300940750989
    - type: nauc_map_at_1000_std
      value: 20.872586122198552
    - type: nauc_map_at_100_diff1
      value: 21.917408170465833
    - type: nauc_map_at_100_max
      value: 37.4654466815513
    - type: nauc_map_at_100_std
      value: 20.621643878648534
    - type: nauc_map_at_10_diff1
      value: 22.914388723621183
    - type: nauc_map_at_10_max
      value: 36.468131213468794
    - type: nauc_map_at_10_std
      value: 16.760980140791492
    - type: nauc_map_at_1_diff1
      value: 29.00799502838457
    - type: nauc_map_at_1_max
      value: 26.64926291797503
    - type: nauc_map_at_1_std
      value: 8.167291261637361
    - type: nauc_map_at_20_diff1
      value: 22.46580947804047
    - type: nauc_map_at_20_max
      value: 36.656294842562275
    - type: nauc_map_at_20_std
      value: 18.099232417722078
    - type: nauc_map_at_3_diff1
      value: 23.436009032045934
    - type: nauc_map_at_3_max
      value: 31.325807212280914
    - type: nauc_map_at_3_std
      value: 9.780905232048852
    - type: nauc_map_at_5_diff1
      value: 22.891704394665528
    - type: nauc_map_at_5_max
      value: 35.40584466642894
    - type: nauc_map_at_5_std
      value: 13.476986099394656
    - type: nauc_mrr_at_1000_diff1
      value: 25.052937655397866
    - type: nauc_mrr_at_1000_max
      value: 29.64431912670108
    - type: nauc_mrr_at_1000_std
      value: 14.549744963988044
    - type: nauc_mrr_at_100_diff1
      value: 25.070871266969224
    - type: nauc_mrr_at_100_max
      value: 29.68743604652336
    - type: nauc_mrr_at_100_std
      value: 14.582010154574432
    - type: nauc_mrr_at_10_diff1
      value: 24.88881466938897
    - type: nauc_mrr_at_10_max
      value: 29.488430770768144
    - type: nauc_mrr_at_10_std
      value: 14.269241073852266
    - type: nauc_mrr_at_1_diff1
      value: 29.220540327267503
    - type: nauc_mrr_at_1_max
      value: 26.81908580507911
    - type: nauc_mrr_at_1_std
      value: 8.00840295809718
    - type: nauc_mrr_at_20_diff1
      value: 25.067912695721944
    - type: nauc_mrr_at_20_max
      value: 29.759227563849628
    - type: nauc_mrr_at_20_std
      value: 14.685076859257357
    - type: nauc_mrr_at_3_diff1
      value: 24.645848739182696
    - type: nauc_mrr_at_3_max
      value: 27.73368549660351
    - type: nauc_mrr_at_3_std
      value: 11.475742805586943
    - type: nauc_mrr_at_5_diff1
      value: 24.895295760909946
    - type: nauc_mrr_at_5_max
      value: 29.130755033240423
    - type: nauc_mrr_at_5_std
      value: 12.955802929145404
    - type: nauc_ndcg_at_1000_diff1
      value: 20.68434434777729
    - type: nauc_ndcg_at_1000_max
      value: 37.67055146424174
    - type: nauc_ndcg_at_1000_std
      value: 29.57493715069776
    - type: nauc_ndcg_at_100_diff1
      value: 20.396834816492383
    - type: nauc_ndcg_at_100_max
      value: 37.460575228670514
    - type: nauc_ndcg_at_100_std
      value: 27.826534756761944
    - type: nauc_ndcg_at_10_diff1
      value: 22.640844106236027
    - type: nauc_ndcg_at_10_max
      value: 35.21291764462327
    - type: nauc_ndcg_at_10_std
      value: 19.53289455984506
    - type: nauc_ndcg_at_1_diff1
      value: 29.220540327267503
    - type: nauc_ndcg_at_1_max
      value: 26.81908580507911
    - type: nauc_ndcg_at_1_std
      value: 8.00840295809718
    - type: nauc_ndcg_at_20_diff1
      value: 22.117126657768623
    - type: nauc_ndcg_at_20_max
      value: 35.79395781940806
    - type: nauc_ndcg_at_20_std
      value: 22.242748346260786
    - type: nauc_ndcg_at_3_diff1
      value: 23.00596063212187
    - type: nauc_ndcg_at_3_max
      value: 30.149013627580523
    - type: nauc_ndcg_at_3_std
      value: 11.07904064662722
    - type: nauc_ndcg_at_5_diff1
      value: 22.81875419630523
    - type: nauc_ndcg_at_5_max
      value: 34.24267468356626
    - type: nauc_ndcg_at_5_std
      value: 15.307780280752088
    - type: nauc_precision_at_1000_diff1
      value: 9.606677689029972
    - type: nauc_precision_at_1000_max
      value: 32.74855550489271
    - type: nauc_precision_at_1000_std
      value: 42.65372585937895
    - type: nauc_precision_at_100_diff1
      value: 11.528981313529545
    - type: nauc_precision_at_100_max
      value: 35.642529490132404
    - type: nauc_precision_at_100_std
      value: 38.146151426052306
    - type: nauc_precision_at_10_diff1
      value: 18.783957183811836
    - type: nauc_precision_at_10_max
      value: 36.1982008334257
    - type: nauc_precision_at_10_std
      value: 25.09349473195891
    - type: nauc_precision_at_1_diff1
      value: 29.220540327267503
    - type: nauc_precision_at_1_max
      value: 26.81908580507911
    - type: nauc_precision_at_1_std
      value: 8.00840295809718
    - type: nauc_precision_at_20_diff1
      value: 17.458766320828214
    - type: nauc_precision_at_20_max
      value: 36.000404903025235
    - type: nauc_precision_at_20_std
      value: 29.1608044138323
    - type: nauc_precision_at_3_diff1
      value: 20.213669462067166
    - type: nauc_precision_at_3_max
      value: 31.120650847205912
    - type: nauc_precision_at_3_std
      value: 12.390972418818118
    - type: nauc_precision_at_5_diff1
      value: 20.114245715785678
    - type: nauc_precision_at_5_max
      value: 37.30360111495823
    - type: nauc_precision_at_5_std
      value: 19.053109037822853
    - type: nauc_recall_at_1000_diff1
      value: 9.85800049032612
    - type: nauc_recall_at_1000_max
      value: 32.48319160802687
    - type: nauc_recall_at_1000_std
      value: 43.79941601741161
    - type: nauc_recall_at_100_diff1
      value: 11.375255270968337
    - type: nauc_recall_at_100_max
      value: 35.1868784124497
    - type: nauc_recall_at_100_std
      value: 38.422680583482666
    - type: nauc_recall_at_10_diff1
      value: 18.445783123521938
    - type: nauc_recall_at_10_max
      value: 35.633267936276766
    - type: nauc_recall_at_10_std
      value: 24.94469506254716
    - type: nauc_recall_at_1_diff1
      value: 29.00799502838457
    - type: nauc_recall_at_1_max
      value: 26.64926291797503
    - type: nauc_recall_at_1_std
      value: 8.167291261637361
    - type: nauc_recall_at_20_diff1
      value: 17.314906604151936
    - type: nauc_recall_at_20_max
      value: 35.66067699203996
    - type: nauc_recall_at_20_std
      value: 29.400137012506082
    - type: nauc_recall_at_3_diff1
      value: 19.873710875648698
    - type: nauc_recall_at_3_max
      value: 30.92404718742849
    - type: nauc_recall_at_3_std
      value: 12.400871018075199
    - type: nauc_recall_at_5_diff1
      value: 19.869948324233192
    - type: nauc_recall_at_5_max
      value: 37.06832511687574
    - type: nauc_recall_at_5_std
      value: 19.0798814966156
    - type: ndcg_at_1
      value: 17.7
    - type: ndcg_at_10
      value: 15.379999999999999
    - type: ndcg_at_100
      value: 22.09
    - type: ndcg_at_1000
      value: 27.151999999999997
    - type: ndcg_at_20
      value: 17.576
    - type: ndcg_at_3
      value: 14.219999999999999
    - type: ndcg_at_5
      value: 12.579
    - type: precision_at_1
      value: 17.7
    - type: precision_at_10
      value: 8.08
    - type: precision_at_100
      value: 1.7840000000000003
    - type: precision_at_1000
      value: 0.3
    - type: precision_at_20
      value: 5.305
    - type: precision_at_3
      value: 13.167000000000002
    - type: precision_at_5
      value: 11.06
    - type: recall_at_1
      value: 3.6029999999999998
    - type: recall_at_10
      value: 16.413
    - type: recall_at_100
      value: 36.263
    - type: recall_at_1000
      value: 61.016999999999996
    - type: recall_at_20
      value: 21.587999999999997
    - type: recall_at_3
      value: 8.013
    - type: recall_at_5
      value: 11.198
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB SciFact-PL (default)
      revision: 47932a35f045ef8ed01ba82bf9ff67f6e109207e
      split: test
      type: clarin-knext/scifact-pl
    metrics:
    - type: main_score
      value: 64.764
    - type: map_at_1
      value: 49.778
    - type: map_at_10
      value: 59.88
    - type: map_at_100
      value: 60.707
    - type: map_at_1000
      value: 60.729
    - type: map_at_20
      value: 60.419999999999995
    - type: map_at_3
      value: 57.45400000000001
    - type: map_at_5
      value: 58.729
    - type: mrr_at_1
      value: 52.33333333333333
    - type: mrr_at_10
      value: 61.29193121693122
    - type: mrr_at_100
      value: 61.95817765126313
    - type: mrr_at_1000
      value: 61.97583284368782
    - type: mrr_at_20
      value: 61.72469949641003
    - type: mrr_at_3
      value: 59.44444444444444
    - type: mrr_at_5
      value: 60.494444444444454
    - type: nauc_map_at_1000_diff1
      value: 62.21235294015774
    - type: nauc_map_at_1000_max
      value: 48.83996609100249
    - type: nauc_map_at_1000_std
      value: 5.23892781043174
    - type: nauc_map_at_100_diff1
      value: 62.20170226789429
    - type: nauc_map_at_100_max
      value: 48.8391766453537
    - type: nauc_map_at_100_std
      value: 5.2664077457917715
    - type: nauc_map_at_10_diff1
      value: 61.961975488329024
    - type: nauc_map_at_10_max
      value: 48.397109987625186
    - type: nauc_map_at_10_std
      value: 4.314859710827481
    - type: nauc_map_at_1_diff1
      value: 65.0865197011516
    - type: nauc_map_at_1_max
      value: 41.38862781954889
    - type: nauc_map_at_1_std
      value: -0.9182122632530586
    - type: nauc_map_at_20_diff1
      value: 61.99173935851292
    - type: nauc_map_at_20_max
      value: 48.79961814179307
    - type: nauc_map_at_20_std
      value: 5.262181845825118
    - type: nauc_map_at_3_diff1
      value: 62.37910539880477
    - type: nauc_map_at_3_max
      value: 47.13627890977091
    - type: nauc_map_at_3_std
      value: 2.327897198087264
    - type: nauc_map_at_5_diff1
      value: 61.60080757149592
    - type: nauc_map_at_5_max
      value: 47.60052458345962
    - type: nauc_map_at_5_std
      value: 3.1770196981231047
    - type: nauc_mrr_at_1000_diff1
      value: 62.86810952814966
    - type: nauc_mrr_at_1000_max
      value: 52.13248094447774
    - type: nauc_mrr_at_1000_std
      value: 10.100485746570733
    - type: nauc_mrr_at_100_diff1
      value: 62.85364829491874
    - type: nauc_mrr_at_100_max
      value: 52.134528010631854
    - type: nauc_mrr_at_100_std
      value: 10.120945685447369
    - type: nauc_mrr_at_10_diff1
      value: 62.65679301829915
    - type: nauc_mrr_at_10_max
      value: 52.09270719182349
    - type: nauc_mrr_at_10_std
      value: 9.913834434725441
    - type: nauc_mrr_at_1_diff1
      value: 66.84108271415636
    - type: nauc_mrr_at_1_max
      value: 46.67646429855176
    - type: nauc_mrr_at_1_std
      value: 5.5505252956352304
    - type: nauc_mrr_at_20_diff1
      value: 62.72473227039611
    - type: nauc_mrr_at_20_max
      value: 52.13479097802757
    - type: nauc_mrr_at_20_std
      value: 10.188278833464084
    - type: nauc_mrr_at_3_diff1
      value: 63.797429185518496
    - type: nauc_mrr_at_3_max
      value: 52.16486999573481
    - type: nauc_mrr_at_3_std
      value: 9.094360767062762
    - type: nauc_mrr_at_5_diff1
      value: 62.592917975475494
    - type: nauc_mrr_at_5_max
      value: 52.330741486107414
    - type: nauc_mrr_at_5_std
      value: 9.742175534421389
    - type: nauc_ndcg_at_1000_diff1
      value: 61.38859337672476
    - type: nauc_ndcg_at_1000_max
      value: 51.48380058339184
    - type: nauc_ndcg_at_1000_std
      value: 9.670547660897673
    - type: nauc_ndcg_at_100_diff1
      value: 61.02438489641434
    - type: nauc_ndcg_at_100_max
      value: 51.781246646780865
    - type: nauc_ndcg_at_100_std
      value: 10.592961553245187
    - type: nauc_ndcg_at_10_diff1
      value: 60.03678353308358
    - type: nauc_ndcg_at_10_max
      value: 50.70725688848762
    - type: nauc_ndcg_at_10_std
      value: 7.9472446491016315
    - type: nauc_ndcg_at_1_diff1
      value: 66.84108271415636
    - type: nauc_ndcg_at_1_max
      value: 46.67646429855176
    - type: nauc_ndcg_at_1_std
      value: 5.5505252956352304
    - type: nauc_ndcg_at_20_diff1
      value: 59.828482718480224
    - type: nauc_ndcg_at_20_max
      value: 51.45831789601284
    - type: nauc_ndcg_at_20_std
      value: 10.722673683272049
    - type: nauc_ndcg_at_3_diff1
      value: 61.68982937524109
    - type: nauc_ndcg_at_3_max
      value: 49.745326748604775
    - type: nauc_ndcg_at_3_std
      value: 4.948298621202247
    - type: nauc_ndcg_at_5_diff1
      value: 59.67396171973207
    - type: nauc_ndcg_at_5_max
      value: 49.87855139298281
    - type: nauc_ndcg_at_5_std
      value: 6.08990428055584
    - type: nauc_precision_at_1000_diff1
      value: -1.594227972036865
    - type: nauc_precision_at_1000_max
      value: 32.48431723086185
    - type: nauc_precision_at_1000_std
      value: 53.84748466965268
    - type: nauc_precision_at_100_diff1
      value: 8.06411455192293
    - type: nauc_precision_at_100_max
      value: 39.91003601878948
    - type: nauc_precision_at_100_std
      value: 55.52979711075091
    - type: nauc_precision_at_10_diff1
      value: 26.610514456014066
    - type: nauc_precision_at_10_max
      value: 47.09062494321172
    - type: nauc_precision_at_10_std
      value: 33.91984226498748
    - type: nauc_precision_at_1_diff1
      value: 66.84108271415636
    - type: nauc_precision_at_1_max
      value: 46.67646429855176
    - type: nauc_precision_at_1_std
      value: 5.5505252956352304
    - type: nauc_precision_at_20_diff1
      value: 16.947688843085583
    - type: nauc_precision_at_20_max
      value: 45.40488186572008
    - type: nauc_precision_at_20_std
      value: 48.354421924500905
    - type: nauc_precision_at_3_diff1
      value: 49.11263981720622
    - type: nauc_precision_at_3_max
      value: 52.7084625111683
    - type: nauc_precision_at_3_std
      value: 16.734612173556453
    - type: nauc_precision_at_5_diff1
      value: 39.06503705015792
    - type: nauc_precision_at_5_max
      value: 52.21710506893391
    - type: nauc_precision_at_5_std
      value: 23.350948149460233
    - type: nauc_recall_at_1000_diff1
      value: 43.1559290382817
    - type: nauc_recall_at_1000_max
      value: 83.66013071895456
    - type: nauc_recall_at_1000_std
      value: 86.27450980392177
    - type: nauc_recall_at_100_diff1
      value: 46.016860850620375
    - type: nauc_recall_at_100_max
      value: 69.3944888744547
    - type: nauc_recall_at_100_std
      value: 55.286945696152735
    - type: nauc_recall_at_10_diff1
      value: 49.65877895350921
    - type: nauc_recall_at_10_max
      value: 53.02636695700889
    - type: nauc_recall_at_10_std
      value: 13.967608945823828
    - type: nauc_recall_at_1_diff1
      value: 65.0865197011516
    - type: nauc_recall_at_1_max
      value: 41.38862781954889
    - type: nauc_recall_at_1_std
      value: -0.9182122632530586
    - type: nauc_recall_at_20_diff1
      value: 43.355308229973524
    - type: nauc_recall_at_20_max
      value: 57.04187909533764
    - type: nauc_recall_at_20_std
      value: 33.578720846660524
    - type: nauc_recall_at_3_diff1
      value: 56.922996057428165
    - type: nauc_recall_at_3_max
      value: 50.74417041895424
    - type: nauc_recall_at_3_std
      value: 5.623890124328387
    - type: nauc_recall_at_5_diff1
      value: 50.55620076865238
    - type: nauc_recall_at_5_max
      value: 51.3316854622085
    - type: nauc_recall_at_5_std
      value: 8.995457887269255
    - type: ndcg_at_1
      value: 52.333
    - type: ndcg_at_10
      value: 64.764
    - type: ndcg_at_100
      value: 68.167
    - type: ndcg_at_1000
      value: 68.816
    - type: ndcg_at_20
      value: 66.457
    - type: ndcg_at_3
      value: 60.346
    - type: ndcg_at_5
      value: 62.365
    - type: precision_at_1
      value: 52.333
    - type: precision_at_10
      value: 8.799999999999999
    - type: precision_at_100
      value: 1.057
    - type: precision_at_1000
      value: 0.11100000000000002
    - type: precision_at_20
      value: 4.8
    - type: precision_at_3
      value: 23.889
    - type: precision_at_5
      value: 15.6
    - type: recall_at_1
      value: 49.778
    - type: recall_at_10
      value: 78.206
    - type: recall_at_100
      value: 93.10000000000001
    - type: recall_at_1000
      value: 98.333
    - type: recall_at_20
      value: 84.467
    - type: recall_at_3
      value: 66.367
    - type: recall_at_5
      value: 71.35000000000001
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB TRECCOVID-PL (default)
      revision: 81bcb408f33366c2a20ac54adafad1ae7e877fdd
      split: test
      type: clarin-knext/trec-covid-pl
    metrics:
    - type: main_score
      value: 72.18900000000001
    - type: map_at_1
      value: 0.214
    - type: map_at_10
      value: 1.755
    - type: map_at_100
      value: 9.944
    - type: map_at_1000
      value: 24.205
    - type: map_at_20
      value: 3.1510000000000002
    - type: map_at_3
      value: 0.6
    - type: map_at_5
      value: 0.9560000000000001
    - type: mrr_at_1
      value: 82.0
    - type: mrr_at_10
      value: 89.06666666666666
    - type: mrr_at_100
      value: 89.06666666666666
    - type: mrr_at_1000
      value: 89.06666666666666
    - type: mrr_at_20
      value: 89.06666666666666
    - type: mrr_at_3
      value: 87.66666666666666
    - type: mrr_at_5
      value: 89.06666666666666
    - type: nauc_map_at_1000_diff1
      value: -9.342037623635543
    - type: nauc_map_at_1000_max
      value: 45.71499810252398
    - type: nauc_map_at_1000_std
      value: 76.86482845196852
    - type: nauc_map_at_100_diff1
      value: -6.932395299866198
    - type: nauc_map_at_100_max
      value: 36.097801891181604
    - type: nauc_map_at_100_std
      value: 65.6085215411685
    - type: nauc_map_at_10_diff1
      value: -6.3654843824342775
    - type: nauc_map_at_10_max
      value: 9.564437521432714
    - type: nauc_map_at_10_std
      value: 21.8377319336476
    - type: nauc_map_at_1_diff1
      value: 8.269590874255034
    - type: nauc_map_at_1_max
      value: 3.482498491294516
    - type: nauc_map_at_1_std
      value: 8.985226819412189
    - type: nauc_map_at_20_diff1
      value: -4.971435767877232
    - type: nauc_map_at_20_max
      value: 22.88801858567121
    - type: nauc_map_at_20_std
      value: 32.38492618534027
    - type: nauc_map_at_3_diff1
      value: 1.1615973694623123
    - type: nauc_map_at_3_max
      value: 1.935417800315643
    - type: nauc_map_at_3_std
      value: 10.289328305818698
    - type: nauc_map_at_5_diff1
      value: -2.4675967231444105
    - type: nauc_map_at_5_max
      value: 2.4611483736622373
    - type: nauc_map_at_5_std
      value: 15.082324305750811
    - type: nauc_mrr_at_1000_diff1
      value: 13.098526703499063
    - type: nauc_mrr_at_1000_max
      value: 56.37362177417431
    - type: nauc_mrr_at_1000_std
      value: 73.2456769749587
    - type: nauc_mrr_at_100_diff1
      value: 13.098526703499063
    - type: nauc_mrr_at_100_max
      value: 56.37362177417431
    - type: nauc_mrr_at_100_std
      value: 73.2456769749587
    - type: nauc_mrr_at_10_diff1
      value: 13.098526703499063
    - type: nauc_mrr_at_10_max
      value: 56.37362177417431
    - type: nauc_mrr_at_10_std
      value: 73.2456769749587
    - type: nauc_mrr_at_1_diff1
      value: 12.099350148694809
    - type: nauc_mrr_at_1_max
      value: 53.75041304108387
    - type: nauc_mrr_at_1_std
      value: 68.84018063663402
    - type: nauc_mrr_at_20_diff1
      value: 13.098526703499063
    - type: nauc_mrr_at_20_max
      value: 56.37362177417431
    - type: nauc_mrr_at_20_std
      value: 73.2456769749587
    - type: nauc_mrr_at_3_diff1
      value: 12.173557857011161
    - type: nauc_mrr_at_3_max
      value: 57.540780562363395
    - type: nauc_mrr_at_3_std
      value: 75.42098189580211
    - type: nauc_mrr_at_5_diff1
      value: 13.098526703499063
    - type: nauc_mrr_at_5_max
      value: 56.37362177417431
    - type: nauc_mrr_at_5_std
      value: 73.2456769749587
    - type: nauc_ndcg_at_1000_diff1
      value: -8.951471847310401
    - type: nauc_ndcg_at_1000_max
      value: 43.86942237288822
    - type: nauc_ndcg_at_1000_std
      value: 74.61077735148591
    - type: nauc_ndcg_at_100_diff1
      value: -17.754559361083817
    - type: nauc_ndcg_at_100_max
      value: 53.97187119773482
    - type: nauc_ndcg_at_100_std
      value: 80.7944136146514
    - type: nauc_ndcg_at_10_diff1
      value: -26.637734697836414
    - type: nauc_ndcg_at_10_max
      value: 47.70102699133149
    - type: nauc_ndcg_at_10_std
      value: 70.26909560828646
    - type: nauc_ndcg_at_1_diff1
      value: -1.2250530785563207
    - type: nauc_ndcg_at_1_max
      value: 46.60509554140131
    - type: nauc_ndcg_at_1_std
      value: 62.63906581740976
    - type: nauc_ndcg_at_20_diff1
      value: -22.44286466550908
    - type: nauc_ndcg_at_20_max
      value: 55.40492058090103
    - type: nauc_ndcg_at_20_std
      value: 72.11813912145738
    - type: nauc_ndcg_at_3_diff1
      value: -14.8152721896563
    - type: nauc_ndcg_at_3_max
      value: 38.952259383027595
    - type: nauc_ndcg_at_3_std
      value: 59.819750166537766
    - type: nauc_ndcg_at_5_diff1
      value: -19.150105688904375
    - type: nauc_ndcg_at_5_max
      value: 42.311180547775315
    - type: nauc_ndcg_at_5_std
      value: 66.6632229321094
    - type: nauc_precision_at_1000_diff1
      value: -11.555591477978941
    - type: nauc_precision_at_1000_max
      value: 43.7311644834851
    - type: nauc_precision_at_1000_std
      value: 52.10644767999648
    - type: nauc_precision_at_100_diff1
      value: -16.94803099801117
    - type: nauc_precision_at_100_max
      value: 54.08281631067633
    - type: nauc_precision_at_100_std
      value: 82.77237347891331
    - type: nauc_precision_at_10_diff1
      value: -27.351332814863355
    - type: nauc_precision_at_10_max
      value: 48.08237549065846
    - type: nauc_precision_at_10_std
      value: 69.37250843534329
    - type: nauc_precision_at_1_diff1
      value: 12.099350148694809
    - type: nauc_precision_at_1_max
      value: 53.75041304108387
    - type: nauc_precision_at_1_std
      value: 68.84018063663402
    - type: nauc_precision_at_20_diff1
      value: -18.2422222283388
    - type: nauc_precision_at_20_max
      value: 59.517328129343696
    - type: nauc_precision_at_20_std
      value: 72.05149307342747
    - type: nauc_precision_at_3_diff1
      value: -10.226547543075897
    - type: nauc_precision_at_3_max
      value: 43.14684818832875
    - type: nauc_precision_at_3_std
      value: 57.31936467418288
    - type: nauc_precision_at_5_diff1
      value: -14.28521589468673
    - type: nauc_precision_at_5_max
      value: 41.633426753962596
    - type: nauc_precision_at_5_std
      value: 64.94400576804541
    - type: nauc_recall_at_1000_diff1
      value: -0.9648831207497152
    - type: nauc_recall_at_1000_max
      value: 31.70832946085005
    - type: nauc_recall_at_1000_std
      value: 63.21471613968869
    - type: nauc_recall_at_100_diff1
      value: -1.360254380933586
    - type: nauc_recall_at_100_max
      value: 25.960597782099605
    - type: nauc_recall_at_100_std
      value: 51.52757589609674
    - type: nauc_recall_at_10_diff1
      value: -0.3899439424189566
    - type: nauc_recall_at_10_max
      value: 5.094341897886072
    - type: nauc_recall_at_10_std
      value: 11.266045616925698
    - type: nauc_recall_at_1_diff1
      value: 8.269590874255034
    - type: nauc_recall_at_1_max
      value: 3.482498491294516
    - type: nauc_recall_at_1_std
      value: 8.985226819412189
    - type: nauc_recall_at_20_diff1
      value: 6.4797098359254175
    - type: nauc_recall_at_20_max
      value: 15.663700985336124
    - type: nauc_recall_at_20_std
      value: 17.154099587904913
    - type: nauc_recall_at_3_diff1
      value: 3.7245972450393507
    - type: nauc_recall_at_3_max
      value: 0.4063857187240345
    - type: nauc_recall_at_3_std
      value: 6.641948062821941
    - type: nauc_recall_at_5_diff1
      value: 4.013879477591466
    - type: nauc_recall_at_5_max
      value: -1.4266586618013566
    - type: nauc_recall_at_5_std
      value: 7.311601874411205
    - type: ndcg_at_1
      value: 75.0
    - type: ndcg_at_10
      value: 72.18900000000001
    - type: ndcg_at_100
      value: 54.022999999999996
    - type: ndcg_at_1000
      value: 49.492000000000004
    - type: ndcg_at_20
      value: 68.51
    - type: ndcg_at_3
      value: 73.184
    - type: ndcg_at_5
      value: 72.811
    - type: precision_at_1
      value: 82.0
    - type: precision_at_10
      value: 77.4
    - type: precision_at_100
      value: 55.24
    - type: precision_at_1000
      value: 21.822
    - type: precision_at_20
      value: 73.0
    - type: precision_at_3
      value: 79.333
    - type: precision_at_5
      value: 79.2
    - type: recall_at_1
      value: 0.214
    - type: recall_at_10
      value: 1.9980000000000002
    - type: recall_at_100
      value: 13.328999999999999
    - type: recall_at_1000
      value: 47.204
    - type: recall_at_20
      value: 3.7310000000000003
    - type: recall_at_3
      value: 0.628
    - type: recall_at_5
      value: 1.049
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CEDRClassification (default)
      revision: c0ba03d058e3e1b2f3fd20518875a4563dd12db4
      split: test
      type: ai-forever/cedr-classification
    metrics:
    - type: accuracy
      value: 47.30605738575983
    - type: f1
      value: 41.26091043925065
    - type: lrap
      value: 72.89452709883206
    - type: main_score
      value: 47.30605738575983
    task:
      type: MultilabelClassification
  - dataset:
      config: ru
      name: MTEB MIRACLReranking (ru)
      revision: 6d1962c527217f8927fca80f890f14f36b2802af
      split: dev
      type: miracl/mmteb-miracl-reranking
    metrics:
    - type: MAP@1(MIRACL)
      value: 20.721999999999998
    - type: MAP@10(MIRACL)
      value: 33.900999999999996
    - type: MAP@100(MIRACL)
      value: 36.813
    - type: MAP@1000(MIRACL)
      value: 36.813
    - type: MAP@20(MIRACL)
      value: 35.684
    - type: MAP@3(MIRACL)
      value: 28.141
    - type: MAP@5(MIRACL)
      value: 31.075000000000003
    - type: NDCG@1(MIRACL)
      value: 32.799
    - type: NDCG@10(MIRACL)
      value: 42.065000000000005
    - type: NDCG@100(MIRACL)
      value: 49.730999999999995
    - type: NDCG@1000(MIRACL)
      value: 49.730999999999995
    - type: NDCG@20(MIRACL)
      value: 46.0
    - type: NDCG@3(MIRACL)
      value: 34.481
    - type: NDCG@5(MIRACL)
      value: 37.452999999999996
    - type: P@1(MIRACL)
      value: 32.799
    - type: P@10(MIRACL)
      value: 11.668000000000001
    - type: P@100(MIRACL)
      value: 1.9529999999999998
    - type: P@1000(MIRACL)
      value: 0.19499999999999998
    - type: P@20(MIRACL)
      value: 7.51
    - type: P@3(MIRACL)
      value: 20.823
    - type: P@5(MIRACL)
      value: 16.728
    - type: Recall@1(MIRACL)
      value: 20.721999999999998
    - type: Recall@10(MIRACL)
      value: 54.762
    - type: Recall@100(MIRACL)
      value: 79.952
    - type: Recall@1000(MIRACL)
      value: 79.952
    - type: Recall@20(MIRACL)
      value: 66.26100000000001
    - type: Recall@3(MIRACL)
      value: 34.410000000000004
    - type: Recall@5(MIRACL)
      value: 42.659000000000006
    - type: main_score
      value: 42.065000000000005
    - type: nAUC_MAP@1000_diff1(MIRACL)
      value: 14.33534992502818
    - type: nAUC_MAP@1000_max(MIRACL)
      value: 12.367998764646115
    - type: nAUC_MAP@1000_std(MIRACL)
      value: 4.569686002935006
    - type: nAUC_MAP@100_diff1(MIRACL)
      value: 14.33534992502818
    - type: nAUC_MAP@100_max(MIRACL)
      value: 12.367998764646115
    - type: nAUC_MAP@100_std(MIRACL)
      value: 4.569686002935006
    - type: nAUC_MAP@10_diff1(MIRACL)
      value: 16.920323975680027
    - type: nAUC_MAP@10_max(MIRACL)
      value: 9.327171297204082
    - type: nAUC_MAP@10_std(MIRACL)
      value: 3.2039133783079015
    - type: nAUC_MAP@1_diff1(MIRACL)
      value: 28.698973487482206
    - type: nAUC_MAP@1_max(MIRACL)
      value: 2.9217687660885034
    - type: nAUC_MAP@1_std(MIRACL)
      value: -1.1247408800976524
    - type: nAUC_MAP@20_diff1(MIRACL)
      value: 15.359083081640476
    - type: nAUC_MAP@20_max(MIRACL)
      value: 11.310494233946345
    - type: nAUC_MAP@20_std(MIRACL)
      value: 4.4171898386022885
    - type: nAUC_MAP@3_diff1(MIRACL)
      value: 22.27430591851617
    - type: nAUC_MAP@3_max(MIRACL)
      value: 6.407438291284658
    - type: nAUC_MAP@3_std(MIRACL)
      value: 0.9799184530397409
    - type: nAUC_MAP@5_diff1(MIRACL)
      value: 19.20571689941054
    - type: nAUC_MAP@5_max(MIRACL)
      value: 7.987468654026893
    - type: nAUC_MAP@5_std(MIRACL)
      value: 1.8324246565938962
    - type: nAUC_NDCG@1000_diff1(MIRACL)
      value: 3.7537669018914768
    - type: nAUC_NDCG@1000_max(MIRACL)
      value: 20.7944707840533
    - type: nAUC_NDCG@1000_std(MIRACL)
      value: 8.444837055303063
    - type: nAUC_NDCG@100_diff1(MIRACL)
      value: 3.7537669018914768
    - type: nAUC_NDCG@100_max(MIRACL)
      value: 20.7944707840533
    - type: nAUC_NDCG@100_std(MIRACL)
      value: 8.444837055303063
    - type: nAUC_NDCG@10_diff1(MIRACL)
      value: 10.829575656103888
    - type: nAUC_NDCG@10_max(MIRACL)
      value: 13.0445496498929
    - type: nAUC_NDCG@10_std(MIRACL)
      value: 6.050412212625362
    - type: nAUC_NDCG@1_diff1(MIRACL)
      value: 19.1388712233292
    - type: nAUC_NDCG@1_max(MIRACL)
      value: 10.871900994781642
    - type: nAUC_NDCG@1_std(MIRACL)
      value: 3.218568248751811
    - type: nAUC_NDCG@20_diff1(MIRACL)
      value: 7.093172181746442
    - type: nAUC_NDCG@20_max(MIRACL)
      value: 16.955238078958836
    - type: nAUC_NDCG@20_std(MIRACL)
      value: 8.325656379573035
    - type: nAUC_NDCG@3_diff1(MIRACL)
      value: 17.134437303330802
    - type: nAUC_NDCG@3_max(MIRACL)
      value: 10.235328822955793
    - type: nAUC_NDCG@3_std(MIRACL)
      value: 3.2341358691084814
    - type: nAUC_NDCG@5_diff1(MIRACL)
      value: 14.733664618337636
    - type: nAUC_NDCG@5_max(MIRACL)
      value: 11.181897412035282
    - type: nAUC_NDCG@5_std(MIRACL)
      value: 3.642277088791985
    - type: nAUC_P@1000_diff1(MIRACL)
      value: -26.330038284867573
    - type: nAUC_P@1000_max(MIRACL)
      value: 28.450694137240458
    - type: nAUC_P@1000_std(MIRACL)
      value: 9.892993775474912
    - type: nAUC_P@100_diff1(MIRACL)
      value: -26.330038284867552
    - type: nAUC_P@100_max(MIRACL)
      value: 28.45069413724051
    - type: nAUC_P@100_std(MIRACL)
      value: 9.892993775474928
    - type: nAUC_P@10_diff1(MIRACL)
      value: -17.436937353231112
    - type: nAUC_P@10_max(MIRACL)
      value: 24.327018012947857
    - type: nAUC_P@10_std(MIRACL)
      value: 11.78803527706634
    - type: nAUC_P@1_diff1(MIRACL)
      value: 19.1388712233292
    - type: nAUC_P@1_max(MIRACL)
      value: 10.871900994781642
    - type: nAUC_P@1_std(MIRACL)
      value: 3.218568248751811
    - type: nAUC_P@20_diff1(MIRACL)
      value: -22.947528755272426
    - type: nAUC_P@20_max(MIRACL)
      value: 27.773093471902538
    - type: nAUC_P@20_std(MIRACL)
      value: 14.898619107087221
    - type: nAUC_P@3_diff1(MIRACL)
      value: 1.4100426412400944
    - type: nAUC_P@3_max(MIRACL)
      value: 17.397472872058845
    - type: nAUC_P@3_std(MIRACL)
      value: 8.240008229861875
    - type: nAUC_P@5_diff1(MIRACL)
      value: -7.971349332207021
    - type: nAUC_P@5_max(MIRACL)
      value: 22.198441167940963
    - type: nAUC_P@5_std(MIRACL)
      value: 9.00265164460082
    - type: nAUC_Recall@1000_diff1(MIRACL)
      value: -38.69835271863148
    - type: nAUC_Recall@1000_max(MIRACL)
      value: 50.9545152809108
    - type: nAUC_Recall@1000_std(MIRACL)
      value: 20.44270887092116
    - type: nAUC_Recall@100_diff1(MIRACL)
      value: -38.69835271863148
    - type: nAUC_Recall@100_max(MIRACL)
      value: 50.9545152809108
    - type: nAUC_Recall@100_std(MIRACL)
      value: 20.44270887092116
    - type: nAUC_Recall@10_diff1(MIRACL)
      value: -0.08109036309433801
    - type: nAUC_Recall@10_max(MIRACL)
      value: 12.696619907773568
    - type: nAUC_Recall@10_std(MIRACL)
      value: 8.791982704261589
    - type: nAUC_Recall@1_diff1(MIRACL)
      value: 28.698973487482206
    - type: nAUC_Recall@1_max(MIRACL)
      value: 2.9217687660885034
    - type: nAUC_Recall@1_std(MIRACL)
      value: -1.1247408800976524
    - type: nAUC_Recall@20_diff1(MIRACL)
      value: -13.312171017942623
    - type: nAUC_Recall@20_max(MIRACL)
      value: 24.19847346821666
    - type: nAUC_Recall@20_std(MIRACL)
      value: 15.8157702609797
    - type: nAUC_Recall@3_diff1(MIRACL)
      value: 16.909128321353343
    - type: nAUC_Recall@3_max(MIRACL)
      value: 6.552122731902991
    - type: nAUC_Recall@3_std(MIRACL)
      value: 1.9963898223457228
    - type: nAUC_Recall@5_diff1(MIRACL)
      value: 9.990292655247721
    - type: nAUC_Recall@5_max(MIRACL)
      value: 9.361722273507574
    - type: nAUC_Recall@5_std(MIRACL)
      value: 3.270918827854495
    task:
      type: Reranking
  
  - dataset:
      config: default
      name: MTEB SensitiveTopicsClassification (default)
      revision: 416b34a802308eac30e4192afc0ff99bb8dcc7f2
      split: test
      type: ai-forever/sensitive-topics-classification
    metrics:
    - type: accuracy
      value: 30.634765625
    - type: f1
      value: 32.647559808678665
    - type: lrap
      value: 45.94319661458259
    - type: main_score
      value: 30.634765625
    task:
      type: MultilabelClassification
  - dataset:
      config: default
      name: MTEB ATEC (default)
      revision: 0f319b1142f28d00e055a6770f3f726ae9b7d865
      split: test
      type: C-MTEB/ATEC
    metrics:
    - type: cosine_pearson
      value: 47.541497334563296
    - type: cosine_spearman
      value: 49.06268944206629
    - type: euclidean_pearson
      value: 51.838926748581635
    - type: euclidean_spearman
      value: 48.930697157135356
    - type: main_score
      value: 49.06268944206629
    - type: manhattan_pearson
      value: 51.835306769406365
    - type: manhattan_spearman
      value: 48.86135493444834
    - type: pearson
      value: 47.541497334563296
    - type: spearman
      value: 49.06268944206629
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB AllegroReviews (default)
      revision: b89853e6de927b0e3bfa8ecc0e56fe4e02ceafc6
      split: test
      type: PL-MTEB/allegro-reviews
    metrics:
    - type: accuracy
      value: 49.51292246520874
    - type: f1
      value: 44.14350234332397
    - type: f1_weighted
      value: 51.65508998354552
    - type: main_score
      value: 49.51292246520874
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB AlloProfClusteringP2P (default)
      revision: 392ba3f5bcc8c51f578786c1fc3dae648662cb9b
      split: test
      type: lyon-nlp/alloprof
    metrics:
    - type: main_score
      value: 63.883383458621665
    - type: v_measure
      value: 63.883383458621665
    - type: v_measure_std
      value: 2.693666879958465
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB 8TagsClustering
      revision: None
      split: test
      type: PL-MTEB/8tags-clustering
    metrics:
    - type: v_measure
      value: 43.657212124525546
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB AlloProfClusteringS2S (default)
      revision: 392ba3f5bcc8c51f578786c1fc3dae648662cb9b
      split: test
      type: lyon-nlp/alloprof
    metrics:
    - type: main_score
      value: 46.85924588755251
    - type: v_measure
      value: 46.85924588755251
    - type: v_measure_std
      value: 2.1918258880872377
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB AlloprofReranking (default)
      revision: e40c8a63ce02da43200eccb5b0846fcaa888f562
      split: test
      type: lyon-nlp/mteb-fr-reranking-alloprof-s2p
    metrics:
    - type: map
      value: 66.39013753839347
    - type: mrr
      value: 67.68045617786551
    - type: main_score
      value: 66.39013753839347
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB AlloprofRetrieval (default)
      revision: fcf295ea64c750f41fadbaa37b9b861558e1bfbd
      split: test
      type: lyon-nlp/alloprof
    metrics:
    - type: main_score
      value: 54.284
    - type: map_at_1
      value: 37.047000000000004
    - type: map_at_10
      value: 48.53
    - type: map_at_100
      value: 49.357
    - type: map_at_1000
      value: 49.39
    - type: map_at_20
      value: 49.064
    - type: map_at_3
      value: 45.675
    - type: map_at_5
      value: 47.441
    - type: mrr_at_1
      value: 37.04663212435233
    - type: mrr_at_10
      value: 48.5300326232969
    - type: mrr_at_100
      value: 49.35708199037581
    - type: mrr_at_1000
      value: 49.39005824603193
    - type: mrr_at_20
      value: 49.06417416464799
    - type: mrr_at_3
      value: 45.67501439263105
    - type: mrr_at_5
      value: 47.44099021301103
    - type: nauc_map_at_1000_diff1
      value: 43.32474221868009
    - type: nauc_map_at_1000_max
      value: 39.407334029058575
    - type: nauc_map_at_1000_std
      value: -2.3728154448932606
    - type: nauc_map_at_100_diff1
      value: 43.32336300929909
    - type: nauc_map_at_100_max
      value: 39.432174777554835
    - type: nauc_map_at_100_std
      value: -2.356396922384349
    - type: nauc_map_at_10_diff1
      value: 43.1606520154482
    - type: nauc_map_at_10_max
      value: 39.33734650558226
    - type: nauc_map_at_10_std
      value: -2.5156222475075256
    - type: nauc_map_at_1_diff1
      value: 46.2178975214499
    - type: nauc_map_at_1_max
      value: 36.26173199049361
    - type: nauc_map_at_1_std
      value: -3.0897555582816443
    - type: nauc_map_at_20_diff1
      value: 43.272980702916456
    - type: nauc_map_at_20_max
      value: 39.4896977052276
    - type: nauc_map_at_20_std
      value: -2.3305501742917043
    - type: nauc_map_at_3_diff1
      value: 43.49525042967079
    - type: nauc_map_at_3_max
      value: 38.66352501824728
    - type: nauc_map_at_3_std
      value: -3.202794391620473
    - type: nauc_map_at_5_diff1
      value: 43.2266692546611
    - type: nauc_map_at_5_max
      value: 38.77368661115743
    - type: nauc_map_at_5_std
      value: -3.0897532130127954
    - type: nauc_mrr_at_1000_diff1
      value: 43.32474221868009
    - type: nauc_mrr_at_1000_max
      value: 39.407334029058575
    - type: nauc_mrr_at_1000_std
      value: -2.3728154448932606
    - type: nauc_mrr_at_100_diff1
      value: 43.32336300929909
    - type: nauc_mrr_at_100_max
      value: 39.432174777554835
    - type: nauc_mrr_at_100_std
      value: -2.356396922384349
    - type: nauc_mrr_at_10_diff1
      value: 43.1606520154482
    - type: nauc_mrr_at_10_max
      value: 39.33734650558226
    - type: nauc_mrr_at_10_std
      value: -2.5156222475075256
    - type: nauc_mrr_at_1_diff1
      value: 46.2178975214499
    - type: nauc_mrr_at_1_max
      value: 36.26173199049361
    - type: nauc_mrr_at_1_std
      value: -3.0897555582816443
    - type: nauc_mrr_at_20_diff1
      value: 43.272980702916456
    - type: nauc_mrr_at_20_max
      value: 39.4896977052276
    - type: nauc_mrr_at_20_std
      value: -2.3305501742917043
    - type: nauc_mrr_at_3_diff1
      value: 43.49525042967079
    - type: nauc_mrr_at_3_max
      value: 38.66352501824728
    - type: nauc_mrr_at_3_std
      value: -3.202794391620473
    - type: nauc_mrr_at_5_diff1
      value: 43.2266692546611
    - type: nauc_mrr_at_5_max
      value: 38.77368661115743
    - type: nauc_mrr_at_5_std
      value: -3.0897532130127954
    - type: nauc_ndcg_at_1000_diff1
      value: 43.01903168202974
    - type: nauc_ndcg_at_1000_max
      value: 40.75496622942232
    - type: nauc_ndcg_at_1000_std
      value: -1.3150412981845496
    - type: nauc_ndcg_at_100_diff1
      value: 42.98016493758145
    - type: nauc_ndcg_at_100_max
      value: 41.55869635162325
    - type: nauc_ndcg_at_100_std
      value: -0.5355252976886055
    - type: nauc_ndcg_at_10_diff1
      value: 42.218755211347506
    - type: nauc_ndcg_at_10_max
      value: 41.305042275175765
    - type: nauc_ndcg_at_10_std
      value: -1.4034484444573714
    - type: nauc_ndcg_at_1_diff1
      value: 46.2178975214499
    - type: nauc_ndcg_at_1_max
      value: 36.26173199049361
    - type: nauc_ndcg_at_1_std
      value: -3.0897555582816443
    - type: nauc_ndcg_at_20_diff1
      value: 42.66574440095576
    - type: nauc_ndcg_at_20_max
      value: 42.014620115124515
    - type: nauc_ndcg_at_20_std
      value: -0.5176162553751498
    - type: nauc_ndcg_at_3_diff1
      value: 42.837450505106055
    - type: nauc_ndcg_at_3_max
      value: 39.525369733082414
    - type: nauc_ndcg_at_3_std
      value: -3.1605948245795155
    - type: nauc_ndcg_at_5_diff1
      value: 42.37951815451173
    - type: nauc_ndcg_at_5_max
      value: 39.78840132935179
    - type: nauc_ndcg_at_5_std
      value: -2.936898430768135
    - type: nauc_precision_at_1000_diff1
      value: 49.69224988612385
    - type: nauc_precision_at_1000_max
      value: 79.57897547128005
    - type: nauc_precision_at_1000_std
      value: 45.040371354764645
    - type: nauc_precision_at_100_diff1
      value: 42.70597486048422
    - type: nauc_precision_at_100_max
      value: 65.74628759606188
    - type: nauc_precision_at_100_std
      value: 25.49157745244855
    - type: nauc_precision_at_10_diff1
      value: 38.565609931689345
    - type: nauc_precision_at_10_max
      value: 50.0239696180852
    - type: nauc_precision_at_10_std
      value: 3.976354829503967
    - type: nauc_precision_at_1_diff1
      value: 46.2178975214499
    - type: nauc_precision_at_1_max
      value: 36.26173199049361
    - type: nauc_precision_at_1_std
      value: -3.0897555582816443
    - type: nauc_precision_at_20_diff1
      value: 40.4134718566864
    - type: nauc_precision_at_20_max
      value: 57.121778108665374
    - type: nauc_precision_at_20_std
      value: 11.46021975428544
    - type: nauc_precision_at_3_diff1
      value: 40.90538379461529
    - type: nauc_precision_at_3_max
      value: 42.18393248057992
    - type: nauc_precision_at_3_std
      value: -3.005249943837297
    - type: nauc_precision_at_5_diff1
      value: 39.60162965860782
    - type: nauc_precision_at_5_max
      value: 43.28317158174058
    - type: nauc_precision_at_5_std
      value: -2.3469094487738054
    - type: nauc_recall_at_1000_diff1
      value: 49.69224988612252
    - type: nauc_recall_at_1000_max
      value: 79.57897547127862
    - type: nauc_recall_at_1000_std
      value: 45.04037135476256
    - type: nauc_recall_at_100_diff1
      value: 42.70597486048432
    - type: nauc_recall_at_100_max
      value: 65.74628759606213
    - type: nauc_recall_at_100_std
      value: 25.491577452448727
    - type: nauc_recall_at_10_diff1
      value: 38.56560993168935
    - type: nauc_recall_at_10_max
      value: 50.02396961808522
    - type: nauc_recall_at_10_std
      value: 3.9763548295040314
    - type: nauc_recall_at_1_diff1
      value: 46.2178975214499
    - type: nauc_recall_at_1_max
      value: 36.26173199049361
    - type: nauc_recall_at_1_std
      value: -3.0897555582816443
    - type: nauc_recall_at_20_diff1
      value: 40.41347185668637
    - type: nauc_recall_at_20_max
      value: 57.12177810866533
    - type: nauc_recall_at_20_std
      value: 11.460219754285431
    - type: nauc_recall_at_3_diff1
      value: 40.90538379461527
    - type: nauc_recall_at_3_max
      value: 42.18393248057989
    - type: nauc_recall_at_3_std
      value: -3.005249943837297
    - type: nauc_recall_at_5_diff1
      value: 39.601629658607784
    - type: nauc_recall_at_5_max
      value: 43.28317158174053
    - type: nauc_recall_at_5_std
      value: -2.3469094487738054
    - type: ndcg_at_1
      value: 37.047000000000004
    - type: ndcg_at_10
      value: 54.284
    - type: ndcg_at_100
      value: 58.34
    - type: ndcg_at_1000
      value: 59.303
    - type: ndcg_at_20
      value: 56.235
    - type: ndcg_at_3
      value: 48.503
    - type: ndcg_at_5
      value: 51.686
    - type: precision_at_1
      value: 37.047000000000004
    - type: precision_at_10
      value: 7.237
    - type: precision_at_100
      value: 0.914
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.005
    - type: precision_at_3
      value: 18.898
    - type: precision_at_5
      value: 12.884
    - type: recall_at_1
      value: 37.047000000000004
    - type: recall_at_10
      value: 72.366
    - type: recall_at_100
      value: 91.408
    - type: recall_at_1000
      value: 99.136
    - type: recall_at_20
      value: 80.095
    - type: recall_at_3
      value: 56.693000000000005
    - type: recall_at_5
      value: 64.42099999999999
    task:
      type: Retrieval
  - dataset:
      config: en
      name: MTEB AmazonCounterfactualClassification (en)
      revision: e8379541af4e31359cca9fbcf4b00f2671dba205
      split: test
      type: mteb/amazon_counterfactual
    metrics:
    - type: accuracy
      value: 89.49253731343283
    - type: ap
      value: 61.88098616359918
    - type: ap_weighted
      value: 61.88098616359918
    - type: f1
      value: 84.76516623679144
    - type: f1_weighted
      value: 89.92745276292968
    - type: main_score
      value: 89.49253731343283
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB AmazonCounterfactualClassification (de)
      revision: e8379541af4e31359cca9fbcf4b00f2671dba205
      split: test
      type: mteb/amazon_counterfactual
    metrics:
    - type: accuracy
      value: 89.61456102783727
    - type: ap
      value: 93.11816566733742
    - type: ap_weighted
      value: 93.11816566733742
    - type: f1
      value: 88.27635757733722
    - type: f1_weighted
      value: 89.82581568285453
    - type: main_score
      value: 89.61456102783727
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB AmazonPolarityClassification (default)
      revision: e2d317d38cd51312af73b3d32a06d1a08b442046
      split: test
      type: mteb/amazon_polarity
    metrics:
    - type: accuracy
      value: 95.3825
    - type: ap
      value: 93.393033869502
    - type: ap_weighted
      value: 93.393033869502
    - type: f1
      value: 95.38109007966307
    - type: f1_weighted
      value: 95.38109007966305
    - type: main_score
      value: 95.3825
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB AmazonReviewsClassification (en)
      revision: 1399c76144fd37290681b995c656ef9b2e06e26d
      split: test
      type: mteb/amazon_reviews_multi
    metrics:
    - type: accuracy
      value: 49.768
    - type: f1
      value: 48.95084821944411
    - type: f1_weighted
      value: 48.9508482194441
    - type: main_score
      value: 49.768
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB AmazonReviewsClassification (de)
      revision: 1399c76144fd37290681b995c656ef9b2e06e26d
      split: test
      type: mteb/amazon_reviews_multi
    metrics:
    - type: accuracy
      value: 48.071999999999996
    - type: f1
      value: 47.24171107487612
    - type: f1_weighted
      value: 47.24171107487612
    - type: main_score
      value: 48.071999999999996
    task:
      type: Classification
  - dataset:
      config: es
      name: MTEB AmazonReviewsClassification (es)
      revision: 1399c76144fd37290681b995c656ef9b2e06e26d
      split: test
      type: mteb/amazon_reviews_multi
    metrics:
    - type: accuracy
      value: 48.102000000000004
    - type: f1
      value: 47.27193805278696
    - type: f1_weighted
      value: 47.27193805278696
    - type: main_score
      value: 48.102000000000004
    task:
      type: Classification
  - dataset:
      config: fr
      name: MTEB AmazonReviewsClassification (fr)
      revision: 1399c76144fd37290681b995c656ef9b2e06e26d
      split: test
      type: mteb/amazon_reviews_multi
    metrics:
    - type: accuracy
      value: 47.30800000000001
    - type: f1
      value: 46.41683358017851
    - type: f1_weighted
      value: 46.41683358017851
    - type: main_score
      value: 47.30800000000001
    task:
      type: Classification
  - dataset:
      config: zh
      name: MTEB AmazonReviewsClassification (zh)
      revision: 1399c76144fd37290681b995c656ef9b2e06e26d
      split: test
      type: mteb/amazon_reviews_multi
    metrics:
    - type: accuracy
      value: 44.944
    - type: f1
      value: 44.223824487744395
    - type: f1_weighted
      value: 44.22382448774439
    - type: main_score
      value: 44.944
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB ArguAna (default)
      revision: c22ab2a51041ffd869aaddef7af8d8215647e41a
      split: test
      type: mteb/arguana
    metrics:
    - type: map_at_1
      value: 29.232000000000003
    - type: map_at_10
      value: 45.117000000000004
    - type: map_at_100
      value: 45.977000000000004
    - type: map_at_1000
      value: 45.98
    - type: map_at_20
      value: 45.815
    - type: map_at_3
      value: 39.912
    - type: map_at_5
      value: 42.693
    - type: mrr_at_1
      value: 29.659000000000002
    - type: mrr_at_10
      value: 45.253
    - type: mrr_at_100
      value: 46.125
    - type: mrr_at_1000
      value: 46.129
    - type: mrr_at_20
      value: 45.964
    - type: mrr_at_3
      value: 40.043
    - type: mrr_at_5
      value: 42.870000000000005
    - type: ndcg_at_1
      value: 29.232000000000003
    - type: ndcg_at_10
      value: 54.327999999999996
    - type: ndcg_at_100
      value: 57.86
    - type: ndcg_at_1000
      value: 57.935
    - type: ndcg_at_20
      value: 56.794
    - type: ndcg_at_3
      value: 43.516
    - type: ndcg_at_5
      value: 48.512
    - type: precision_at_1
      value: 29.232000000000003
    - type: precision_at_10
      value: 8.393
    - type: precision_at_100
      value: 0.991
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.676
    - type: precision_at_3
      value: 17.994
    - type: precision_at_5
      value: 13.215
    - type: recall_at_1
      value: 29.232000000000003
    - type: recall_at_10
      value: 83.926
    - type: recall_at_100
      value: 99.075
    - type: recall_at_1000
      value: 99.644
    - type: recall_at_20
      value: 93.528
    - type: recall_at_3
      value: 53.983000000000004
    - type: recall_at_5
      value: 66.074
    - type: main_score
      value: 54.327999999999996
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB ArxivClusteringP2P (default)
      revision: a122ad7f3f0291bf49cc6f4d32aa80929df69d5d
      split: test
      type: mteb/arxiv-clustering-p2p
    metrics:
    - type: main_score
      value: 46.6636824632419
    - type: v_measure
      value: 46.6636824632419
    - type: v_measure_std
      value: 13.817129140714963
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB ArxivClusteringS2S (default)
      revision: f910caf1a6075f7329cdf8c1a6135696f37dbd53
      split: test
      type: mteb/arxiv-clustering-s2s
    metrics:
    - type: main_score
      value: 39.271141892800024
    - type: v_measure
      value: 39.271141892800024
    - type: v_measure_std
      value: 14.276782483454827
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB AskUbuntuDupQuestions (default)
      revision: 2000358ca161889fa9c082cb41daa8dcfb161a54
      split: test
      type: mteb/askubuntudupquestions-reranking
    metrics:
    - type: map
      value: 65.04363277324629
    - type: mrr
      value: 78.2372598162072
    - type: main_score
      value: 65.04363277324629
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB MindSmallReranking (default)
      revision: 3bdac13927fdc888b903db93b2ffdbd90b295a69
      split: test
      type: mteb/mind_small
    metrics:
    - type: map
      value: 30.83
    - type: main_score
      value: 30.83
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB BIOSSES (default)
      revision: d3fb88f8f02e40887cd149695127462bbcf29b4a
      split: test
      type: mteb/biosses-sts
    metrics:
    - type: cosine_pearson
      value: 88.80382082011027
    - type: cosine_spearman
      value: 88.68876782169106
    - type: euclidean_pearson
      value: 87.00802890147176
    - type: euclidean_spearman
      value: 87.43211268192712
    - type: main_score
      value: 88.68876782169106
    - type: manhattan_pearson
      value: 87.14062537179474
    - type: manhattan_spearman
      value: 87.59115245033443
    - type: pearson
      value: 88.80382082011027
    - type: spearman
      value: 88.68876782169106
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB BQ (default)
      revision: e3dda5e115e487b39ec7e618c0c6a29137052a55
      split: test
      type: C-MTEB/BQ
    metrics:
    - type: cosine_pearson
      value: 61.588006604878196
    - type: cosine_spearman
      value: 63.20615427154465
    - type: euclidean_pearson
      value: 61.818547092516496
    - type: euclidean_spearman
      value: 63.21558009151778
    - type: main_score
      value: 63.20615427154465
    - type: manhattan_pearson
      value: 61.665588158487616
    - type: manhattan_spearman
      value: 63.051544488238584
    - type: pearson
      value: 61.588006604878196
    - type: spearman
      value: 63.20615427154465
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB BSARDRetrieval (default)
      revision: 5effa1b9b5fa3b0f9e12523e6e43e5f86a6e6d59
      split: test
      type: maastrichtlawtech/bsard
    metrics:
    - type: main_score
      value: 64.414
    - type: map_at_1
      value: 14.865
    - type: map_at_10
      value: 21.605
    - type: map_at_100
      value: 22.762
    - type: map_at_1000
      value: 22.854
    - type: map_at_20
      value: 22.259999999999998
    - type: map_at_3
      value: 20.119999999999997
    - type: map_at_5
      value: 20.931
    - type: mrr_at_1
      value: 14.864864864864865
    - type: mrr_at_10
      value: 21.605176605176606
    - type: mrr_at_100
      value: 22.7622306460065
    - type: mrr_at_1000
      value: 22.85383406410312
    - type: mrr_at_20
      value: 22.259528463088845
    - type: mrr_at_3
      value: 20.12012012012012
    - type: mrr_at_5
      value: 20.930930930930934
    - type: nauc_map_at_1000_diff1
      value: 17.486265968689338
    - type: nauc_map_at_1000_max
      value: 22.736799291688836
    - type: nauc_map_at_1000_std
      value: 9.831687441977147
    - type: nauc_map_at_100_diff1
      value: 17.50754492049086
    - type: nauc_map_at_100_max
      value: 22.77693662806787
    - type: nauc_map_at_100_std
      value: 9.853899509675395
    - type: nauc_map_at_10_diff1
      value: 17.42133968580952
    - type: nauc_map_at_10_max
      value: 22.45861793882279
    - type: nauc_map_at_10_std
      value: 8.964888472915938
    - type: nauc_map_at_1_diff1
      value: 19.433947086968093
    - type: nauc_map_at_1_max
      value: 24.75657047550517
    - type: nauc_map_at_1_std
      value: 15.122329157218505
    - type: nauc_map_at_20_diff1
      value: 17.429856756008785
    - type: nauc_map_at_20_max
      value: 22.438850987431017
    - type: nauc_map_at_20_std
      value: 9.172746012213558
    - type: nauc_map_at_3_diff1
      value: 18.218182689678475
    - type: nauc_map_at_3_max
      value: 23.57169444088667
    - type: nauc_map_at_3_std
      value: 10.464473559366356
    - type: nauc_map_at_5_diff1
      value: 18.6075342519133
    - type: nauc_map_at_5_max
      value: 23.308845973576673
    - type: nauc_map_at_5_std
      value: 9.364009996445652
    - type: nauc_mrr_at_1000_diff1
      value: 17.486265968689338
    - type: nauc_mrr_at_1000_max
      value: 22.736799291688836
    - type: nauc_mrr_at_1000_std
      value: 9.831687441977147
    - type: nauc_mrr_at_100_diff1
      value: 17.50754492049086
    - type: nauc_mrr_at_100_max
      value: 22.77693662806787
    - type: nauc_mrr_at_100_std
      value: 9.853899509675395
    - type: nauc_mrr_at_10_diff1
      value: 17.42133968580952
    - type: nauc_mrr_at_10_max
      value: 22.45861793882279
    - type: nauc_mrr_at_10_std
      value: 8.964888472915938
    - type: nauc_mrr_at_1_diff1
      value: 19.433947086968093
    - type: nauc_mrr_at_1_max
      value: 24.75657047550517
    - type: nauc_mrr_at_1_std
      value: 15.122329157218505
    - type: nauc_mrr_at_20_diff1
      value: 17.429856756008785
    - type: nauc_mrr_at_20_max
      value: 22.438850987431017
    - type: nauc_mrr_at_20_std
      value: 9.172746012213558
    - type: nauc_mrr_at_3_diff1
      value: 18.218182689678475
    - type: nauc_mrr_at_3_max
      value: 23.57169444088667
    - type: nauc_mrr_at_3_std
      value: 10.464473559366356
    - type: nauc_mrr_at_5_diff1
      value: 18.6075342519133
    - type: nauc_mrr_at_5_max
      value: 23.308845973576673
    - type: nauc_mrr_at_5_std
      value: 9.364009996445652
    - type: nauc_ndcg_at_1000_diff1
      value: 16.327871824135745
    - type: nauc_ndcg_at_1000_max
      value: 23.308241052911495
    - type: nauc_ndcg_at_1000_std
      value: 11.50905911184097
    - type: nauc_ndcg_at_100_diff1
      value: 16.676226744692773
    - type: nauc_ndcg_at_100_max
      value: 24.323253721240974
    - type: nauc_ndcg_at_100_std
      value: 11.952612443651557
    - type: nauc_ndcg_at_10_diff1
      value: 16.030325121764594
    - type: nauc_ndcg_at_10_max
      value: 21.306799242079542
    - type: nauc_ndcg_at_10_std
      value: 6.63359364302513
    - type: nauc_ndcg_at_1_diff1
      value: 19.433947086968093
    - type: nauc_ndcg_at_1_max
      value: 24.75657047550517
    - type: nauc_ndcg_at_1_std
      value: 15.122329157218505
    - type: nauc_ndcg_at_20_diff1
      value: 16.013173605999857
    - type: nauc_ndcg_at_20_max
      value: 21.607217260736576
    - type: nauc_ndcg_at_20_std
      value: 7.319482417138996
    - type: nauc_ndcg_at_3_diff1
      value: 17.97958548328493
    - type: nauc_ndcg_at_3_max
      value: 23.58346522810145
    - type: nauc_ndcg_at_3_std
      value: 9.392582854708314
    - type: nauc_ndcg_at_5_diff1
      value: 18.734733324685287
    - type: nauc_ndcg_at_5_max
      value: 23.273244317623742
    - type: nauc_ndcg_at_5_std
      value: 7.638611545253834
    - type: nauc_precision_at_1000_diff1
      value: 7.919843339380295
    - type: nauc_precision_at_1000_max
      value: 31.575386234270486
    - type: nauc_precision_at_1000_std
      value: 39.332224386769404
    - type: nauc_precision_at_100_diff1
      value: 15.018050960000052
    - type: nauc_precision_at_100_max
      value: 34.98209513759861
    - type: nauc_precision_at_100_std
      value: 26.970034484359022
    - type: nauc_precision_at_10_diff1
      value: 12.102191084210922
    - type: nauc_precision_at_10_max
      value: 18.112541150340675
    - type: nauc_precision_at_10_std
      value: 0.7358784689406018
    - type: nauc_precision_at_1_diff1
      value: 19.433947086968093
    - type: nauc_precision_at_1_max
      value: 24.75657047550517
    - type: nauc_precision_at_1_std
      value: 15.122329157218505
    - type: nauc_precision_at_20_diff1
      value: 12.018814361204328
    - type: nauc_precision_at_20_max
      value: 19.75123746049928
    - type: nauc_precision_at_20_std
      value: 3.012204650582264
    - type: nauc_precision_at_3_diff1
      value: 17.41375604940955
    - type: nauc_precision_at_3_max
      value: 23.699834627021037
    - type: nauc_precision_at_3_std
      value: 6.793486779050103
    - type: nauc_precision_at_5_diff1
      value: 19.194631963780257
    - type: nauc_precision_at_5_max
      value: 23.31708702442155
    - type: nauc_precision_at_5_std
      value: 3.4591358279667332
    - type: nauc_recall_at_1000_diff1
      value: 7.919843339380378
    - type: nauc_recall_at_1000_max
      value: 31.57538623427063
    - type: nauc_recall_at_1000_std
      value: 39.332224386769546
    - type: nauc_recall_at_100_diff1
      value: 15.018050960000085
    - type: nauc_recall_at_100_max
      value: 34.9820951375986
    - type: nauc_recall_at_100_std
      value: 26.97003448435901
    - type: nauc_recall_at_10_diff1
      value: 12.102191084210837
    - type: nauc_recall_at_10_max
      value: 18.112541150340594
    - type: nauc_recall_at_10_std
      value: 0.7358784689405188
    - type: nauc_recall_at_1_diff1
      value: 19.433947086968093
    - type: nauc_recall_at_1_max
      value: 24.75657047550517
    - type: nauc_recall_at_1_std
      value: 15.122329157218505
    - type: nauc_recall_at_20_diff1
      value: 12.01881436120429
    - type: nauc_recall_at_20_max
      value: 19.751237460499222
    - type: nauc_recall_at_20_std
      value: 3.0122046505822135
    - type: nauc_recall_at_3_diff1
      value: 17.413756049409503
    - type: nauc_recall_at_3_max
      value: 23.699834627020998
    - type: nauc_recall_at_3_std
      value: 6.793486779050083
    - type: nauc_recall_at_5_diff1
      value: 19.194631963780203
    - type: nauc_recall_at_5_max
      value: 23.3170870244215
    - type: nauc_recall_at_5_std
      value: 3.459135827966664
    - type: ndcg_at_1
      value: 14.865
    - type: ndcg_at_10
      value: 24.764
    - type: ndcg_at_100
      value: 30.861
    - type: ndcg_at_1000
      value: 33.628
    - type: ndcg_at_20
      value: 27.078000000000003
    - type: ndcg_at_3
      value: 21.675
    - type: ndcg_at_5
      value: 23.148
    - type: precision_at_1
      value: 14.865
    - type: precision_at_10
      value: 3.4680000000000004
    - type: precision_at_100
      value: 0.644
    - type: precision_at_1000
      value: 0.087
    - type: precision_at_20
      value: 2.185
    - type: precision_at_3
      value: 8.709
    - type: precision_at_5
      value: 5.946
    - type: recall_at_1
      value: 14.865
    - type: recall_at_10
      value: 34.685
    - type: recall_at_100
      value: 64.414
    - type: recall_at_1000
      value: 86.937
    - type: recall_at_20
      value: 43.694
    - type: recall_at_3
      value: 26.125999999999998
    - type: recall_at_5
      value: 29.73
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB Banking77Classification (default)
      revision: 0fd18e25b25c072e09e0d92ab615fda904d66300
      split: test
      type: mteb/banking77
    metrics:
    - type: accuracy
      value: 84.08116883116882
    - type: f1
      value: 84.05587055990273
    - type: f1_weighted
      value: 84.05587055990274
    - type: main_score
      value: 84.08116883116882
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB BiorxivClusteringP2P (default)
      revision: 65b79d1d13f80053f67aca9498d9402c2d9f1f40
      split: test
      type: mteb/biorxiv-clustering-p2p
    metrics:
    - type: main_score
      value: 38.1941007822277
    - type: v_measure
      value: 38.1941007822277
    - type: v_measure_std
      value: 0.7502113547288178
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB BiorxivClusteringS2S (default)
      revision: 258694dd0231531bc1fd9de6ceb52a0853c6d908
      split: test
      type: mteb/biorxiv-clustering-s2s
    metrics:
    - type: main_score
      value: 34.42075599178318
    - type: v_measure
      value: 34.42075599178318
    - type: v_measure_std
      value: 0.600256720497283
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB BlurbsClusteringP2P (default)
      revision: a2dd5b02a77de3466a3eaa98ae586b5610314496
      split: test
      type: slvnwhrl/blurbs-clustering-p2p
    metrics:
    - type: main_score
      value: 41.634627363047265
    - type: v_measure
      value: 41.634627363047265
    - type: v_measure_std
      value: 9.726923191225307
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB BlurbsClusteringS2S (default)
      revision: 22793b6a6465bf00120ad525e38c51210858132c
      split: test
      type: slvnwhrl/blurbs-clustering-s2s
    metrics:
    - type: main_score
      value: 20.996468295584197
    - type: v_measure
      value: 20.996468295584197
    - type: v_measure_std
      value: 9.225766688272197
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB CBD (default)
      revision: 36ddb419bcffe6a5374c3891957912892916f28d
      split: test
      type: PL-MTEB/cbd
    metrics:
    - type: accuracy
      value: 69.99
    - type: ap
      value: 22.57826353116948
    - type: ap_weighted
      value: 22.57826353116948
    - type: f1
      value: 59.04574955548393
    - type: f1_weighted
      value: 74.36235022309789
    - type: main_score
      value: 69.99
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB CDSC-E (default)
      revision: 0a3d4aa409b22f80eb22cbf59b492637637b536d
      split: test
      type: PL-MTEB/cdsce-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 88.7
    - type: cosine_accuracy_threshold
      value: 97.37848043441772
    - type: cosine_ap
      value: 73.0405088928302
    - type: cosine_f1
      value: 63.52201257861635
    - type: cosine_f1_threshold
      value: 96.98888063430786
    - type: cosine_precision
      value: 78.90625
    - type: cosine_recall
      value: 53.1578947368421
    - type: dot_accuracy
      value: 84.89999999999999
    - type: dot_accuracy_threshold
      value: 43603.09753417969
    - type: dot_ap
      value: 56.98157569085279
    - type: dot_f1
      value: 57.606490872210955
    - type: dot_f1_threshold
      value: 40406.23779296875
    - type: dot_precision
      value: 46.864686468646866
    - type: dot_recall
      value: 74.73684210526315
    - type: euclidean_accuracy
      value: 88.5
    - type: euclidean_accuracy_threshold
      value: 498.0483055114746
    - type: euclidean_ap
      value: 72.97328234816734
    - type: euclidean_f1
      value: 63.722397476340696
    - type: euclidean_f1_threshold
      value: 508.6186408996582
    - type: euclidean_precision
      value: 79.52755905511812
    - type: euclidean_recall
      value: 53.1578947368421
    - type: main_score
      value: 73.0405088928302
    - type: manhattan_accuracy
      value: 88.6
    - type: manhattan_accuracy_threshold
      value: 12233.079528808594
    - type: manhattan_ap
      value: 72.92148503992615
    - type: manhattan_f1
      value: 63.69426751592356
    - type: manhattan_f1_threshold
      value: 12392.754364013672
    - type: manhattan_precision
      value: 80.64516129032258
    - type: manhattan_recall
      value: 52.63157894736842
    - type: max_accuracy
      value: 88.7
    - type: max_ap
      value: 73.0405088928302
    - type: max_f1
      value: 63.722397476340696
    - type: max_precision
      value: 80.64516129032258
    - type: max_recall
      value: 74.73684210526315
    - type: similarity_accuracy
      value: 88.7
    - type: similarity_accuracy_threshold
      value: 97.37848043441772
    - type: similarity_ap
      value: 73.0405088928302
    - type: similarity_f1
      value: 63.52201257861635
    - type: similarity_f1_threshold
      value: 96.98888063430786
    - type: similarity_precision
      value: 78.90625
    - type: similarity_recall
      value: 53.1578947368421
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB CDSC-R (default)
      revision: 1cd6abbb00df7d14be3dbd76a7dcc64b3a79a7cd
      split: test
      type: PL-MTEB/cdscr-sts
    metrics:
    - type: cosine_pearson
      value: 92.97492495289738
    - type: cosine_spearman
      value: 92.63248098608472
    - type: euclidean_pearson
      value: 92.04712487782031
    - type: euclidean_spearman
      value: 92.19679486755008
    - type: main_score
      value: 92.63248098608472
    - type: manhattan_pearson
      value: 92.0101187740438
    - type: manhattan_spearman
      value: 92.20926859332754
    - type: pearson
      value: 92.97492495289738
    - type: spearman
      value: 92.63248098608472
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB CLSClusteringP2P (default)
      revision: 4b6227591c6c1a73bc76b1055f3b7f3588e72476
      split: test
      type: C-MTEB/CLSClusteringP2P
    metrics:
    - type: main_score
      value: 39.96377851800628
    - type: v_measure
      value: 39.96377851800628
    - type: v_measure_std
      value: 0.9793033243093288
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB CLSClusteringS2S (default)
      revision: e458b3f5414b62b7f9f83499ac1f5497ae2e869f
      split: test
      type: C-MTEB/CLSClusteringS2S
    metrics:
    - type: main_score
      value: 38.788850224595784
    - type: v_measure
      value: 38.788850224595784
    - type: v_measure_std
      value: 1.0712604145916924
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB CMedQAv1
      revision: 8d7f1e942507dac42dc58017c1a001c3717da7df
      split: test
      type: C-MTEB/CMedQAv1-reranking
    metrics:
    - type: map
      value: 77.95952507806115
    - type: mrr
      value: 80.8643253968254
    - type: main_score
      value: 77.95952507806115
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB CMedQAv2
      revision: 23d186750531a14a0357ca22cd92d712fd512ea0
      split: test
      type: C-MTEB/CMedQAv2-reranking
    metrics:
    - type: map
      value: 78.21522500165045
    - type: mrr
      value: 81.28194444444443
    - type: main_score
      value: 78.21522500165045
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB CQADupstackAndroidRetrieval (default)
      revision: f46a197baaae43b4f621051089b82a364682dfeb
      split: test
      type: mteb/cqadupstack-android
    metrics:
    - type: map_at_1
      value: 33.377
    - type: map_at_10
      value: 46.371
    - type: map_at_100
      value: 47.829
    - type: map_at_1000
      value: 47.94
    - type: map_at_20
      value: 47.205000000000005
    - type: map_at_3
      value: 42.782
    - type: map_at_5
      value: 44.86
    - type: mrr_at_1
      value: 41.345
    - type: mrr_at_10
      value: 52.187
    - type: mrr_at_100
      value: 52.893
    - type: mrr_at_1000
      value: 52.929
    - type: mrr_at_20
      value: 52.637
    - type: mrr_at_3
      value: 49.714000000000006
    - type: mrr_at_5
      value: 51.373000000000005
    - type: ndcg_at_1
      value: 41.345
    - type: ndcg_at_10
      value: 52.946000000000005
    - type: ndcg_at_100
      value: 57.92699999999999
    - type: ndcg_at_1000
      value: 59.609
    - type: ndcg_at_20
      value: 54.900999999999996
    - type: ndcg_at_3
      value: 48.357
    - type: ndcg_at_5
      value: 50.739000000000004
    - type: precision_at_1
      value: 41.345
    - type: precision_at_10
      value: 10.186
    - type: precision_at_100
      value: 1.554
    - type: precision_at_1000
      value: 0.2
    - type: precision_at_20
      value: 5.959
    - type: precision_at_3
      value: 23.796
    - type: precision_at_5
      value: 17.024
    - type: recall_at_1
      value: 33.377
    - type: recall_at_10
      value: 65.067
    - type: recall_at_100
      value: 86.04899999999999
    - type: recall_at_1000
      value: 96.54899999999999
    - type: recall_at_20
      value: 72.071
    - type: recall_at_3
      value: 51.349999999999994
    - type: recall_at_5
      value: 58.41
    - type: main_score
      value: 52.946000000000005
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackEnglishRetrieval (default)
      revision: ad9991cb51e31e31e430383c75ffb2885547b5f0
      split: test
      type: mteb/cqadupstack-english
    metrics:
    - type: map_at_1
      value: 31.097
    - type: map_at_10
      value: 42.183
    - type: map_at_100
      value: 43.580999999999996
    - type: map_at_1000
      value: 43.718
    - type: map_at_20
      value: 42.921
    - type: map_at_3
      value: 38.963
    - type: map_at_5
      value: 40.815
    - type: mrr_at_1
      value: 39.745000000000005
    - type: mrr_at_10
      value: 48.736000000000004
    - type: mrr_at_100
      value: 49.405
    - type: mrr_at_1000
      value: 49.452
    - type: mrr_at_20
      value: 49.118
    - type: mrr_at_3
      value: 46.497
    - type: mrr_at_5
      value: 47.827999999999996
    - type: ndcg_at_1
      value: 39.745000000000005
    - type: ndcg_at_10
      value: 48.248000000000005
    - type: ndcg_at_100
      value: 52.956
    - type: ndcg_at_1000
      value: 54.99699999999999
    - type: ndcg_at_20
      value: 50.01
    - type: ndcg_at_3
      value: 43.946000000000005
    - type: ndcg_at_5
      value: 46.038000000000004
    - type: precision_at_1
      value: 39.745000000000005
    - type: precision_at_10
      value: 9.229
    - type: precision_at_100
      value: 1.5070000000000001
    - type: precision_at_1000
      value: 0.199
    - type: precision_at_20
      value: 5.489999999999999
    - type: precision_at_3
      value: 21.38
    - type: precision_at_5
      value: 15.274
    - type: recall_at_1
      value: 31.097
    - type: recall_at_10
      value: 58.617
    - type: recall_at_100
      value: 78.55199999999999
    - type: recall_at_1000
      value: 91.13900000000001
    - type: recall_at_20
      value: 64.92
    - type: recall_at_3
      value: 45.672000000000004
    - type: recall_at_5
      value: 51.669
    - type: main_score
      value: 48.248000000000005
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackGamingRetrieval (default)
      revision: 4885aa143210c98657558c04aaf3dc47cfb54340
      split: test
      type: mteb/cqadupstack-gaming
    metrics:
    - type: map_at_1
      value: 39.745000000000005
    - type: map_at_10
      value: 52.063
    - type: map_at_100
      value: 53.077
    - type: map_at_1000
      value: 53.13
    - type: map_at_20
      value: 52.66
    - type: map_at_3
      value: 48.662
    - type: map_at_5
      value: 50.507000000000005
    - type: mrr_at_1
      value: 45.391999999999996
    - type: mrr_at_10
      value: 55.528
    - type: mrr_at_100
      value: 56.16100000000001
    - type: mrr_at_1000
      value: 56.192
    - type: mrr_at_20
      value: 55.923
    - type: mrr_at_3
      value: 52.93600000000001
    - type: mrr_at_5
      value: 54.435
    - type: ndcg_at_1
      value: 45.391999999999996
    - type: ndcg_at_10
      value: 58.019
    - type: ndcg_at_100
      value: 61.936
    - type: ndcg_at_1000
      value: 63.015
    - type: ndcg_at_20
      value: 59.691
    - type: ndcg_at_3
      value: 52.294
    - type: ndcg_at_5
      value: 55.017
    - type: precision_at_1
      value: 45.391999999999996
    - type: precision_at_10
      value: 9.386
    - type: precision_at_100
      value: 1.232
    - type: precision_at_1000
      value: 0.136
    - type: precision_at_20
      value: 5.223
    - type: precision_at_3
      value: 23.177
    - type: precision_at_5
      value: 15.9
    - type: recall_at_1
      value: 39.745000000000005
    - type: recall_at_10
      value: 72.08099999999999
    - type: recall_at_100
      value: 88.85300000000001
    - type: recall_at_1000
      value: 96.569
    - type: recall_at_20
      value: 78.203
    - type: recall_at_3
      value: 56.957
    - type: recall_at_5
      value: 63.63100000000001
    - type: main_score
      value: 58.019
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackGisRetrieval (default)
      revision: 5003b3064772da1887988e05400cf3806fe491f2
      split: test
      type: mteb/cqadupstack-gis
    metrics:
    - type: map_at_1
      value: 26.651999999999997
    - type: map_at_10
      value: 35.799
    - type: map_at_100
      value: 36.846000000000004
    - type: map_at_1000
      value: 36.931000000000004
    - type: map_at_20
      value: 36.341
    - type: map_at_3
      value: 32.999
    - type: map_at_5
      value: 34.597
    - type: mrr_at_1
      value: 28.814
    - type: mrr_at_10
      value: 37.869
    - type: mrr_at_100
      value: 38.728
    - type: mrr_at_1000
      value: 38.795
    - type: mrr_at_20
      value: 38.317
    - type: mrr_at_3
      value: 35.235
    - type: mrr_at_5
      value: 36.738
    - type: ndcg_at_1
      value: 28.814
    - type: ndcg_at_10
      value: 41.028
    - type: ndcg_at_100
      value: 46.162
    - type: ndcg_at_1000
      value: 48.15
    - type: ndcg_at_20
      value: 42.824
    - type: ndcg_at_3
      value: 35.621
    - type: ndcg_at_5
      value: 38.277
    - type: precision_at_1
      value: 28.814
    - type: precision_at_10
      value: 6.361999999999999
    - type: precision_at_100
      value: 0.9450000000000001
    - type: precision_at_1000
      value: 0.11399999999999999
    - type: precision_at_20
      value: 3.6159999999999997
    - type: precision_at_3
      value: 15.140999999999998
    - type: precision_at_5
      value: 10.712000000000002
    - type: recall_at_1
      value: 26.651999999999997
    - type: recall_at_10
      value: 55.038
    - type: recall_at_100
      value: 78.806
    - type: recall_at_1000
      value: 93.485
    - type: recall_at_20
      value: 61.742
    - type: recall_at_3
      value: 40.682
    - type: recall_at_5
      value: 46.855000000000004
    - type: main_score
      value: 41.028
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackMathematicaRetrieval (default)
      revision: 90fceea13679c63fe563ded68f3b6f06e50061de
      split: test
      type: mteb/cqadupstack-mathematica
    metrics:
    - type: map_at_1
      value: 17.627000000000002
    - type: map_at_10
      value: 26.436999999999998
    - type: map_at_100
      value: 27.85
    - type: map_at_1000
      value: 27.955999999999996
    - type: map_at_20
      value: 27.233
    - type: map_at_3
      value: 23.777
    - type: map_at_5
      value: 25.122
    - type: mrr_at_1
      value: 22.387999999999998
    - type: mrr_at_10
      value: 31.589
    - type: mrr_at_100
      value: 32.641999999999996
    - type: mrr_at_1000
      value: 32.696999999999996
    - type: mrr_at_20
      value: 32.201
    - type: mrr_at_3
      value: 28.98
    - type: mrr_at_5
      value: 30.342000000000002
    - type: ndcg_at_1
      value: 22.387999999999998
    - type: ndcg_at_10
      value: 32.129999999999995
    - type: ndcg_at_100
      value: 38.562999999999995
    - type: ndcg_at_1000
      value: 40.903
    - type: ndcg_at_20
      value: 34.652
    - type: ndcg_at_3
      value: 27.26
    - type: ndcg_at_5
      value: 29.235
    - type: precision_at_1
      value: 22.387999999999998
    - type: precision_at_10
      value: 5.970000000000001
    - type: precision_at_100
      value: 1.068
    - type: precision_at_1000
      value: 0.13899999999999998
    - type: precision_at_20
      value: 3.6999999999999997
    - type: precision_at_3
      value: 13.267000000000001
    - type: precision_at_5
      value: 9.403
    - type: recall_at_1
      value: 17.627000000000002
    - type: recall_at_10
      value: 44.71
    - type: recall_at_100
      value: 72.426
    - type: recall_at_1000
      value: 88.64699999999999
    - type: recall_at_20
      value: 53.65
    - type: recall_at_3
      value: 30.989
    - type: recall_at_5
      value: 36.237
    - type: main_score
      value: 32.129999999999995
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackPhysicsRetrieval (default)
      revision: 79531abbd1fb92d06c6d6315a0cbbbf5bb247ea4
      split: test
      type: mteb/cqadupstack-physics
    metrics:
    - type: map_at_1
      value: 30.891000000000002
    - type: map_at_10
      value: 41.519
    - type: map_at_100
      value: 42.896
    - type: map_at_1000
      value: 42.992999999999995
    - type: map_at_20
      value: 42.287
    - type: map_at_3
      value: 37.822
    - type: map_at_5
      value: 39.976
    - type: mrr_at_1
      value: 37.921
    - type: mrr_at_10
      value: 47.260999999999996
    - type: mrr_at_100
      value: 48.044
    - type: mrr_at_1000
      value: 48.08
    - type: mrr_at_20
      value: 47.699999999999996
    - type: mrr_at_3
      value: 44.513999999999996
    - type: mrr_at_5
      value: 46.064
    - type: ndcg_at_1
      value: 37.921
    - type: ndcg_at_10
      value: 47.806
    - type: ndcg_at_100
      value: 53.274
    - type: ndcg_at_1000
      value: 55.021
    - type: ndcg_at_20
      value: 49.973
    - type: ndcg_at_3
      value: 42.046
    - type: ndcg_at_5
      value: 44.835
    - type: precision_at_1
      value: 37.921
    - type: precision_at_10
      value: 8.767999999999999
    - type: precision_at_100
      value: 1.353
    - type: precision_at_1000
      value: 0.168
    - type: precision_at_20
      value: 5.135
    - type: precision_at_3
      value: 20.051
    - type: precision_at_5
      value: 14.398
    - type: recall_at_1
      value: 30.891000000000002
    - type: recall_at_10
      value: 60.897999999999996
    - type: recall_at_100
      value: 83.541
    - type: recall_at_1000
      value: 94.825
    - type: recall_at_20
      value: 68.356
    - type: recall_at_3
      value: 44.65
    - type: recall_at_5
      value: 51.919000000000004
    - type: main_score
      value: 47.806
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackProgrammersRetrieval (default)
      revision: 6184bc1440d2dbc7612be22b50686b8826d22b32
      split: test
      type: mteb/cqadupstack-programmers
    metrics:
    - type: map_at_1
      value: 27.654
    - type: map_at_10
      value: 38.025999999999996
    - type: map_at_100
      value: 39.425
    - type: map_at_1000
      value: 39.528
    - type: map_at_20
      value: 38.838
    - type: map_at_3
      value: 34.745
    - type: map_at_5
      value: 36.537
    - type: mrr_at_1
      value: 34.018
    - type: mrr_at_10
      value: 43.314
    - type: mrr_at_100
      value: 44.283
    - type: mrr_at_1000
      value: 44.327
    - type: mrr_at_20
      value: 43.929
    - type: mrr_at_3
      value: 40.868
    - type: mrr_at_5
      value: 42.317
    - type: ndcg_at_1
      value: 34.018
    - type: ndcg_at_10
      value: 43.887
    - type: ndcg_at_100
      value: 49.791000000000004
    - type: ndcg_at_1000
      value: 51.834
    - type: ndcg_at_20
      value: 46.376
    - type: ndcg_at_3
      value: 38.769999999999996
    - type: ndcg_at_5
      value: 41.144
    - type: precision_at_1
      value: 34.018
    - type: precision_at_10
      value: 8.001999999999999
    - type: precision_at_100
      value: 1.2630000000000001
    - type: precision_at_1000
      value: 0.16
    - type: precision_at_20
      value: 4.737
    - type: precision_at_3
      value: 18.417
    - type: precision_at_5
      value: 13.150999999999998
    - type: recall_at_1
      value: 27.654
    - type: recall_at_10
      value: 56.111
    - type: recall_at_100
      value: 81.136
    - type: recall_at_1000
      value: 94.788
    - type: recall_at_20
      value: 65.068
    - type: recall_at_3
      value: 41.713
    - type: recall_at_5
      value: 48.106
    - type: main_score
      value: 43.887
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackRetrieval (default)
      revision: CQADupstackRetrieval_is_a_combined_dataset
      split: test
      type: CQADupstackRetrieval_is_a_combined_dataset
    metrics:
    - type: main_score
      value: 42.58858333333333
    - type: ndcg_at_10
      value: 42.58858333333333
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackStatsRetrieval (default)
      revision: 65ac3a16b8e91f9cee4c9828cc7c335575432a2a
      split: test
      type: mteb/cqadupstack-stats
    metrics:
    - type: map_at_1
      value: 24.501
    - type: map_at_10
      value: 32.814
    - type: map_at_100
      value: 33.754
    - type: map_at_1000
      value: 33.859
    - type: map_at_20
      value: 33.324
    - type: map_at_3
      value: 30.758000000000003
    - type: map_at_5
      value: 31.936999999999998
    - type: mrr_at_1
      value: 27.761000000000003
    - type: mrr_at_10
      value: 35.662
    - type: mrr_at_100
      value: 36.443999999999996
    - type: mrr_at_1000
      value: 36.516999999999996
    - type: mrr_at_20
      value: 36.085
    - type: mrr_at_3
      value: 33.742
    - type: mrr_at_5
      value: 34.931
    - type: ndcg_at_1
      value: 27.761000000000003
    - type: ndcg_at_10
      value: 37.208000000000006
    - type: ndcg_at_100
      value: 41.839
    - type: ndcg_at_1000
      value: 44.421
    - type: ndcg_at_20
      value: 38.917
    - type: ndcg_at_3
      value: 33.544000000000004
    - type: ndcg_at_5
      value: 35.374
    - type: precision_at_1
      value: 27.761000000000003
    - type: precision_at_10
      value: 5.92
    - type: precision_at_100
      value: 0.899
    - type: precision_at_1000
      value: 0.12
    - type: precision_at_20
      value: 3.4130000000000003
    - type: precision_at_3
      value: 15.031
    - type: precision_at_5
      value: 10.306999999999999
    - type: recall_at_1
      value: 24.501
    - type: recall_at_10
      value: 47.579
    - type: recall_at_100
      value: 69.045
    - type: recall_at_1000
      value: 88.032
    - type: recall_at_20
      value: 54.125
    - type: recall_at_3
      value: 37.202
    - type: recall_at_5
      value: 41.927
    - type: main_score
      value: 37.208000000000006
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackTexRetrieval (default)
      revision: 46989137a86843e03a6195de44b09deda022eec7
      split: test
      type: mteb/cqadupstack-tex
    metrics:
    - type: map_at_1
      value: 18.29
    - type: map_at_10
      value: 26.183
    - type: map_at_100
      value: 27.351999999999997
    - type: map_at_1000
      value: 27.483999999999998
    - type: map_at_20
      value: 26.798
    - type: map_at_3
      value: 23.629
    - type: map_at_5
      value: 24.937
    - type: mrr_at_1
      value: 22.299
    - type: mrr_at_10
      value: 30.189
    - type: mrr_at_100
      value: 31.098
    - type: mrr_at_1000
      value: 31.177
    - type: mrr_at_20
      value: 30.697000000000003
    - type: mrr_at_3
      value: 27.862
    - type: mrr_at_5
      value: 29.066
    - type: ndcg_at_1
      value: 22.299
    - type: ndcg_at_10
      value: 31.202
    - type: ndcg_at_100
      value: 36.617
    - type: ndcg_at_1000
      value: 39.544000000000004
    - type: ndcg_at_20
      value: 33.177
    - type: ndcg_at_3
      value: 26.639000000000003
    - type: ndcg_at_5
      value: 28.526
    - type: precision_at_1
      value: 22.299
    - type: precision_at_10
      value: 5.8020000000000005
    - type: precision_at_100
      value: 1.0070000000000001
    - type: precision_at_1000
      value: 0.14400000000000002
    - type: precision_at_20
      value: 3.505
    - type: precision_at_3
      value: 12.698
    - type: precision_at_5
      value: 9.174
    - type: recall_at_1
      value: 18.29
    - type: recall_at_10
      value: 42.254999999999995
    - type: recall_at_100
      value: 66.60000000000001
    - type: recall_at_1000
      value: 87.31400000000001
    - type: recall_at_20
      value: 49.572
    - type: recall_at_3
      value: 29.342000000000002
    - type: recall_at_5
      value: 34.221000000000004
    - type: main_score
      value: 31.202
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackUnixRetrieval (default)
      revision: 6c6430d3a6d36f8d2a829195bc5dc94d7e063e53
      split: test
      type: mteb/cqadupstack-unix
    metrics:
    - type: map_at_1
      value: 27.722
    - type: map_at_10
      value: 37.698
    - type: map_at_100
      value: 38.899
    - type: map_at_1000
      value: 38.998
    - type: map_at_20
      value: 38.381
    - type: map_at_3
      value: 34.244
    - type: map_at_5
      value: 36.295
    - type: mrr_at_1
      value: 32.183
    - type: mrr_at_10
      value: 41.429
    - type: mrr_at_100
      value: 42.308
    - type: mrr_at_1000
      value: 42.358000000000004
    - type: mrr_at_20
      value: 41.957
    - type: mrr_at_3
      value: 38.401999999999994
    - type: mrr_at_5
      value: 40.294999999999995
    - type: ndcg_at_1
      value: 32.183
    - type: ndcg_at_10
      value: 43.519000000000005
    - type: ndcg_at_100
      value: 48.786
    - type: ndcg_at_1000
      value: 50.861999999999995
    - type: ndcg_at_20
      value: 45.654
    - type: ndcg_at_3
      value: 37.521
    - type: ndcg_at_5
      value: 40.615
    - type: precision_at_1
      value: 32.183
    - type: precision_at_10
      value: 7.603
    - type: precision_at_100
      value: 1.135
    - type: precision_at_1000
      value: 0.14200000000000002
    - type: precision_at_20
      value: 4.408
    - type: precision_at_3
      value: 17.071
    - type: precision_at_5
      value: 12.668
    - type: recall_at_1
      value: 27.722
    - type: recall_at_10
      value: 57.230000000000004
    - type: recall_at_100
      value: 79.97999999999999
    - type: recall_at_1000
      value: 94.217
    - type: recall_at_20
      value: 64.864
    - type: recall_at_3
      value: 41.215
    - type: recall_at_5
      value: 48.774
    - type: main_score
      value: 43.519000000000005
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackWebmastersRetrieval (default)
      revision: 160c094312a0e1facb97e55eeddb698c0abe3571
      split: test
      type: mteb/cqadupstack-webmasters
    metrics:
    - type: map_at_1
      value: 25.852999999999998
    - type: map_at_10
      value: 35.394999999999996
    - type: map_at_100
      value: 37.291999999999994
    - type: map_at_1000
      value: 37.495
    - type: map_at_20
      value: 36.372
    - type: map_at_3
      value: 32.336
    - type: map_at_5
      value: 34.159
    - type: mrr_at_1
      value: 31.818
    - type: mrr_at_10
      value: 40.677
    - type: mrr_at_100
      value: 41.728
    - type: mrr_at_1000
      value: 41.778
    - type: mrr_at_20
      value: 41.301
    - type: mrr_at_3
      value: 38.208
    - type: mrr_at_5
      value: 39.592
    - type: ndcg_at_1
      value: 31.818
    - type: ndcg_at_10
      value: 41.559000000000005
    - type: ndcg_at_100
      value: 48.012
    - type: ndcg_at_1000
      value: 50.234
    - type: ndcg_at_20
      value: 44.15
    - type: ndcg_at_3
      value: 36.918
    - type: ndcg_at_5
      value: 39.227000000000004
    - type: precision_at_1
      value: 31.818
    - type: precision_at_10
      value: 8.043
    - type: precision_at_100
      value: 1.625
    - type: precision_at_1000
      value: 0.245
    - type: precision_at_20
      value: 5.2170000000000005
    - type: precision_at_3
      value: 17.655
    - type: precision_at_5
      value: 12.845999999999998
    - type: recall_at_1
      value: 25.852999999999998
    - type: recall_at_10
      value: 53.093
    - type: recall_at_100
      value: 81.05799999999999
    - type: recall_at_1000
      value: 94.657
    - type: recall_at_20
      value: 62.748000000000005
    - type: recall_at_3
      value: 39.300000000000004
    - type: recall_at_5
      value: 45.754
    - type: main_score
      value: 41.559000000000005
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CQADupstackWordpressRetrieval (default)
      revision: 4ffe81d471b1924886b33c7567bfb200e9eec5c4
      split: test
      type: mteb/cqadupstack-wordpress
    metrics:
    - type: map_at_1
      value: 19.23
    - type: map_at_10
      value: 28.128999999999998
    - type: map_at_100
      value: 29.195
    - type: map_at_1000
      value: 29.310000000000002
    - type: map_at_20
      value: 28.713
    - type: map_at_3
      value: 25.191000000000003
    - type: map_at_5
      value: 26.69
    - type: mrr_at_1
      value: 21.257
    - type: mrr_at_10
      value: 30.253999999999998
    - type: mrr_at_100
      value: 31.195
    - type: mrr_at_1000
      value: 31.270999999999997
    - type: mrr_at_20
      value: 30.747999999999998
    - type: mrr_at_3
      value: 27.633999999999997
    - type: mrr_at_5
      value: 28.937
    - type: ndcg_at_1
      value: 21.257
    - type: ndcg_at_10
      value: 33.511
    - type: ndcg_at_100
      value: 38.733000000000004
    - type: ndcg_at_1000
      value: 41.489
    - type: ndcg_at_20
      value: 35.476
    - type: ndcg_at_3
      value: 27.845
    - type: ndcg_at_5
      value: 30.264999999999997
    - type: precision_at_1
      value: 21.257
    - type: precision_at_10
      value: 5.619
    - type: precision_at_100
      value: 0.893
    - type: precision_at_1000
      value: 0.124
    - type: precision_at_20
      value: 3.29
    - type: precision_at_3
      value: 12.508
    - type: precision_at_5
      value: 8.946
    - type: recall_at_1
      value: 19.23
    - type: recall_at_10
      value: 48.185
    - type: recall_at_100
      value: 71.932
    - type: recall_at_1000
      value: 92.587
    - type: recall_at_20
      value: 55.533
    - type: recall_at_3
      value: 32.865
    - type: recall_at_5
      value: 38.577
    - type: main_score
      value: 33.511
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB ClimateFEVER (default)
      revision: 47f2ac6acb640fc46020b02a5b59fdda04d39380
      split: test
      type: mteb/climate-fever
    metrics:
    - type: map_at_1
      value: 19.594
    - type: map_at_10
      value: 32.519
    - type: map_at_100
      value: 34.1
    - type: map_at_1000
      value: 34.263
    - type: map_at_20
      value: 33.353
    - type: map_at_3
      value: 27.898
    - type: map_at_5
      value: 30.524
    - type: mrr_at_1
      value: 46.515
    - type: mrr_at_10
      value: 56.958
    - type: mrr_at_100
      value: 57.54899999999999
    - type: mrr_at_1000
      value: 57.574999999999996
    - type: mrr_at_20
      value: 57.315000000000005
    - type: mrr_at_3
      value: 54.852999999999994
    - type: mrr_at_5
      value: 56.153
    - type: ndcg_at_1
      value: 46.515
    - type: ndcg_at_10
      value: 42.363
    - type: ndcg_at_100
      value: 48.233
    - type: ndcg_at_1000
      value: 50.993
    - type: ndcg_at_20
      value: 44.533
    - type: ndcg_at_3
      value: 37.297000000000004
    - type: ndcg_at_5
      value: 38.911
    - type: precision_at_1
      value: 46.515
    - type: precision_at_10
      value: 12.520999999999999
    - type: precision_at_100
      value: 1.8980000000000001
    - type: precision_at_1000
      value: 0.242
    - type: precision_at_20
      value: 7.212000000000001
    - type: precision_at_3
      value: 27.752
    - type: precision_at_5
      value: 20.391000000000002
    - type: recall_at_1
      value: 19.594
    - type: recall_at_10
      value: 46.539
    - type: recall_at_100
      value: 66.782
    - type: recall_at_1000
      value: 82.049
    - type: recall_at_20
      value: 52.611
    - type: recall_at_3
      value: 32.528
    - type: recall_at_5
      value: 38.933
    - type: main_score
      value: 42.363
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB CmedqaRetrieval (default)
      revision: cd540c506dae1cf9e9a59c3e06f42030d54e7301
      split: dev
      type: C-MTEB/CmedqaRetrieval
    metrics:
    - type: main_score
      value: 35.927
    - type: map_at_1
      value: 20.144000000000002
    - type: map_at_10
      value: 29.94
    - type: map_at_100
      value: 31.630000000000003
    - type: map_at_1000
      value: 31.778000000000002
    - type: map_at_20
      value: 30.798
    - type: map_at_3
      value: 26.534999999999997
    - type: map_at_5
      value: 28.33
    - type: mrr_at_1
      value: 31.23280820205051
    - type: mrr_at_10
      value: 38.66781179421835
    - type: mrr_at_100
      value: 39.656936166081785
    - type: mrr_at_1000
      value: 39.724602893117414
    - type: mrr_at_20
      value: 39.21272461558451
    - type: mrr_at_3
      value: 36.30907726931729
    - type: mrr_at_5
      value: 37.59814953738436
    - type: nauc_map_at_1000_diff1
      value: 44.5755334437146
    - type: nauc_map_at_1000_max
      value: 40.726916781400746
    - type: nauc_map_at_1000_std
      value: -19.591835061497367
    - type: nauc_map_at_100_diff1
      value: 44.54542899921038
    - type: nauc_map_at_100_max
      value: 40.68305902532837
    - type: nauc_map_at_100_std
      value: -19.658902089283487
    - type: nauc_map_at_10_diff1
      value: 44.56110529630953
    - type: nauc_map_at_10_max
      value: 39.89826167846008
    - type: nauc_map_at_10_std
      value: -20.62910633667902
    - type: nauc_map_at_1_diff1
      value: 50.82120107004449
    - type: nauc_map_at_1_max
      value: 33.208851367861584
    - type: nauc_map_at_1_std
      value: -20.29409730258174
    - type: nauc_map_at_20_diff1
      value: 44.51171242433788
    - type: nauc_map_at_20_max
      value: 40.30431132782945
    - type: nauc_map_at_20_std
      value: -20.290524142792417
    - type: nauc_map_at_3_diff1
      value: 45.80394138665133
    - type: nauc_map_at_3_max
      value: 37.766191281426956
    - type: nauc_map_at_3_std
      value: -21.223601997333876
    - type: nauc_map_at_5_diff1
      value: 45.00457218474283
    - type: nauc_map_at_5_max
      value: 38.901044576388365
    - type: nauc_map_at_5_std
      value: -20.893069613941634
    - type: nauc_mrr_at_1000_diff1
      value: 50.09855359231429
    - type: nauc_mrr_at_1000_max
      value: 46.481000170008826
    - type: nauc_mrr_at_1000_std
      value: -16.053461377096102
    - type: nauc_mrr_at_100_diff1
      value: 50.08205026347746
    - type: nauc_mrr_at_100_max
      value: 46.47262126963331
    - type: nauc_mrr_at_100_std
      value: -16.049112778748693
    - type: nauc_mrr_at_10_diff1
      value: 50.02363239081706
    - type: nauc_mrr_at_10_max
      value: 46.39287859062042
    - type: nauc_mrr_at_10_std
      value: -16.280866744769657
    - type: nauc_mrr_at_1_diff1
      value: 55.692503735317445
    - type: nauc_mrr_at_1_max
      value: 47.334834529801014
    - type: nauc_mrr_at_1_std
      value: -16.985483585693512
    - type: nauc_mrr_at_20_diff1
      value: 50.07725225722074
    - type: nauc_mrr_at_20_max
      value: 46.47279295070193
    - type: nauc_mrr_at_20_std
      value: -16.15168364678318
    - type: nauc_mrr_at_3_diff1
      value: 51.18685337274134
    - type: nauc_mrr_at_3_max
      value: 46.7286365021621
    - type: nauc_mrr_at_3_std
      value: -16.708451287313718
    - type: nauc_mrr_at_5_diff1
      value: 50.46777237893576
    - type: nauc_mrr_at_5_max
      value: 46.5352076502249
    - type: nauc_mrr_at_5_std
      value: -16.557413659905034
    - type: nauc_ndcg_at_1000_diff1
      value: 43.974299434438066
    - type: nauc_ndcg_at_1000_max
      value: 43.44628675071857
    - type: nauc_ndcg_at_1000_std
      value: -15.3495102005021
    - type: nauc_ndcg_at_100_diff1
      value: 43.336365081508504
    - type: nauc_ndcg_at_100_max
      value: 43.11345604460776
    - type: nauc_ndcg_at_100_std
      value: -15.571128070860615
    - type: nauc_ndcg_at_10_diff1
      value: 43.41266214720136
    - type: nauc_ndcg_at_10_max
      value: 41.519676787851914
    - type: nauc_ndcg_at_10_std
      value: -19.217175017223568
    - type: nauc_ndcg_at_1_diff1
      value: 55.692503735317445
    - type: nauc_ndcg_at_1_max
      value: 47.334834529801014
    - type: nauc_ndcg_at_1_std
      value: -16.985483585693512
    - type: nauc_ndcg_at_20_diff1
      value: 43.351653862834496
    - type: nauc_ndcg_at_20_max
      value: 42.11608469750499
    - type: nauc_ndcg_at_20_std
      value: -18.485363540641664
    - type: nauc_ndcg_at_3_diff1
      value: 45.64193888236677
    - type: nauc_ndcg_at_3_max
      value: 42.497135099009995
    - type: nauc_ndcg_at_3_std
      value: -18.764012041130094
    - type: nauc_ndcg_at_5_diff1
      value: 44.523392133895186
    - type: nauc_ndcg_at_5_max
      value: 41.564242030096345
    - type: nauc_ndcg_at_5_std
      value: -19.31080790984941
    - type: nauc_precision_at_1000_diff1
      value: 6.383464615714393
    - type: nauc_precision_at_1000_max
      value: 27.439930931284657
    - type: nauc_precision_at_1000_std
      value: 19.070716188143034
    - type: nauc_precision_at_100_diff1
      value: 12.599136754501284
    - type: nauc_precision_at_100_max
      value: 35.886310962337795
    - type: nauc_precision_at_100_std
      value: 14.06587592659196
    - type: nauc_precision_at_10_diff1
      value: 25.388891173150206
    - type: nauc_precision_at_10_max
      value: 46.10269270777384
    - type: nauc_precision_at_10_std
      value: -5.993803607158499
    - type: nauc_precision_at_1_diff1
      value: 55.692503735317445
    - type: nauc_precision_at_1_max
      value: 47.334834529801014
    - type: nauc_precision_at_1_std
      value: -16.985483585693512
    - type: nauc_precision_at_20_diff1
      value: 20.984013463099707
    - type: nauc_precision_at_20_max
      value: 42.9471854616888
    - type: nauc_precision_at_20_std
      value: -0.8045549929346024
    - type: nauc_precision_at_3_diff1
      value: 36.191850547148356
    - type: nauc_precision_at_3_max
      value: 48.09923832376049
    - type: nauc_precision_at_3_std
      value: -13.159407051271321
    - type: nauc_precision_at_5_diff1
      value: 31.04967966700407
    - type: nauc_precision_at_5_max
      value: 47.62867673349624
    - type: nauc_precision_at_5_std
      value: -10.345790325137353
    - type: nauc_recall_at_1000_diff1
      value: 11.03436839065707
    - type: nauc_recall_at_1000_max
      value: 42.32265076651575
    - type: nauc_recall_at_1000_std
      value: 30.478521053399206
    - type: nauc_recall_at_100_diff1
      value: 24.788349084510806
    - type: nauc_recall_at_100_max
      value: 36.72097184821956
    - type: nauc_recall_at_100_std
      value: -0.2241144179522076
    - type: nauc_recall_at_10_diff1
      value: 31.613053567704885
    - type: nauc_recall_at_10_max
      value: 34.4597322828833
    - type: nauc_recall_at_10_std
      value: -18.00022912690819
    - type: nauc_recall_at_1_diff1
      value: 50.82120107004449
    - type: nauc_recall_at_1_max
      value: 33.208851367861584
    - type: nauc_recall_at_1_std
      value: -20.29409730258174
    - type: nauc_recall_at_20_diff1
      value: 30.277002670708384
    - type: nauc_recall_at_20_max
      value: 35.212475675060375
    - type: nauc_recall_at_20_std
      value: -15.822788854733687
    - type: nauc_recall_at_3_diff1
      value: 38.87844958322257
    - type: nauc_recall_at_3_max
      value: 34.66914910044104
    - type: nauc_recall_at_3_std
      value: -20.234707300209127
    - type: nauc_recall_at_5_diff1
      value: 35.551139991687776
    - type: nauc_recall_at_5_max
      value: 34.61009958820695
    - type: nauc_recall_at_5_std
      value: -19.519180149293444
    - type: ndcg_at_1
      value: 31.233
    - type: ndcg_at_10
      value: 35.927
    - type: ndcg_at_100
      value: 43.037
    - type: ndcg_at_1000
      value: 45.900999999999996
    - type: ndcg_at_20
      value: 38.39
    - type: ndcg_at_3
      value: 31.366
    - type: ndcg_at_5
      value: 33.108
    - type: precision_at_1
      value: 31.233
    - type: precision_at_10
      value: 8.15
    - type: precision_at_100
      value: 1.402
    - type: precision_at_1000
      value: 0.17700000000000002
    - type: precision_at_20
      value: 4.91
    - type: precision_at_3
      value: 17.871000000000002
    - type: precision_at_5
      value: 12.948
    - type: recall_at_1
      value: 20.144000000000002
    - type: recall_at_10
      value: 44.985
    - type: recall_at_100
      value: 74.866
    - type: recall_at_1000
      value: 94.477
    - type: recall_at_20
      value: 53.37
    - type: recall_at_3
      value: 31.141000000000002
    - type: recall_at_5
      value: 36.721
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB Cmnli (default)
      revision: None
      split: validation
      type: C-MTEB/CMNLI
    metrics:
    - type: cos_sim_accuracy
      value: 71.25676488274203
    - type: cos_sim_accuracy_threshold
      value: 78.11152935028076
    - type: cos_sim_ap
      value: 79.10444825556077
    - type: cos_sim_f1
      value: 74.10750923266312
    - type: cos_sim_f1_threshold
      value: 75.2312421798706
    - type: cos_sim_precision
      value: 66.02083714129044
    - type: cos_sim_recall
      value: 84.45171849427169
    - type: dot_accuracy
      value: 68.11785929043896
    - type: dot_accuracy_threshold
      value: 34783.23974609375
    - type: dot_ap
      value: 75.80201827987712
    - type: dot_f1
      value: 72.31670990679349
    - type: dot_f1_threshold
      value: 31978.036499023438
    - type: dot_precision
      value: 61.386623164763456
    - type: dot_recall
      value: 87.98223053542202
    - type: euclidean_accuracy
      value: 71.41310883944678
    - type: euclidean_accuracy_threshold
      value: 1374.9353408813477
    - type: euclidean_ap
      value: 79.23359768836457
    - type: euclidean_f1
      value: 74.38512297540491
    - type: euclidean_f1_threshold
      value: 1512.6035690307617
    - type: euclidean_precision
      value: 64.97816593886463
    - type: euclidean_recall
      value: 86.97685293429974
    - type: manhattan_accuracy
      value: 71.32892363199038
    - type: manhattan_accuracy_threshold
      value: 33340.49072265625
    - type: manhattan_ap
      value: 79.11973684118587
    - type: manhattan_f1
      value: 74.29401993355481
    - type: manhattan_f1_threshold
      value: 36012.52746582031
    - type: manhattan_precision
      value: 66.81605975723622
    - type: manhattan_recall
      value: 83.65676876315175
    - type: max_accuracy
      value: 71.41310883944678
    - type: max_ap
      value: 79.23359768836457
    - type: max_f1
      value: 74.38512297540491
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB CovidRetrieval (default)
      revision: 1271c7809071a13532e05f25fb53511ffce77117
      split: dev
      type: C-MTEB/CovidRetrieval
    metrics:
    - type: main_score
      value: 78.917
    - type: map_at_1
      value: 67.281
    - type: map_at_10
      value: 75.262
    - type: map_at_100
      value: 75.60900000000001
    - type: map_at_1000
      value: 75.618
    - type: map_at_20
      value: 75.50200000000001
    - type: map_at_3
      value: 73.455
    - type: map_at_5
      value: 74.657
    - type: mrr_at_1
      value: 67.43940990516333
    - type: mrr_at_10
      value: 75.27367989696756
    - type: mrr_at_100
      value: 75.62029353306437
    - type: mrr_at_1000
      value: 75.62934741874726
    - type: mrr_at_20
      value: 75.51356607409173
    - type: mrr_at_3
      value: 73.5159817351598
    - type: mrr_at_5
      value: 74.73832103969093
    - type: nauc_map_at_1000_diff1
      value: 77.26666391867634
    - type: nauc_map_at_1000_max
      value: 49.928541012203496
    - type: nauc_map_at_1000_std
      value: -40.494469470474456
    - type: nauc_map_at_100_diff1
      value: 77.26087423162396
    - type: nauc_map_at_100_max
      value: 49.944275615664424
    - type: nauc_map_at_100_std
      value: -40.48299992715398
    - type: nauc_map_at_10_diff1
      value: 76.97400113500906
    - type: nauc_map_at_10_max
      value: 49.84177029115674
    - type: nauc_map_at_10_std
      value: -40.829250876511445
    - type: nauc_map_at_1_diff1
      value: 81.44050620630395
    - type: nauc_map_at_1_max
      value: 48.97711944070578
    - type: nauc_map_at_1_std
      value: -38.963689457570254
    - type: nauc_map_at_20_diff1
      value: 77.21791353089375
    - type: nauc_map_at_20_max
      value: 49.958206759079424
    - type: nauc_map_at_20_std
      value: -40.53067571658996
    - type: nauc_map_at_3_diff1
      value: 77.3555925208868
    - type: nauc_map_at_3_max
      value: 49.32158146451256
    - type: nauc_map_at_3_std
      value: -41.93552426981978
    - type: nauc_map_at_5_diff1
      value: 77.07099950431504
    - type: nauc_map_at_5_max
      value: 49.54190504495002
    - type: nauc_map_at_5_std
      value: -41.814968130918096
    - type: nauc_mrr_at_1000_diff1
      value: 77.31388774540477
    - type: nauc_mrr_at_1000_max
      value: 49.96779699175759
    - type: nauc_mrr_at_1000_std
      value: -40.43739645160277
    - type: nauc_mrr_at_100_diff1
      value: 77.30817786449413
    - type: nauc_mrr_at_100_max
      value: 49.982514428937655
    - type: nauc_mrr_at_100_std
      value: -40.42876582797744
    - type: nauc_mrr_at_10_diff1
      value: 77.02048060465756
    - type: nauc_mrr_at_10_max
      value: 49.87937207270602
    - type: nauc_mrr_at_10_std
      value: -40.77596560333177
    - type: nauc_mrr_at_1_diff1
      value: 81.27219599516599
    - type: nauc_mrr_at_1_max
      value: 49.3083394026327
    - type: nauc_mrr_at_1_std
      value: -38.31023037552026
    - type: nauc_mrr_at_20_diff1
      value: 77.26497089316055
    - type: nauc_mrr_at_20_max
      value: 49.996257597621415
    - type: nauc_mrr_at_20_std
      value: -40.476723608868014
    - type: nauc_mrr_at_3_diff1
      value: 77.38971294099257
    - type: nauc_mrr_at_3_max
      value: 49.38110328987404
    - type: nauc_mrr_at_3_std
      value: -41.7118646715979
    - type: nauc_mrr_at_5_diff1
      value: 77.08286142519952
    - type: nauc_mrr_at_5_max
      value: 49.655249374588685
    - type: nauc_mrr_at_5_std
      value: -41.48173039989406
    - type: nauc_ndcg_at_1000_diff1
      value: 76.47399204021758
    - type: nauc_ndcg_at_1000_max
      value: 50.55770139961048
    - type: nauc_ndcg_at_1000_std
      value: -39.55650430279072
    - type: nauc_ndcg_at_100_diff1
      value: 76.29355616618253
    - type: nauc_ndcg_at_100_max
      value: 51.003608112592936
    - type: nauc_ndcg_at_100_std
      value: -39.24769744605206
    - type: nauc_ndcg_at_10_diff1
      value: 74.88697528447634
    - type: nauc_ndcg_at_10_max
      value: 50.398416372815234
    - type: nauc_ndcg_at_10_std
      value: -40.76526585772833
    - type: nauc_ndcg_at_1_diff1
      value: 81.27219599516599
    - type: nauc_ndcg_at_1_max
      value: 49.3083394026327
    - type: nauc_ndcg_at_1_std
      value: -38.31023037552026
    - type: nauc_ndcg_at_20_diff1
      value: 75.85463512091866
    - type: nauc_ndcg_at_20_max
      value: 50.97338683654334
    - type: nauc_ndcg_at_20_std
      value: -39.353128774903404
    - type: nauc_ndcg_at_3_diff1
      value: 75.94015726123543
    - type: nauc_ndcg_at_3_max
      value: 49.22194251063148
    - type: nauc_ndcg_at_3_std
      value: -43.040457030630435
    - type: nauc_ndcg_at_5_diff1
      value: 75.19166189770303
    - type: nauc_ndcg_at_5_max
      value: 49.65696229797189
    - type: nauc_ndcg_at_5_std
      value: -42.81534909184424
    - type: nauc_precision_at_1000_diff1
      value: -14.830901395815788
    - type: nauc_precision_at_1000_max
      value: 19.686297136854623
    - type: nauc_precision_at_1000_std
      value: 61.19310360166978
    - type: nauc_precision_at_100_diff1
      value: 20.55469986751769
    - type: nauc_precision_at_100_max
      value: 50.78431835075583
    - type: nauc_precision_at_100_std
      value: 31.54986568374813
    - type: nauc_precision_at_10_diff1
      value: 45.991938532558656
    - type: nauc_precision_at_10_max
      value: 46.386318595630385
    - type: nauc_precision_at_10_std
      value: -23.463011435224608
    - type: nauc_precision_at_1_diff1
      value: 81.27219599516599
    - type: nauc_precision_at_1_max
      value: 49.3083394026327
    - type: nauc_precision_at_1_std
      value: -38.31023037552026
    - type: nauc_precision_at_20_diff1
      value: 41.53180472410822
    - type: nauc_precision_at_20_max
      value: 49.89800247204318
    - type: nauc_precision_at_20_std
      value: -2.4192847331537095
    - type: nauc_precision_at_3_diff1
      value: 67.37504651209993
    - type: nauc_precision_at_3_max
      value: 47.893537208629496
    - type: nauc_precision_at_3_std
      value: -43.2362212382819
    - type: nauc_precision_at_5_diff1
      value: 60.03438883791718
    - type: nauc_precision_at_5_max
      value: 48.29770502354206
    - type: nauc_precision_at_5_std
      value: -40.39588448271546
    - type: nauc_recall_at_1000_diff1
      value: 71.04741174480844
    - type: nauc_recall_at_1000_max
      value: 93.19056506596002
    - type: nauc_recall_at_1000_std
      value: 62.96994797650912
    - type: nauc_recall_at_100_diff1
      value: 65.00418176852641
    - type: nauc_recall_at_100_max
      value: 85.27352708427193
    - type: nauc_recall_at_100_std
      value: 2.8812005546518886
    - type: nauc_recall_at_10_diff1
      value: 61.263254794998865
    - type: nauc_recall_at_10_max
      value: 54.17618329507141
    - type: nauc_recall_at_10_std
      value: -39.80603966142593
    - type: nauc_recall_at_1_diff1
      value: 81.44050620630395
    - type: nauc_recall_at_1_max
      value: 48.97711944070578
    - type: nauc_recall_at_1_std
      value: -38.963689457570254
    - type: nauc_recall_at_20_diff1
      value: 64.42106091745396
    - type: nauc_recall_at_20_max
      value: 63.10796640821887
    - type: nauc_recall_at_20_std
      value: -22.60117424572222
    - type: nauc_recall_at_3_diff1
      value: 70.66311436592945
    - type: nauc_recall_at_3_max
      value: 48.69498944323469
    - type: nauc_recall_at_3_std
      value: -47.37847524874532
    - type: nauc_recall_at_5_diff1
      value: 66.12701111728848
    - type: nauc_recall_at_5_max
      value: 49.91763957934711
    - type: nauc_recall_at_5_std
      value: -48.173252920584126
    - type: ndcg_at_1
      value: 67.43900000000001
    - type: ndcg_at_10
      value: 78.917
    - type: ndcg_at_100
      value: 80.53399999999999
    - type: ndcg_at_1000
      value: 80.768
    - type: ndcg_at_20
      value: 79.813
    - type: ndcg_at_3
      value: 75.37
    - type: ndcg_at_5
      value: 77.551
    - type: precision_at_1
      value: 67.43900000000001
    - type: precision_at_10
      value: 9.115
    - type: precision_at_100
      value: 0.985
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.737
    - type: precision_at_3
      value: 27.081
    - type: precision_at_5
      value: 17.345
    - type: recall_at_1
      value: 67.281
    - type: recall_at_10
      value: 90.2
    - type: recall_at_100
      value: 97.576
    - type: recall_at_1000
      value: 99.368
    - type: recall_at_20
      value: 93.783
    - type: recall_at_3
      value: 80.822
    - type: recall_at_5
      value: 86.091
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB DBPedia (default)
      revision: c0f706b76e590d620bd6618b3ca8efdd34e2d659
      split: test
      type: mteb/dbpedia
    metrics:
    - type: map_at_1
      value: 9.041
    - type: map_at_10
      value: 18.662
    - type: map_at_100
      value: 26.054
    - type: map_at_1000
      value: 27.769
    - type: map_at_20
      value: 21.499
    - type: map_at_3
      value: 13.628000000000002
    - type: map_at_5
      value: 15.617
    - type: mrr_at_1
      value: 67.25
    - type: mrr_at_10
      value: 74.673
    - type: mrr_at_100
      value: 75.022
    - type: mrr_at_1000
      value: 75.031
    - type: mrr_at_20
      value: 74.895
    - type: mrr_at_3
      value: 73.042
    - type: mrr_at_5
      value: 74.179
    - type: ndcg_at_1
      value: 55.75
    - type: ndcg_at_10
      value: 41.004000000000005
    - type: ndcg_at_100
      value: 44.912
    - type: ndcg_at_1000
      value: 51.946000000000005
    - type: ndcg_at_20
      value: 40.195
    - type: ndcg_at_3
      value: 45.803
    - type: ndcg_at_5
      value: 42.976
    - type: precision_at_1
      value: 67.25
    - type: precision_at_10
      value: 31.874999999999996
    - type: precision_at_100
      value: 10.37
    - type: precision_at_1000
      value: 2.1430000000000002
    - type: precision_at_20
      value: 24.275
    - type: precision_at_3
      value: 48.417
    - type: precision_at_5
      value: 40.2
    - type: recall_at_1
      value: 9.041
    - type: recall_at_10
      value: 23.592
    - type: recall_at_100
      value: 49.476
    - type: recall_at_1000
      value: 71.677
    - type: recall_at_20
      value: 30.153000000000002
    - type: recall_at_3
      value: 14.777000000000001
    - type: recall_at_5
      value: 17.829
    - type: main_score
      value: 41.004000000000005
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB DuRetrieval (default)
      revision: a1a333e290fe30b10f3f56498e3a0d911a693ced
      split: dev
      type: C-MTEB/DuRetrieval
    metrics:
    - type: main_score
      value: 83.134
    - type: map_at_1
      value: 23.907999999999998
    - type: map_at_10
      value: 74.566
    - type: map_at_100
      value: 77.706
    - type: map_at_1000
      value: 77.762
    - type: map_at_20
      value: 76.943
    - type: map_at_3
      value: 50.971999999999994
    - type: map_at_5
      value: 64.429
    - type: mrr_at_1
      value: 84.8
    - type: mrr_at_10
      value: 89.73218253968246
    - type: mrr_at_100
      value: 89.82853630655774
    - type: mrr_at_1000
      value: 89.83170411703153
    - type: mrr_at_20
      value: 89.79582030091501
    - type: mrr_at_3
      value: 89.32499999999992
    - type: mrr_at_5
      value: 89.58749999999992
    - type: nauc_map_at_1000_diff1
      value: -2.2736020650163717
    - type: nauc_map_at_1000_max
      value: 45.3937519555142
    - type: nauc_map_at_1000_std
      value: 10.824778228268581
    - type: nauc_map_at_100_diff1
      value: -2.2662939752750066
    - type: nauc_map_at_100_max
      value: 45.423960626031366
    - type: nauc_map_at_100_std
      value: 10.804239351738717
    - type: nauc_map_at_10_diff1
      value: 0.9395752585654343
    - type: nauc_map_at_10_max
      value: 42.53814836940551
    - type: nauc_map_at_10_std
      value: 0.7199313235265218
    - type: nauc_map_at_1_diff1
      value: 45.19415865267676
    - type: nauc_map_at_1_max
      value: -1.7261947382471912
    - type: nauc_map_at_1_std
      value: -32.16144291613605
    - type: nauc_map_at_20_diff1
      value: -1.884514152147472
    - type: nauc_map_at_20_max
      value: 44.830401115927174
    - type: nauc_map_at_20_std
      value: 8.118530414377219
    - type: nauc_map_at_3_diff1
      value: 25.678881127059967
    - type: nauc_map_at_3_max
      value: 12.191400431839758
    - type: nauc_map_at_3_std
      value: -27.201740587642327
    - type: nauc_map_at_5_diff1
      value: 13.227128780829572
    - type: nauc_map_at_5_max
      value: 26.978282739708977
    - type: nauc_map_at_5_std
      value: -17.555610348070584
    - type: nauc_mrr_at_1000_diff1
      value: 21.073512437502178
    - type: nauc_mrr_at_1000_max
      value: 64.9680257861005
    - type: nauc_mrr_at_1000_std
      value: 19.626288754404293
    - type: nauc_mrr_at_100_diff1
      value: 21.074637426957732
    - type: nauc_mrr_at_100_max
      value: 64.97612675661915
    - type: nauc_mrr_at_100_std
      value: 19.649504127800878
    - type: nauc_mrr_at_10_diff1
      value: 21.12003267626651
    - type: nauc_mrr_at_10_max
      value: 65.24362289059766
    - type: nauc_mrr_at_10_std
      value: 19.92351276180984
    - type: nauc_mrr_at_1_diff1
      value: 22.711430629147635
    - type: nauc_mrr_at_1_max
      value: 58.4059429497403
    - type: nauc_mrr_at_1_std
      value: 11.967886722567973
    - type: nauc_mrr_at_20_diff1
      value: 20.98220830510272
    - type: nauc_mrr_at_20_max
      value: 65.05737535197835
    - type: nauc_mrr_at_20_std
      value: 19.66672900782771
    - type: nauc_mrr_at_3_diff1
      value: 20.924796220048528
    - type: nauc_mrr_at_3_max
      value: 65.71388669932584
    - type: nauc_mrr_at_3_std
      value: 20.05912197134477
    - type: nauc_mrr_at_5_diff1
      value: 20.61978649468208
    - type: nauc_mrr_at_5_max
      value: 65.50709154526211
    - type: nauc_mrr_at_5_std
      value: 20.241434276181838
    - type: nauc_ndcg_at_1000_diff1
      value: 0.25363171946133656
    - type: nauc_ndcg_at_1000_max
      value: 54.12840465309885
    - type: nauc_ndcg_at_1000_std
      value: 20.749184325412546
    - type: nauc_ndcg_at_100_diff1
      value: 0.15649430250272792
    - type: nauc_ndcg_at_100_max
      value: 54.47995322413234
    - type: nauc_ndcg_at_100_std
      value: 21.266786634233267
    - type: nauc_ndcg_at_10_diff1
      value: 0.14579250840386346
    - type: nauc_ndcg_at_10_max
      value: 49.8643037948353
    - type: nauc_ndcg_at_10_std
      value: 12.960701643914216
    - type: nauc_ndcg_at_1_diff1
      value: 22.711430629147635
    - type: nauc_ndcg_at_1_max
      value: 58.4059429497403
    - type: nauc_ndcg_at_1_std
      value: 11.967886722567973
    - type: nauc_ndcg_at_20_diff1
      value: -0.6701559981776763
    - type: nauc_ndcg_at_20_max
      value: 52.95443437012488
    - type: nauc_ndcg_at_20_std
      value: 16.708883972005758
    - type: nauc_ndcg_at_3_diff1
      value: -0.19084922341962388
    - type: nauc_ndcg_at_3_max
      value: 46.2110230886874
    - type: nauc_ndcg_at_3_std
      value: 13.363250229683038
    - type: nauc_ndcg_at_5_diff1
      value: 0.9840019268192548
    - type: nauc_ndcg_at_5_max
      value: 43.56594891798146
    - type: nauc_ndcg_at_5_std
      value: 8.577017104088146
    - type: nauc_precision_at_1000_diff1
      value: -30.779179091501145
    - type: nauc_precision_at_1000_max
      value: 16.056094258615673
    - type: nauc_precision_at_1000_std
      value: 49.96303902363283
    - type: nauc_precision_at_100_diff1
      value: -31.583236638899585
    - type: nauc_precision_at_100_max
      value: 19.16571713603373
    - type: nauc_precision_at_100_std
      value: 51.870647903980036
    - type: nauc_precision_at_10_diff1
      value: -35.62134572732597
    - type: nauc_precision_at_10_max
      value: 31.6935186494612
    - type: nauc_precision_at_10_std
      value: 46.68659723766723
    - type: nauc_precision_at_1_diff1
      value: 22.711430629147635
    - type: nauc_precision_at_1_max
      value: 58.4059429497403
    - type: nauc_precision_at_1_std
      value: 11.967886722567973
    - type: nauc_precision_at_20_diff1
      value: -33.875460046920495
    - type: nauc_precision_at_20_max
      value: 24.188420133566442
    - type: nauc_precision_at_20_std
      value: 50.02387762958483
    - type: nauc_precision_at_3_diff1
      value: -28.875998450906827
    - type: nauc_precision_at_3_max
      value: 44.77058831167941
    - type: nauc_precision_at_3_std
      value: 31.77993710437207
    - type: nauc_precision_at_5_diff1
      value: -34.92525440306491
    - type: nauc_precision_at_5_max
      value: 39.855219917077086
    - type: nauc_precision_at_5_std
      value: 37.95432046169299
    - type: nauc_recall_at_1000_diff1
      value: -14.293309371874733
    - type: nauc_recall_at_1000_max
      value: 59.06948692482579
    - type: nauc_recall_at_1000_std
      value: 62.586254868312686
    - type: nauc_recall_at_100_diff1
      value: -4.344100947212704
    - type: nauc_recall_at_100_max
      value: 58.42120421043602
    - type: nauc_recall_at_100_std
      value: 46.48562009316997
    - type: nauc_recall_at_10_diff1
      value: 0.04948662912161709
    - type: nauc_recall_at_10_max
      value: 42.42809687119093
    - type: nauc_recall_at_10_std
      value: 0.6892504250411409
    - type: nauc_recall_at_1_diff1
      value: 45.19415865267676
    - type: nauc_recall_at_1_max
      value: -1.7261947382471912
    - type: nauc_recall_at_1_std
      value: -32.16144291613605
    - type: nauc_recall_at_20_diff1
      value: -7.634587864605111
    - type: nauc_recall_at_20_max
      value: 49.21327187174134
    - type: nauc_recall_at_20_std
      value: 16.408481068336346
    - type: nauc_recall_at_3_diff1
      value: 24.72546591038644
    - type: nauc_recall_at_3_max
      value: 6.620763400972902
    - type: nauc_recall_at_3_std
      value: -29.994703323331684
    - type: nauc_recall_at_5_diff1
      value: 12.65527364845842
    - type: nauc_recall_at_5_max
      value: 20.400121385794694
    - type: nauc_recall_at_5_std
      value: -22.34284568447213
    - type: ndcg_at_1
      value: 84.8
    - type: ndcg_at_10
      value: 83.134
    - type: ndcg_at_100
      value: 86.628
    - type: ndcg_at_1000
      value: 87.151
    - type: ndcg_at_20
      value: 85.092
    - type: ndcg_at_3
      value: 81.228
    - type: ndcg_at_5
      value: 80.2
    - type: precision_at_1
      value: 84.8
    - type: precision_at_10
      value: 40.394999999999996
    - type: precision_at_100
      value: 4.745
    - type: precision_at_1000
      value: 0.488
    - type: precision_at_20
      value: 22.245
    - type: precision_at_3
      value: 73.25
    - type: precision_at_5
      value: 61.86000000000001
    - type: recall_at_1
      value: 23.907999999999998
    - type: recall_at_10
      value: 85.346
    - type: recall_at_100
      value: 96.515
    - type: recall_at_1000
      value: 99.156
    - type: recall_at_20
      value: 91.377
    - type: recall_at_3
      value: 54.135
    - type: recall_at_5
      value: 70.488
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB EcomRetrieval (default)
      revision: 687de13dc7294d6fd9be10c6945f9e8fec8166b9
      split: dev
      type: C-MTEB/EcomRetrieval
    metrics:
    - type: main_score
      value: 60.887
    - type: map_at_1
      value: 46.6
    - type: map_at_10
      value: 56.035000000000004
    - type: map_at_100
      value: 56.741
    - type: map_at_1000
      value: 56.764
    - type: map_at_20
      value: 56.513999999999996
    - type: map_at_3
      value: 53.733
    - type: map_at_5
      value: 54.913000000000004
    - type: mrr_at_1
      value: 46.6
    - type: mrr_at_10
      value: 56.034523809523776
    - type: mrr_at_100
      value: 56.74056360434383
    - type: mrr_at_1000
      value: 56.76373487222486
    - type: mrr_at_20
      value: 56.51374873879128
    - type: mrr_at_3
      value: 53.73333333333328
    - type: mrr_at_5
      value: 54.91333333333327
    - type: nauc_map_at_1000_diff1
      value: 65.13546939953387
    - type: nauc_map_at_1000_max
      value: 43.358890946774494
    - type: nauc_map_at_1000_std
      value: -9.973282105235036
    - type: nauc_map_at_100_diff1
      value: 65.12449309472493
    - type: nauc_map_at_100_max
      value: 43.377100882923145
    - type: nauc_map_at_100_std
      value: -9.971781228240555
    - type: nauc_map_at_10_diff1
      value: 64.83020018537475
    - type: nauc_map_at_10_max
      value: 43.25969482323034
    - type: nauc_map_at_10_std
      value: -10.120272176001547
    - type: nauc_map_at_1_diff1
      value: 69.58727592100516
    - type: nauc_map_at_1_max
      value: 38.236494689522026
    - type: nauc_map_at_1_std
      value: -14.833390831689597
    - type: nauc_map_at_20_diff1
      value: 65.01159809914586
    - type: nauc_map_at_20_max
      value: 43.33440319829618
    - type: nauc_map_at_20_std
      value: -10.039958228659726
    - type: nauc_map_at_3_diff1
      value: 65.2396323885909
    - type: nauc_map_at_3_max
      value: 42.26904017378952
    - type: nauc_map_at_3_std
      value: -11.793017036934044
    - type: nauc_map_at_5_diff1
      value: 64.96397227898036
    - type: nauc_map_at_5_max
      value: 43.231333789145424
    - type: nauc_map_at_5_std
      value: -10.349933732151372
    - type: nauc_mrr_at_1000_diff1
      value: 65.13546939953387
    - type: nauc_mrr_at_1000_max
      value: 43.358890946774494
    - type: nauc_mrr_at_1000_std
      value: -9.973282105235036
    - type: nauc_mrr_at_100_diff1
      value: 65.12449309472493
    - type: nauc_mrr_at_100_max
      value: 43.377100882923145
    - type: nauc_mrr_at_100_std
      value: -9.971781228240555
    - type: nauc_mrr_at_10_diff1
      value: 64.83020018537475
    - type: nauc_mrr_at_10_max
      value: 43.25969482323034
    - type: nauc_mrr_at_10_std
      value: -10.120272176001547
    - type: nauc_mrr_at_1_diff1
      value: 69.58727592100516
    - type: nauc_mrr_at_1_max
      value: 38.236494689522026
    - type: nauc_mrr_at_1_std
      value: -14.833390831689597
    - type: nauc_mrr_at_20_diff1
      value: 65.01159809914586
    - type: nauc_mrr_at_20_max
      value: 43.33440319829618
    - type: nauc_mrr_at_20_std
      value: -10.039958228659726
    - type: nauc_mrr_at_3_diff1
      value: 65.2396323885909
    - type: nauc_mrr_at_3_max
      value: 42.26904017378952
    - type: nauc_mrr_at_3_std
      value: -11.793017036934044
    - type: nauc_mrr_at_5_diff1
      value: 64.96397227898036
    - type: nauc_mrr_at_5_max
      value: 43.231333789145424
    - type: nauc_mrr_at_5_std
      value: -10.349933732151372
    - type: nauc_ndcg_at_1000_diff1
      value: 64.26802655199876
    - type: nauc_ndcg_at_1000_max
      value: 45.854310744745185
    - type: nauc_ndcg_at_1000_std
      value: -6.184417305204082
    - type: nauc_ndcg_at_100_diff1
      value: 63.99268329609827
    - type: nauc_ndcg_at_100_max
      value: 46.31270128748375
    - type: nauc_ndcg_at_100_std
      value: -6.1393433180558965
    - type: nauc_ndcg_at_10_diff1
      value: 62.6735104141137
    - type: nauc_ndcg_at_10_max
      value: 45.54954799462398
    - type: nauc_ndcg_at_10_std
      value: -7.348851199024871
    - type: nauc_ndcg_at_1_diff1
      value: 69.58727592100516
    - type: nauc_ndcg_at_1_max
      value: 38.236494689522026
    - type: nauc_ndcg_at_1_std
      value: -14.833390831689597
    - type: nauc_ndcg_at_20_diff1
      value: 63.25899651677274
    - type: nauc_ndcg_at_20_max
      value: 45.952196968886014
    - type: nauc_ndcg_at_20_std
      value: -6.807607465125713
    - type: nauc_ndcg_at_3_diff1
      value: 63.65618337476822
    - type: nauc_ndcg_at_3_max
      value: 43.507890965228945
    - type: nauc_ndcg_at_3_std
      value: -10.73845622217601
    - type: nauc_ndcg_at_5_diff1
      value: 63.079162432921855
    - type: nauc_ndcg_at_5_max
      value: 45.38303443868148
    - type: nauc_ndcg_at_5_std
      value: -8.063657824835534
    - type: nauc_precision_at_1000_diff1
      value: 63.01459977930557
    - type: nauc_precision_at_1000_max
      value: 92.4253034547151
    - type: nauc_precision_at_1000_std
      value: 84.4845513963158
    - type: nauc_precision_at_100_diff1
      value: 57.17217119405878
    - type: nauc_precision_at_100_max
      value: 80.70049725316484
    - type: nauc_precision_at_100_std
      value: 41.78392287147403
    - type: nauc_precision_at_10_diff1
      value: 53.115665404390725
    - type: nauc_precision_at_10_max
      value: 55.73825657341263
    - type: nauc_precision_at_10_std
      value: 5.406226305013257
    - type: nauc_precision_at_1_diff1
      value: 69.58727592100516
    - type: nauc_precision_at_1_max
      value: 38.236494689522026
    - type: nauc_precision_at_1_std
      value: -14.833390831689597
    - type: nauc_precision_at_20_diff1
      value: 53.77730697622828
    - type: nauc_precision_at_20_max
      value: 61.88170819253054
    - type: nauc_precision_at_20_std
      value: 13.678730470003856
    - type: nauc_precision_at_3_diff1
      value: 58.580196992291455
    - type: nauc_precision_at_3_max
      value: 47.404834585376626
    - type: nauc_precision_at_3_std
      value: -7.374978769024051
    - type: nauc_precision_at_5_diff1
      value: 56.44564652606437
    - type: nauc_precision_at_5_max
      value: 53.08973975162324
    - type: nauc_precision_at_5_std
      value: 0.22762700141423803
    - type: nauc_recall_at_1000_diff1
      value: 63.01459977930565
    - type: nauc_recall_at_1000_max
      value: 92.42530345471532
    - type: nauc_recall_at_1000_std
      value: 84.48455139631602
    - type: nauc_recall_at_100_diff1
      value: 57.17217119405904
    - type: nauc_recall_at_100_max
      value: 80.70049725316468
    - type: nauc_recall_at_100_std
      value: 41.783922871474275
    - type: nauc_recall_at_10_diff1
      value: 53.11566540439087
    - type: nauc_recall_at_10_max
      value: 55.738256573412656
    - type: nauc_recall_at_10_std
      value: 5.406226305013377
    - type: nauc_recall_at_1_diff1
      value: 69.58727592100516
    - type: nauc_recall_at_1_max
      value: 38.236494689522026
    - type: nauc_recall_at_1_std
      value: -14.833390831689597
    - type: nauc_recall_at_20_diff1
      value: 53.77730697622846
    - type: nauc_recall_at_20_max
      value: 61.881708192530525
    - type: nauc_recall_at_20_std
      value: 13.678730470003947
    - type: nauc_recall_at_3_diff1
      value: 58.5801969922914
    - type: nauc_recall_at_3_max
      value: 47.40483458537654
    - type: nauc_recall_at_3_std
      value: -7.37497876902413
    - type: nauc_recall_at_5_diff1
      value: 56.445646526064394
    - type: nauc_recall_at_5_max
      value: 53.08973975162332
    - type: nauc_recall_at_5_std
      value: 0.22762700141428024
    - type: ndcg_at_1
      value: 46.6
    - type: ndcg_at_10
      value: 60.887
    - type: ndcg_at_100
      value: 64.18199999999999
    - type: ndcg_at_1000
      value: 64.726
    - type: ndcg_at_20
      value: 62.614999999999995
    - type: ndcg_at_3
      value: 56.038
    - type: ndcg_at_5
      value: 58.150999999999996
    - type: precision_at_1
      value: 46.6
    - type: precision_at_10
      value: 7.630000000000001
    - type: precision_at_100
      value: 0.914
    - type: precision_at_1000
      value: 0.096
    - type: precision_at_20
      value: 4.154999999999999
    - type: precision_at_3
      value: 20.9
    - type: precision_at_5
      value: 13.56
    - type: recall_at_1
      value: 46.6
    - type: recall_at_10
      value: 76.3
    - type: recall_at_100
      value: 91.4
    - type: recall_at_1000
      value: 95.6
    - type: recall_at_20
      value: 83.1
    - type: recall_at_3
      value: 62.7
    - type: recall_at_5
      value: 67.80000000000001
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB EmotionClassification (default)
      revision: 4f58c6b202a23cf9a4da393831edf4f9183cad37
      split: test
      type: mteb/emotion
    metrics:
    - type: accuracy
      value: 73.29999999999998
    - type: f1
      value: 67.71473706580302
    - type: f1_weighted
      value: 74.83537255312045
    - type: main_score
      value: 73.29999999999998
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB FEVER (default)
      revision: bea83ef9e8fb933d90a2f1d5515737465d613e12
      split: test
      type: mteb/fever
    metrics:
    - type: map_at_1
      value: 78.371
    - type: map_at_10
      value: 85.762
    - type: map_at_100
      value: 85.954
    - type: map_at_1000
      value: 85.966
    - type: map_at_20
      value: 85.887
    - type: map_at_3
      value: 84.854
    - type: map_at_5
      value: 85.408
    - type: mrr_at_1
      value: 84.443
    - type: mrr_at_10
      value: 90.432
    - type: mrr_at_100
      value: 90.483
    - type: mrr_at_1000
      value: 90.484
    - type: mrr_at_20
      value: 90.473
    - type: mrr_at_3
      value: 89.89399999999999
    - type: mrr_at_5
      value: 90.244
    - type: ndcg_at_1
      value: 84.443
    - type: ndcg_at_10
      value: 89.05499999999999
    - type: ndcg_at_100
      value: 89.68
    - type: ndcg_at_1000
      value: 89.87899999999999
    - type: ndcg_at_20
      value: 89.381
    - type: ndcg_at_3
      value: 87.73100000000001
    - type: ndcg_at_5
      value: 88.425
    - type: precision_at_1
      value: 84.443
    - type: precision_at_10
      value: 10.520999999999999
    - type: precision_at_100
      value: 1.103
    - type: precision_at_1000
      value: 0.11399999999999999
    - type: precision_at_20
      value: 5.362
    - type: precision_at_3
      value: 33.198
    - type: precision_at_5
      value: 20.441000000000003
    - type: recall_at_1
      value: 78.371
    - type: recall_at_10
      value: 94.594
    - type: recall_at_100
      value: 96.97099999999999
    - type: recall_at_1000
      value: 98.18
    - type: recall_at_20
      value: 95.707
    - type: recall_at_3
      value: 90.853
    - type: recall_at_5
      value: 92.74799999999999
    - type: main_score
      value: 89.05499999999999
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB FiQA2018 (default)
      revision: 27a168819829fe9bcd655c2df245fb19452e8e06
      split: test
      type: mteb/fiqa
    metrics:
    - type: map_at_1
      value: 23.810000000000002
    - type: map_at_10
      value: 39.051
    - type: map_at_100
      value: 41.231
    - type: map_at_1000
      value: 41.376000000000005
    - type: map_at_20
      value: 40.227000000000004
    - type: map_at_3
      value: 33.915
    - type: map_at_5
      value: 36.459
    - type: mrr_at_1
      value: 48.148
    - type: mrr_at_10
      value: 55.765
    - type: mrr_at_100
      value: 56.495
    - type: mrr_at_1000
      value: 56.525999999999996
    - type: mrr_at_20
      value: 56.213
    - type: mrr_at_3
      value: 53.086
    - type: mrr_at_5
      value: 54.513999999999996
    - type: ndcg_at_1
      value: 48.148
    - type: ndcg_at_10
      value: 47.349999999999994
    - type: ndcg_at_100
      value: 54.61899999999999
    - type: ndcg_at_1000
      value: 56.830000000000005
    - type: ndcg_at_20
      value: 50.143
    - type: ndcg_at_3
      value: 43.108000000000004
    - type: ndcg_at_5
      value: 44.023
    - type: precision_at_1
      value: 48.148
    - type: precision_at_10
      value: 13.441
    - type: precision_at_100
      value: 2.085
    - type: precision_at_1000
      value: 0.248
    - type: precision_at_20
      value: 7.870000000000001
    - type: precision_at_3
      value: 28.909000000000002
    - type: precision_at_5
      value: 20.957
    - type: recall_at_1
      value: 23.810000000000002
    - type: recall_at_10
      value: 54.303000000000004
    - type: recall_at_100
      value: 81.363
    - type: recall_at_1000
      value: 94.391
    - type: recall_at_20
      value: 63.056999999999995
    - type: recall_at_3
      value: 38.098
    - type: recall_at_5
      value: 44.414
    - type: main_score
      value: 47.349999999999994
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB GeoreviewClassification (default)
      revision: 3765c0d1de6b7d264bc459433c45e5a75513839c
      split: test
      type: ai-forever/georeview-classification
    metrics:
    - type: accuracy
      value: 48.0126953125
    - type: f1
      value: 47.65764016160488
    - type: f1_weighted
      value: 47.65701659482088
    - type: main_score
      value: 48.0126953125
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB GeoreviewClusteringP2P (default)
      revision: 97a313c8fc85b47f13f33e7e9a95c1ad888c7fec
      split: test
      type: ai-forever/georeview-clustering-p2p
    metrics:
    - type: main_score
      value: 73.62357853672266
    - type: v_measure
      value: 73.62357853672266
    - type: v_measure_std
      value: 0.5942247545535766
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB GerDaLIR (default)
      revision: 0bb47f1d73827e96964edb84dfe552f62f4fd5eb
      split: test
      type: jinaai/ger_da_lir
    metrics:
    - type: main_score
      value: 16.227
    - type: map_at_1
      value: 8.082
    - type: map_at_10
      value: 12.959999999999999
    - type: map_at_100
      value: 13.923
    - type: map_at_1000
      value: 14.030999999999999
    - type: map_at_20
      value: 13.453000000000001
    - type: map_at_3
      value: 11.018
    - type: map_at_5
      value: 12.056000000000001
    - type: mrr_at_1
      value: 8.993332249146203
    - type: mrr_at_10
      value: 13.994013092850247
    - type: mrr_at_100
      value: 14.913737673149308
    - type: mrr_at_1000
      value: 15.00843809934407
    - type: mrr_at_20
      value: 14.470268462334007
    - type: mrr_at_3
      value: 12.000596302921846
    - type: mrr_at_5
      value: 13.070689000921561
    - type: nauc_map_at_1000_diff1
      value: 28.559639584013286
    - type: nauc_map_at_1000_max
      value: 25.533800126086714
    - type: nauc_map_at_1000_std
      value: 9.826551026628666
    - type: nauc_map_at_100_diff1
      value: 28.544724499331696
    - type: nauc_map_at_100_max
      value: 25.46734324526386
    - type: nauc_map_at_100_std
      value: 9.739314481785591
    - type: nauc_map_at_10_diff1
      value: 28.77447517718118
    - type: nauc_map_at_10_max
      value: 24.7431615237795
    - type: nauc_map_at_10_std
      value: 8.349878188033646
    - type: nauc_map_at_1_diff1
      value: 37.405452629895514
    - type: nauc_map_at_1_max
      value: 24.444208978394023
    - type: nauc_map_at_1_std
      value: 4.043820373810528
    - type: nauc_map_at_20_diff1
      value: 28.69764217789062
    - type: nauc_map_at_20_max
      value: 25.111848355996496
    - type: nauc_map_at_20_std
      value: 9.034829905305918
    - type: nauc_map_at_3_diff1
      value: 30.89053285076882
    - type: nauc_map_at_3_max
      value: 24.862886115911152
    - type: nauc_map_at_3_std
      value: 6.654260832396586
    - type: nauc_map_at_5_diff1
      value: 29.230629676604263
    - type: nauc_map_at_5_max
      value: 24.374302288018583
    - type: nauc_map_at_5_std
      value: 7.341846952319046
    - type: nauc_mrr_at_1000_diff1
      value: 28.086147932781426
    - type: nauc_mrr_at_1000_max
      value: 25.98698528264653
    - type: nauc_mrr_at_1000_std
      value: 9.917554348624545
    - type: nauc_mrr_at_100_diff1
      value: 28.069163279791336
    - type: nauc_mrr_at_100_max
      value: 25.949440010886804
    - type: nauc_mrr_at_100_std
      value: 9.874340979732578
    - type: nauc_mrr_at_10_diff1
      value: 28.239920869530046
    - type: nauc_mrr_at_10_max
      value: 25.351271409498576
    - type: nauc_mrr_at_10_std
      value: 8.669862759875162
    - type: nauc_mrr_at_1_diff1
      value: 35.96543040207856
    - type: nauc_mrr_at_1_max
      value: 25.488936487231967
    - type: nauc_mrr_at_1_std
      value: 4.76439131038345
    - type: nauc_mrr_at_20_diff1
      value: 28.18865871284607
    - type: nauc_mrr_at_20_max
      value: 25.67121763344746
    - type: nauc_mrr_at_20_std
      value: 9.297910707519472
    - type: nauc_mrr_at_3_diff1
      value: 30.166714199740717
    - type: nauc_mrr_at_3_max
      value: 25.541792491964877
    - type: nauc_mrr_at_3_std
      value: 7.083090296398472
    - type: nauc_mrr_at_5_diff1
      value: 28.68475284656478
    - type: nauc_mrr_at_5_max
      value: 24.994071363482835
    - type: nauc_mrr_at_5_std
      value: 7.687507254902365
    - type: nauc_ndcg_at_1000_diff1
      value: 25.292792613586467
    - type: nauc_ndcg_at_1000_max
      value: 29.211905289377178
    - type: nauc_ndcg_at_1000_std
      value: 18.088867467320355
    - type: nauc_ndcg_at_100_diff1
      value: 25.026905011089152
    - type: nauc_ndcg_at_100_max
      value: 27.98822281254431
    - type: nauc_ndcg_at_100_std
      value: 16.69456904301902
    - type: nauc_ndcg_at_10_diff1
      value: 25.972279051109503
    - type: nauc_ndcg_at_10_max
      value: 24.86486482734957
    - type: nauc_ndcg_at_10_std
      value: 10.398605822106353
    - type: nauc_ndcg_at_1_diff1
      value: 36.134710485184826
    - type: nauc_ndcg_at_1_max
      value: 25.384572790326025
    - type: nauc_ndcg_at_1_std
      value: 4.591863033771824
    - type: nauc_ndcg_at_20_diff1
      value: 25.850033660205536
    - type: nauc_ndcg_at_20_max
      value: 25.944243193140515
    - type: nauc_ndcg_at_20_std
      value: 12.392409721204892
    - type: nauc_ndcg_at_3_diff1
      value: 29.1966056380018
    - type: nauc_ndcg_at_3_max
      value: 24.978843156259913
    - type: nauc_ndcg_at_3_std
      value: 7.353914459205087
    - type: nauc_ndcg_at_5_diff1
      value: 26.795315295756282
    - type: nauc_ndcg_at_5_max
      value: 24.1196789150412
    - type: nauc_ndcg_at_5_std
      value: 8.311970988265172
    - type: nauc_precision_at_1000_diff1
      value: 9.128270550217984
    - type: nauc_precision_at_1000_max
      value: 35.79286915973607
    - type: nauc_precision_at_1000_std
      value: 39.15669472887154
    - type: nauc_precision_at_100_diff1
      value: 14.770289799034384
    - type: nauc_precision_at_100_max
      value: 34.58262232264337
    - type: nauc_precision_at_100_std
      value: 34.101148102981384
    - type: nauc_precision_at_10_diff1
      value: 19.899104673118178
    - type: nauc_precision_at_10_max
      value: 26.636940338985625
    - type: nauc_precision_at_10_std
      value: 15.73871357255849
    - type: nauc_precision_at_1_diff1
      value: 36.134710485184826
    - type: nauc_precision_at_1_max
      value: 25.384572790326025
    - type: nauc_precision_at_1_std
      value: 4.591863033771824
    - type: nauc_precision_at_20_diff1
      value: 19.423457975148942
    - type: nauc_precision_at_20_max
      value: 29.58123490878582
    - type: nauc_precision_at_20_std
      value: 20.847850110821618
    - type: nauc_precision_at_3_diff1
      value: 24.986416623492918
    - type: nauc_precision_at_3_max
      value: 25.973548400472975
    - type: nauc_precision_at_3_std
      value: 9.486410455972823
    - type: nauc_precision_at_5_diff1
      value: 21.237741424923332
    - type: nauc_precision_at_5_max
      value: 24.647141028200164
    - type: nauc_precision_at_5_std
      value: 11.102785032334147
    - type: nauc_recall_at_1000_diff1
      value: 15.999714888817829
    - type: nauc_recall_at_1000_max
      value: 44.34701908906545
    - type: nauc_recall_at_1000_std
      value: 51.13471291594717
    - type: nauc_recall_at_100_diff1
      value: 17.401714890483706
    - type: nauc_recall_at_100_max
      value: 33.39042631654808
    - type: nauc_recall_at_100_std
      value: 33.944446168451584
    - type: nauc_recall_at_10_diff1
      value: 20.30036232399894
    - type: nauc_recall_at_10_max
      value: 24.006718284396786
    - type: nauc_recall_at_10_std
      value: 14.049375108518669
    - type: nauc_recall_at_1_diff1
      value: 37.405452629895514
    - type: nauc_recall_at_1_max
      value: 24.444208978394023
    - type: nauc_recall_at_1_std
      value: 4.043820373810528
    - type: nauc_recall_at_20_diff1
      value: 20.23582802609045
    - type: nauc_recall_at_20_max
      value: 26.408063410785243
    - type: nauc_recall_at_20_std
      value: 18.617479515468112
    - type: nauc_recall_at_3_diff1
      value: 25.53221830103098
    - type: nauc_recall_at_3_max
      value: 24.283712329152678
    - type: nauc_recall_at_3_std
      value: 8.428947805841867
    - type: nauc_recall_at_5_diff1
      value: 21.741499601020823
    - type: nauc_recall_at_5_max
      value: 22.754924586295296
    - type: nauc_recall_at_5_std
      value: 9.966736688169814
    - type: ndcg_at_1
      value: 8.977
    - type: ndcg_at_10
      value: 16.227
    - type: ndcg_at_100
      value: 21.417
    - type: ndcg_at_1000
      value: 24.451
    - type: ndcg_at_20
      value: 17.982
    - type: ndcg_at_3
      value: 12.206999999999999
    - type: ndcg_at_5
      value: 14.059
    - type: precision_at_1
      value: 8.977
    - type: precision_at_10
      value: 2.933
    - type: precision_at_100
      value: 0.59
    - type: precision_at_1000
      value: 0.087
    - type: precision_at_20
      value: 1.8599999999999999
    - type: precision_at_3
      value: 5.550999999999999
    - type: precision_at_5
      value: 4.340999999999999
    - type: recall_at_1
      value: 8.082
    - type: recall_at_10
      value: 25.52
    - type: recall_at_100
      value: 50.32
    - type: recall_at_1000
      value: 74.021
    - type: recall_at_20
      value: 32.229
    - type: recall_at_3
      value: 14.66
    - type: recall_at_5
      value: 19.062
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB GermanDPR (default)
      revision: 5129d02422a66be600ac89cd3e8531b4f97d347d
      split: test
      type: deepset/germandpr
    metrics:
    - type: main_score
      value: 82.422
    - type: map_at_1
      value: 64.39
    - type: map_at_10
      value: 77.273
    - type: map_at_100
      value: 77.375
    - type: map_at_1000
      value: 77.376
    - type: map_at_20
      value: 77.351
    - type: map_at_3
      value: 75.46300000000001
    - type: map_at_5
      value: 76.878
    - type: mrr_at_1
      value: 64.19512195121952
    - type: mrr_at_10
      value: 77.15842044134736
    - type: mrr_at_100
      value: 77.2604854308704
    - type: mrr_at_1000
      value: 77.26087882190109
    - type: mrr_at_20
      value: 77.23572154560611
    - type: mrr_at_3
      value: 75.34959349593504
    - type: mrr_at_5
      value: 76.76422764227652
    - type: nauc_map_at_1000_diff1
      value: 49.73135253389972
    - type: nauc_map_at_1000_max
      value: 8.665570717396145
    - type: nauc_map_at_1000_std
      value: -25.920927572114522
    - type: nauc_map_at_100_diff1
      value: 49.729170775336605
    - type: nauc_map_at_100_max
      value: 8.66717979705074
    - type: nauc_map_at_100_std
      value: -25.918338868918596
    - type: nauc_map_at_10_diff1
      value: 49.708681691445925
    - type: nauc_map_at_10_max
      value: 8.830640635692113
    - type: nauc_map_at_10_std
      value: -25.843238986304858
    - type: nauc_map_at_1_diff1
      value: 51.750022350988914
    - type: nauc_map_at_1_max
      value: 3.599863010364626
    - type: nauc_map_at_1_std
      value: -27.670122127567314
    - type: nauc_map_at_20_diff1
      value: 49.72609185887161
    - type: nauc_map_at_20_max
      value: 8.766556053409218
    - type: nauc_map_at_20_std
      value: -25.85975887517904
    - type: nauc_map_at_3_diff1
      value: 49.328512536255595
    - type: nauc_map_at_3_max
      value: 9.475682028996795
    - type: nauc_map_at_3_std
      value: -26.277349632171017
    - type: nauc_map_at_5_diff1
      value: 49.42801822186142
    - type: nauc_map_at_5_max
      value: 8.788822474357252
    - type: nauc_map_at_5_std
      value: -25.959260882028573
    - type: nauc_mrr_at_1000_diff1
      value: 50.13038598302397
    - type: nauc_mrr_at_1000_max
      value: 8.734338637484832
    - type: nauc_mrr_at_1000_std
      value: -26.653343549855908
    - type: nauc_mrr_at_100_diff1
      value: 50.12820392111392
    - type: nauc_mrr_at_100_max
      value: 8.735940503917966
    - type: nauc_mrr_at_100_std
      value: -26.65074918231251
    - type: nauc_mrr_at_10_diff1
      value: 50.10567888458267
    - type: nauc_mrr_at_10_max
      value: 8.898451291748575
    - type: nauc_mrr_at_10_std
      value: -26.572046921975655
    - type: nauc_mrr_at_1_diff1
      value: 52.22769994409465
    - type: nauc_mrr_at_1_max
      value: 3.6490820146062015
    - type: nauc_mrr_at_1_std
      value: -28.535100562320498
    - type: nauc_mrr_at_20_diff1
      value: 50.12462222100699
    - type: nauc_mrr_at_20_max
      value: 8.83487018268756
    - type: nauc_mrr_at_20_std
      value: -26.591437036958332
    - type: nauc_mrr_at_3_diff1
      value: 49.6987353700016
    - type: nauc_mrr_at_3_max
      value: 9.531003760756258
    - type: nauc_mrr_at_3_std
      value: -26.949799063124818
    - type: nauc_mrr_at_5_diff1
      value: 49.823881656376585
    - type: nauc_mrr_at_5_max
      value: 8.850404667985085
    - type: nauc_mrr_at_5_std
      value: -26.680008966088582
    - type: nauc_ndcg_at_1000_diff1
      value: 49.41721203361181
    - type: nauc_ndcg_at_1000_max
      value: 9.41093067609825
    - type: nauc_ndcg_at_1000_std
      value: -25.499543637737567
    - type: nauc_ndcg_at_100_diff1
      value: 49.32810419509252
    - type: nauc_ndcg_at_100_max
      value: 9.476216458766897
    - type: nauc_ndcg_at_100_std
      value: -25.393856250990414
    - type: nauc_ndcg_at_10_diff1
      value: 49.181984436623694
    - type: nauc_ndcg_at_10_max
      value: 10.65234732763274
    - type: nauc_ndcg_at_10_std
      value: -24.737669349012297
    - type: nauc_ndcg_at_1_diff1
      value: 51.750022350988914
    - type: nauc_ndcg_at_1_max
      value: 3.599863010364626
    - type: nauc_ndcg_at_1_std
      value: -27.670122127567314
    - type: nauc_ndcg_at_20_diff1
      value: 49.275394594995056
    - type: nauc_ndcg_at_20_max
      value: 10.402059796651923
    - type: nauc_ndcg_at_20_std
      value: -24.82329915806705
    - type: nauc_ndcg_at_3_diff1
      value: 48.22614352152889
    - type: nauc_ndcg_at_3_max
      value: 11.67464280791404
    - type: nauc_ndcg_at_3_std
      value: -25.867824868234095
    - type: nauc_ndcg_at_5_diff1
      value: 48.35583502987241
    - type: nauc_ndcg_at_5_max
      value: 10.494278750448451
    - type: nauc_ndcg_at_5_std
      value: -25.11599634172764
    - type: nauc_precision_at_1000_diff1
      value: .nan
    - type: nauc_precision_at_1000_max
      value: .nan
    - type: nauc_precision_at_1000_std
      value: .nan
    - type: nauc_precision_at_100_diff1
      value: -56.39478136433852
    - type: nauc_precision_at_100_max
      value: 86.93518577529493
    - type: nauc_precision_at_100_std
      value: 100.0
    - type: nauc_precision_at_10_diff1
      value: 38.662829729133094
    - type: nauc_precision_at_10_max
      value: 56.38018435740605
    - type: nauc_precision_at_10_std
      value: 6.288091897081105
    - type: nauc_precision_at_1_diff1
      value: 51.750022350988914
    - type: nauc_precision_at_1_max
      value: 3.599863010364626
    - type: nauc_precision_at_1_std
      value: -27.670122127567314
    - type: nauc_precision_at_20_diff1
      value: 34.739153182429085
    - type: nauc_precision_at_20_max
      value: 84.86908403000989
    - type: nauc_precision_at_20_std
      value: 29.156199421219455
    - type: nauc_precision_at_3_diff1
      value: 42.09287362529135
    - type: nauc_precision_at_3_max
      value: 23.629152759287074
    - type: nauc_precision_at_3_std
      value: -23.721376911302492
    - type: nauc_precision_at_5_diff1
      value: 36.03866171924644
    - type: nauc_precision_at_5_max
      value: 29.166173558775327
    - type: nauc_precision_at_5_std
      value: -15.096374563068448
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: -56.39478136433541
    - type: nauc_recall_at_100_max
      value: 86.93518577528111
    - type: nauc_recall_at_100_std
      value: 100.0
    - type: nauc_recall_at_10_diff1
      value: 38.66282972913384
    - type: nauc_recall_at_10_max
      value: 56.3801843574071
    - type: nauc_recall_at_10_std
      value: 6.288091897082639
    - type: nauc_recall_at_1_diff1
      value: 51.750022350988914
    - type: nauc_recall_at_1_max
      value: 3.599863010364626
    - type: nauc_recall_at_1_std
      value: -27.670122127567314
    - type: nauc_recall_at_20_diff1
      value: 34.7391531824321
    - type: nauc_recall_at_20_max
      value: 84.86908403001016
    - type: nauc_recall_at_20_std
      value: 29.156199421220748
    - type: nauc_recall_at_3_diff1
      value: 42.09287362529107
    - type: nauc_recall_at_3_max
      value: 23.629152759286946
    - type: nauc_recall_at_3_std
      value: -23.72137691130291
    - type: nauc_recall_at_5_diff1
      value: 36.0386617192469
    - type: nauc_recall_at_5_max
      value: 29.1661735587759
    - type: nauc_recall_at_5_std
      value: -15.09637456306774
    - type: ndcg_at_1
      value: 64.39
    - type: ndcg_at_10
      value: 82.422
    - type: ndcg_at_100
      value: 82.86099999999999
    - type: ndcg_at_1000
      value: 82.87299999999999
    - type: ndcg_at_20
      value: 82.67999999999999
    - type: ndcg_at_3
      value: 78.967
    - type: ndcg_at_5
      value: 81.50699999999999
    - type: precision_at_1
      value: 64.39
    - type: precision_at_10
      value: 9.795
    - type: precision_at_100
      value: 0.9990000000000001
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.946
    - type: precision_at_3
      value: 29.691000000000003
    - type: precision_at_5
      value: 19.044
    - type: recall_at_1
      value: 64.39
    - type: recall_at_10
      value: 97.951
    - type: recall_at_100
      value: 99.902
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 98.92699999999999
    - type: recall_at_3
      value: 89.07300000000001
    - type: recall_at_5
      value: 95.22
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB GermanQuAD-Retrieval (default)
      revision: f5c87ae5a2e7a5106606314eef45255f03151bb3
      split: test
      type: mteb/germanquad-retrieval
    metrics:
    - type: main_score
      value: 94.15532365396247
    - type: map_at_1
      value: 90.789
    - type: map_at_10
      value: 94.24
    - type: map_at_100
      value: 94.283
    - type: map_at_1000
      value: 94.284
    - type: map_at_20
      value: 94.272
    - type: map_at_3
      value: 93.913
    - type: map_at_5
      value: 94.155
    - type: mrr_at_1
      value: 90.78947368421053
    - type: mrr_at_10
      value: 94.23987411056376
    - type: mrr_at_100
      value: 94.28320936825
    - type: mrr_at_1000
      value: 94.28350209115848
    - type: mrr_at_20
      value: 94.271919092559
    - type: mrr_at_3
      value: 93.91258318209313
    - type: mrr_at_5
      value: 94.15532365396247
    - type: nauc_map_at_1000_diff1
      value: 89.29089310650436
    - type: nauc_map_at_1000_max
      value: 73.83868784032414
    - type: nauc_map_at_1000_std
      value: -11.635778561889989
    - type: nauc_map_at_100_diff1
      value: 89.29077225707755
    - type: nauc_map_at_100_max
      value: 73.84002740580378
    - type: nauc_map_at_100_std
      value: -11.644096256165092
    - type: nauc_map_at_10_diff1
      value: 89.29117612292366
    - type: nauc_map_at_10_max
      value: 73.97487984981221
    - type: nauc_map_at_10_std
      value: -11.35191794373827
    - type: nauc_map_at_1_diff1
      value: 89.35436544117584
    - type: nauc_map_at_1_max
      value: 70.35936815057701
    - type: nauc_map_at_1_std
      value: -13.598996360976903
    - type: nauc_map_at_20_diff1
      value: 89.2530394052653
    - type: nauc_map_at_20_max
      value: 73.83537529419839
    - type: nauc_map_at_20_std
      value: -11.628272822028478
    - type: nauc_map_at_3_diff1
      value: 89.375111893546
    - type: nauc_map_at_3_max
      value: 74.78900366026112
    - type: nauc_map_at_3_std
      value: -12.720905253503274
    - type: nauc_map_at_5_diff1
      value: 89.35358300820893
    - type: nauc_map_at_5_max
      value: 74.31996219723239
    - type: nauc_map_at_5_std
      value: -10.768642638210867
    - type: nauc_mrr_at_1000_diff1
      value: 89.29089310650436
    - type: nauc_mrr_at_1000_max
      value: 73.83868784032414
    - type: nauc_mrr_at_1000_std
      value: -11.635778561889989
    - type: nauc_mrr_at_100_diff1
      value: 89.29077225707755
    - type: nauc_mrr_at_100_max
      value: 73.84002740580378
    - type: nauc_mrr_at_100_std
      value: -11.644096256165092
    - type: nauc_mrr_at_10_diff1
      value: 89.29117612292366
    - type: nauc_mrr_at_10_max
      value: 73.97487984981221
    - type: nauc_mrr_at_10_std
      value: -11.35191794373827
    - type: nauc_mrr_at_1_diff1
      value: 89.35436544117584
    - type: nauc_mrr_at_1_max
      value: 70.35936815057701
    - type: nauc_mrr_at_1_std
      value: -13.598996360976903
    - type: nauc_mrr_at_20_diff1
      value: 89.2530394052653
    - type: nauc_mrr_at_20_max
      value: 73.83537529419839
    - type: nauc_mrr_at_20_std
      value: -11.628272822028478
    - type: nauc_mrr_at_3_diff1
      value: 89.375111893546
    - type: nauc_mrr_at_3_max
      value: 74.78900366026112
    - type: nauc_mrr_at_3_std
      value: -12.720905253503274
    - type: nauc_mrr_at_5_diff1
      value: 89.35358300820893
    - type: nauc_mrr_at_5_max
      value: 74.31996219723239
    - type: nauc_mrr_at_5_std
      value: -10.768642638210867
    - type: nauc_ndcg_at_1000_diff1
      value: 89.27620775856863
    - type: nauc_ndcg_at_1000_max
      value: 74.2985757362615
    - type: nauc_ndcg_at_1000_std
      value: -11.236142819703023
    - type: nauc_ndcg_at_100_diff1
      value: 89.27284787540731
    - type: nauc_ndcg_at_100_max
      value: 74.33539303365968
    - type: nauc_ndcg_at_100_std
      value: -11.469413615851936
    - type: nauc_ndcg_at_10_diff1
      value: 89.21496710661724
    - type: nauc_ndcg_at_10_max
      value: 75.02035398490516
    - type: nauc_ndcg_at_10_std
      value: -9.903255803665814
    - type: nauc_ndcg_at_1_diff1
      value: 89.35436544117584
    - type: nauc_ndcg_at_1_max
      value: 70.35936815057701
    - type: nauc_ndcg_at_1_std
      value: -13.598996360976903
    - type: nauc_ndcg_at_20_diff1
      value: 89.03561289544179
    - type: nauc_ndcg_at_20_max
      value: 74.4006766600049
    - type: nauc_ndcg_at_20_std
      value: -11.129237862587743
    - type: nauc_ndcg_at_3_diff1
      value: 89.46540193201693
    - type: nauc_ndcg_at_3_max
      value: 76.87093548368378
    - type: nauc_ndcg_at_3_std
      value: -12.484902872086767
    - type: nauc_ndcg_at_5_diff1
      value: 89.39924941584766
    - type: nauc_ndcg_at_5_max
      value: 75.96975269092722
    - type: nauc_ndcg_at_5_std
      value: -8.180295581144833
    - type: nauc_precision_at_1000_diff1
      value: 100.0
    - type: nauc_precision_at_1000_max
      value: 100.0
    - type: nauc_precision_at_1000_std
      value: 100.0
    - type: nauc_precision_at_100_diff1
      value: 86.93074003795302
    - type: nauc_precision_at_100_max
      value: 100.0
    - type: nauc_precision_at_100_std
      value: -174.07785375176616
    - type: nauc_precision_at_10_diff1
      value: 87.43064119412082
    - type: nauc_precision_at_10_max
      value: 90.60785783417448
    - type: nauc_precision_at_10_std
      value: 15.378710059645906
    - type: nauc_precision_at_1_diff1
      value: 89.35436544117584
    - type: nauc_precision_at_1_max
      value: 70.35936815057701
    - type: nauc_precision_at_1_std
      value: -13.598996360976903
    - type: nauc_precision_at_20_diff1
      value: 78.78206037685919
    - type: nauc_precision_at_20_max
      value: 82.52264166455923
    - type: nauc_precision_at_20_std
      value: -5.95806599216658
    - type: nauc_precision_at_3_diff1
      value: 90.12709256456401
    - type: nauc_precision_at_3_max
      value: 90.72678805838154
    - type: nauc_precision_at_3_std
      value: -11.047599315631993
    - type: nauc_precision_at_5_diff1
      value: 89.9066873566561
    - type: nauc_precision_at_5_max
      value: 93.51571626543664
    - type: nauc_precision_at_5_std
      value: 22.632403279126162
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: 86.93074003793416
    - type: nauc_recall_at_100_max
      value: 100.0
    - type: nauc_recall_at_100_std
      value: -174.07785375175723
    - type: nauc_recall_at_10_diff1
      value: 87.43064119411991
    - type: nauc_recall_at_10_max
      value: 90.60785783417579
    - type: nauc_recall_at_10_std
      value: 15.378710059643607
    - type: nauc_recall_at_1_diff1
      value: 89.35436544117584
    - type: nauc_recall_at_1_max
      value: 70.35936815057701
    - type: nauc_recall_at_1_std
      value: -13.598996360976903
    - type: nauc_recall_at_20_diff1
      value: 78.78206037685645
    - type: nauc_recall_at_20_max
      value: 82.52264166455791
    - type: nauc_recall_at_20_std
      value: -5.958065992168697
    - type: nauc_recall_at_3_diff1
      value: 90.12709256456463
    - type: nauc_recall_at_3_max
      value: 90.7267880583832
    - type: nauc_recall_at_3_std
      value: -11.047599315631881
    - type: nauc_recall_at_5_diff1
      value: 89.90668735665676
    - type: nauc_recall_at_5_max
      value: 93.51571626543753
    - type: nauc_recall_at_5_std
      value: 22.632403279126112
    - type: ndcg_at_1
      value: 90.789
    - type: ndcg_at_10
      value: 95.46
    - type: ndcg_at_100
      value: 95.652
    - type: ndcg_at_1000
      value: 95.659
    - type: ndcg_at_20
      value: 95.575
    - type: ndcg_at_3
      value: 94.82000000000001
    - type: ndcg_at_5
      value: 95.26400000000001
    - type: precision_at_1
      value: 90.789
    - type: precision_at_10
      value: 9.908999999999999
    - type: precision_at_100
      value: 1.0
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.977
    - type: precision_at_3
      value: 32.471
    - type: precision_at_5
      value: 19.701
    - type: recall_at_1
      value: 90.789
    - type: recall_at_10
      value: 99.093
    - type: recall_at_100
      value: 99.955
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 99.546
    - type: recall_at_3
      value: 97.414
    - type: recall_at_5
      value: 98.503
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB GermanSTSBenchmark (default)
      revision: e36907544d44c3a247898ed81540310442329e20
      split: test
      type: jinaai/german-STSbenchmark
    metrics:
    - type: cosine_pearson
      value: 86.55319003300265
    - type: cosine_spearman
      value: 87.50267373081324
    - type: euclidean_pearson
      value: 87.41630636501863
    - type: euclidean_spearman
      value: 88.02170803409365
    - type: main_score
      value: 87.50267373081324
    - type: manhattan_pearson
      value: 87.33703179056744
    - type: manhattan_spearman
      value: 87.99192826922514
    - type: pearson
      value: 86.55319003300265
    - type: spearman
      value: 87.50267373081324
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB HALClusteringS2S (default)
      revision: e06ebbbb123f8144bef1a5d18796f3dec9ae2915
      split: test
      type: lyon-nlp/clustering-hal-s2s
    metrics:
    - type: main_score
      value: 27.477557517301303
    - type: v_measure
      value: 27.477557517301303
    - type: v_measure_std
      value: 3.3525736581861336
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB HeadlineClassification (default)
      revision: 2fe05ee6b5832cda29f2ef7aaad7b7fe6a3609eb
      split: test
      type: ai-forever/headline-classification
    metrics:
    - type: accuracy
      value: 75.0830078125
    - type: f1
      value: 75.08863209267814
    - type: f1_weighted
      value: 75.08895979060917
    - type: main_score
      value: 75.0830078125
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB HotpotQA (default)
      revision: ab518f4d6fcca38d87c25209f94beba119d02014
      split: test
      type: mteb/hotpotqa
    metrics:
    - type: map_at_1
      value: 38.143
    - type: map_at_10
      value: 55.916999999999994
    - type: map_at_100
      value: 56.706
    - type: map_at_1000
      value: 56.77100000000001
    - type: map_at_20
      value: 56.367
    - type: map_at_3
      value: 53.111
    - type: map_at_5
      value: 54.839000000000006
    - type: mrr_at_1
      value: 76.286
    - type: mrr_at_10
      value: 81.879
    - type: mrr_at_100
      value: 82.09100000000001
    - type: mrr_at_1000
      value: 82.101
    - type: mrr_at_20
      value: 82.01
    - type: mrr_at_3
      value: 80.972
    - type: mrr_at_5
      value: 81.537
    - type: ndcg_at_1
      value: 76.286
    - type: ndcg_at_10
      value: 64.673
    - type: ndcg_at_100
      value: 67.527
    - type: ndcg_at_1000
      value: 68.857
    - type: ndcg_at_20
      value: 65.822
    - type: ndcg_at_3
      value: 60.616
    - type: ndcg_at_5
      value: 62.827999999999996
    - type: precision_at_1
      value: 76.286
    - type: precision_at_10
      value: 13.196
    - type: precision_at_100
      value: 1.544
    - type: precision_at_1000
      value: 0.172
    - type: precision_at_20
      value: 6.968000000000001
    - type: precision_at_3
      value: 37.992
    - type: precision_at_5
      value: 24.54
    - type: recall_at_1
      value: 38.143
    - type: recall_at_10
      value: 65.982
    - type: recall_at_100
      value: 77.225
    - type: recall_at_1000
      value: 86.077
    - type: recall_at_20
      value: 69.68299999999999
    - type: recall_at_3
      value: 56.989000000000004
    - type: recall_at_5
      value: 61.35
    - type: main_score
      value: 64.673
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB IFlyTek (default)
      revision: 421605374b29664c5fc098418fe20ada9bd55f8a
      split: validation
      type: C-MTEB/IFlyTek-classification
    metrics:
    - type: accuracy
      value: 41.67756829549827
    - type: f1
      value: 33.929325579581636
    - type: f1_weighted
      value: 43.03952025643197
    - type: main_score
      value: 41.67756829549827
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB ImdbClassification (default)
      revision: 3d86128a09e091d6018b6d26cad27f2739fc2db7
      split: test
      type: mteb/imdb
    metrics:
    - type: accuracy
      value: 91.90440000000001
    - type: ap
      value: 88.78663714603425
    - type: ap_weighted
      value: 88.78663714603425
    - type: f1
      value: 91.89564361975891
    - type: f1_weighted
      value: 91.89564361975891
    - type: main_score
      value: 91.90440000000001
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB InappropriatenessClassification (default)
      revision: 601651fdc45ef243751676e62dd7a19f491c0285
      split: test
      type: ai-forever/inappropriateness-classification
    metrics:
    - type: accuracy
      value: 61.0498046875
    - type: ap
      value: 57.04240566648215
    - type: ap_weighted
      value: 57.04240566648215
    - type: f1
      value: 60.867630038606954
    - type: f1_weighted
      value: 60.867630038606954
    - type: main_score
      value: 61.0498046875
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB JDReview (default)
      revision: b7c64bd89eb87f8ded463478346f76731f07bf8b
      split: test
      type: C-MTEB/JDReview-classification
    metrics:
    - type: accuracy
      value: 83.50844277673546
    - type: ap
      value: 48.46732380712268
    - type: ap_weighted
      value: 48.46732380712268
    - type: f1
      value: 77.43967451387445
    - type: f1_weighted
      value: 84.78462929014114
    - type: main_score
      value: 83.50844277673546
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB KinopoiskClassification (default)
      revision: 5911f26666ac11af46cb9c6849d0dc80a378af24
      split: test
      type: ai-forever/kinopoisk-sentiment-classification
    metrics:
    - type: accuracy
      value: 62.393333333333324
    - type: f1
      value: 61.35940129568015
    - type: f1_weighted
      value: 61.35940129568015
    - type: main_score
      value: 62.393333333333324
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB LCQMC (default)
      revision: 17f9b096f80380fce5ed12a9be8be7784b337daf
      split: test
      type: C-MTEB/LCQMC
    metrics:
    - type: cosine_pearson
      value: 67.74375505907872
    - type: cosine_spearman
      value: 75.94582231399434
    - type: euclidean_pearson
      value: 74.52501692443582
    - type: euclidean_spearman
      value: 75.88428434746646
    - type: main_score
      value: 75.94582231399434
    - type: manhattan_pearson
      value: 74.55015441749529
    - type: manhattan_spearman
      value: 75.83288262176175
    - type: pearson
      value: 67.74375505907872
    - type: spearman
      value: 75.94582231399434
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB LEMBNarrativeQARetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: test
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 23.093
    - type: map_at_10
      value: 30.227999999999998
    - type: map_at_100
      value: 31.423000000000002
    - type: map_at_1000
      value: 31.533
    - type: map_at_20
      value: 30.835
    - type: map_at_3
      value: 27.983999999999998
    - type: map_at_5
      value: 29.253
    - type: mrr_at_1
      value: 23.093
    - type: mrr_at_10
      value: 30.227999999999998
    - type: mrr_at_100
      value: 31.423000000000002
    - type: mrr_at_1000
      value: 31.533
    - type: mrr_at_20
      value: 30.835
    - type: mrr_at_3
      value: 27.983999999999998
    - type: mrr_at_5
      value: 29.253
    - type: ndcg_at_1
      value: 23.093
    - type: ndcg_at_10
      value: 34.297
    - type: ndcg_at_100
      value: 41.049
    - type: ndcg_at_1000
      value: 43.566
    - type: ndcg_at_20
      value: 36.52
    - type: ndcg_at_3
      value: 29.629
    - type: ndcg_at_5
      value: 31.926
    - type: precision_at_1
      value: 23.093
    - type: precision_at_10
      value: 4.735
    - type: precision_at_100
      value: 0.8109999999999999
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 2.8080000000000003
    - type: precision_at_3
      value: 11.468
    - type: precision_at_5
      value: 8.001
    - type: recall_at_1
      value: 23.093
    - type: recall_at_10
      value: 47.354
    - type: recall_at_100
      value: 81.147
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 56.16799999999999
    - type: recall_at_3
      value: 34.405
    - type: recall_at_5
      value: 40.004
    - type: main_score
      value: 34.297
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB LEMBNeedleRetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: test_256
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 64.0
    - type: map_at_10
      value: 77.083
    - type: map_at_100
      value: 77.265
    - type: map_at_1000
      value: 77.265
    - type: map_at_20
      value: 77.265
    - type: map_at_3
      value: 76.333
    - type: map_at_5
      value: 76.833
    - type: mrr_at_1
      value: 64.0
    - type: mrr_at_10
      value: 77.083
    - type: mrr_at_100
      value: 77.265
    - type: mrr_at_1000
      value: 77.265
    - type: mrr_at_20
      value: 77.265
    - type: mrr_at_3
      value: 76.333
    - type: mrr_at_5
      value: 76.833
    - type: ndcg_at_1
      value: 64.0
    - type: ndcg_at_10
      value: 82.325
    - type: ndcg_at_100
      value: 82.883
    - type: ndcg_at_1000
      value: 82.883
    - type: ndcg_at_20
      value: 82.883
    - type: ndcg_at_3
      value: 80.833
    - type: ndcg_at_5
      value: 81.694
    - type: precision_at_1
      value: 64.0
    - type: precision_at_10
      value: 9.8
    - type: precision_at_100
      value: 1.0
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 5.0
    - type: precision_at_3
      value: 31.333
    - type: precision_at_5
      value: 19.2
    - type: recall_at_1
      value: 64.0
    - type: recall_at_10
      value: 98.0
    - type: recall_at_100
      value: 100.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 100.0
    - type: recall_at_3
      value: 94.0
    - type: recall_at_5
      value: 96.0
    - type: main_score
      value: 64.0
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB LEMBPasskeyRetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: test_256
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 100.0
    - type: map_at_10
      value: 100.0
    - type: map_at_100
      value: 100.0
    - type: map_at_1000
      value: 100.0
    - type: map_at_20
      value: 100.0
    - type: map_at_3
      value: 100.0
    - type: map_at_5
      value: 100.0
    - type: mrr_at_1
      value: 100.0
    - type: mrr_at_10
      value: 100.0
    - type: mrr_at_100
      value: 100.0
    - type: mrr_at_1000
      value: 100.0
    - type: mrr_at_20
      value: 100.0
    - type: mrr_at_3
      value: 100.0
    - type: mrr_at_5
      value: 100.0
    - type: ndcg_at_1
      value: 100.0
    - type: ndcg_at_10
      value: 100.0
    - type: ndcg_at_100
      value: 100.0
    - type: ndcg_at_1000
      value: 100.0
    - type: ndcg_at_20
      value: 100.0
    - type: ndcg_at_3
      value: 100.0
    - type: ndcg_at_5
      value: 100.0
    - type: precision_at_1
      value: 100.0
    - type: precision_at_10
      value: 10.0
    - type: precision_at_100
      value: 1.0
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 5.0
    - type: precision_at_3
      value: 33.333
    - type: precision_at_5
      value: 20.0
    - type: recall_at_1
      value: 100.0
    - type: recall_at_10
      value: 100.0
    - type: recall_at_100
      value: 100.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 100.0
    - type: recall_at_3
      value: 100.0
    - type: recall_at_5
      value: 100.0
    - type: main_score
      value: 100.0
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB LEMBQMSumRetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: test
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 24.361
    - type: map_at_10
      value: 33.641
    - type: map_at_100
      value: 35.104
    - type: map_at_1000
      value: 35.127
    - type: map_at_20
      value: 34.388999999999996
    - type: map_at_3
      value: 30.255
    - type: map_at_5
      value: 32.079
    - type: mrr_at_1
      value: 24.361
    - type: mrr_at_10
      value: 33.641
    - type: mrr_at_100
      value: 35.104
    - type: mrr_at_1000
      value: 35.127
    - type: mrr_at_20
      value: 34.388999999999996
    - type: mrr_at_3
      value: 30.255
    - type: mrr_at_5
      value: 32.079
    - type: ndcg_at_1
      value: 24.361
    - type: ndcg_at_10
      value: 39.337
    - type: ndcg_at_100
      value: 47.384
    - type: ndcg_at_1000
      value: 47.75
    - type: ndcg_at_20
      value: 42.077999999999996
    - type: ndcg_at_3
      value: 32.235
    - type: ndcg_at_5
      value: 35.524
    - type: precision_at_1
      value: 24.361
    - type: precision_at_10
      value: 5.783
    - type: precision_at_100
      value: 0.975
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 3.435
    - type: precision_at_3
      value: 12.661
    - type: precision_at_5
      value: 9.193999999999999
    - type: recall_at_1
      value: 24.361
    - type: recall_at_10
      value: 57.826
    - type: recall_at_100
      value: 97.51100000000001
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 68.697
    - type: recall_at_3
      value: 37.983
    - type: recall_at_5
      value: 45.972
    - type: main_score
      value: 39.337
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB LEMBSummScreenFDRetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: validation
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 84.821
    - type: map_at_10
      value: 90.11200000000001
    - type: map_at_100
      value: 90.158
    - type: map_at_1000
      value: 90.158
    - type: map_at_20
      value: 90.137
    - type: map_at_3
      value: 89.385
    - type: map_at_5
      value: 89.876
    - type: mrr_at_1
      value: 84.821
    - type: mrr_at_10
      value: 90.11200000000001
    - type: mrr_at_100
      value: 90.158
    - type: mrr_at_1000
      value: 90.158
    - type: mrr_at_20
      value: 90.137
    - type: mrr_at_3
      value: 89.385
    - type: mrr_at_5
      value: 89.876
    - type: ndcg_at_1
      value: 84.821
    - type: ndcg_at_10
      value: 92.334
    - type: ndcg_at_100
      value: 92.535
    - type: ndcg_at_1000
      value: 92.535
    - type: ndcg_at_20
      value: 92.414
    - type: ndcg_at_3
      value: 90.887
    - type: ndcg_at_5
      value: 91.758
    - type: precision_at_1
      value: 84.821
    - type: precision_at_10
      value: 9.911
    - type: precision_at_100
      value: 1.0
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.97
    - type: precision_at_3
      value: 31.746000000000002
    - type: precision_at_5
      value: 19.464000000000002
    - type: recall_at_1
      value: 84.821
    - type: recall_at_10
      value: 99.107
    - type: recall_at_100
      value: 100.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 99.405
    - type: recall_at_3
      value: 95.238
    - type: recall_at_5
      value: 97.321
    - type: main_score
      value: 92.334
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB LEMBWikimQARetrieval (default)
      revision: 6e346642246bfb4928c560ee08640dc84d074e8c
      split: test
      type: dwzhu/LongEmbed
    metrics:
    - type: map_at_1
      value: 53.667
    - type: map_at_10
      value: 61.719
    - type: map_at_100
      value: 62.471
    - type: map_at_1000
      value: 62.492000000000004
    - type: map_at_20
      value: 62.153000000000006
    - type: map_at_3
      value: 59.167
    - type: map_at_5
      value: 60.95
    - type: mrr_at_1
      value: 53.667
    - type: mrr_at_10
      value: 61.719
    - type: mrr_at_100
      value: 62.471
    - type: mrr_at_1000
      value: 62.492000000000004
    - type: mrr_at_20
      value: 62.153000000000006
    - type: mrr_at_3
      value: 59.167
    - type: mrr_at_5
      value: 60.95
    - type: ndcg_at_1
      value: 53.667
    - type: ndcg_at_10
      value: 66.018
    - type: ndcg_at_100
      value: 69.726
    - type: ndcg_at_1000
      value: 70.143
    - type: ndcg_at_20
      value: 67.61399999999999
    - type: ndcg_at_3
      value: 60.924
    - type: ndcg_at_5
      value: 64.10900000000001
    - type: precision_at_1
      value: 53.667
    - type: precision_at_10
      value: 7.9670000000000005
    - type: precision_at_100
      value: 0.97
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.3
    - type: precision_at_3
      value: 22.0
    - type: precision_at_5
      value: 14.732999999999999
    - type: recall_at_1
      value: 53.667
    - type: recall_at_10
      value: 79.667
    - type: recall_at_100
      value: 97.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 86.0
    - type: recall_at_3
      value: 66.0
    - type: recall_at_5
      value: 73.667
    - type: main_score
      value: 66.018
    task:
      type: Retrieval
  - dataset:
      config: deu-deu
      name: MTEB MLQARetrieval (deu-deu)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 67.548
    - type: map_at_1
      value: 56.559000000000005
    - type: map_at_10
      value: 63.867
    - type: map_at_100
      value: 64.429
    - type: map_at_1000
      value: 64.457
    - type: map_at_20
      value: 64.215
    - type: map_at_3
      value: 62.109
    - type: map_at_5
      value: 63.101
    - type: mrr_at_1
      value: 56.56990915134057
    - type: mrr_at_10
      value: 63.86820789324668
    - type: mrr_at_100
      value: 64.42973602152581
    - type: mrr_at_1000
      value: 64.45818598090155
    - type: mrr_at_20
      value: 64.2163052263868
    - type: mrr_at_3
      value: 62.10946155550634
    - type: mrr_at_5
      value: 63.10104143585199
    - type: nauc_map_at_1000_diff1
      value: 73.78440163370111
    - type: nauc_map_at_1000_max
      value: 66.37875518052162
    - type: nauc_map_at_1000_std
      value: -17.063915098135396
    - type: nauc_map_at_100_diff1
      value: 73.77180802985815
    - type: nauc_map_at_100_max
      value: 66.38365998362033
    - type: nauc_map_at_100_std
      value: -17.053345109661972
    - type: nauc_map_at_10_diff1
      value: 73.70041876696037
    - type: nauc_map_at_10_max
      value: 66.33213342705997
    - type: nauc_map_at_10_std
      value: -17.40657791273925
    - type: nauc_map_at_1_diff1
      value: 76.8784374396948
    - type: nauc_map_at_1_max
      value: 64.07170606935357
    - type: nauc_map_at_1_std
      value: -18.464213686790654
    - type: nauc_map_at_20_diff1
      value: 73.72371377231813
    - type: nauc_map_at_20_max
      value: 66.42108121059451
    - type: nauc_map_at_20_std
      value: -17.05384923889036
    - type: nauc_map_at_3_diff1
      value: 74.08287018839246
    - type: nauc_map_at_3_max
      value: 66.42422337760333
    - type: nauc_map_at_3_std
      value: -17.79503404131652
    - type: nauc_map_at_5_diff1
      value: 73.9294779027339
    - type: nauc_map_at_5_max
      value: 66.51752041065726
    - type: nauc_map_at_5_std
      value: -17.67309805113804
    - type: nauc_mrr_at_1000_diff1
      value: 73.78389736923545
    - type: nauc_mrr_at_1000_max
      value: 66.37929720858341
    - type: nauc_mrr_at_1000_std
      value: -17.058591711291278
    - type: nauc_mrr_at_100_diff1
      value: 73.77126451253136
    - type: nauc_mrr_at_100_max
      value: 66.38405917246607
    - type: nauc_mrr_at_100_std
      value: -17.047251035212863
    - type: nauc_mrr_at_10_diff1
      value: 73.69960470665124
    - type: nauc_mrr_at_10_max
      value: 66.33265194210313
    - type: nauc_mrr_at_10_std
      value: -17.399659076827998
    - type: nauc_mrr_at_1_diff1
      value: 76.8689850260726
    - type: nauc_mrr_at_1_max
      value: 64.09858188287487
    - type: nauc_mrr_at_1_std
      value: -18.46064784201847
    - type: nauc_mrr_at_20_diff1
      value: 73.72312682063128
    - type: nauc_mrr_at_20_max
      value: 66.42181932858745
    - type: nauc_mrr_at_20_std
      value: -17.04690257511092
    - type: nauc_mrr_at_3_diff1
      value: 74.08287018839246
    - type: nauc_mrr_at_3_max
      value: 66.42422337760333
    - type: nauc_mrr_at_3_std
      value: -17.79503404131652
    - type: nauc_mrr_at_5_diff1
      value: 73.9294779027339
    - type: nauc_mrr_at_5_max
      value: 66.51752041065726
    - type: nauc_mrr_at_5_std
      value: -17.67309805113804
    - type: nauc_ndcg_at_1000_diff1
      value: 72.97825548342801
    - type: nauc_ndcg_at_1000_max
      value: 66.96275437178257
    - type: nauc_ndcg_at_1000_std
      value: -15.611902299641587
    - type: nauc_ndcg_at_100_diff1
      value: 72.58724738936613
    - type: nauc_ndcg_at_100_max
      value: 67.16774012704182
    - type: nauc_ndcg_at_100_std
      value: -14.945088654796812
    - type: nauc_ndcg_at_10_diff1
      value: 72.16253640477947
    - type: nauc_ndcg_at_10_max
      value: 67.01746849484621
    - type: nauc_ndcg_at_10_std
      value: -16.46102507270809
    - type: nauc_ndcg_at_1_diff1
      value: 76.8689850260726
    - type: nauc_ndcg_at_1_max
      value: 64.09858188287487
    - type: nauc_ndcg_at_1_std
      value: -18.46064784201847
    - type: nauc_ndcg_at_20_diff1
      value: 72.19995325129975
    - type: nauc_ndcg_at_20_max
      value: 67.39639713797962
    - type: nauc_ndcg_at_20_std
      value: -15.091689370748531
    - type: nauc_ndcg_at_3_diff1
      value: 73.13123604206514
    - type: nauc_ndcg_at_3_max
      value: 67.23123167871547
    - type: nauc_ndcg_at_3_std
      value: -17.492755234009156
    - type: nauc_ndcg_at_5_diff1
      value: 72.8154718929895
    - type: nauc_ndcg_at_5_max
      value: 67.44578008373777
    - type: nauc_ndcg_at_5_std
      value: -17.251840358751362
    - type: nauc_precision_at_1000_diff1
      value: 47.89748325983604
    - type: nauc_precision_at_1000_max
      value: 70.47466197804906
    - type: nauc_precision_at_1000_std
      value: 72.66193512114775
    - type: nauc_precision_at_100_diff1
      value: 59.493743734005356
    - type: nauc_precision_at_100_max
      value: 74.02140147220713
    - type: nauc_precision_at_100_std
      value: 17.26664098026236
    - type: nauc_precision_at_10_diff1
      value: 64.94415011040277
    - type: nauc_precision_at_10_max
      value: 69.6963814950747
    - type: nauc_precision_at_10_std
      value: -11.663043657012954
    - type: nauc_precision_at_1_diff1
      value: 76.8689850260726
    - type: nauc_precision_at_1_max
      value: 64.09858188287487
    - type: nauc_precision_at_1_std
      value: -18.46064784201847
    - type: nauc_precision_at_20_diff1
      value: 63.145886909986416
    - type: nauc_precision_at_20_max
      value: 72.95708033630744
    - type: nauc_precision_at_20_std
      value: -1.5039593629280323
    - type: nauc_precision_at_3_diff1
      value: 69.88902201644449
    - type: nauc_precision_at_3_max
      value: 69.80499971089935
    - type: nauc_precision_at_3_std
      value: -16.444680766676647
    - type: nauc_precision_at_5_diff1
      value: 68.60869967062919
    - type: nauc_precision_at_5_max
      value: 70.75998207564281
    - type: nauc_precision_at_5_std
      value: -15.62613396998262
    - type: nauc_recall_at_1000_diff1
      value: 62.6646436338833
    - type: nauc_recall_at_1000_max
      value: 86.17801636476078
    - type: nauc_recall_at_1000_std
      value: 71.84718775540334
    - type: nauc_recall_at_100_diff1
      value: 61.110492191439505
    - type: nauc_recall_at_100_max
      value: 75.45730686603042
    - type: nauc_recall_at_100_std
      value: 16.202465011589428
    - type: nauc_recall_at_10_diff1
      value: 65.1522196516815
    - type: nauc_recall_at_10_max
      value: 69.7626435962161
    - type: nauc_recall_at_10_std
      value: -11.801178474770449
    - type: nauc_recall_at_1_diff1
      value: 76.8784374396948
    - type: nauc_recall_at_1_max
      value: 64.07170606935357
    - type: nauc_recall_at_1_std
      value: -18.464213686790654
    - type: nauc_recall_at_20_diff1
      value: 63.40332739504143
    - type: nauc_recall_at_20_max
      value: 73.04113661090965
    - type: nauc_recall_at_20_std
      value: -1.6609741140266947
    - type: nauc_recall_at_3_diff1
      value: 70.03728086098866
    - type: nauc_recall_at_3_max
      value: 69.85953774320521
    - type: nauc_recall_at_3_std
      value: -16.482993123411706
    - type: nauc_recall_at_5_diff1
      value: 68.77396121765933
    - type: nauc_recall_at_5_max
      value: 70.8231205493519
    - type: nauc_recall_at_5_std
      value: -15.668037770700863
    - type: ndcg_at_1
      value: 56.57
    - type: ndcg_at_10
      value: 67.548
    - type: ndcg_at_100
      value: 70.421
    - type: ndcg_at_1000
      value: 71.198
    - type: ndcg_at_20
      value: 68.829
    - type: ndcg_at_3
      value: 63.88700000000001
    - type: ndcg_at_5
      value: 65.689
    - type: precision_at_1
      value: 56.57
    - type: precision_at_10
      value: 7.922
    - type: precision_at_100
      value: 0.9299999999999999
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.216
    - type: precision_at_3
      value: 23.015
    - type: precision_at_5
      value: 14.691
    - type: recall_at_1
      value: 56.559000000000005
    - type: recall_at_10
      value: 79.182
    - type: recall_at_100
      value: 92.946
    - type: recall_at_1000
      value: 99.092
    - type: recall_at_20
      value: 84.27900000000001
    - type: recall_at_3
      value: 69.023
    - type: recall_at_5
      value: 73.432
    task:
      type: Retrieval
  - dataset:
      config: deu-spa
      name: MTEB MLQARetrieval (deu-spa)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 70.645
    - type: map_at_1
      value: 58.423
    - type: map_at_10
      value: 66.613
    - type: map_at_100
      value: 67.14099999999999
    - type: map_at_1000
      value: 67.161
    - type: map_at_20
      value: 66.965
    - type: map_at_3
      value: 64.714
    - type: map_at_5
      value: 65.835
    - type: mrr_at_1
      value: 58.4225352112676
    - type: mrr_at_10
      value: 66.61321260898735
    - type: mrr_at_100
      value: 67.13991570812132
    - type: mrr_at_1000
      value: 67.1598532168174
    - type: mrr_at_20
      value: 66.96384710024888
    - type: mrr_at_3
      value: 64.71361502347425
    - type: mrr_at_5
      value: 65.83474178403769
    - type: nauc_map_at_1000_diff1
      value: 73.9485117118935
    - type: nauc_map_at_1000_max
      value: 65.74479869396299
    - type: nauc_map_at_1000_std
      value: -20.300269749495563
    - type: nauc_map_at_100_diff1
      value: 73.93900406302829
    - type: nauc_map_at_100_max
      value: 65.75508449194885
    - type: nauc_map_at_100_std
      value: -20.265330791570175
    - type: nauc_map_at_10_diff1
      value: 73.84863233472605
    - type: nauc_map_at_10_max
      value: 65.89377317378211
    - type: nauc_map_at_10_std
      value: -20.404123131964695
    - type: nauc_map_at_1_diff1
      value: 76.73627284218519
    - type: nauc_map_at_1_max
      value: 62.94957512510876
    - type: nauc_map_at_1_std
      value: -20.99649749330682
    - type: nauc_map_at_20_diff1
      value: 73.88712006109598
    - type: nauc_map_at_20_max
      value: 65.82057018162664
    - type: nauc_map_at_20_std
      value: -20.269476512431915
    - type: nauc_map_at_3_diff1
      value: 74.21419190161502
    - type: nauc_map_at_3_max
      value: 65.64993368062119
    - type: nauc_map_at_3_std
      value: -21.34641749007071
    - type: nauc_map_at_5_diff1
      value: 74.0119419385777
    - type: nauc_map_at_5_max
      value: 65.69809416369732
    - type: nauc_map_at_5_std
      value: -21.16901556082261
    - type: nauc_mrr_at_1000_diff1
      value: 73.94915184134923
    - type: nauc_mrr_at_1000_max
      value: 65.74522469633418
    - type: nauc_mrr_at_1000_std
      value: -20.303028367132246
    - type: nauc_mrr_at_100_diff1
      value: 73.93964394728808
    - type: nauc_mrr_at_100_max
      value: 65.75550992323707
    - type: nauc_mrr_at_100_std
      value: -20.26808820438918
    - type: nauc_mrr_at_10_diff1
      value: 73.84863233472605
    - type: nauc_mrr_at_10_max
      value: 65.89377317378211
    - type: nauc_mrr_at_10_std
      value: -20.404123131964695
    - type: nauc_mrr_at_1_diff1
      value: 76.73627284218519
    - type: nauc_mrr_at_1_max
      value: 62.94957512510876
    - type: nauc_mrr_at_1_std
      value: -20.99649749330682
    - type: nauc_mrr_at_20_diff1
      value: 73.88775721128745
    - type: nauc_mrr_at_20_max
      value: 65.820991355628
    - type: nauc_mrr_at_20_std
      value: -20.272216587019734
    - type: nauc_mrr_at_3_diff1
      value: 74.21419190161502
    - type: nauc_mrr_at_3_max
      value: 65.64993368062119
    - type: nauc_mrr_at_3_std
      value: -21.34641749007071
    - type: nauc_mrr_at_5_diff1
      value: 74.0119419385777
    - type: nauc_mrr_at_5_max
      value: 65.69809416369732
    - type: nauc_mrr_at_5_std
      value: -21.16901556082261
    - type: nauc_ndcg_at_1000_diff1
      value: 73.29396365944277
    - type: nauc_ndcg_at_1000_max
      value: 66.44879592109541
    - type: nauc_ndcg_at_1000_std
      value: -19.285991058788195
    - type: nauc_ndcg_at_100_diff1
      value: 73.0159172721162
    - type: nauc_ndcg_at_100_max
      value: 66.76216389231388
    - type: nauc_ndcg_at_100_std
      value: -18.27931368094887
    - type: nauc_ndcg_at_10_diff1
      value: 72.42096650774693
    - type: nauc_ndcg_at_10_max
      value: 67.48592688463306
    - type: nauc_ndcg_at_10_std
      value: -18.91453756077581
    - type: nauc_ndcg_at_1_diff1
      value: 76.73627284218519
    - type: nauc_ndcg_at_1_max
      value: 62.94957512510876
    - type: nauc_ndcg_at_1_std
      value: -20.99649749330682
    - type: nauc_ndcg_at_20_diff1
      value: 72.53699362385684
    - type: nauc_ndcg_at_20_max
      value: 67.22763976357872
    - type: nauc_ndcg_at_20_std
      value: -18.299910635008338
    - type: nauc_ndcg_at_3_diff1
      value: 73.3698453761989
    - type: nauc_ndcg_at_3_max
      value: 66.71056987289383
    - type: nauc_ndcg_at_3_std
      value: -21.405154376652803
    - type: nauc_ndcg_at_5_diff1
      value: 72.9491030712935
    - type: nauc_ndcg_at_5_max
      value: 66.85786103137077
    - type: nauc_ndcg_at_5_std
      value: -21.04005053344073
    - type: nauc_precision_at_1000_diff1
      value: 17.02462370967451
    - type: nauc_precision_at_1000_max
      value: 48.03260752496052
    - type: nauc_precision_at_1000_std
      value: 87.56077915079334
    - type: nauc_precision_at_100_diff1
      value: 58.590352501194985
    - type: nauc_precision_at_100_max
      value: 78.2649015433222
    - type: nauc_precision_at_100_std
      value: 28.05030453158992
    - type: nauc_precision_at_10_diff1
      value: 64.89497928764766
    - type: nauc_precision_at_10_max
      value: 75.93257124951242
    - type: nauc_precision_at_10_std
      value: -9.825306994117462
    - type: nauc_precision_at_1_diff1
      value: 76.73627284218519
    - type: nauc_precision_at_1_max
      value: 62.94957512510876
    - type: nauc_precision_at_1_std
      value: -20.99649749330682
    - type: nauc_precision_at_20_diff1
      value: 62.11366204321558
    - type: nauc_precision_at_20_max
      value: 75.9571427846493
    - type: nauc_precision_at_20_std
      value: -0.94585212808191
    - type: nauc_precision_at_3_diff1
      value: 70.52940972112398
    - type: nauc_precision_at_3_max
      value: 70.3402053170779
    - type: nauc_precision_at_3_std
      value: -21.579778424241304
    - type: nauc_precision_at_5_diff1
      value: 68.78962580223575
    - type: nauc_precision_at_5_max
      value: 71.41410894398376
    - type: nauc_precision_at_5_std
      value: -20.415603405161956
    - type: nauc_recall_at_1000_diff1
      value: 55.88625447348128
    - type: nauc_recall_at_1000_max
      value: 100.0
    - type: nauc_recall_at_1000_std
      value: 100.0
    - type: nauc_recall_at_100_diff1
      value: 61.17942268389525
    - type: nauc_recall_at_100_max
      value: 81.12207841563487
    - type: nauc_recall_at_100_std
      value: 27.141215257528113
    - type: nauc_recall_at_10_diff1
      value: 64.8949792876478
    - type: nauc_recall_at_10_max
      value: 75.93257124951249
    - type: nauc_recall_at_10_std
      value: -9.825306994117323
    - type: nauc_recall_at_1_diff1
      value: 76.73627284218519
    - type: nauc_recall_at_1_max
      value: 62.94957512510876
    - type: nauc_recall_at_1_std
      value: -20.99649749330682
    - type: nauc_recall_at_20_diff1
      value: 63.07808719241162
    - type: nauc_recall_at_20_max
      value: 76.96808746317542
    - type: nauc_recall_at_20_std
      value: -1.5235053258631275
    - type: nauc_recall_at_3_diff1
      value: 70.52940972112405
    - type: nauc_recall_at_3_max
      value: 70.3402053170779
    - type: nauc_recall_at_3_std
      value: -21.57977842424124
    - type: nauc_recall_at_5_diff1
      value: 68.78962580223575
    - type: nauc_recall_at_5_max
      value: 71.41410894398392
    - type: nauc_recall_at_5_std
      value: -20.415603405161793
    - type: ndcg_at_1
      value: 58.423
    - type: ndcg_at_10
      value: 70.645
    - type: ndcg_at_100
      value: 73.277
    - type: ndcg_at_1000
      value: 73.785
    - type: ndcg_at_20
      value: 71.918
    - type: ndcg_at_3
      value: 66.679
    - type: ndcg_at_5
      value: 68.72200000000001
    - type: precision_at_1
      value: 58.423
    - type: precision_at_10
      value: 8.338
    - type: precision_at_100
      value: 0.959
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.423
    - type: precision_at_3
      value: 24.113
    - type: precision_at_5
      value: 15.47
    - type: recall_at_1
      value: 58.423
    - type: recall_at_10
      value: 83.38
    - type: recall_at_100
      value: 95.887
    - type: recall_at_1000
      value: 99.831
    - type: recall_at_20
      value: 88.39399999999999
    - type: recall_at_3
      value: 72.33800000000001
    - type: recall_at_5
      value: 77.352
    task:
      type: Retrieval
  - dataset:
      config: deu-eng
      name: MTEB MLQARetrieval (deu-eng)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 67.067
    - type: map_at_1
      value: 55.861000000000004
    - type: map_at_10
      value: 63.42100000000001
    - type: map_at_100
      value: 64.03
    - type: map_at_1000
      value: 64.05999999999999
    - type: map_at_20
      value: 63.819
    - type: map_at_3
      value: 61.773
    - type: map_at_5
      value: 62.736999999999995
    - type: mrr_at_1
      value: 55.88300465322402
    - type: mrr_at_10
      value: 63.43111082973707
    - type: mrr_at_100
      value: 64.03962373590272
    - type: mrr_at_1000
      value: 64.0698259866376
    - type: mrr_at_20
      value: 63.82871766489112
    - type: mrr_at_3
      value: 61.78447448112865
    - type: mrr_at_5
      value: 62.74835659945346
    - type: nauc_map_at_1000_diff1
      value: 74.58505763417352
    - type: nauc_map_at_1000_max
      value: 66.26060764852198
    - type: nauc_map_at_1000_std
      value: -16.896178230873897
    - type: nauc_map_at_100_diff1
      value: 74.57057487892857
    - type: nauc_map_at_100_max
      value: 66.26600433283826
    - type: nauc_map_at_100_std
      value: -16.87596113104189
    - type: nauc_map_at_10_diff1
      value: 74.53453636322749
    - type: nauc_map_at_10_max
      value: 66.27501737773804
    - type: nauc_map_at_10_std
      value: -17.178743257781775
    - type: nauc_map_at_1_diff1
      value: 77.63067209375254
    - type: nauc_map_at_1_max
      value: 64.17718675702672
    - type: nauc_map_at_1_std
      value: -17.639521106853717
    - type: nauc_map_at_20_diff1
      value: 74.52007402431164
    - type: nauc_map_at_20_max
      value: 66.28276291359268
    - type: nauc_map_at_20_std
      value: -16.939292897754758
    - type: nauc_map_at_3_diff1
      value: 74.79187974631951
    - type: nauc_map_at_3_max
      value: 66.23256568210611
    - type: nauc_map_at_3_std
      value: -17.894889918934112
    - type: nauc_map_at_5_diff1
      value: 74.63011328882517
    - type: nauc_map_at_5_max
      value: 66.35411054978499
    - type: nauc_map_at_5_std
      value: -17.50140342194211
    - type: nauc_mrr_at_1000_diff1
      value: 74.57520089771667
    - type: nauc_mrr_at_1000_max
      value: 66.27270912845914
    - type: nauc_mrr_at_1000_std
      value: -16.84012675362397
    - type: nauc_mrr_at_100_diff1
      value: 74.56070964572156
    - type: nauc_mrr_at_100_max
      value: 66.2780701126926
    - type: nauc_mrr_at_100_std
      value: -16.820035083069865
    - type: nauc_mrr_at_10_diff1
      value: 74.52455978435117
    - type: nauc_mrr_at_10_max
      value: 66.28697244023137
    - type: nauc_mrr_at_10_std
      value: -17.122477723330523
    - type: nauc_mrr_at_1_diff1
      value: 77.60643512422061
    - type: nauc_mrr_at_1_max
      value: 64.21736966061896
    - type: nauc_mrr_at_1_std
      value: -17.56627338275146
    - type: nauc_mrr_at_20_diff1
      value: 74.5099814266373
    - type: nauc_mrr_at_20_max
      value: 66.29485560556576
    - type: nauc_mrr_at_20_std
      value: -16.882350027335306
    - type: nauc_mrr_at_3_diff1
      value: 74.78132817375507
    - type: nauc_mrr_at_3_max
      value: 66.24761860047623
    - type: nauc_mrr_at_3_std
      value: -17.833128575678998
    - type: nauc_mrr_at_5_diff1
      value: 74.6193031207433
    - type: nauc_mrr_at_5_max
      value: 66.36951764432901
    - type: nauc_mrr_at_5_std
      value: -17.438203106324227
    - type: nauc_ndcg_at_1000_diff1
      value: 73.79386161629151
    - type: nauc_ndcg_at_1000_max
      value: 66.84013038018082
    - type: nauc_ndcg_at_1000_std
      value: -15.387358822700667
    - type: nauc_ndcg_at_100_diff1
      value: 73.36132885277745
    - type: nauc_ndcg_at_100_max
      value: 67.04416926901568
    - type: nauc_ndcg_at_100_std
      value: -14.503256942521972
    - type: nauc_ndcg_at_10_diff1
      value: 73.11847332785027
    - type: nauc_ndcg_at_10_max
      value: 67.02149621303091
    - type: nauc_ndcg_at_10_std
      value: -16.142234662067782
    - type: nauc_ndcg_at_1_diff1
      value: 77.60643512422061
    - type: nauc_ndcg_at_1_max
      value: 64.21736966061896
    - type: nauc_ndcg_at_1_std
      value: -17.56627338275146
    - type: nauc_ndcg_at_20_diff1
      value: 72.97961452569768
    - type: nauc_ndcg_at_20_max
      value: 67.12369127081152
    - type: nauc_ndcg_at_20_std
      value: -15.11921773223936
    - type: nauc_ndcg_at_3_diff1
      value: 73.77769312598772
    - type: nauc_ndcg_at_3_max
      value: 66.94438755852309
    - type: nauc_ndcg_at_3_std
      value: -17.75960443830741
    - type: nauc_ndcg_at_5_diff1
      value: 73.43991209562891
    - type: nauc_ndcg_at_5_max
      value: 67.21682951737418
    - type: nauc_ndcg_at_5_std
      value: -17.013510008231805
    - type: nauc_precision_at_1000_diff1
      value: 51.30633281948362
    - type: nauc_precision_at_1000_max
      value: 76.78675288883846
    - type: nauc_precision_at_1000_std
      value: 71.70041985304397
    - type: nauc_precision_at_100_diff1
      value: 59.86656455853326
    - type: nauc_precision_at_100_max
      value: 74.41958422732161
    - type: nauc_precision_at_100_std
      value: 22.098920296069124
    - type: nauc_precision_at_10_diff1
      value: 66.4696166928741
    - type: nauc_precision_at_10_max
      value: 69.88463108697104
    - type: nauc_precision_at_10_std
      value: -10.707950954702742
    - type: nauc_precision_at_1_diff1
      value: 77.60643512422061
    - type: nauc_precision_at_1_max
      value: 64.21736966061896
    - type: nauc_precision_at_1_std
      value: -17.56627338275146
    - type: nauc_precision_at_20_diff1
      value: 63.45094585276983
    - type: nauc_precision_at_20_max
      value: 71.57741245347195
    - type: nauc_precision_at_20_std
      value: -2.2211545419051744
    - type: nauc_precision_at_3_diff1
      value: 70.28060818081384
    - type: nauc_precision_at_3_max
      value: 69.22652927816439
    - type: nauc_precision_at_3_std
      value: -17.158576243559434
    - type: nauc_precision_at_5_diff1
      value: 68.90765418427162
    - type: nauc_precision_at_5_max
      value: 70.32585273389111
    - type: nauc_precision_at_5_std
      value: -14.950363729664524
    - type: nauc_recall_at_1000_diff1
      value: 65.11255117927331
    - type: nauc_recall_at_1000_max
      value: 88.35641213283338
    - type: nauc_recall_at_1000_std
      value: 69.89792573640547
    - type: nauc_recall_at_100_diff1
      value: 61.46376457272238
    - type: nauc_recall_at_100_max
      value: 75.48265142243015
    - type: nauc_recall_at_100_std
      value: 21.223182712042178
    - type: nauc_recall_at_10_diff1
      value: 66.89353375308997
    - type: nauc_recall_at_10_max
      value: 70.06655416883785
    - type: nauc_recall_at_10_std
      value: -11.100871879439435
    - type: nauc_recall_at_1_diff1
      value: 77.63067209375254
    - type: nauc_recall_at_1_max
      value: 64.17718675702672
    - type: nauc_recall_at_1_std
      value: -17.639521106853717
    - type: nauc_recall_at_20_diff1
      value: 63.98532276331878
    - type: nauc_recall_at_20_max
      value: 71.81562599791899
    - type: nauc_recall_at_20_std
      value: -2.696537977147695
    - type: nauc_recall_at_3_diff1
      value: 70.4507655865698
    - type: nauc_recall_at_3_max
      value: 69.25705030141037
    - type: nauc_recall_at_3_std
      value: -17.299948348202836
    - type: nauc_recall_at_5_diff1
      value: 69.09152857901888
    - type: nauc_recall_at_5_max
      value: 70.35609636026405
    - type: nauc_recall_at_5_std
      value: -15.105012139255896
    - type: ndcg_at_1
      value: 55.883
    - type: ndcg_at_10
      value: 67.067
    - type: ndcg_at_100
      value: 70.07
    - type: ndcg_at_1000
      value: 70.875
    - type: ndcg_at_20
      value: 68.498
    - type: ndcg_at_3
      value: 63.666
    - type: ndcg_at_5
      value: 65.40599999999999
    - type: precision_at_1
      value: 55.883
    - type: precision_at_10
      value: 7.8549999999999995
    - type: precision_at_100
      value: 0.928
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.2090000000000005
    - type: precision_at_3
      value: 23.052
    - type: precision_at_5
      value: 14.677999999999999
    - type: recall_at_1
      value: 55.861000000000004
    - type: recall_at_10
      value: 78.495
    - type: recall_at_100
      value: 92.688
    - type: recall_at_1000
      value: 99.02499999999999
    - type: recall_at_20
      value: 84.124
    - type: recall_at_3
      value: 69.123
    - type: recall_at_5
      value: 73.355
    task:
      type: Retrieval
  - dataset:
      config: spa-deu
      name: MTEB MLQARetrieval (spa-deu)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 73.90299999999999
    - type: map_at_1
      value: 61.236000000000004
    - type: map_at_10
      value: 69.88799999999999
    - type: map_at_100
      value: 70.319
    - type: map_at_1000
      value: 70.341
    - type: map_at_20
      value: 70.16799999999999
    - type: map_at_3
      value: 68.104
    - type: map_at_5
      value: 69.164
    - type: mrr_at_1
      value: 61.2739571589628
    - type: mrr_at_10
      value: 69.92589162684993
    - type: mrr_at_100
      value: 70.35245455509234
    - type: mrr_at_1000
      value: 70.37438351396742
    - type: mrr_at_20
      value: 70.20247469915404
    - type: mrr_at_3
      value: 68.14167606163099
    - type: mrr_at_5
      value: 69.20142803457354
    - type: nauc_map_at_1000_diff1
      value: 74.70416754842327
    - type: nauc_map_at_1000_max
      value: 65.86915994583384
    - type: nauc_map_at_1000_std
      value: -19.04437483534443
    - type: nauc_map_at_100_diff1
      value: 74.70011798058674
    - type: nauc_map_at_100_max
      value: 65.88507779167188
    - type: nauc_map_at_100_std
      value: -19.018670970643786
    - type: nauc_map_at_10_diff1
      value: 74.6362126804427
    - type: nauc_map_at_10_max
      value: 66.05733054427198
    - type: nauc_map_at_10_std
      value: -19.034317737897354
    - type: nauc_map_at_1_diff1
      value: 77.24970536833601
    - type: nauc_map_at_1_max
      value: 62.07820573048406
    - type: nauc_map_at_1_std
      value: -20.917086586335078
    - type: nauc_map_at_20_diff1
      value: 74.64113920401083
    - type: nauc_map_at_20_max
      value: 65.89991740166793
    - type: nauc_map_at_20_std
      value: -19.09987515041243
    - type: nauc_map_at_3_diff1
      value: 74.6518162332119
    - type: nauc_map_at_3_max
      value: 66.10312348194024
    - type: nauc_map_at_3_std
      value: -18.95881457716116
    - type: nauc_map_at_5_diff1
      value: 74.55141020670321
    - type: nauc_map_at_5_max
      value: 65.94345752979342
    - type: nauc_map_at_5_std
      value: -19.453976877992304
    - type: nauc_mrr_at_1000_diff1
      value: 74.64458488344088
    - type: nauc_mrr_at_1000_max
      value: 65.84575328456057
    - type: nauc_mrr_at_1000_std
      value: -18.901614615119904
    - type: nauc_mrr_at_100_diff1
      value: 74.64058497924627
    - type: nauc_mrr_at_100_max
      value: 65.86170461767928
    - type: nauc_mrr_at_100_std
      value: -18.87601697091505
    - type: nauc_mrr_at_10_diff1
      value: 74.57266634464752
    - type: nauc_mrr_at_10_max
      value: 66.03331587645152
    - type: nauc_mrr_at_10_std
      value: -18.87888060105393
    - type: nauc_mrr_at_1_diff1
      value: 77.19578272647183
    - type: nauc_mrr_at_1_max
      value: 62.05252035478773
    - type: nauc_mrr_at_1_std
      value: -20.790530940625267
    - type: nauc_mrr_at_20_diff1
      value: 74.5808171250021
    - type: nauc_mrr_at_20_max
      value: 65.87643606587798
    - type: nauc_mrr_at_20_std
      value: -18.95476583474199
    - type: nauc_mrr_at_3_diff1
      value: 74.5917053289191
    - type: nauc_mrr_at_3_max
      value: 66.08044079438714
    - type: nauc_mrr_at_3_std
      value: -18.81168463163586
    - type: nauc_mrr_at_5_diff1
      value: 74.48934579694608
    - type: nauc_mrr_at_5_max
      value: 65.91993162383771
    - type: nauc_mrr_at_5_std
      value: -19.302710791338797
    - type: nauc_ndcg_at_1000_diff1
      value: 74.20191283992186
    - type: nauc_ndcg_at_1000_max
      value: 66.60831175771229
    - type: nauc_ndcg_at_1000_std
      value: -18.175208725175484
    - type: nauc_ndcg_at_100_diff1
      value: 74.07713451642955
    - type: nauc_ndcg_at_100_max
      value: 67.02028626335476
    - type: nauc_ndcg_at_100_std
      value: -17.36560972181693
    - type: nauc_ndcg_at_10_diff1
      value: 73.63235521598476
    - type: nauc_ndcg_at_10_max
      value: 67.8118473312638
    - type: nauc_ndcg_at_10_std
      value: -17.647560577355915
    - type: nauc_ndcg_at_1_diff1
      value: 77.19578272647183
    - type: nauc_ndcg_at_1_max
      value: 62.05252035478773
    - type: nauc_ndcg_at_1_std
      value: -20.790530940625267
    - type: nauc_ndcg_at_20_diff1
      value: 73.65300308228291
    - type: nauc_ndcg_at_20_max
      value: 67.18353402731985
    - type: nauc_ndcg_at_20_std
      value: -17.9240756389792
    - type: nauc_ndcg_at_3_diff1
      value: 73.73764900202292
    - type: nauc_ndcg_at_3_max
      value: 67.60840957876889
    - type: nauc_ndcg_at_3_std
      value: -17.962667543518933
    - type: nauc_ndcg_at_5_diff1
      value: 73.49040500302092
    - type: nauc_ndcg_at_5_max
      value: 67.41251918514402
    - type: nauc_ndcg_at_5_std
      value: -18.851877225955523
    - type: nauc_precision_at_1000_diff1
      value: -18.652906102973922
    - type: nauc_precision_at_1000_max
      value: 2.1701672475574885
    - type: nauc_precision_at_1000_std
      value: 61.713411950188835
    - type: nauc_precision_at_100_diff1
      value: 62.37565302288498
    - type: nauc_precision_at_100_max
      value: 76.96921843049006
    - type: nauc_precision_at_100_std
      value: 19.152009040219678
    - type: nauc_precision_at_10_diff1
      value: 68.14047344105212
    - type: nauc_precision_at_10_max
      value: 77.7177273849099
    - type: nauc_precision_at_10_std
      value: -9.124325941493698
    - type: nauc_precision_at_1_diff1
      value: 77.19578272647183
    - type: nauc_precision_at_1_max
      value: 62.05252035478773
    - type: nauc_precision_at_1_std
      value: -20.790530940625267
    - type: nauc_precision_at_20_diff1
      value: 65.38487456362745
    - type: nauc_precision_at_20_max
      value: 74.61122933443669
    - type: nauc_precision_at_20_std
      value: -8.129775929648341
    - type: nauc_precision_at_3_diff1
      value: 70.45937744142297
    - type: nauc_precision_at_3_max
      value: 73.03004233073901
    - type: nauc_precision_at_3_std
      value: -14.246554579025158
    - type: nauc_precision_at_5_diff1
      value: 69.02821772428955
    - type: nauc_precision_at_5_max
      value: 73.52949774726446
    - type: nauc_precision_at_5_std
      value: -16.355747231517757
    - type: nauc_recall_at_1000_diff1
      value: 35.804192824985755
    - type: nauc_recall_at_1000_max
      value: 61.367785756485894
    - type: nauc_recall_at_1000_std
      value: 54.01380822466869
    - type: nauc_recall_at_100_diff1
      value: 67.96210883597479
    - type: nauc_recall_at_100_max
      value: 82.38124823732169
    - type: nauc_recall_at_100_std
      value: 16.814922595309966
    - type: nauc_recall_at_10_diff1
      value: 68.21964459634341
    - type: nauc_recall_at_10_max
      value: 77.68301934858845
    - type: nauc_recall_at_10_std
      value: -9.430792913885066
    - type: nauc_recall_at_1_diff1
      value: 77.24970536833601
    - type: nauc_recall_at_1_max
      value: 62.07820573048406
    - type: nauc_recall_at_1_std
      value: -20.917086586335078
    - type: nauc_recall_at_20_diff1
      value: 66.60569906579487
    - type: nauc_recall_at_20_max
      value: 75.66163186604354
    - type: nauc_recall_at_20_std
      value: -9.09826205489828
    - type: nauc_recall_at_3_diff1
      value: 70.52323701841641
    - type: nauc_recall_at_3_max
      value: 73.03478107411232
    - type: nauc_recall_at_3_std
      value: -14.432325989967962
    - type: nauc_recall_at_5_diff1
      value: 69.08521261524373
    - type: nauc_recall_at_5_max
      value: 73.51150270382094
    - type: nauc_recall_at_5_std
      value: -16.569387503524368
    - type: ndcg_at_1
      value: 61.273999999999994
    - type: ndcg_at_10
      value: 73.90299999999999
    - type: ndcg_at_100
      value: 75.983
    - type: ndcg_at_1000
      value: 76.488
    - type: ndcg_at_20
      value: 74.921
    - type: ndcg_at_3
      value: 70.277
    - type: ndcg_at_5
      value: 72.172
    - type: precision_at_1
      value: 61.273999999999994
    - type: precision_at_10
      value: 8.641
    - type: precision_at_100
      value: 0.962
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 4.524
    - type: precision_at_3
      value: 25.517
    - type: precision_at_5
      value: 16.223000000000003
    - type: recall_at_1
      value: 61.236000000000004
    - type: recall_at_10
      value: 86.37700000000001
    - type: recall_at_100
      value: 96.054
    - type: recall_at_1000
      value: 99.887
    - type: recall_at_20
      value: 90.398
    - type: recall_at_3
      value: 76.51299999999999
    - type: recall_at_5
      value: 81.07900000000001
    task:
      type: Retrieval
  - dataset:
      config: spa-spa
      name: MTEB MLQARetrieval (spa-spa)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 68.632
    - type: map_at_1
      value: 57.046
    - type: map_at_10
      value: 64.869
    - type: map_at_100
      value: 65.384
    - type: map_at_1000
      value: 65.413
    - type: map_at_20
      value: 65.185
    - type: map_at_3
      value: 63.178
    - type: map_at_5
      value: 64.12
    - type: mrr_at_1
      value: 57.05579889544848
    - type: mrr_at_10
      value: 64.8806425382317
    - type: mrr_at_100
      value: 65.39469233244084
    - type: mrr_at_1000
      value: 65.42342199403159
    - type: mrr_at_20
      value: 65.19634815919534
    - type: mrr_at_3
      value: 63.18796419729591
    - type: mrr_at_5
      value: 64.13159398209874
    - type: nauc_map_at_1000_diff1
      value: 73.23803038674018
    - type: nauc_map_at_1000_max
      value: 67.44156201421714
    - type: nauc_map_at_1000_std
      value: -8.60143026450049
    - type: nauc_map_at_100_diff1
      value: 73.22575613034235
    - type: nauc_map_at_100_max
      value: 67.44735143420195
    - type: nauc_map_at_100_std
      value: -8.576905069492895
    - type: nauc_map_at_10_diff1
      value: 73.11950129610865
    - type: nauc_map_at_10_max
      value: 67.45107232305055
    - type: nauc_map_at_10_std
      value: -8.799837857015392
    - type: nauc_map_at_1_diff1
      value: 76.18354072047988
    - type: nauc_map_at_1_max
      value: 65.03342186728786
    - type: nauc_map_at_1_std
      value: -10.867650288695796
    - type: nauc_map_at_20_diff1
      value: 73.21570748770948
    - type: nauc_map_at_20_max
      value: 67.50340321088724
    - type: nauc_map_at_20_std
      value: -8.594057184944676
    - type: nauc_map_at_3_diff1
      value: 73.17239276163892
    - type: nauc_map_at_3_max
      value: 67.06319504819103
    - type: nauc_map_at_3_std
      value: -9.883216310270528
    - type: nauc_map_at_5_diff1
      value: 73.11913507367727
    - type: nauc_map_at_5_max
      value: 67.27497019567078
    - type: nauc_map_at_5_std
      value: -9.497714822103118
    - type: nauc_mrr_at_1000_diff1
      value: 73.22971233311306
    - type: nauc_mrr_at_1000_max
      value: 67.42977229057223
    - type: nauc_mrr_at_1000_std
      value: -8.550068702273297
    - type: nauc_mrr_at_100_diff1
      value: 73.21744467317815
    - type: nauc_mrr_at_100_max
      value: 67.43557491068093
    - type: nauc_mrr_at_100_std
      value: -8.52559275190607
    - type: nauc_mrr_at_10_diff1
      value: 73.11075619726137
    - type: nauc_mrr_at_10_max
      value: 67.43889760205286
    - type: nauc_mrr_at_10_std
      value: -8.74617232559183
    - type: nauc_mrr_at_1_diff1
      value: 76.17529975949547
    - type: nauc_mrr_at_1_max
      value: 65.02401127001608
    - type: nauc_mrr_at_1_std
      value: -10.817814457633952
    - type: nauc_mrr_at_20_diff1
      value: 73.20689275225138
    - type: nauc_mrr_at_20_max
      value: 67.49111752272192
    - type: nauc_mrr_at_20_std
      value: -8.539827528410353
    - type: nauc_mrr_at_3_diff1
      value: 73.16291729623958
    - type: nauc_mrr_at_3_max
      value: 67.05300993427998
    - type: nauc_mrr_at_3_std
      value: -9.827915885680811
    - type: nauc_mrr_at_5_diff1
      value: 73.11055686484109
    - type: nauc_mrr_at_5_max
      value: 67.26299851089122
    - type: nauc_mrr_at_5_std
      value: -9.445190276650903
    - type: nauc_ndcg_at_1000_diff1
      value: 72.58833638407177
    - type: nauc_ndcg_at_1000_max
      value: 68.10447506371374
    - type: nauc_ndcg_at_1000_std
      value: -6.910306241546282
    - type: nauc_ndcg_at_100_diff1
      value: 72.24524849631476
    - type: nauc_ndcg_at_100_max
      value: 68.30659210081238
    - type: nauc_ndcg_at_100_std
      value: -6.04305364268931
    - type: nauc_ndcg_at_10_diff1
      value: 71.87363502582961
    - type: nauc_ndcg_at_10_max
      value: 68.5010009653693
    - type: nauc_ndcg_at_10_std
      value: -7.021281296450588
    - type: nauc_ndcg_at_1_diff1
      value: 76.17529975949547
    - type: nauc_ndcg_at_1_max
      value: 65.02401127001608
    - type: nauc_ndcg_at_1_std
      value: -10.817814457633952
    - type: nauc_ndcg_at_20_diff1
      value: 72.21241010439327
    - type: nauc_ndcg_at_20_max
      value: 68.71743274030551
    - type: nauc_ndcg_at_20_std
      value: -6.186629577195946
    - type: nauc_ndcg_at_3_diff1
      value: 72.08204674794459
    - type: nauc_ndcg_at_3_max
      value: 67.5958365046156
    - type: nauc_ndcg_at_3_std
      value: -9.576418336610345
    - type: nauc_ndcg_at_5_diff1
      value: 71.93179095844508
    - type: nauc_ndcg_at_5_max
      value: 68.01914639754217
    - type: nauc_ndcg_at_5_std
      value: -8.833768332910777
    - type: nauc_precision_at_1000_diff1
      value: 63.0051360227489
    - type: nauc_precision_at_1000_max
      value: 79.93532442313229
    - type: nauc_precision_at_1000_std
      value: 52.869517607133254
    - type: nauc_precision_at_100_diff1
      value: 62.43301501857154
    - type: nauc_precision_at_100_max
      value: 75.57280416668183
    - type: nauc_precision_at_100_std
      value: 26.758300486132747
    - type: nauc_precision_at_10_diff1
      value: 66.29806375971134
    - type: nauc_precision_at_10_max
      value: 73.40301413754797
    - type: nauc_precision_at_10_std
      value: 1.9858547295235462
    - type: nauc_precision_at_1_diff1
      value: 76.17529975949547
    - type: nauc_precision_at_1_max
      value: 65.02401127001608
    - type: nauc_precision_at_1_std
      value: -10.817814457633952
    - type: nauc_precision_at_20_diff1
      value: 67.05111836051105
    - type: nauc_precision_at_20_max
      value: 76.09783190824155
    - type: nauc_precision_at_20_std
      value: 9.906010659515564
    - type: nauc_precision_at_3_diff1
      value: 68.44186679250453
    - type: nauc_precision_at_3_max
      value: 69.30301351119388
    - type: nauc_precision_at_3_std
      value: -8.566522518882348
    - type: nauc_precision_at_5_diff1
      value: 67.51737199297388
    - type: nauc_precision_at_5_max
      value: 70.75887601590472
    - type: nauc_precision_at_5_std
      value: -6.278983102710238
    - type: nauc_recall_at_1000_diff1
      value: 65.12360093170948
    - type: nauc_recall_at_1000_max
      value: 82.60209843191132
    - type: nauc_recall_at_1000_std
      value: 51.740179583368636
    - type: nauc_recall_at_100_diff1
      value: 62.82007697326819
    - type: nauc_recall_at_100_max
      value: 76.04844844677562
    - type: nauc_recall_at_100_std
      value: 26.4678415019248
    - type: nauc_recall_at_10_diff1
      value: 66.28557566848767
    - type: nauc_recall_at_10_max
      value: 73.40302709828738
    - type: nauc_recall_at_10_std
      value: 1.9224272854613582
    - type: nauc_recall_at_1_diff1
      value: 76.18354072047988
    - type: nauc_recall_at_1_max
      value: 65.03342186728786
    - type: nauc_recall_at_1_std
      value: -10.867650288695796
    - type: nauc_recall_at_20_diff1
      value: 67.03430451094992
    - type: nauc_recall_at_20_max
      value: 76.09474005171319
    - type: nauc_recall_at_20_std
      value: 9.815888637851074
    - type: nauc_recall_at_3_diff1
      value: 68.44411411344718
    - type: nauc_recall_at_3_max
      value: 69.30502737137265
    - type: nauc_recall_at_3_std
      value: -8.629526329714132
    - type: nauc_recall_at_5_diff1
      value: 67.51469265953514
    - type: nauc_recall_at_5_max
      value: 70.76969893818111
    - type: nauc_recall_at_5_std
      value: -6.325600167105444
    - type: ndcg_at_1
      value: 57.056
    - type: ndcg_at_10
      value: 68.632
    - type: ndcg_at_100
      value: 71.202
    - type: ndcg_at_1000
      value: 71.97099999999999
    - type: ndcg_at_20
      value: 69.785
    - type: ndcg_at_3
      value: 65.131
    - type: ndcg_at_5
      value: 66.834
    - type: precision_at_1
      value: 57.056
    - type: precision_at_10
      value: 8.044
    - type: precision_at_100
      value: 0.9259999999999999
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.251
    - type: precision_at_3
      value: 23.589
    - type: precision_at_5
      value: 14.984
    - type: recall_at_1
      value: 57.046
    - type: recall_at_10
      value: 80.423
    - type: recall_at_100
      value: 92.582
    - type: recall_at_1000
      value: 98.638
    - type: recall_at_20
      value: 84.993
    - type: recall_at_3
      value: 70.758
    - type: recall_at_5
      value: 74.9
    task:
      type: Retrieval
  - dataset:
      config: spa-eng
      name: MTEB MLQARetrieval (spa-eng)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 68.765
    - type: map_at_1
      value: 56.538999999999994
    - type: map_at_10
      value: 64.816
    - type: map_at_100
      value: 65.325
    - type: map_at_1000
      value: 65.352
    - type: map_at_20
      value: 65.113
    - type: map_at_3
      value: 62.934999999999995
    - type: map_at_5
      value: 64.063
    - type: mrr_at_1
      value: 56.539120502569965
    - type: mrr_at_10
      value: 64.81561556661505
    - type: mrr_at_100
      value: 65.32464238613954
    - type: mrr_at_1000
      value: 65.35206516602133
    - type: mrr_at_20
      value: 65.11270445292227
    - type: mrr_at_3
      value: 62.935465448315384
    - type: mrr_at_5
      value: 64.06339234723022
    - type: nauc_map_at_1000_diff1
      value: 73.20701050428072
    - type: nauc_map_at_1000_max
      value: 67.32797480614404
    - type: nauc_map_at_1000_std
      value: -6.211540626528362
    - type: nauc_map_at_100_diff1
      value: 73.19497683923063
    - type: nauc_map_at_100_max
      value: 67.33392646467817
    - type: nauc_map_at_100_std
      value: -6.196671563900051
    - type: nauc_map_at_10_diff1
      value: 73.16010547612956
    - type: nauc_map_at_10_max
      value: 67.37793741307372
    - type: nauc_map_at_10_std
      value: -6.3443240322521675
    - type: nauc_map_at_1_diff1
      value: 76.63696578575964
    - type: nauc_map_at_1_max
      value: 65.08189618178105
    - type: nauc_map_at_1_std
      value: -8.594195451782733
    - type: nauc_map_at_20_diff1
      value: 73.15233479381568
    - type: nauc_map_at_20_max
      value: 67.3679607256072
    - type: nauc_map_at_20_std
      value: -6.175928265286352
    - type: nauc_map_at_3_diff1
      value: 73.14853380980746
    - type: nauc_map_at_3_max
      value: 67.10354198073468
    - type: nauc_map_at_3_std
      value: -7.409679815529866
    - type: nauc_map_at_5_diff1
      value: 73.13425961877715
    - type: nauc_map_at_5_max
      value: 67.22452899371224
    - type: nauc_map_at_5_std
      value: -6.895257774506354
    - type: nauc_mrr_at_1000_diff1
      value: 73.20701050428072
    - type: nauc_mrr_at_1000_max
      value: 67.32797480614404
    - type: nauc_mrr_at_1000_std
      value: -6.211540626528362
    - type: nauc_mrr_at_100_diff1
      value: 73.19497683923063
    - type: nauc_mrr_at_100_max
      value: 67.33392646467817
    - type: nauc_mrr_at_100_std
      value: -6.196671563900051
    - type: nauc_mrr_at_10_diff1
      value: 73.16010547612956
    - type: nauc_mrr_at_10_max
      value: 67.37793741307372
    - type: nauc_mrr_at_10_std
      value: -6.3443240322521675
    - type: nauc_mrr_at_1_diff1
      value: 76.63696578575964
    - type: nauc_mrr_at_1_max
      value: 65.08189618178105
    - type: nauc_mrr_at_1_std
      value: -8.594195451782733
    - type: nauc_mrr_at_20_diff1
      value: 73.15233479381568
    - type: nauc_mrr_at_20_max
      value: 67.3679607256072
    - type: nauc_mrr_at_20_std
      value: -6.175928265286352
    - type: nauc_mrr_at_3_diff1
      value: 73.14853380980746
    - type: nauc_mrr_at_3_max
      value: 67.10354198073468
    - type: nauc_mrr_at_3_std
      value: -7.409679815529866
    - type: nauc_mrr_at_5_diff1
      value: 73.13425961877715
    - type: nauc_mrr_at_5_max
      value: 67.22452899371224
    - type: nauc_mrr_at_5_std
      value: -6.895257774506354
    - type: nauc_ndcg_at_1000_diff1
      value: 72.44364625096874
    - type: nauc_ndcg_at_1000_max
      value: 67.93635761141552
    - type: nauc_ndcg_at_1000_std
      value: -4.616429464350954
    - type: nauc_ndcg_at_100_diff1
      value: 72.11352383758482
    - type: nauc_ndcg_at_100_max
      value: 68.1627312575955
    - type: nauc_ndcg_at_100_std
      value: -3.894213672131282
    - type: nauc_ndcg_at_10_diff1
      value: 71.8526850770812
    - type: nauc_ndcg_at_10_max
      value: 68.41366561888562
    - type: nauc_ndcg_at_10_std
      value: -4.472146861145989
    - type: nauc_ndcg_at_1_diff1
      value: 76.63696578575964
    - type: nauc_ndcg_at_1_max
      value: 65.08189618178105
    - type: nauc_ndcg_at_1_std
      value: -8.594195451782733
    - type: nauc_ndcg_at_20_diff1
      value: 71.76464418138866
    - type: nauc_ndcg_at_20_max
      value: 68.41174963313698
    - type: nauc_ndcg_at_20_std
      value: -3.7449762037540157
    - type: nauc_ndcg_at_3_diff1
      value: 71.93808990683131
    - type: nauc_ndcg_at_3_max
      value: 67.7010029507334
    - type: nauc_ndcg_at_3_std
      value: -6.971858419379321
    - type: nauc_ndcg_at_5_diff1
      value: 71.8505224811326
    - type: nauc_ndcg_at_5_max
      value: 67.97139549500251
    - type: nauc_ndcg_at_5_std
      value: -5.958491308070017
    - type: nauc_precision_at_1000_diff1
      value: 62.20956180320043
    - type: nauc_precision_at_1000_max
      value: 82.53412670611299
    - type: nauc_precision_at_1000_std
      value: 55.57278124999575
    - type: nauc_precision_at_100_diff1
      value: 62.03792857023201
    - type: nauc_precision_at_100_max
      value: 76.77130713424538
    - type: nauc_precision_at_100_std
      value: 26.674102719959564
    - type: nauc_precision_at_10_diff1
      value: 65.89798055049931
    - type: nauc_precision_at_10_max
      value: 73.41908620140674
    - type: nauc_precision_at_10_std
      value: 5.21818573283179
    - type: nauc_precision_at_1_diff1
      value: 76.63696578575964
    - type: nauc_precision_at_1_max
      value: 65.08189618178105
    - type: nauc_precision_at_1_std
      value: -8.594195451782733
    - type: nauc_precision_at_20_diff1
      value: 63.734308542647355
    - type: nauc_precision_at_20_max
      value: 74.69578825096144
    - type: nauc_precision_at_20_std
      value: 12.627842502659162
    - type: nauc_precision_at_3_diff1
      value: 67.91189666671904
    - type: nauc_precision_at_3_max
      value: 69.64986036783209
    - type: nauc_precision_at_3_std
      value: -5.505669087429055
    - type: nauc_precision_at_5_diff1
      value: 67.01880006360248
    - type: nauc_precision_at_5_max
      value: 70.78916423358686
    - type: nauc_precision_at_5_std
      value: -2.2273742736401045
    - type: nauc_recall_at_1000_diff1
      value: 62.20956180319936
    - type: nauc_recall_at_1000_max
      value: 82.53412670611287
    - type: nauc_recall_at_1000_std
      value: 55.57278124999549
    - type: nauc_recall_at_100_diff1
      value: 62.03792857023208
    - type: nauc_recall_at_100_max
      value: 76.77130713424577
    - type: nauc_recall_at_100_std
      value: 26.67410271995973
    - type: nauc_recall_at_10_diff1
      value: 65.8979805504994
    - type: nauc_recall_at_10_max
      value: 73.41908620140678
    - type: nauc_recall_at_10_std
      value: 5.2181857328318655
    - type: nauc_recall_at_1_diff1
      value: 76.63696578575964
    - type: nauc_recall_at_1_max
      value: 65.08189618178105
    - type: nauc_recall_at_1_std
      value: -8.594195451782733
    - type: nauc_recall_at_20_diff1
      value: 63.734308542647334
    - type: nauc_recall_at_20_max
      value: 74.69578825096123
    - type: nauc_recall_at_20_std
      value: 12.627842502658982
    - type: nauc_recall_at_3_diff1
      value: 67.91189666671897
    - type: nauc_recall_at_3_max
      value: 69.64986036783203
    - type: nauc_recall_at_3_std
      value: -5.505669087428989
    - type: nauc_recall_at_5_diff1
      value: 67.01880006360243
    - type: nauc_recall_at_5_max
      value: 70.78916423358686
    - type: nauc_recall_at_5_std
      value: -2.227374273640135
    - type: ndcg_at_1
      value: 56.538999999999994
    - type: ndcg_at_10
      value: 68.765
    - type: ndcg_at_100
      value: 71.314
    - type: ndcg_at_1000
      value: 72.038
    - type: ndcg_at_20
      value: 69.828
    - type: ndcg_at_3
      value: 64.937
    - type: ndcg_at_5
      value: 66.956
    - type: precision_at_1
      value: 56.538999999999994
    - type: precision_at_10
      value: 8.113
    - type: precision_at_100
      value: 0.932
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.265
    - type: precision_at_3
      value: 23.567
    - type: precision_at_5
      value: 15.115
    - type: recall_at_1
      value: 56.538999999999994
    - type: recall_at_10
      value: 81.135
    - type: recall_at_100
      value: 93.223
    - type: recall_at_1000
      value: 98.896
    - type: recall_at_20
      value: 85.304
    - type: recall_at_3
      value: 70.702
    - type: recall_at_5
      value: 75.576
    task:
      type: Retrieval
  - dataset:
      config: eng-deu
      name: MTEB MLQARetrieval (eng-deu)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 69.298
    - type: map_at_1
      value: 58.553
    - type: map_at_10
      value: 65.769
    - type: map_at_100
      value: 66.298
    - type: map_at_1000
      value: 66.328
    - type: map_at_20
      value: 66.101
    - type: map_at_3
      value: 64.048
    - type: map_at_5
      value: 65.09
    - type: mrr_at_1
      value: 58.564148016840235
    - type: mrr_at_10
      value: 65.7685997066675
    - type: mrr_at_100
      value: 66.29874034432214
    - type: mrr_at_1000
      value: 66.32844979939088
    - type: mrr_at_20
      value: 66.10120513957821
    - type: mrr_at_3
      value: 64.04830489696437
    - type: mrr_at_5
      value: 65.08974074894746
    - type: nauc_map_at_1000_diff1
      value: 76.8409650183994
    - type: nauc_map_at_1000_max
      value: 71.86367015521367
    - type: nauc_map_at_1000_std
      value: -14.464881539957256
    - type: nauc_map_at_100_diff1
      value: 76.82536521842064
    - type: nauc_map_at_100_max
      value: 71.86811127965429
    - type: nauc_map_at_100_std
      value: -14.441105539722244
    - type: nauc_map_at_10_diff1
      value: 76.75522453447859
    - type: nauc_map_at_10_max
      value: 71.87677500176706
    - type: nauc_map_at_10_std
      value: -14.741331625103559
    - type: nauc_map_at_1_diff1
      value: 79.64060747740989
    - type: nauc_map_at_1_max
      value: 69.84278563569617
    - type: nauc_map_at_1_std
      value: -15.936904929655832
    - type: nauc_map_at_20_diff1
      value: 76.78894776059715
    - type: nauc_map_at_20_max
      value: 71.89637938044827
    - type: nauc_map_at_20_std
      value: -14.500564106990769
    - type: nauc_map_at_3_diff1
      value: 77.20562577450342
    - type: nauc_map_at_3_max
      value: 71.80578229361525
    - type: nauc_map_at_3_std
      value: -15.344134588512201
    - type: nauc_map_at_5_diff1
      value: 77.00480147367867
    - type: nauc_map_at_5_max
      value: 71.98335924076163
    - type: nauc_map_at_5_std
      value: -15.16537653041026
    - type: nauc_mrr_at_1000_diff1
      value: 76.84165367691193
    - type: nauc_mrr_at_1000_max
      value: 71.8642679499795
    - type: nauc_mrr_at_1000_std
      value: -14.461717954593158
    - type: nauc_mrr_at_100_diff1
      value: 76.8263363557998
    - type: nauc_mrr_at_100_max
      value: 71.86874522368626
    - type: nauc_mrr_at_100_std
      value: -14.437105168707426
    - type: nauc_mrr_at_10_diff1
      value: 76.75522453447859
    - type: nauc_mrr_at_10_max
      value: 71.87677500176706
    - type: nauc_mrr_at_10_std
      value: -14.741331625103559
    - type: nauc_mrr_at_1_diff1
      value: 79.65642669321981
    - type: nauc_mrr_at_1_max
      value: 69.89135358784799
    - type: nauc_mrr_at_1_std
      value: -15.919357002229589
    - type: nauc_mrr_at_20_diff1
      value: 76.78883171270601
    - type: nauc_mrr_at_20_max
      value: 71.89806887245291
    - type: nauc_mrr_at_20_std
      value: -14.497139746907905
    - type: nauc_mrr_at_3_diff1
      value: 77.20562577450342
    - type: nauc_mrr_at_3_max
      value: 71.80578229361525
    - type: nauc_mrr_at_3_std
      value: -15.344134588512201
    - type: nauc_mrr_at_5_diff1
      value: 77.00480147367867
    - type: nauc_mrr_at_5_max
      value: 71.98335924076163
    - type: nauc_mrr_at_5_std
      value: -15.16537653041026
    - type: nauc_ndcg_at_1000_diff1
      value: 76.07802417817047
    - type: nauc_ndcg_at_1000_max
      value: 72.31792804426776
    - type: nauc_ndcg_at_1000_std
      value: -13.049160715132244
    - type: nauc_ndcg_at_100_diff1
      value: 75.63343849116544
    - type: nauc_ndcg_at_100_max
      value: 72.48362076101817
    - type: nauc_ndcg_at_100_std
      value: -12.089600993516777
    - type: nauc_ndcg_at_10_diff1
      value: 75.23387929929208
    - type: nauc_ndcg_at_10_max
      value: 72.51436288271807
    - type: nauc_ndcg_at_10_std
      value: -13.624132103038104
    - type: nauc_ndcg_at_1_diff1
      value: 79.65642669321981
    - type: nauc_ndcg_at_1_max
      value: 69.89135358784799
    - type: nauc_ndcg_at_1_std
      value: -15.919357002229589
    - type: nauc_ndcg_at_20_diff1
      value: 75.32926047656296
    - type: nauc_ndcg_at_20_max
      value: 72.61254165918145
    - type: nauc_ndcg_at_20_std
      value: -12.683157599238701
    - type: nauc_ndcg_at_3_diff1
      value: 76.3089337665469
    - type: nauc_ndcg_at_3_max
      value: 72.40014674426054
    - type: nauc_ndcg_at_3_std
      value: -15.08624226353458
    - type: nauc_ndcg_at_5_diff1
      value: 75.88857331641834
    - type: nauc_ndcg_at_5_max
      value: 72.7719386827224
    - type: nauc_ndcg_at_5_std
      value: -14.70546521089236
    - type: nauc_precision_at_1000_diff1
      value: 59.66563879069911
    - type: nauc_precision_at_1000_max
      value: 74.57123562956772
    - type: nauc_precision_at_1000_std
      value: 58.61396866718965
    - type: nauc_precision_at_100_diff1
      value: 62.8695896550042
    - type: nauc_precision_at_100_max
      value: 77.81408796785
    - type: nauc_precision_at_100_std
      value: 23.819735672317826
    - type: nauc_precision_at_10_diff1
      value: 68.08051625224569
    - type: nauc_precision_at_10_max
      value: 75.14432336036869
    - type: nauc_precision_at_10_std
      value: -7.97602345252735
    - type: nauc_precision_at_1_diff1
      value: 79.65642669321981
    - type: nauc_precision_at_1_max
      value: 69.89135358784799
    - type: nauc_precision_at_1_std
      value: -15.919357002229589
    - type: nauc_precision_at_20_diff1
      value: 66.7168005185165
    - type: nauc_precision_at_20_max
      value: 76.58522761697147
    - type: nauc_precision_at_20_std
      value: -0.17923428317323292
    - type: nauc_precision_at_3_diff1
      value: 73.23394851561207
    - type: nauc_precision_at_3_max
      value: 74.32517846819215
    - type: nauc_precision_at_3_std
      value: -14.142301336188348
    - type: nauc_precision_at_5_diff1
      value: 71.5666882547012
    - type: nauc_precision_at_5_max
      value: 75.71098205440033
    - type: nauc_precision_at_5_std
      value: -12.808362513638052
    - type: nauc_recall_at_1000_diff1
      value: 71.73736112325805
    - type: nauc_recall_at_1000_max
      value: 86.70743436225898
    - type: nauc_recall_at_1000_std
      value: 54.45802578371167
    - type: nauc_recall_at_100_diff1
      value: 64.07053861428128
    - type: nauc_recall_at_100_max
      value: 78.8348308099261
    - type: nauc_recall_at_100_std
      value: 22.72263677785103
    - type: nauc_recall_at_10_diff1
      value: 68.20272901407903
    - type: nauc_recall_at_10_max
      value: 75.16315335381938
    - type: nauc_recall_at_10_std
      value: -8.060716748913386
    - type: nauc_recall_at_1_diff1
      value: 79.64060747740989
    - type: nauc_recall_at_1_max
      value: 69.84278563569617
    - type: nauc_recall_at_1_std
      value: -15.936904929655832
    - type: nauc_recall_at_20_diff1
      value: 66.88206981973654
    - type: nauc_recall_at_20_max
      value: 76.54824917595687
    - type: nauc_recall_at_20_std
      value: -0.40294589316962287
    - type: nauc_recall_at_3_diff1
      value: 73.33076087258938
    - type: nauc_recall_at_3_max
      value: 74.33763112508771
    - type: nauc_recall_at_3_std
      value: -14.213355414905399
    - type: nauc_recall_at_5_diff1
      value: 71.67487623469464
    - type: nauc_recall_at_5_max
      value: 75.72770292516316
    - type: nauc_recall_at_5_std
      value: -12.887572274644818
    - type: ndcg_at_1
      value: 58.56400000000001
    - type: ndcg_at_10
      value: 69.298
    - type: ndcg_at_100
      value: 71.95899999999999
    - type: ndcg_at_1000
      value: 72.735
    - type: ndcg_at_20
      value: 70.50699999999999
    - type: ndcg_at_3
      value: 65.81700000000001
    - type: ndcg_at_5
      value: 67.681
    - type: precision_at_1
      value: 58.56400000000001
    - type: precision_at_10
      value: 8.039
    - type: precision_at_100
      value: 0.931
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.259
    - type: precision_at_3
      value: 23.65
    - type: precision_at_5
      value: 15.09
    - type: recall_at_1
      value: 58.553
    - type: recall_at_10
      value: 80.368
    - type: recall_at_100
      value: 93.013
    - type: recall_at_1000
      value: 99.092
    - type: recall_at_20
      value: 85.143
    - type: recall_at_3
      value: 70.928
    - type: recall_at_5
      value: 75.42699999999999
    task:
      type: Retrieval
  - dataset:
      config: eng-spa
      name: MTEB MLQARetrieval (eng-spa)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 66.374
    - type: map_at_1
      value: 55.494
    - type: map_at_10
      value: 62.763999999999996
    - type: map_at_100
      value: 63.33
    - type: map_at_1000
      value: 63.36000000000001
    - type: map_at_20
      value: 63.104000000000006
    - type: map_at_3
      value: 61.065000000000005
    - type: map_at_5
      value: 62.053000000000004
    - type: mrr_at_1
      value: 55.49419158255571
    - type: mrr_at_10
      value: 62.765195140457095
    - type: mrr_at_100
      value: 63.33083349354529
    - type: mrr_at_1000
      value: 63.3611897014839
    - type: mrr_at_20
      value: 63.10543590095977
    - type: mrr_at_3
      value: 61.06455913159412
    - type: mrr_at_5
      value: 62.052942296705474
    - type: nauc_map_at_1000_diff1
      value: 75.04200018088618
    - type: nauc_map_at_1000_max
      value: 70.49937782771909
    - type: nauc_map_at_1000_std
      value: -5.257206317083184
    - type: nauc_map_at_100_diff1
      value: 75.02786834256312
    - type: nauc_map_at_100_max
      value: 70.5016476500189
    - type: nauc_map_at_100_std
      value: -5.228770832077681
    - type: nauc_map_at_10_diff1
      value: 74.9626552701647
    - type: nauc_map_at_10_max
      value: 70.56253732243214
    - type: nauc_map_at_10_std
      value: -5.359037281768563
    - type: nauc_map_at_1_diff1
      value: 78.46858307815857
    - type: nauc_map_at_1_max
      value: 69.03908373759435
    - type: nauc_map_at_1_std
      value: -7.479412070736642
    - type: nauc_map_at_20_diff1
      value: 74.98121458084796
    - type: nauc_map_at_20_max
      value: 70.51885366822565
    - type: nauc_map_at_20_std
      value: -5.286051287133815
    - type: nauc_map_at_3_diff1
      value: 75.36078454383373
    - type: nauc_map_at_3_max
      value: 70.34997144546014
    - type: nauc_map_at_3_std
      value: -6.663517224039184
    - type: nauc_map_at_5_diff1
      value: 75.0274512828238
    - type: nauc_map_at_5_max
      value: 70.45292551591874
    - type: nauc_map_at_5_std
      value: -6.029224488640147
    - type: nauc_mrr_at_1000_diff1
      value: 75.04018768469983
    - type: nauc_mrr_at_1000_max
      value: 70.49855509132635
    - type: nauc_mrr_at_1000_std
      value: -5.258929961409948
    - type: nauc_mrr_at_100_diff1
      value: 75.02605732810112
    - type: nauc_mrr_at_100_max
      value: 70.50082584929103
    - type: nauc_mrr_at_100_std
      value: -5.2304917988542154
    - type: nauc_mrr_at_10_diff1
      value: 74.96079080525713
    - type: nauc_mrr_at_10_max
      value: 70.56167294920391
    - type: nauc_mrr_at_10_std
      value: -5.360650630655072
    - type: nauc_mrr_at_1_diff1
      value: 78.46858307815857
    - type: nauc_mrr_at_1_max
      value: 69.03908373759435
    - type: nauc_mrr_at_1_std
      value: -7.479412070736642
    - type: nauc_mrr_at_20_diff1
      value: 74.97939804960517
    - type: nauc_mrr_at_20_max
      value: 70.51804078965411
    - type: nauc_mrr_at_20_std
      value: -5.287681954889177
    - type: nauc_mrr_at_3_diff1
      value: 75.36078454383373
    - type: nauc_mrr_at_3_max
      value: 70.34997144546014
    - type: nauc_mrr_at_3_std
      value: -6.663517224039184
    - type: nauc_mrr_at_5_diff1
      value: 75.0274512828238
    - type: nauc_mrr_at_5_max
      value: 70.45292551591874
    - type: nauc_mrr_at_5_std
      value: -6.029224488640147
    - type: nauc_ndcg_at_1000_diff1
      value: 74.22106834748942
    - type: nauc_ndcg_at_1000_max
      value: 70.93625922934912
    - type: nauc_ndcg_at_1000_std
      value: -3.4878399005946017
    - type: nauc_ndcg_at_100_diff1
      value: 73.74068883646733
    - type: nauc_ndcg_at_100_max
      value: 71.02357018347472
    - type: nauc_ndcg_at_100_std
      value: -2.462293184201324
    - type: nauc_ndcg_at_10_diff1
      value: 73.40967965536565
    - type: nauc_ndcg_at_10_max
      value: 71.29379828672067
    - type: nauc_ndcg_at_10_std
      value: -3.295547756383108
    - type: nauc_ndcg_at_1_diff1
      value: 78.46858307815857
    - type: nauc_ndcg_at_1_max
      value: 69.03908373759435
    - type: nauc_ndcg_at_1_std
      value: -7.479412070736642
    - type: nauc_ndcg_at_20_diff1
      value: 73.45790057693699
    - type: nauc_ndcg_at_20_max
      value: 71.16598432419126
    - type: nauc_ndcg_at_20_std
      value: -2.962877157646097
    - type: nauc_ndcg_at_3_diff1
      value: 74.30696173964847
    - type: nauc_ndcg_at_3_max
      value: 70.79878978459556
    - type: nauc_ndcg_at_3_std
      value: -6.297286578628299
    - type: nauc_ndcg_at_5_diff1
      value: 73.65858211199816
    - type: nauc_ndcg_at_5_max
      value: 71.01122417463776
    - type: nauc_ndcg_at_5_std
      value: -5.075990882646765
    - type: nauc_precision_at_1000_diff1
      value: 68.71065091972568
    - type: nauc_precision_at_1000_max
      value: 81.38173585624777
    - type: nauc_precision_at_1000_std
      value: 58.035497889797895
    - type: nauc_precision_at_100_diff1
      value: 61.93634256957017
    - type: nauc_precision_at_100_max
      value: 74.84191770203093
    - type: nauc_precision_at_100_std
      value: 31.3325983123831
    - type: nauc_precision_at_10_diff1
      value: 66.68247010944937
    - type: nauc_precision_at_10_max
      value: 74.48773524654571
    - type: nauc_precision_at_10_std
      value: 6.560421880785153
    - type: nauc_precision_at_1_diff1
      value: 78.46858307815857
    - type: nauc_precision_at_1_max
      value: 69.03908373759435
    - type: nauc_precision_at_1_std
      value: -7.479412070736642
    - type: nauc_precision_at_20_diff1
      value: 65.51592872758067
    - type: nauc_precision_at_20_max
      value: 74.50684066823096
    - type: nauc_precision_at_20_std
      value: 10.830479877698208
    - type: nauc_precision_at_3_diff1
      value: 70.89587884861588
    - type: nauc_precision_at_3_max
      value: 72.25310558370424
    - type: nauc_precision_at_3_std
      value: -5.0796100900749765
    - type: nauc_precision_at_5_diff1
      value: 68.71885719845497
    - type: nauc_precision_at_5_max
      value: 73.02601751485672
    - type: nauc_precision_at_5_std
      value: -1.4382681421626857
    - type: nauc_recall_at_1000_diff1
      value: 71.95510299834734
    - type: nauc_recall_at_1000_max
      value: 84.03647166092985
    - type: nauc_recall_at_1000_std
      value: 56.87490604776847
    - type: nauc_recall_at_100_diff1
      value: 62.446624924715955
    - type: nauc_recall_at_100_max
      value: 75.25666892464507
    - type: nauc_recall_at_100_std
      value: 31.068789794554686
    - type: nauc_recall_at_10_diff1
      value: 66.70676336328988
    - type: nauc_recall_at_10_max
      value: 74.4963699656397
    - type: nauc_recall_at_10_std
      value: 6.57498399706916
    - type: nauc_recall_at_1_diff1
      value: 78.46858307815857
    - type: nauc_recall_at_1_max
      value: 69.03908373759435
    - type: nauc_recall_at_1_std
      value: -7.479412070736642
    - type: nauc_recall_at_20_diff1
      value: 65.54082767974772
    - type: nauc_recall_at_20_max
      value: 74.5111529838772
    - type: nauc_recall_at_20_std
      value: 10.84574829707354
    - type: nauc_recall_at_3_diff1
      value: 70.89587884861584
    - type: nauc_recall_at_3_max
      value: 72.25310558370421
    - type: nauc_recall_at_3_std
      value: -5.07961009007491
    - type: nauc_recall_at_5_diff1
      value: 68.71885719845501
    - type: nauc_recall_at_5_max
      value: 73.02601751485666
    - type: nauc_recall_at_5_std
      value: -1.4382681421626995
    - type: ndcg_at_1
      value: 55.494
    - type: ndcg_at_10
      value: 66.374
    - type: ndcg_at_100
      value: 69.254
    - type: ndcg_at_1000
      value: 70.136
    - type: ndcg_at_20
      value: 67.599
    - type: ndcg_at_3
      value: 62.863
    - type: ndcg_at_5
      value: 64.644
    - type: precision_at_1
      value: 55.494
    - type: precision_at_10
      value: 7.776
    - type: precision_at_100
      value: 0.9159999999999999
    - type: precision_at_1000
      value: 0.099
    - type: precision_at_20
      value: 4.1290000000000004
    - type: precision_at_3
      value: 22.688
    - type: precision_at_5
      value: 14.477
    - type: recall_at_1
      value: 55.494
    - type: recall_at_10
      value: 77.747
    - type: recall_at_100
      value: 91.535
    - type: recall_at_1000
      value: 98.619
    - type: recall_at_20
      value: 82.565
    - type: recall_at_3
      value: 68.063
    - type: recall_at_5
      value: 72.386
    task:
      type: Retrieval
  - dataset:
      config: eng-eng
      name: MTEB MLQARetrieval (eng-eng)
      revision: 397ed406c1a7902140303e7faf60fff35b58d285
      split: test
      type: facebook/mlqa
    metrics:
    - type: main_score
      value: 64.723
    - type: map_at_1
      value: 54.308
    - type: map_at_10
      value: 61.26200000000001
    - type: map_at_100
      value: 61.82299999999999
    - type: map_at_1000
      value: 61.856
    - type: map_at_20
      value: 61.575
    - type: map_at_3
      value: 59.565
    - type: map_at_5
      value: 60.561
    - type: mrr_at_1
      value: 54.31704368848212
    - type: mrr_at_10
      value: 61.26520216098834
    - type: mrr_at_100
      value: 61.82588321127103
    - type: mrr_at_1000
      value: 61.859333030574334
    - type: mrr_at_20
      value: 61.57780339921337
    - type: mrr_at_3
      value: 59.569446842801646
    - type: mrr_at_5
      value: 60.56323029989004
    - type: nauc_map_at_1000_diff1
      value: 74.21413722468635
    - type: nauc_map_at_1000_max
      value: 70.41741227882316
    - type: nauc_map_at_1000_std
      value: -2.5438707209848506
    - type: nauc_map_at_100_diff1
      value: 74.19812315947975
    - type: nauc_map_at_100_max
      value: 70.41589146728445
    - type: nauc_map_at_100_std
      value: -2.5336117059429553
    - type: nauc_map_at_10_diff1
      value: 74.21810561152937
    - type: nauc_map_at_10_max
      value: 70.48816115200171
    - type: nauc_map_at_10_std
      value: -2.7443834681406734
    - type: nauc_map_at_1_diff1
      value: 77.69378738778958
    - type: nauc_map_at_1_max
      value: 68.64652310701173
    - type: nauc_map_at_1_std
      value: -4.667071946448379
    - type: nauc_map_at_20_diff1
      value: 74.16105697562438
    - type: nauc_map_at_20_max
      value: 70.42491994631179
    - type: nauc_map_at_20_std
      value: -2.6070416022440472
    - type: nauc_map_at_3_diff1
      value: 74.60449392878863
    - type: nauc_map_at_3_max
      value: 70.39888609914269
    - type: nauc_map_at_3_std
      value: -3.5401151125723986
    - type: nauc_map_at_5_diff1
      value: 74.2423420992663
    - type: nauc_map_at_5_max
      value: 70.36574501826757
    - type: nauc_map_at_5_std
      value: -3.2707393116898964
    - type: nauc_mrr_at_1000_diff1
      value: 74.21029843731323
    - type: nauc_mrr_at_1000_max
      value: 70.43020492688913
    - type: nauc_mrr_at_1000_std
      value: -2.526895582202081
    - type: nauc_mrr_at_100_diff1
      value: 74.19440960479243
    - type: nauc_mrr_at_100_max
      value: 70.4288998824232
    - type: nauc_mrr_at_100_std
      value: -2.5160929945118107
    - type: nauc_mrr_at_10_diff1
      value: 74.2141357266166
    - type: nauc_mrr_at_10_max
      value: 70.5005683347807
    - type: nauc_mrr_at_10_std
      value: -2.727154557882168
    - type: nauc_mrr_at_1_diff1
      value: 77.69891248239793
    - type: nauc_mrr_at_1_max
      value: 68.68255231164922
    - type: nauc_mrr_at_1_std
      value: -4.630226727154317
    - type: nauc_mrr_at_20_diff1
      value: 74.15705434409723
    - type: nauc_mrr_at_20_max
      value: 70.43741835972747
    - type: nauc_mrr_at_20_std
      value: -2.5896756472464495
    - type: nauc_mrr_at_3_diff1
      value: 74.5981844349412
    - type: nauc_mrr_at_3_max
      value: 70.41834937080564
    - type: nauc_mrr_at_3_std
      value: -3.5161656408031163
    - type: nauc_mrr_at_5_diff1
      value: 74.23847535424844
    - type: nauc_mrr_at_5_max
      value: 70.37763810013656
    - type: nauc_mrr_at_5_std
      value: -3.2560955164581733
    - type: nauc_ndcg_at_1000_diff1
      value: 73.20994496725493
    - type: nauc_ndcg_at_1000_max
      value: 70.8903016277125
    - type: nauc_ndcg_at_1000_std
      value: -0.625772298462309
    - type: nauc_ndcg_at_100_diff1
      value: 72.6847141682645
    - type: nauc_ndcg_at_100_max
      value: 70.86564422034162
    - type: nauc_ndcg_at_100_std
      value: -0.07195786766326141
    - type: nauc_ndcg_at_10_diff1
      value: 72.78806493754281
    - type: nauc_ndcg_at_10_max
      value: 71.21957067926769
    - type: nauc_ndcg_at_10_std
      value: -1.2760418313382227
    - type: nauc_ndcg_at_1_diff1
      value: 77.69891248239793
    - type: nauc_ndcg_at_1_max
      value: 68.68255231164922
    - type: nauc_ndcg_at_1_std
      value: -4.630226727154317
    - type: nauc_ndcg_at_20_diff1
      value: 72.52082440882546
    - type: nauc_ndcg_at_20_max
      value: 70.98185004796734
    - type: nauc_ndcg_at_20_std
      value: -0.6908280874815464
    - type: nauc_ndcg_at_3_diff1
      value: 73.59870660843939
    - type: nauc_ndcg_at_3_max
      value: 70.94391957288654
    - type: nauc_ndcg_at_3_std
      value: -3.147723179140428
    - type: nauc_ndcg_at_5_diff1
      value: 72.90122868193457
    - type: nauc_ndcg_at_5_max
      value: 70.89376368965165
    - type: nauc_ndcg_at_5_std
      value: -2.6451807385626744
    - type: nauc_precision_at_1000_diff1
      value: 58.14737201864067
    - type: nauc_precision_at_1000_max
      value: 78.79011251144826
    - type: nauc_precision_at_1000_std
      value: 59.98985420476577
    - type: nauc_precision_at_100_diff1
      value: 59.21069121644552
    - type: nauc_precision_at_100_max
      value: 73.00557835912306
    - type: nauc_precision_at_100_std
      value: 26.85027406282173
    - type: nauc_precision_at_10_diff1
      value: 66.8760831023675
    - type: nauc_precision_at_10_max
      value: 74.21167950452596
    - type: nauc_precision_at_10_std
      value: 5.453652499335947
    - type: nauc_precision_at_1_diff1
      value: 77.69891248239793
    - type: nauc_precision_at_1_max
      value: 68.68255231164922
    - type: nauc_precision_at_1_std
      value: -4.630226727154317
    - type: nauc_precision_at_20_diff1
      value: 64.3118559132602
    - type: nauc_precision_at_20_max
      value: 73.33078184673825
    - type: nauc_precision_at_20_std
      value: 9.993299523049402
    - type: nauc_precision_at_3_diff1
      value: 70.38667185155593
    - type: nauc_precision_at_3_max
      value: 72.66495006030951
    - type: nauc_precision_at_3_std
      value: -1.8532839591326276
    - type: nauc_precision_at_5_diff1
      value: 68.12161337583686
    - type: nauc_precision_at_5_max
      value: 72.65644960375046
    - type: nauc_precision_at_5_std
      value: -0.33317164167012875
    - type: nauc_recall_at_1000_diff1
      value: 61.63204394739985
    - type: nauc_recall_at_1000_max
      value: 81.77241537319897
    - type: nauc_recall_at_1000_std
      value: 58.44841544062308
    - type: nauc_recall_at_100_diff1
      value: 59.72072697224705
    - type: nauc_recall_at_100_max
      value: 73.28519507061553
    - type: nauc_recall_at_100_std
      value: 26.27318390763456
    - type: nauc_recall_at_10_diff1
      value: 66.9757135465418
    - type: nauc_recall_at_10_max
      value: 74.21919493374149
    - type: nauc_recall_at_10_std
      value: 5.323369605377166
    - type: nauc_recall_at_1_diff1
      value: 77.69378738778958
    - type: nauc_recall_at_1_max
      value: 68.64652310701173
    - type: nauc_recall_at_1_std
      value: -4.667071946448379
    - type: nauc_recall_at_20_diff1
      value: 64.42290081731899
    - type: nauc_recall_at_20_max
      value: 73.3358289439033
    - type: nauc_recall_at_20_std
      value: 9.846598361586073
    - type: nauc_recall_at_3_diff1
      value: 70.41211290964785
    - type: nauc_recall_at_3_max
      value: 72.64451776775402
    - type: nauc_recall_at_3_std
      value: -1.916280959835826
    - type: nauc_recall_at_5_diff1
      value: 68.20695272727916
    - type: nauc_recall_at_5_max
      value: 72.66404224006101
    - type: nauc_recall_at_5_std
      value: -0.431125323007886
    - type: ndcg_at_1
      value: 54.31700000000001
    - type: ndcg_at_10
      value: 64.723
    - type: ndcg_at_100
      value: 67.648
    - type: ndcg_at_1000
      value: 68.619
    - type: ndcg_at_20
      value: 65.85499999999999
    - type: ndcg_at_3
      value: 61.244
    - type: ndcg_at_5
      value: 63.038000000000004
    - type: precision_at_1
      value: 54.31700000000001
    - type: precision_at_10
      value: 7.564
    - type: precision_at_100
      value: 0.898
    - type: precision_at_1000
      value: 0.098
    - type: precision_at_20
      value: 4.005
    - type: precision_at_3
      value: 22.034000000000002
    - type: precision_at_5
      value: 14.093
    - type: recall_at_1
      value: 54.308
    - type: recall_at_10
      value: 75.622
    - type: recall_at_100
      value: 89.744
    - type: recall_at_1000
      value: 97.539
    - type: recall_at_20
      value: 80.085
    - type: recall_at_3
      value: 66.09
    - type: recall_at_5
      value: 70.446
    task:
      type: Retrieval
  - dataset:
      config: de
      name: MTEB MLSUMClusteringP2P (de)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 41.267647761702854
    - type: v_measure
      value: 41.267647761702854
    - type: v_measure_std
      value: 10.93390895077248
    task:
      type: Clustering
  - dataset:
      config: fr
      name: MTEB MLSUMClusteringP2P (fr)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 44.68714862333979
    - type: v_measure
      value: 44.68714862333979
    - type: v_measure_std
      value: 1.811036989797814
    task:
      type: Clustering
  - dataset:
      config: ru
      name: MTEB MLSUMClusteringP2P (ru)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 41.92518785753813
    - type: v_measure
      value: 41.92518785753813
    - type: v_measure_std
      value: 5.9356661900220775
    task:
      type: Clustering
  - dataset:
      config: es
      name: MTEB MLSUMClusteringP2P (es)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 48.69875719812033
    - type: v_measure
      value: 48.69875719812033
    - type: v_measure_std
      value: 1.204253881950113
    task:
      type: Clustering
  - dataset:
      config: de
      name: MTEB MLSUMClusteringS2S (de)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 40.07927325071353
    - type: v_measure
      value: 40.07927325071353
    - type: v_measure_std
      value: 9.296680835266145
    task:
      type: Clustering
  - dataset:
      config: fr
      name: MTEB MLSUMClusteringS2S (fr)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 44.88484854069901
    - type: v_measure
      value: 44.88484854069901
    - type: v_measure_std
      value: 2.3704247819781843
    task:
      type: Clustering
  - dataset:
      config: ru
      name: MTEB MLSUMClusteringS2S (ru)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 43.97657450929179
    - type: v_measure
      value: 43.97657450929179
    - type: v_measure_std
      value: 6.087547931333613
    task:
      type: Clustering
  - dataset:
      config: es
      name: MTEB MLSUMClusteringS2S (es)
      revision: b5d54f8f3b61ae17845046286940f03c6bc79bc7
      split: test
      type: reciTAL/mlsum
    metrics:
    - type: main_score
      value: 48.41108671948728
    - type: v_measure
      value: 48.41108671948728
    - type: v_measure_std
      value: 1.3848320630151243
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB MMarcoReranking (default)
      revision: 8e0c766dbe9e16e1d221116a3f36795fbade07f6
      split: dev
      type: C-MTEB/Mmarco-reranking
    metrics:
    - type: map
      value: 21.050447576170395
    - type: mrr
      value: 20.201984126984126
    - type: main_score
      value: 21.050447576170395
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB MMarcoRetrieval (default)
      revision: 539bbde593d947e2a124ba72651aafc09eb33fc2
      split: dev
      type: C-MTEB/MMarcoRetrieval
    metrics:
    - type: main_score
      value: 79.687
    - type: map_at_1
      value: 66.872
    - type: map_at_10
      value: 75.949
    - type: map_at_100
      value: 76.25
    - type: map_at_1000
      value: 76.259
    - type: map_at_20
      value: 76.145
    - type: map_at_3
      value: 74.01299999999999
    - type: map_at_5
      value: 75.232
    - type: mrr_at_1
      value: 69.18338108882521
    - type: mrr_at_10
      value: 76.5424227952881
    - type: mrr_at_100
      value: 76.8019342792628
    - type: mrr_at_1000
      value: 76.81002278342808
    - type: mrr_at_20
      value: 76.7115234815896
    - type: mrr_at_3
      value: 74.83046800382044
    - type: mrr_at_5
      value: 75.88490926456515
    - type: nauc_map_at_1000_diff1
      value: 78.06933310424179
    - type: nauc_map_at_1000_max
      value: 49.392948209665896
    - type: nauc_map_at_1000_std
      value: -15.126109322591166
    - type: nauc_map_at_100_diff1
      value: 78.06612779298378
    - type: nauc_map_at_100_max
      value: 49.40761618630397
    - type: nauc_map_at_100_std
      value: -15.099282408159349
    - type: nauc_map_at_10_diff1
      value: 77.94565685470538
    - type: nauc_map_at_10_max
      value: 49.50559610363201
    - type: nauc_map_at_10_std
      value: -15.182130695916355
    - type: nauc_map_at_1_diff1
      value: 79.84814509858211
    - type: nauc_map_at_1_max
      value: 40.78978466656547
    - type: nauc_map_at_1_std
      value: -19.96189264026715
    - type: nauc_map_at_20_diff1
      value: 78.03597839981245
    - type: nauc_map_at_20_max
      value: 49.49477427223376
    - type: nauc_map_at_20_std
      value: -15.084990000838378
    - type: nauc_map_at_3_diff1
      value: 78.0637014655507
    - type: nauc_map_at_3_max
      value: 48.63214001973341
    - type: nauc_map_at_3_std
      value: -17.093950563306596
    - type: nauc_map_at_5_diff1
      value: 77.94068229240348
    - type: nauc_map_at_5_max
      value: 49.38930719689204
    - type: nauc_map_at_5_std
      value: -15.9919454201954
    - type: nauc_mrr_at_1000_diff1
      value: 78.34582398092816
    - type: nauc_mrr_at_1000_max
      value: 49.623566992784156
    - type: nauc_mrr_at_1000_std
      value: -14.381347765493265
    - type: nauc_mrr_at_100_diff1
      value: 78.3429966714221
    - type: nauc_mrr_at_100_max
      value: 49.63684922240546
    - type: nauc_mrr_at_100_std
      value: -14.354914066301236
    - type: nauc_mrr_at_10_diff1
      value: 78.2208070219624
    - type: nauc_mrr_at_10_max
      value: 49.77720536573364
    - type: nauc_mrr_at_10_std
      value: -14.316233764741812
    - type: nauc_mrr_at_1_diff1
      value: 80.22305496572142
    - type: nauc_mrr_at_1_max
      value: 44.30231210192536
    - type: nauc_mrr_at_1_std
      value: -18.942549914934492
    - type: nauc_mrr_at_20_diff1
      value: 78.31006724240147
    - type: nauc_mrr_at_20_max
      value: 49.72338465276142
    - type: nauc_mrr_at_20_std
      value: -14.30722621948953
    - type: nauc_mrr_at_3_diff1
      value: 78.39832634634523
    - type: nauc_mrr_at_3_max
      value: 49.24985961036677
    - type: nauc_mrr_at_3_std
      value: -15.966286866763191
    - type: nauc_mrr_at_5_diff1
      value: 78.2406507247798
    - type: nauc_mrr_at_5_max
      value: 49.71276359754787
    - type: nauc_mrr_at_5_std
      value: -14.979526226149698
    - type: nauc_ndcg_at_1000_diff1
      value: 77.74892471071016
    - type: nauc_ndcg_at_1000_max
      value: 51.11543344053061
    - type: nauc_ndcg_at_1000_std
      value: -12.208878737005096
    - type: nauc_ndcg_at_100_diff1
      value: 77.67462502211228
    - type: nauc_ndcg_at_100_max
      value: 51.593977338939034
    - type: nauc_ndcg_at_100_std
      value: -11.312126179513802
    - type: nauc_ndcg_at_10_diff1
      value: 77.0571291760012
    - type: nauc_ndcg_at_10_max
      value: 52.35435572808972
    - type: nauc_ndcg_at_10_std
      value: -11.33242546164059
    - type: nauc_ndcg_at_1_diff1
      value: 80.22305496572142
    - type: nauc_ndcg_at_1_max
      value: 44.30231210192536
    - type: nauc_ndcg_at_1_std
      value: -18.942549914934492
    - type: nauc_ndcg_at_20_diff1
      value: 77.4141216117471
    - type: nauc_ndcg_at_20_max
      value: 52.340600871365375
    - type: nauc_ndcg_at_20_std
      value: -10.989010161550912
    - type: nauc_ndcg_at_3_diff1
      value: 77.43971989259062
    - type: nauc_ndcg_at_3_max
      value: 50.59251358320663
    - type: nauc_ndcg_at_3_std
      value: -15.59337960636058
    - type: nauc_ndcg_at_5_diff1
      value: 77.12174287031847
    - type: nauc_ndcg_at_5_max
      value: 51.97108510288907
    - type: nauc_ndcg_at_5_std
      value: -13.474902612427167
    - type: nauc_precision_at_1000_diff1
      value: -19.36793534929367
    - type: nauc_precision_at_1000_max
      value: 11.803383262344036
    - type: nauc_precision_at_1000_std
      value: 24.304436015177046
    - type: nauc_precision_at_100_diff1
      value: -6.273790806909921
    - type: nauc_precision_at_100_max
      value: 23.372606271300747
    - type: nauc_precision_at_100_std
      value: 29.085768971612342
    - type: nauc_precision_at_10_diff1
      value: 21.67045907336595
    - type: nauc_precision_at_10_max
      value: 41.68948432407223
    - type: nauc_precision_at_10_std
      value: 17.837055074458092
    - type: nauc_precision_at_1_diff1
      value: 80.22305496572142
    - type: nauc_precision_at_1_max
      value: 44.30231210192536
    - type: nauc_precision_at_1_std
      value: -18.942549914934492
    - type: nauc_precision_at_20_diff1
      value: 12.577671896684803
    - type: nauc_precision_at_20_max
      value: 37.44944702246691
    - type: nauc_precision_at_20_std
      value: 23.635897665206087
    - type: nauc_precision_at_3_diff1
      value: 47.165335112814056
    - type: nauc_precision_at_3_max
      value: 47.0458691263379
    - type: nauc_precision_at_3_std
      value: -3.3181861146890217
    - type: nauc_precision_at_5_diff1
      value: 35.406205343514806
    - type: nauc_precision_at_5_max
      value: 45.56549449285401
    - type: nauc_precision_at_5_std
      value: 5.612378074562386
    - type: nauc_recall_at_1000_diff1
      value: 72.32762520815842
    - type: nauc_recall_at_1000_max
      value: 85.64979256307343
    - type: nauc_recall_at_1000_std
      value: 73.61925297037476
    - type: nauc_recall_at_100_diff1
      value: 72.31946328709962
    - type: nauc_recall_at_100_max
      value: 83.76576070068353
    - type: nauc_recall_at_100_std
      value: 57.39376538662535
    - type: nauc_recall_at_10_diff1
      value: 69.51307788072499
    - type: nauc_recall_at_10_max
      value: 69.60124733654142
    - type: nauc_recall_at_10_std
      value: 13.483540424716892
    - type: nauc_recall_at_1_diff1
      value: 79.84814509858211
    - type: nauc_recall_at_1_max
      value: 40.78978466656547
    - type: nauc_recall_at_1_std
      value: -19.96189264026715
    - type: nauc_recall_at_20_diff1
      value: 70.92168324710599
    - type: nauc_recall_at_20_max
      value: 76.09106252420084
    - type: nauc_recall_at_20_std
      value: 25.406842300761447
    - type: nauc_recall_at_3_diff1
      value: 74.1212680517145
    - type: nauc_recall_at_3_max
      value: 56.24921832879403
    - type: nauc_recall_at_3_std
      value: -11.55542913578436
    - type: nauc_recall_at_5_diff1
      value: 72.31262959872993
    - type: nauc_recall_at_5_max
      value: 62.761214896697915
    - type: nauc_recall_at_5_std
      value: -3.280167584070396
    - type: ndcg_at_1
      value: 69.18299999999999
    - type: ndcg_at_10
      value: 79.687
    - type: ndcg_at_100
      value: 81.062
    - type: ndcg_at_1000
      value: 81.312
    - type: ndcg_at_20
      value: 80.34599999999999
    - type: ndcg_at_3
      value: 75.98700000000001
    - type: ndcg_at_5
      value: 78.039
    - type: precision_at_1
      value: 69.18299999999999
    - type: precision_at_10
      value: 9.636
    - type: precision_at_100
      value: 1.0330000000000001
    - type: precision_at_1000
      value: 0.105
    - type: precision_at_20
      value: 4.958
    - type: precision_at_3
      value: 28.515
    - type: precision_at_5
      value: 18.201
    - type: recall_at_1
      value: 66.872
    - type: recall_at_10
      value: 90.688
    - type: recall_at_100
      value: 96.99
    - type: recall_at_1000
      value: 98.958
    - type: recall_at_20
      value: 93.21199999999999
    - type: recall_at_3
      value: 80.84599999999999
    - type: recall_at_5
      value: 85.732
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB MSMARCO (default)
      revision: c5a29a104738b98a9e76336939199e264163d4a0
      split: dev
      type: mteb/msmarco
    metrics:
    - type: map_at_1
      value: 21.861
    - type: map_at_10
      value: 34.008
    - type: map_at_100
      value: 35.174
    - type: map_at_1000
      value: 35.224
    - type: map_at_20
      value: 34.705999999999996
    - type: map_at_3
      value: 30.209000000000003
    - type: map_at_5
      value: 32.351
    - type: mrr_at_1
      value: 22.493
    - type: mrr_at_10
      value: 34.583999999999996
    - type: mrr_at_100
      value: 35.691
    - type: mrr_at_1000
      value: 35.736000000000004
    - type: mrr_at_20
      value: 35.257
    - type: mrr_at_3
      value: 30.85
    - type: mrr_at_5
      value: 32.962
    - type: ndcg_at_1
      value: 22.493
    - type: ndcg_at_10
      value: 40.815
    - type: ndcg_at_100
      value: 46.483999999999995
    - type: ndcg_at_1000
      value: 47.73
    - type: ndcg_at_20
      value: 43.302
    - type: ndcg_at_3
      value: 33.056000000000004
    - type: ndcg_at_5
      value: 36.879
    - type: precision_at_1
      value: 22.493
    - type: precision_at_10
      value: 6.465999999999999
    - type: precision_at_100
      value: 0.932
    - type: precision_at_1000
      value: 0.104
    - type: precision_at_20
      value: 3.752
    - type: precision_at_3
      value: 14.069
    - type: precision_at_5
      value: 10.384
    - type: recall_at_1
      value: 21.861
    - type: recall_at_10
      value: 61.781
    - type: recall_at_100
      value: 88.095
    - type: recall_at_1000
      value: 97.625
    - type: recall_at_20
      value: 71.44500000000001
    - type: recall_at_3
      value: 40.653
    - type: recall_at_5
      value: 49.841
    - type: main_score
      value: 40.815
    task:
      type: Retrieval
  - dataset:
      config: en
      name: MTEB MTOPDomainClassification (en)
      revision: d80d48c1eb48d3562165c59d59d0034df9fff0bf
      split: test
      type: mteb/mtop_domain
    metrics:
    - type: accuracy
      value: 97.4874601003192
    - type: f1
      value: 97.19067544931094
    - type: f1_weighted
      value: 97.49331776181019
    - type: main_score
      value: 97.4874601003192
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB MTOPDomainClassification (de)
      revision: d80d48c1eb48d3562165c59d59d0034df9fff0bf
      split: test
      type: mteb/mtop_domain
    metrics:
    - type: accuracy
      value: 96.89489997182305
    - type: f1
      value: 96.51138586512977
    - type: f1_weighted
      value: 96.89723065967186
    - type: main_score
      value: 96.89489997182305
    task:
      type: Classification
  - dataset:
      config: es
      name: MTEB MTOPDomainClassification (es)
      revision: d80d48c1eb48d3562165c59d59d0034df9fff0bf
      split: test
      type: mteb/mtop_domain
    metrics:
    - type: accuracy
      value: 97.17144763175452
    - type: f1
      value: 96.81785681878274
    - type: f1_weighted
      value: 97.1778974586874
    - type: main_score
      value: 97.17144763175452
    task:
      type: Classification
  - dataset:
      config: fr
      name: MTEB MTOPDomainClassification (fr)
      revision: d80d48c1eb48d3562165c59d59d0034df9fff0bf
      split: test
      type: mteb/mtop_domain
    metrics:
    - type: accuracy
      value: 96.30128405887879
    - type: f1
      value: 95.94555923088487
    - type: f1_weighted
      value: 96.30399416794926
    - type: main_score
      value: 96.30128405887879
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB MTOPIntentClassification (en)
      revision: ae001d0e6b1228650b7bd1c2c65fb50ad11a8aba
      split: test
      type: mteb/mtop_intent
    metrics:
    - type: accuracy
      value: 84.53488372093022
    - type: f1
      value: 61.77995074251401
    - type: f1_weighted
      value: 86.8005170485101
    - type: main_score
      value: 84.53488372093022
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB MTOPIntentClassification (de)
      revision: ae001d0e6b1228650b7bd1c2c65fb50ad11a8aba
      split: test
      type: mteb/mtop_intent
    metrics:
    - type: accuracy
      value: 80.79459002535924
    - type: f1
      value: 56.08938302001448
    - type: f1_weighted
      value: 83.66582131948252
    - type: main_score
      value: 80.79459002535924
    task:
      type: Classification
  - dataset:
      config: es
      name: MTEB MTOPIntentClassification (es)
      revision: ae001d0e6b1228650b7bd1c2c65fb50ad11a8aba
      split: test
      type: mteb/mtop_intent
    metrics:
    - type: accuracy
      value: 84.7765176784523
    - type: f1
      value: 61.39860057885528
    - type: f1_weighted
      value: 86.94881745670745
    - type: main_score
      value: 84.7765176784523
    task:
      type: Classification
  - dataset:
      config: fr
      name: MTEB MTOPIntentClassification (fr)
      revision: ae001d0e6b1228650b7bd1c2c65fb50ad11a8aba
      split: test
      type: mteb/mtop_intent
    metrics:
    - type: accuracy
      value: 82.2079549013467
    - type: f1
      value: 59.90260478749016
    - type: f1_weighted
      value: 84.36861708593257
    - type: main_score
      value: 82.2079549013467
    task:
      type: Classification
  - dataset:
      config: eng
      name: MTEB MasakhaNEWSClassification (eng)
      revision: 18193f187b92da67168c655c9973a165ed9593dd
      split: test
      type: mteb/masakhanews
    metrics:
    - type: accuracy
      value: 74.98945147679325
    - type: f1
      value: 74.3157483560261
    - type: f1_weighted
      value: 75.01179008904884
    - type: main_score
      value: 74.98945147679325
    task:
      type: Classification
  - dataset:
      config: fra
      name: MTEB MasakhaNEWSClassification (fra)
      revision: 18193f187b92da67168c655c9973a165ed9593dd
      split: test
      type: mteb/masakhanews
    metrics:
    - type: accuracy
      value: 74.02843601895735
    - type: f1
      value: 70.40326349620732
    - type: f1_weighted
      value: 74.6596277063484
    - type: main_score
      value: 74.02843601895735
    task:
      type: Classification
  - dataset:
      config: amh
      name: MTEB MasakhaNEWSClusteringP2P (amh)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 69.45780291725053
    - type: v_measure
      value: 69.45780291725053
    - type: v_measure_std
      value: 36.54340055904091
    task:
      type: Clustering
  - dataset:
      config: eng
      name: MTEB MasakhaNEWSClusteringP2P (eng)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 64.88996119332239
    - type: v_measure
      value: 64.88996119332239
    - type: v_measure_std
      value: 30.017223408197268
    task:
      type: Clustering
  - dataset:
      config: fra
      name: MTEB MasakhaNEWSClusteringP2P (fra)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 42.362383958691666
    - type: v_measure
      value: 42.362383958691666
    - type: v_measure_std
      value: 37.61076788039063
    task:
      type: Clustering
  - dataset:
      config: hau
      name: MTEB MasakhaNEWSClusteringP2P (hau)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 43.29201252405562
    - type: v_measure
      value: 43.29201252405562
    - type: v_measure_std
      value: 34.31987945146255
    task:
      type: Clustering
  - dataset:
      config: ibo
      name: MTEB MasakhaNEWSClusteringP2P (ibo)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 33.59926542995238
    - type: v_measure
      value: 33.59926542995238
    - type: v_measure_std
      value: 35.70048601084112
    task:
      type: Clustering
  - dataset:
      config: lin
      name: MTEB MasakhaNEWSClusteringP2P (lin)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 67.58487601893106
    - type: v_measure
      value: 67.58487601893106
    - type: v_measure_std
      value: 35.16784970777931
    task:
      type: Clustering
  - dataset:
      config: lug
      name: MTEB MasakhaNEWSClusteringP2P (lug)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 50.01220872023533
    - type: v_measure
      value: 50.01220872023533
    - type: v_measure_std
      value: 41.87411574676182
    task:
      type: Clustering
  - dataset:
      config: orm
      name: MTEB MasakhaNEWSClusteringP2P (orm)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 29.007847502598317
    - type: v_measure
      value: 29.007847502598317
    - type: v_measure_std
      value: 38.374997395079994
    task:
      type: Clustering
  - dataset:
      config: pcm
      name: MTEB MasakhaNEWSClusteringP2P (pcm)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 79.13520228554611
    - type: v_measure
      value: 79.13520228554611
    - type: v_measure_std
      value: 18.501843848275183
    task:
      type: Clustering
  - dataset:
      config: run
      name: MTEB MasakhaNEWSClusteringP2P (run)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 60.317213909746656
    - type: v_measure
      value: 60.317213909746656
    - type: v_measure_std
      value: 36.500281823747386
    task:
      type: Clustering
  - dataset:
      config: sna
      name: MTEB MasakhaNEWSClusteringP2P (sna)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 59.395277358240946
    - type: v_measure
      value: 59.395277358240946
    - type: v_measure_std
      value: 37.500916816164654
    task:
      type: Clustering
  - dataset:
      config: som
      name: MTEB MasakhaNEWSClusteringP2P (som)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 38.18638688704302
    - type: v_measure
      value: 38.18638688704302
    - type: v_measure_std
      value: 35.453681137564466
    task:
      type: Clustering
  - dataset:
      config: swa
      name: MTEB MasakhaNEWSClusteringP2P (swa)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 29.49230755729658
    - type: v_measure
      value: 29.49230755729658
    - type: v_measure_std
      value: 28.284313285264645
    task:
      type: Clustering
  - dataset:
      config: tir
      name: MTEB MasakhaNEWSClusteringP2P (tir)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 60.632258622750115
    - type: v_measure
      value: 60.632258622750115
    - type: v_measure_std
      value: 34.429711214740564
    task:
      type: Clustering
  - dataset:
      config: xho
      name: MTEB MasakhaNEWSClusteringP2P (xho)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 41.76322918806381
    - type: v_measure
      value: 41.76322918806381
    - type: v_measure_std
      value: 36.43245296200775
    task:
      type: Clustering
  - dataset:
      config: yor
      name: MTEB MasakhaNEWSClusteringP2P (yor)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 33.17083910808645
    - type: v_measure
      value: 33.17083910808645
    - type: v_measure_std
      value: 34.87547994284835
    task:
      type: Clustering
  - dataset:
      config: amh
      name: MTEB MasakhaNEWSClusteringS2S (amh)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 60.95132147787602
    - type: v_measure
      value: 60.95132147787602
    - type: v_measure_std
      value: 37.330148394033365
    task:
      type: Clustering
  - dataset:
      config: eng
      name: MTEB MasakhaNEWSClusteringS2S (eng)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 60.974810831426595
    - type: v_measure
      value: 60.974810831426595
    - type: v_measure_std
      value: 24.934675467507827
    task:
      type: Clustering
  - dataset:
      config: fra
      name: MTEB MasakhaNEWSClusteringS2S (fra)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 44.479206673553335
    - type: v_measure
      value: 44.479206673553335
    - type: v_measure_std
      value: 32.58254804499339
    task:
      type: Clustering
  - dataset:
      config: hau
      name: MTEB MasakhaNEWSClusteringS2S (hau)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 26.4742082741682
    - type: v_measure
      value: 26.4742082741682
    - type: v_measure_std
      value: 22.344929192323097
    task:
      type: Clustering
  - dataset:
      config: ibo
      name: MTEB MasakhaNEWSClusteringS2S (ibo)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 38.906129911741985
    - type: v_measure
      value: 38.906129911741985
    - type: v_measure_std
      value: 34.785601792668444
    task:
      type: Clustering
  - dataset:
      config: lin
      name: MTEB MasakhaNEWSClusteringS2S (lin)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 62.60982020876592
    - type: v_measure
      value: 62.60982020876592
    - type: v_measure_std
      value: 40.7368955715045
    task:
      type: Clustering
  - dataset:
      config: lug
      name: MTEB MasakhaNEWSClusteringS2S (lug)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 42.70424106365967
    - type: v_measure
      value: 42.70424106365967
    - type: v_measure_std
      value: 46.80946241135087
    task:
      type: Clustering
  - dataset:
      config: orm
      name: MTEB MasakhaNEWSClusteringS2S (orm)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 28.609942199922322
    - type: v_measure
      value: 28.609942199922322
    - type: v_measure_std
      value: 38.46685040191088
    task:
      type: Clustering
  - dataset:
      config: pcm
      name: MTEB MasakhaNEWSClusteringS2S (pcm)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 76.83901348810822
    - type: v_measure
      value: 76.83901348810822
    - type: v_measure_std
      value: 17.57617141269189
    task:
      type: Clustering
  - dataset:
      config: run
      name: MTEB MasakhaNEWSClusteringS2S (run)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 46.89757547846193
    - type: v_measure
      value: 46.89757547846193
    - type: v_measure_std
      value: 44.58903590203438
    task:
      type: Clustering
  - dataset:
      config: sna
      name: MTEB MasakhaNEWSClusteringS2S (sna)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 55.37185207068829
    - type: v_measure
      value: 55.37185207068829
    - type: v_measure_std
      value: 36.944574863543004
    task:
      type: Clustering
  - dataset:
      config: som
      name: MTEB MasakhaNEWSClusteringS2S (som)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 37.44211021681754
    - type: v_measure
      value: 37.44211021681754
    - type: v_measure_std
      value: 33.41469994463241
    task:
      type: Clustering
  - dataset:
      config: swa
      name: MTEB MasakhaNEWSClusteringS2S (swa)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 26.020680621216062
    - type: v_measure
      value: 26.020680621216062
    - type: v_measure_std
      value: 25.480037522570413
    task:
      type: Clustering
  - dataset:
      config: tir
      name: MTEB MasakhaNEWSClusteringS2S (tir)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 63.74306846771303
    - type: v_measure
      value: 63.74306846771303
    - type: v_measure_std
      value: 32.19119631078685
    task:
      type: Clustering
  - dataset:
      config: xho
      name: MTEB MasakhaNEWSClusteringS2S (xho)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 24.580890519243777
    - type: v_measure
      value: 24.580890519243777
    - type: v_measure_std
      value: 37.941836363967106
    task:
      type: Clustering
  - dataset:
      config: yor
      name: MTEB MasakhaNEWSClusteringS2S (yor)
      revision: 8ccc72e69e65f40c70e117d8b3c08306bb788b60
      split: test
      type: masakhane/masakhanews
    metrics:
    - type: main_score
      value: 43.63458888828314
    - type: v_measure
      value: 43.63458888828314
    - type: v_measure_std
      value: 31.28169350649098
    task:
      type: Clustering
  - dataset:
      config: pl
      name: MTEB MassiveIntentClassification (pl)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 75.37323470073974
    - type: f1
      value: 71.1836877753734
    - type: f1_weighted
      value: 75.72073213955457
    - type: main_score
      value: 75.37323470073974
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB MassiveIntentClassification (de)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 74.83523873570948
    - type: f1
      value: 70.72375821116886
    - type: f1_weighted
      value: 75.20800490010755
    - type: main_score
      value: 74.83523873570948
    task:
      type: Classification
  - dataset:
      config: es
      name: MTEB MassiveIntentClassification (es)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 75.31607262945528
    - type: f1
      value: 72.06063554897662
    - type: f1_weighted
      value: 75.72438161355252
    - type: main_score
      value: 75.31607262945528
    task:
      type: Classification
  - dataset:
      config: ru
      name: MTEB MassiveIntentClassification (ru)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 76.7955615332885
    - type: f1
      value: 73.08099648499756
    - type: f1_weighted
      value: 77.18482068239668
    - type: main_score
      value: 76.7955615332885
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB MassiveIntentClassification (en)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 77.60591795561534
    - type: f1
      value: 74.46676705370395
    - type: f1_weighted
      value: 77.69888062336614
    - type: main_score
      value: 77.60591795561534
    task:
      type: Classification
  - dataset:
      config: fr
      name: MTEB MassiveIntentClassification (fr)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 76.32145258910558
    - type: f1
      value: 72.89824154178328
    - type: f1_weighted
      value: 76.6539327979472
    - type: main_score
      value: 76.32145258910558
    task:
      type: Classification
  - dataset:
      config: zh-CN
      name: MTEB MassiveIntentClassification (zh-CN)
      revision: 4672e20407010da34463acc759c162ca9734bca6
      split: test
      type: mteb/amazon_massive_intent
    metrics:
    - type: accuracy
      value: 73.21788836583724
    - type: f1
      value: 70.45594512246377
    - type: f1_weighted
      value: 73.67862536499393
    - type: main_score
      value: 73.21788836583724
    task:
      type: Classification
  - dataset:
      config: zh-CN
      name: MTEB MassiveScenarioClassification (zh-CN)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 80.82044384667114
    - type: f1
      value: 80.53217664465089
    - type: f1_weighted
      value: 80.94535087010512
    - type: main_score
      value: 80.82044384667114
    task:
      type: Classification
  - dataset:
      config: pl
      name: MTEB MassiveScenarioClassification (pl)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 82.1049092131809
    - type: f1
      value: 81.55343463694733
    - type: f1_weighted
      value: 82.33509098770782
    - type: main_score
      value: 82.1049092131809
    task:
      type: Classification
  - dataset:
      config: es
      name: MTEB MassiveScenarioClassification (es)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 82.58238063214526
    - type: f1
      value: 82.27974449333072
    - type: f1_weighted
      value: 82.81337569618209
    - type: main_score
      value: 82.58238063214526
    task:
      type: Classification
  - dataset:
      config: de
      name: MTEB MassiveScenarioClassification (de)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 83.97108271687962
    - type: f1
      value: 83.56285606936076
    - type: f1_weighted
      value: 84.10198745390771
    - type: main_score
      value: 83.97108271687962
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB MassiveScenarioClassification (en)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 84.71082716879623
    - type: f1
      value: 84.09447062371402
    - type: f1_weighted
      value: 84.73765765551342
    - type: main_score
      value: 84.71082716879623
    task:
      type: Classification
  - dataset:
      config: fr
      name: MTEB MassiveScenarioClassification (fr)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 83.093476798924
    - type: f1
      value: 82.72656900752943
    - type: f1_weighted
      value: 83.26606516503364
    - type: main_score
      value: 83.093476798924
    task:
      type: Classification
  - dataset:
      config: ru
      name: MTEB MassiveScenarioClassification (ru)
      revision: fad2c6e8459f9e1c45d9315f4953d921437d70f8
      split: test
      type: mteb/amazon_massive_scenario
    metrics:
    - type: accuracy
      value: 84.05850706119705
    - type: f1
      value: 83.64234048881222
    - type: f1_weighted
      value: 84.17315768381876
    - type: main_score
      value: 84.05850706119705
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB MedicalRetrieval (default)
      revision: 2039188fb5800a9803ba5048df7b76e6fb151fc6
      split: dev
      type: C-MTEB/MedicalRetrieval
    metrics:
    - type: main_score
      value: 56.635999999999996
    - type: map_at_1
      value: 48.699999999999996
    - type: map_at_10
      value: 53.991
    - type: map_at_100
      value: 54.449999999999996
    - type: map_at_1000
      value: 54.515
    - type: map_at_20
      value: 54.212
    - type: map_at_3
      value: 52.833
    - type: map_at_5
      value: 53.503
    - type: mrr_at_1
      value: 48.699999999999996
    - type: mrr_at_10
      value: 53.991309523809505
    - type: mrr_at_100
      value: 54.45008993448266
    - type: mrr_at_1000
      value: 54.515253990549795
    - type: mrr_at_20
      value: 54.21201762247036
    - type: mrr_at_3
      value: 52.8333333333333
    - type: mrr_at_5
      value: 53.50333333333328
    - type: nauc_map_at_1000_diff1
      value: 79.96867989401643
    - type: nauc_map_at_1000_max
      value: 69.75230895599029
    - type: nauc_map_at_1000_std
      value: 2.6418738289740213
    - type: nauc_map_at_100_diff1
      value: 79.95343709599133
    - type: nauc_map_at_100_max
      value: 69.751282671507
    - type: nauc_map_at_100_std
      value: 2.621719966106279
    - type: nauc_map_at_10_diff1
      value: 80.02875864565634
    - type: nauc_map_at_10_max
      value: 69.80948662290187
    - type: nauc_map_at_10_std
      value: 2.329151604733765
    - type: nauc_map_at_1_diff1
      value: 83.616940281383
    - type: nauc_map_at_1_max
      value: 69.08142651929452
    - type: nauc_map_at_1_std
      value: 1.9687791394035643
    - type: nauc_map_at_20_diff1
      value: 79.95555601275339
    - type: nauc_map_at_20_max
      value: 69.76604695002925
    - type: nauc_map_at_20_std
      value: 2.556184141901367
    - type: nauc_map_at_3_diff1
      value: 80.74790131023668
    - type: nauc_map_at_3_max
      value: 70.57797991892402
    - type: nauc_map_at_3_std
      value: 2.7115149849964117
    - type: nauc_map_at_5_diff1
      value: 80.31796539878381
    - type: nauc_map_at_5_max
      value: 69.93573796420061
    - type: nauc_map_at_5_std
      value: 2.0731614029506606
    - type: nauc_mrr_at_1000_diff1
      value: 79.96867999907981
    - type: nauc_mrr_at_1000_max
      value: 69.57395578976896
    - type: nauc_mrr_at_1000_std
      value: 2.46351945887829
    - type: nauc_mrr_at_100_diff1
      value: 79.95343709599133
    - type: nauc_mrr_at_100_max
      value: 69.57322054130803
    - type: nauc_mrr_at_100_std
      value: 2.4436578359073433
    - type: nauc_mrr_at_10_diff1
      value: 80.02875864565634
    - type: nauc_mrr_at_10_max
      value: 69.63292630937411
    - type: nauc_mrr_at_10_std
      value: 2.1525912912060012
    - type: nauc_mrr_at_1_diff1
      value: 83.616940281383
    - type: nauc_mrr_at_1_max
      value: 68.74717310480305
    - type: nauc_mrr_at_1_std
      value: 1.6345257249120868
    - type: nauc_mrr_at_20_diff1
      value: 79.95555601275339
    - type: nauc_mrr_at_20_max
      value: 69.58883608470444
    - type: nauc_mrr_at_20_std
      value: 2.378973276576547
    - type: nauc_mrr_at_3_diff1
      value: 80.74790131023668
    - type: nauc_mrr_at_3_max
      value: 70.40430475488604
    - type: nauc_mrr_at_3_std
      value: 2.5378398209583817
    - type: nauc_mrr_at_5_diff1
      value: 80.31796539878381
    - type: nauc_mrr_at_5_max
      value: 69.7605991748183
    - type: nauc_mrr_at_5_std
      value: 1.898022613568352
    - type: nauc_ndcg_at_1000_diff1
      value: 78.35504059321225
    - type: nauc_ndcg_at_1000_max
      value: 69.06752522437093
    - type: nauc_ndcg_at_1000_std
      value: 3.9624036886099265
    - type: nauc_ndcg_at_100_diff1
      value: 77.79729140249833
    - type: nauc_ndcg_at_100_max
      value: 68.93113791506029
    - type: nauc_ndcg_at_100_std
      value: 3.642178826886181
    - type: nauc_ndcg_at_10_diff1
      value: 78.160158293918
    - type: nauc_ndcg_at_10_max
      value: 69.28122202281361
    - type: nauc_ndcg_at_10_std
      value: 2.438976810940962
    - type: nauc_ndcg_at_1_diff1
      value: 83.616940281383
    - type: nauc_ndcg_at_1_max
      value: 69.08142651929452
    - type: nauc_ndcg_at_1_std
      value: 1.9687791394035643
    - type: nauc_ndcg_at_20_diff1
      value: 77.88514432874997
    - type: nauc_ndcg_at_20_max
      value: 69.06148818508873
    - type: nauc_ndcg_at_20_std
      value: 3.1800249272363676
    - type: nauc_ndcg_at_3_diff1
      value: 79.73510384405803
    - type: nauc_ndcg_at_3_max
      value: 70.78000695123832
    - type: nauc_ndcg_at_3_std
      value: 2.9041415468363274
    - type: nauc_ndcg_at_5_diff1
      value: 78.91872808866195
    - type: nauc_ndcg_at_5_max
      value: 69.61478429620091
    - type: nauc_ndcg_at_5_std
      value: 1.734699636301054
    - type: nauc_precision_at_1000_diff1
      value: 66.37858395390673
    - type: nauc_precision_at_1000_max
      value: 60.651659037598534
    - type: nauc_precision_at_1000_std
      value: 27.388353715469798
    - type: nauc_precision_at_100_diff1
      value: 66.34325807776025
    - type: nauc_precision_at_100_max
      value: 63.63855305621111
    - type: nauc_precision_at_100_std
      value: 10.641748149575351
    - type: nauc_precision_at_10_diff1
      value: 71.3784685491089
    - type: nauc_precision_at_10_max
      value: 67.05313695174542
    - type: nauc_precision_at_10_std
      value: 3.000406867930561
    - type: nauc_precision_at_1_diff1
      value: 83.616940281383
    - type: nauc_precision_at_1_max
      value: 69.08142651929452
    - type: nauc_precision_at_1_std
      value: 1.9687791394035643
    - type: nauc_precision_at_20_diff1
      value: 69.73407910977694
    - type: nauc_precision_at_20_max
      value: 65.77426240320742
    - type: nauc_precision_at_20_std
      value: 6.204416838482586
    - type: nauc_precision_at_3_diff1
      value: 76.63737537643107
    - type: nauc_precision_at_3_max
      value: 71.29710200719668
    - type: nauc_precision_at_3_std
      value: 3.47180961484546
    - type: nauc_precision_at_5_diff1
      value: 74.36945983536717
    - type: nauc_precision_at_5_max
      value: 68.33292218003061
    - type: nauc_precision_at_5_std
      value: 0.47128762620258075
    - type: nauc_recall_at_1000_diff1
      value: 66.37858395390681
    - type: nauc_recall_at_1000_max
      value: 60.65165903759889
    - type: nauc_recall_at_1000_std
      value: 27.388353715469822
    - type: nauc_recall_at_100_diff1
      value: 66.34325807776025
    - type: nauc_recall_at_100_max
      value: 63.63855305621116
    - type: nauc_recall_at_100_std
      value: 10.641748149575351
    - type: nauc_recall_at_10_diff1
      value: 71.37846854910892
    - type: nauc_recall_at_10_max
      value: 67.05313695174546
    - type: nauc_recall_at_10_std
      value: 3.000406867930663
    - type: nauc_recall_at_1_diff1
      value: 83.616940281383
    - type: nauc_recall_at_1_max
      value: 69.08142651929452
    - type: nauc_recall_at_1_std
      value: 1.9687791394035643
    - type: nauc_recall_at_20_diff1
      value: 69.73407910977691
    - type: nauc_recall_at_20_max
      value: 65.77426240320746
    - type: nauc_recall_at_20_std
      value: 6.204416838482536
    - type: nauc_recall_at_3_diff1
      value: 76.63737537643112
    - type: nauc_recall_at_3_max
      value: 71.29710200719668
    - type: nauc_recall_at_3_std
      value: 3.471809614845442
    - type: nauc_recall_at_5_diff1
      value: 74.36945983536715
    - type: nauc_recall_at_5_max
      value: 68.33292218003065
    - type: nauc_recall_at_5_std
      value: 0.4712876262026442
    - type: ndcg_at_1
      value: 48.699999999999996
    - type: ndcg_at_10
      value: 56.635999999999996
    - type: ndcg_at_100
      value: 59.193
    - type: ndcg_at_1000
      value: 60.97
    - type: ndcg_at_20
      value: 57.426
    - type: ndcg_at_3
      value: 54.186
    - type: ndcg_at_5
      value: 55.407
    - type: precision_at_1
      value: 48.699999999999996
    - type: precision_at_10
      value: 6.5
    - type: precision_at_100
      value: 0.777
    - type: precision_at_1000
      value: 0.092
    - type: precision_at_20
      value: 3.405
    - type: precision_at_3
      value: 19.367
    - type: precision_at_5
      value: 12.22
    - type: recall_at_1
      value: 48.699999999999996
    - type: recall_at_10
      value: 65.0
    - type: recall_at_100
      value: 77.7
    - type: recall_at_1000
      value: 91.8
    - type: recall_at_20
      value: 68.10000000000001
    - type: recall_at_3
      value: 58.099999999999994
    - type: recall_at_5
      value: 61.1
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB MedrxivClusteringP2P (default)
      revision: e7a26af6f3ae46b30dde8737f02c07b1505bcc73
      split: test
      type: mteb/medrxiv-clustering-p2p
    metrics:
    - type: main_score
      value: 34.80188561439236
    - type: v_measure
      value: 34.80188561439236
    - type: v_measure_std
      value: 1.5703148841573102
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB MedrxivClusteringS2S (default)
      revision: 35191c8c0dca72d8ff3efcd72aa802307d469663
      split: test
      type: mteb/medrxiv-clustering-s2s
    metrics:
    - type: main_score
      value: 32.42285513996236
    - type: v_measure
      value: 32.42285513996236
    - type: v_measure_std
      value: 1.3769867487457566
    task:
      type: Clustering
  - dataset:
      config: de
      name: MTEB MintakaRetrieval (de)
      revision: efa78cc2f74bbcd21eff2261f9e13aebe40b814e
      split: test
      type: jinaai/mintakaqa
    metrics:
    - type: main_score
      value: 27.025
    - type: map_at_1
      value: 14.532
    - type: map_at_10
      value: 22.612
    - type: map_at_100
      value: 23.802
    - type: map_at_1000
      value: 23.9
    - type: map_at_20
      value: 23.275000000000002
    - type: map_at_3
      value: 20.226
    - type: map_at_5
      value: 21.490000000000002
    - type: mrr_at_1
      value: 14.532434709351305
    - type: mrr_at_10
      value: 22.612077265615575
    - type: mrr_at_100
      value: 23.801523356874675
    - type: mrr_at_1000
      value: 23.900118499340238
    - type: mrr_at_20
      value: 23.275466430108995
    - type: mrr_at_3
      value: 20.22606009547877
    - type: mrr_at_5
      value: 21.489750070204945
    - type: nauc_map_at_1000_diff1
      value: 14.148987799763596
    - type: nauc_map_at_1000_max
      value: 44.70338461387784
    - type: nauc_map_at_1000_std
      value: 15.868006767707637
    - type: nauc_map_at_100_diff1
      value: 14.11371769080442
    - type: nauc_map_at_100_max
      value: 44.67995540936296
    - type: nauc_map_at_100_std
      value: 15.890796502029076
    - type: nauc_map_at_10_diff1
      value: 14.29066834165688
    - type: nauc_map_at_10_max
      value: 45.10997111765282
    - type: nauc_map_at_10_std
      value: 15.508568918629864
    - type: nauc_map_at_1_diff1
      value: 23.473291302576396
    - type: nauc_map_at_1_max
      value: 44.68942599764586
    - type: nauc_map_at_1_std
      value: 12.424377262427253
    - type: nauc_map_at_20_diff1
      value: 14.112652046087831
    - type: nauc_map_at_20_max
      value: 44.82014861413682
    - type: nauc_map_at_20_std
      value: 15.739350613646385
    - type: nauc_map_at_3_diff1
      value: 16.119659221396347
    - type: nauc_map_at_3_max
      value: 46.04766378953525
    - type: nauc_map_at_3_std
      value: 13.969878046315925
    - type: nauc_map_at_5_diff1
      value: 15.095453434076184
    - type: nauc_map_at_5_max
      value: 45.802128149314406
    - type: nauc_map_at_5_std
      value: 14.957442173319949
    - type: nauc_mrr_at_1000_diff1
      value: 14.148987799763596
    - type: nauc_mrr_at_1000_max
      value: 44.70338461387784
    - type: nauc_mrr_at_1000_std
      value: 15.868006767707637
    - type: nauc_mrr_at_100_diff1
      value: 14.11371769080442
    - type: nauc_mrr_at_100_max
      value: 44.67995540936296
    - type: nauc_mrr_at_100_std
      value: 15.890796502029076
    - type: nauc_mrr_at_10_diff1
      value: 14.29066834165688
    - type: nauc_mrr_at_10_max
      value: 45.10997111765282
    - type: nauc_mrr_at_10_std
      value: 15.508568918629864
    - type: nauc_mrr_at_1_diff1
      value: 23.473291302576396
    - type: nauc_mrr_at_1_max
      value: 44.68942599764586
    - type: nauc_mrr_at_1_std
      value: 12.424377262427253
    - type: nauc_mrr_at_20_diff1
      value: 14.112652046087831
    - type: nauc_mrr_at_20_max
      value: 44.82014861413682
    - type: nauc_mrr_at_20_std
      value: 15.739350613646385
    - type: nauc_mrr_at_3_diff1
      value: 16.119659221396347
    - type: nauc_mrr_at_3_max
      value: 46.04766378953525
    - type: nauc_mrr_at_3_std
      value: 13.969878046315925
    - type: nauc_mrr_at_5_diff1
      value: 15.095453434076184
    - type: nauc_mrr_at_5_max
      value: 45.802128149314406
    - type: nauc_mrr_at_5_std
      value: 14.957442173319949
    - type: nauc_ndcg_at_1000_diff1
      value: 11.626606894574028
    - type: nauc_ndcg_at_1000_max
      value: 43.328592841065536
    - type: nauc_ndcg_at_1000_std
      value: 18.049446272245547
    - type: nauc_ndcg_at_100_diff1
      value: 10.485720606660239
    - type: nauc_ndcg_at_100_max
      value: 42.405317674170966
    - type: nauc_ndcg_at_100_std
      value: 19.107151641936987
    - type: nauc_ndcg_at_10_diff1
      value: 11.029351078162982
    - type: nauc_ndcg_at_10_max
      value: 44.36855031964681
    - type: nauc_ndcg_at_10_std
      value: 17.302796171409305
    - type: nauc_ndcg_at_1_diff1
      value: 23.473291302576396
    - type: nauc_ndcg_at_1_max
      value: 44.68942599764586
    - type: nauc_ndcg_at_1_std
      value: 12.424377262427253
    - type: nauc_ndcg_at_20_diff1
      value: 10.356662718168412
    - type: nauc_ndcg_at_20_max
      value: 43.31602680430083
    - type: nauc_ndcg_at_20_std
      value: 18.162891267850316
    - type: nauc_ndcg_at_3_diff1
      value: 14.42844952297869
    - type: nauc_ndcg_at_3_max
      value: 46.26603339466543
    - type: nauc_ndcg_at_3_std
      value: 14.449362723887857
    - type: nauc_ndcg_at_5_diff1
      value: 12.783416563486396
    - type: nauc_ndcg_at_5_max
      value: 45.852176479124424
    - type: nauc_ndcg_at_5_std
      value: 16.11775016428085
    - type: nauc_precision_at_1000_diff1
      value: -8.045361059399795
    - type: nauc_precision_at_1000_max
      value: 21.970273281738777
    - type: nauc_precision_at_1000_std
      value: 49.564650488193266
    - type: nauc_precision_at_100_diff1
      value: -2.118628861593353
    - type: nauc_precision_at_100_max
      value: 31.32498977104778
    - type: nauc_precision_at_100_std
      value: 32.96087731883451
    - type: nauc_precision_at_10_diff1
      value: 3.0335517475367615
    - type: nauc_precision_at_10_max
      value: 42.21620215030219
    - type: nauc_precision_at_10_std
      value: 21.90159732315962
    - type: nauc_precision_at_1_diff1
      value: 23.473291302576396
    - type: nauc_precision_at_1_max
      value: 44.68942599764586
    - type: nauc_precision_at_1_std
      value: 12.424377262427253
    - type: nauc_precision_at_20_diff1
      value: 0.4087201843719047
    - type: nauc_precision_at_20_max
      value: 38.485034773895734
    - type: nauc_precision_at_20_std
      value: 25.077397979916682
    - type: nauc_precision_at_3_diff1
      value: 10.408327736589833
    - type: nauc_precision_at_3_max
      value: 46.757216289175076
    - type: nauc_precision_at_3_std
      value: 15.62594354926867
    - type: nauc_precision_at_5_diff1
      value: 7.326752744229544
    - type: nauc_precision_at_5_max
      value: 45.89190518573553
    - type: nauc_precision_at_5_std
      value: 19.01717163438957
    - type: nauc_recall_at_1000_diff1
      value: -8.045361059400387
    - type: nauc_recall_at_1000_max
      value: 21.97027328173812
    - type: nauc_recall_at_1000_std
      value: 49.56465048819266
    - type: nauc_recall_at_100_diff1
      value: -2.118628861593277
    - type: nauc_recall_at_100_max
      value: 31.324989771047818
    - type: nauc_recall_at_100_std
      value: 32.96087731883457
    - type: nauc_recall_at_10_diff1
      value: 3.0335517475367166
    - type: nauc_recall_at_10_max
      value: 42.21620215030217
    - type: nauc_recall_at_10_std
      value: 21.901597323159606
    - type: nauc_recall_at_1_diff1
      value: 23.473291302576396
    - type: nauc_recall_at_1_max
      value: 44.68942599764586
    - type: nauc_recall_at_1_std
      value: 12.424377262427253
    - type: nauc_recall_at_20_diff1
      value: 0.40872018437190905
    - type: nauc_recall_at_20_max
      value: 38.485034773895734
    - type: nauc_recall_at_20_std
      value: 25.077397979916693
    - type: nauc_recall_at_3_diff1
      value: 10.408327736589843
    - type: nauc_recall_at_3_max
      value: 46.75721628917507
    - type: nauc_recall_at_3_std
      value: 15.625943549268664
    - type: nauc_recall_at_5_diff1
      value: 7.326752744229548
    - type: nauc_recall_at_5_max
      value: 45.89190518573557
    - type: nauc_recall_at_5_std
      value: 19.01717163438958
    - type: ndcg_at_1
      value: 14.532
    - type: ndcg_at_10
      value: 27.025
    - type: ndcg_at_100
      value: 33.305
    - type: ndcg_at_1000
      value: 36.38
    - type: ndcg_at_20
      value: 29.443
    - type: ndcg_at_3
      value: 22.035
    - type: ndcg_at_5
      value: 24.319
    - type: precision_at_1
      value: 14.532
    - type: precision_at_10
      value: 4.115
    - type: precision_at_100
      value: 0.717
    - type: precision_at_1000
      value: 0.097
    - type: precision_at_20
      value: 2.536
    - type: precision_at_3
      value: 9.085
    - type: precision_at_5
      value: 6.563
    - type: recall_at_1
      value: 14.532
    - type: recall_at_10
      value: 41.154
    - type: recall_at_100
      value: 71.651
    - type: recall_at_1000
      value: 96.841
    - type: recall_at_20
      value: 50.71600000000001
    - type: recall_at_3
      value: 27.254
    - type: recall_at_5
      value: 32.814
    task:
      type: Retrieval
  - dataset:
      config: es
      name: MTEB MintakaRetrieval (es)
      revision: efa78cc2f74bbcd21eff2261f9e13aebe40b814e
      split: test
      type: jinaai/mintakaqa
    metrics:
    - type: main_score
      value: 26.912000000000003
    - type: map_at_1
      value: 14.686
    - type: map_at_10
      value: 22.569
    - type: map_at_100
      value: 23.679
    - type: map_at_1000
      value: 23.777
    - type: map_at_20
      value: 23.169
    - type: map_at_3
      value: 20.201
    - type: map_at_5
      value: 21.566
    - type: mrr_at_1
      value: 14.686468646864686
    - type: mrr_at_10
      value: 22.569346220336296
    - type: mrr_at_100
      value: 23.678819125817146
    - type: mrr_at_1000
      value: 23.77713511338264
    - type: mrr_at_20
      value: 23.16850858443442
    - type: mrr_at_3
      value: 20.200770077007665
    - type: mrr_at_5
      value: 21.56628162816276
    - type: nauc_map_at_1000_diff1
      value: 14.129007578838381
    - type: nauc_map_at_1000_max
      value: 44.4255501141499
    - type: nauc_map_at_1000_std
      value: 19.95906154868176
    - type: nauc_map_at_100_diff1
      value: 14.09071870575231
    - type: nauc_map_at_100_max
      value: 44.403179928955566
    - type: nauc_map_at_100_std
      value: 20.00413657519976
    - type: nauc_map_at_10_diff1
      value: 14.149535953153688
    - type: nauc_map_at_10_max
      value: 44.66529917634685
    - type: nauc_map_at_10_std
      value: 19.580235989479394
    - type: nauc_map_at_1_diff1
      value: 23.489813522176636
    - type: nauc_map_at_1_max
      value: 46.54578639925787
    - type: nauc_map_at_1_std
      value: 16.39083721709994
    - type: nauc_map_at_20_diff1
      value: 14.021560420656181
    - type: nauc_map_at_20_max
      value: 44.4825455452467
    - type: nauc_map_at_20_std
      value: 19.886927750826878
    - type: nauc_map_at_3_diff1
      value: 16.182977890477723
    - type: nauc_map_at_3_max
      value: 46.1840554029258
    - type: nauc_map_at_3_std
      value: 18.735671900228958
    - type: nauc_map_at_5_diff1
      value: 14.779126395472833
    - type: nauc_map_at_5_max
      value: 45.23237213817556
    - type: nauc_map_at_5_std
      value: 19.348508580412872
    - type: nauc_mrr_at_1000_diff1
      value: 14.129007578838381
    - type: nauc_mrr_at_1000_max
      value: 44.4255501141499
    - type: nauc_mrr_at_1000_std
      value: 19.95906154868176
    - type: nauc_mrr_at_100_diff1
      value: 14.09071870575231
    - type: nauc_mrr_at_100_max
      value: 44.403179928955566
    - type: nauc_mrr_at_100_std
      value: 20.00413657519976
    - type: nauc_mrr_at_10_diff1
      value: 14.149535953153688
    - type: nauc_mrr_at_10_max
      value: 44.66529917634685
    - type: nauc_mrr_at_10_std
      value: 19.580235989479394
    - type: nauc_mrr_at_1_diff1
      value: 23.489813522176636
    - type: nauc_mrr_at_1_max
      value: 46.54578639925787
    - type: nauc_mrr_at_1_std
      value: 16.39083721709994
    - type: nauc_mrr_at_20_diff1
      value: 14.021560420656181
    - type: nauc_mrr_at_20_max
      value: 44.4825455452467
    - type: nauc_mrr_at_20_std
      value: 19.886927750826878
    - type: nauc_mrr_at_3_diff1
      value: 16.182977890477723
    - type: nauc_mrr_at_3_max
      value: 46.1840554029258
    - type: nauc_mrr_at_3_std
      value: 18.735671900228958
    - type: nauc_mrr_at_5_diff1
      value: 14.779126395472833
    - type: nauc_mrr_at_5_max
      value: 45.23237213817556
    - type: nauc_mrr_at_5_std
      value: 19.348508580412872
    - type: nauc_ndcg_at_1000_diff1
      value: 11.762470380481101
    - type: nauc_ndcg_at_1000_max
      value: 42.8233203033089
    - type: nauc_ndcg_at_1000_std
      value: 21.78503705117719
    - type: nauc_ndcg_at_100_diff1
      value: 10.45886076220022
    - type: nauc_ndcg_at_100_max
      value: 41.85472899256818
    - type: nauc_ndcg_at_100_std
      value: 23.20955486335138
    - type: nauc_ndcg_at_10_diff1
      value: 10.605912468659469
    - type: nauc_ndcg_at_10_max
      value: 43.150942448104715
    - type: nauc_ndcg_at_10_std
      value: 21.120035764826085
    - type: nauc_ndcg_at_1_diff1
      value: 23.489813522176636
    - type: nauc_ndcg_at_1_max
      value: 46.54578639925787
    - type: nauc_ndcg_at_1_std
      value: 16.39083721709994
    - type: nauc_ndcg_at_20_diff1
      value: 10.11291783888644
    - type: nauc_ndcg_at_20_max
      value: 42.51260678842788
    - type: nauc_ndcg_at_20_std
      value: 22.1744949382252
    - type: nauc_ndcg_at_3_diff1
      value: 14.25625326760802
    - type: nauc_ndcg_at_3_max
      value: 45.96162916377383
    - type: nauc_ndcg_at_3_std
      value: 19.557832728215523
    - type: nauc_ndcg_at_5_diff1
      value: 11.956317653823053
    - type: nauc_ndcg_at_5_max
      value: 44.35971268886807
    - type: nauc_ndcg_at_5_std
      value: 20.581696730374233
    - type: nauc_precision_at_1000_diff1
      value: 5.132291843566577
    - type: nauc_precision_at_1000_max
      value: 25.293354576835263
    - type: nauc_precision_at_1000_std
      value: 40.36005126087624
    - type: nauc_precision_at_100_diff1
      value: -1.5252854375008238
    - type: nauc_precision_at_100_max
      value: 31.007586474495984
    - type: nauc_precision_at_100_std
      value: 37.297552993548386
    - type: nauc_precision_at_10_diff1
      value: 1.9663657370770737
    - type: nauc_precision_at_10_max
      value: 39.194092293625125
    - type: nauc_precision_at_10_std
      value: 24.956542621999542
    - type: nauc_precision_at_1_diff1
      value: 23.489813522176636
    - type: nauc_precision_at_1_max
      value: 46.54578639925787
    - type: nauc_precision_at_1_std
      value: 16.39083721709994
    - type: nauc_precision_at_20_diff1
      value: 0.011112090390932373
    - type: nauc_precision_at_20_max
      value: 36.9357074392519
    - type: nauc_precision_at_20_std
      value: 28.611387115093876
    - type: nauc_precision_at_3_diff1
      value: 9.596831091013703
    - type: nauc_precision_at_3_max
      value: 45.3905541893809
    - type: nauc_precision_at_3_std
      value: 21.599314388526945
    - type: nauc_precision_at_5_diff1
      value: 5.175887949900142
    - type: nauc_precision_at_5_max
      value: 42.129467510414464
    - type: nauc_precision_at_5_std
      value: 23.607251548776677
    - type: nauc_recall_at_1000_diff1
      value: 5.132291843566257
    - type: nauc_recall_at_1000_max
      value: 25.29335457683396
    - type: nauc_recall_at_1000_std
      value: 40.36005126087638
    - type: nauc_recall_at_100_diff1
      value: -1.5252854375008988
    - type: nauc_recall_at_100_max
      value: 31.00758647449594
    - type: nauc_recall_at_100_std
      value: 37.29755299354834
    - type: nauc_recall_at_10_diff1
      value: 1.9663657370770793
    - type: nauc_recall_at_10_max
      value: 39.19409229362512
    - type: nauc_recall_at_10_std
      value: 24.956542621999546
    - type: nauc_recall_at_1_diff1
      value: 23.489813522176636
    - type: nauc_recall_at_1_max
      value: 46.54578639925787
    - type: nauc_recall_at_1_std
      value: 16.39083721709994
    - type: nauc_recall_at_20_diff1
      value: 0.011112090390923075
    - type: nauc_recall_at_20_max
      value: 36.93570743925189
    - type: nauc_recall_at_20_std
      value: 28.611387115093883
    - type: nauc_recall_at_3_diff1
      value: 9.596831091013714
    - type: nauc_recall_at_3_max
      value: 45.39055418938087
    - type: nauc_recall_at_3_std
      value: 21.599314388526956
    - type: nauc_recall_at_5_diff1
      value: 5.17588794990012
    - type: nauc_recall_at_5_max
      value: 42.12946751041448
    - type: nauc_recall_at_5_std
      value: 23.607251548776695
    - type: ndcg_at_1
      value: 14.686
    - type: ndcg_at_10
      value: 26.912000000000003
    - type: ndcg_at_100
      value: 32.919
    - type: ndcg_at_1000
      value: 36.119
    - type: ndcg_at_20
      value: 29.079
    - type: ndcg_at_3
      value: 21.995
    - type: ndcg_at_5
      value: 24.474999999999998
    - type: precision_at_1
      value: 14.686
    - type: precision_at_10
      value: 4.08
    - type: precision_at_100
      value: 0.703
    - type: precision_at_1000
      value: 0.097
    - type: precision_at_20
      value: 2.467
    - type: precision_at_3
      value: 9.062000000000001
    - type: precision_at_5
      value: 6.65
    - type: recall_at_1
      value: 14.686
    - type: recall_at_10
      value: 40.8
    - type: recall_at_100
      value: 70.338
    - type: recall_at_1000
      value: 96.82300000000001
    - type: recall_at_20
      value: 49.34
    - type: recall_at_3
      value: 27.186
    - type: recall_at_5
      value: 33.251
    task:
      type: Retrieval
  - dataset:
      config: fr
      name: MTEB MintakaRetrieval (fr)
      revision: efa78cc2f74bbcd21eff2261f9e13aebe40b814e
      split: test
      type: jinaai/mintakaqa
    metrics:
    - type: main_score
      value: 26.909
    - type: map_at_1
      value: 14.701
    - type: map_at_10
      value: 22.613
    - type: map_at_100
      value: 23.729
    - type: map_at_1000
      value: 23.837
    - type: map_at_20
      value: 23.262
    - type: map_at_3
      value: 20.236
    - type: map_at_5
      value: 21.673000000000002
    - type: mrr_at_1
      value: 14.7010647010647
    - type: mrr_at_10
      value: 22.613165113165113
    - type: mrr_at_100
      value: 23.72877605989423
    - type: mrr_at_1000
      value: 23.837150802746805
    - type: mrr_at_20
      value: 23.261627081110596
    - type: mrr_at_3
      value: 20.2361452361452
    - type: mrr_at_5
      value: 21.673491673491625
    - type: nauc_map_at_1000_diff1
      value: 17.08927788889635
    - type: nauc_map_at_1000_max
      value: 47.240929150603336
    - type: nauc_map_at_1000_std
      value: 20.559244258100275
    - type: nauc_map_at_100_diff1
      value: 17.029461792796777
    - type: nauc_map_at_100_max
      value: 47.207381115550696
    - type: nauc_map_at_100_std
      value: 20.581498156895265
    - type: nauc_map_at_10_diff1
      value: 17.351456007804536
    - type: nauc_map_at_10_max
      value: 47.815880040221344
    - type: nauc_map_at_10_std
      value: 20.292999107555794
    - type: nauc_map_at_1_diff1
      value: 27.297525357600776
    - type: nauc_map_at_1_max
      value: 47.18835074959486
    - type: nauc_map_at_1_std
      value: 18.304203168281834
    - type: nauc_map_at_20_diff1
      value: 17.157460199542136
    - type: nauc_map_at_20_max
      value: 47.4776610667456
    - type: nauc_map_at_20_std
      value: 20.499186342964478
    - type: nauc_map_at_3_diff1
      value: 19.393119961356277
    - type: nauc_map_at_3_max
      value: 49.02841822452882
    - type: nauc_map_at_3_std
      value: 19.293122796321292
    - type: nauc_map_at_5_diff1
      value: 17.76275044752008
    - type: nauc_map_at_5_max
      value: 48.01292548040298
    - type: nauc_map_at_5_std
      value: 19.928449977400504
    - type: nauc_mrr_at_1000_diff1
      value: 17.08927788889635
    - type: nauc_mrr_at_1000_max
      value: 47.240929150603336
    - type: nauc_mrr_at_1000_std
      value: 20.559244258100275
    - type: nauc_mrr_at_100_diff1
      value: 17.029461792796777
    - type: nauc_mrr_at_100_max
      value: 47.207381115550696
    - type: nauc_mrr_at_100_std
      value: 20.581498156895265
    - type: nauc_mrr_at_10_diff1
      value: 17.351456007804536
    - type: nauc_mrr_at_10_max
      value: 47.815880040221344
    - type: nauc_mrr_at_10_std
      value: 20.292999107555794
    - type: nauc_mrr_at_1_diff1
      value: 27.297525357600776
    - type: nauc_mrr_at_1_max
      value: 47.18835074959486
    - type: nauc_mrr_at_1_std
      value: 18.304203168281834
    - type: nauc_mrr_at_20_diff1
      value: 17.157460199542136
    - type: nauc_mrr_at_20_max
      value: 47.4776610667456
    - type: nauc_mrr_at_20_std
      value: 20.499186342964478
    - type: nauc_mrr_at_3_diff1
      value: 19.393119961356277
    - type: nauc_mrr_at_3_max
      value: 49.02841822452882
    - type: nauc_mrr_at_3_std
      value: 19.293122796321292
    - type: nauc_mrr_at_5_diff1
      value: 17.76275044752008
    - type: nauc_mrr_at_5_max
      value: 48.01292548040298
    - type: nauc_mrr_at_5_std
      value: 19.928449977400504
    - type: nauc_ndcg_at_1000_diff1
      value: 13.989496006047975
    - type: nauc_ndcg_at_1000_max
      value: 45.626323944336114
    - type: nauc_ndcg_at_1000_std
      value: 22.125600410796515
    - type: nauc_ndcg_at_100_diff1
      value: 12.302204843705244
    - type: nauc_ndcg_at_100_max
      value: 44.46856314559079
    - type: nauc_ndcg_at_100_std
      value: 23.084984546328677
    - type: nauc_ndcg_at_10_diff1
      value: 14.001226213368275
    - type: nauc_ndcg_at_10_max
      value: 47.37780636546918
    - type: nauc_ndcg_at_10_std
      value: 21.702709032840637
    - type: nauc_ndcg_at_1_diff1
      value: 27.297525357600776
    - type: nauc_ndcg_at_1_max
      value: 47.18835074959486
    - type: nauc_ndcg_at_1_std
      value: 18.304203168281834
    - type: nauc_ndcg_at_20_diff1
      value: 13.317759910171056
    - type: nauc_ndcg_at_20_max
      value: 46.25171251043813
    - type: nauc_ndcg_at_20_std
      value: 22.309331575402595
    - type: nauc_ndcg_at_3_diff1
      value: 17.555381234893872
    - type: nauc_ndcg_at_3_max
      value: 49.48635590260059
    - type: nauc_ndcg_at_3_std
      value: 19.734570962933674
    - type: nauc_ndcg_at_5_diff1
      value: 14.844841165765061
    - type: nauc_ndcg_at_5_max
      value: 47.76437065028708
    - type: nauc_ndcg_at_5_std
      value: 20.816034479453954
    - type: nauc_precision_at_1000_diff1
      value: -15.591898698252546
    - type: nauc_precision_at_1000_max
      value: 20.545984285353892
    - type: nauc_precision_at_1000_std
      value: 38.9013414992826
    - type: nauc_precision_at_100_diff1
      value: -5.290395978742176
    - type: nauc_precision_at_100_max
      value: 31.340480360546845
    - type: nauc_precision_at_100_std
      value: 33.6897935720505
    - type: nauc_precision_at_10_diff1
      value: 5.965001997926562
    - type: nauc_precision_at_10_max
      value: 46.12515296162247
    - type: nauc_precision_at_10_std
      value: 25.409433135253558
    - type: nauc_precision_at_1_diff1
      value: 27.297525357600776
    - type: nauc_precision_at_1_max
      value: 47.18835074959486
    - type: nauc_precision_at_1_std
      value: 18.304203168281834
    - type: nauc_precision_at_20_diff1
      value: 3.4438127279827744
    - type: nauc_precision_at_20_max
      value: 42.36095587714494
    - type: nauc_precision_at_20_std
      value: 27.367900512797906
    - type: nauc_precision_at_3_diff1
      value: 13.165017224718916
    - type: nauc_precision_at_3_max
      value: 50.58931825484506
    - type: nauc_precision_at_3_std
      value: 20.852009214609442
    - type: nauc_precision_at_5_diff1
      value: 7.840087177549876
    - type: nauc_precision_at_5_max
      value: 46.99388755575109
    - type: nauc_precision_at_5_std
      value: 23.048702393099834
    - type: nauc_recall_at_1000_diff1
      value: -15.591898698252932
    - type: nauc_recall_at_1000_max
      value: 20.5459842853537
    - type: nauc_recall_at_1000_std
      value: 38.901341499282395
    - type: nauc_recall_at_100_diff1
      value: -5.290395978742165
    - type: nauc_recall_at_100_max
      value: 31.340480360546863
    - type: nauc_recall_at_100_std
      value: 33.68979357205046
    - type: nauc_recall_at_10_diff1
      value: 5.96500199792656
    - type: nauc_recall_at_10_max
      value: 46.1251529616225
    - type: nauc_recall_at_10_std
      value: 25.409433135253543
    - type: nauc_recall_at_1_diff1
      value: 27.297525357600776
    - type: nauc_recall_at_1_max
      value: 47.18835074959486
    - type: nauc_recall_at_1_std
      value: 18.304203168281834
    - type: nauc_recall_at_20_diff1
      value: 3.4438127279827833
    - type: nauc_recall_at_20_max
      value: 42.36095587714498
    - type: nauc_recall_at_20_std
      value: 27.36790051279787
    - type: nauc_recall_at_3_diff1
      value: 13.165017224718916
    - type: nauc_recall_at_3_max
      value: 50.589318254845054
    - type: nauc_recall_at_3_std
      value: 20.852009214609435
    - type: nauc_recall_at_5_diff1
      value: 7.840087177549891
    - type: nauc_recall_at_5_max
      value: 46.99388755575112
    - type: nauc_recall_at_5_std
      value: 23.048702393099845
    - type: ndcg_at_1
      value: 14.701
    - type: ndcg_at_10
      value: 26.909
    - type: ndcg_at_100
      value: 32.727000000000004
    - type: ndcg_at_1000
      value: 36.086
    - type: ndcg_at_20
      value: 29.236
    - type: ndcg_at_3
      value: 22.004
    - type: ndcg_at_5
      value: 24.615000000000002
    - type: precision_at_1
      value: 14.701
    - type: precision_at_10
      value: 4.062
    - type: precision_at_100
      value: 0.688
    - type: precision_at_1000
      value: 0.096
    - type: precision_at_20
      value: 2.488
    - type: precision_at_3
      value: 9.036
    - type: precision_at_5
      value: 6.699
    - type: recall_at_1
      value: 14.701
    - type: recall_at_10
      value: 40.622
    - type: recall_at_100
      value: 68.796
    - type: recall_at_1000
      value: 96.314
    - type: recall_at_20
      value: 49.754
    - type: recall_at_3
      value: 27.108999999999998
    - type: recall_at_5
      value: 33.497
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB MultilingualSentiment (default)
      revision: 46958b007a63fdbf239b7672c25d0bea67b5ea1a
      split: test
      type: C-MTEB/MultilingualSentiment-classification
    metrics:
    - type: accuracy
      value: 73.20999999999998
    - type: f1
      value: 73.18755986777474
    - type: f1_weighted
      value: 73.18755986777475
    - type: main_score
      value: 73.20999999999998
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB NFCorpus (default)
      revision: ec0fa4fe99da2ff19ca1214b7966684033a58814
      split: test
      type: mteb/nfcorpus
    metrics:
    - type: map_at_1
      value: 4.822
    - type: map_at_10
      value: 13.144
    - type: map_at_100
      value: 17.254
    - type: map_at_1000
      value: 18.931
    - type: map_at_20
      value: 14.834
    - type: map_at_3
      value: 8.975
    - type: map_at_5
      value: 10.922
    - type: mrr_at_1
      value: 47.059
    - type: mrr_at_10
      value: 55.806999999999995
    - type: mrr_at_100
      value: 56.286
    - type: mrr_at_1000
      value: 56.327000000000005
    - type: mrr_at_20
      value: 56.00000000000001
    - type: mrr_at_3
      value: 54.17999999999999
    - type: mrr_at_5
      value: 55.155
    - type: ndcg_at_1
      value: 44.427
    - type: ndcg_at_10
      value: 36.623
    - type: ndcg_at_100
      value: 33.664
    - type: ndcg_at_1000
      value: 42.538
    - type: ndcg_at_20
      value: 34.066
    - type: ndcg_at_3
      value: 41.118
    - type: ndcg_at_5
      value: 39.455
    - type: precision_at_1
      value: 46.44
    - type: precision_at_10
      value: 28.607
    - type: precision_at_100
      value: 9.189
    - type: precision_at_1000
      value: 2.261
    - type: precision_at_20
      value: 21.238
    - type: precision_at_3
      value: 39.628
    - type: precision_at_5
      value: 35.604
    - type: recall_at_1
      value: 4.822
    - type: recall_at_10
      value: 17.488999999999997
    - type: recall_at_100
      value: 35.052
    - type: recall_at_1000
      value: 66.67999999999999
    - type: recall_at_20
      value: 21.343999999999998
    - type: recall_at_3
      value: 10.259
    - type: recall_at_5
      value: 13.406
    - type: main_score
      value: 36.623
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB NQ (default)
      revision: b774495ed302d8c44a3a7ea25c90dbce03968f31
      split: test
      type: mteb/nq
    metrics:
    - type: map_at_1
      value: 41.411
    - type: map_at_10
      value: 57.179
    - type: map_at_100
      value: 57.945
    - type: map_at_1000
      value: 57.967999999999996
    - type: map_at_20
      value: 57.687
    - type: map_at_3
      value: 53.46300000000001
    - type: map_at_5
      value: 55.696999999999996
    - type: mrr_at_1
      value: 46.233999999999995
    - type: mrr_at_10
      value: 59.831999999999994
    - type: mrr_at_100
      value: 60.33500000000001
    - type: mrr_at_1000
      value: 60.348
    - type: mrr_at_20
      value: 60.167
    - type: mrr_at_3
      value: 56.972
    - type: mrr_at_5
      value: 58.74
    - type: ndcg_at_1
      value: 46.205
    - type: ndcg_at_10
      value: 64.23100000000001
    - type: ndcg_at_100
      value: 67.242
    - type: ndcg_at_1000
      value: 67.72500000000001
    - type: ndcg_at_20
      value: 65.77300000000001
    - type: ndcg_at_3
      value: 57.516
    - type: ndcg_at_5
      value: 61.11600000000001
    - type: precision_at_1
      value: 46.205
    - type: precision_at_10
      value: 9.873
    - type: precision_at_100
      value: 1.158
    - type: precision_at_1000
      value: 0.12
    - type: precision_at_20
      value: 5.319
    - type: precision_at_3
      value: 25.424999999999997
    - type: precision_at_5
      value: 17.375
    - type: recall_at_1
      value: 41.411
    - type: recall_at_10
      value: 82.761
    - type: recall_at_100
      value: 95.52199999999999
    - type: recall_at_1000
      value: 99.02499999999999
    - type: recall_at_20
      value: 88.34
    - type: recall_at_3
      value: 65.73
    - type: recall_at_5
      value: 73.894
    - type: main_score
      value: 64.23100000000001
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB Ocnli (default)
      revision: 66e76a618a34d6d565d5538088562851e6daa7ec
      split: validation
      type: C-MTEB/OCNLI
    metrics:
    - type: cosine_accuracy
      value: 62.3714131023281
    - type: cosine_accuracy_threshold
      value: 79.70921993255615
    - type: cosine_ap
      value: 66.41380155495659
    - type: cosine_f1
      value: 68.89547185780786
    - type: cosine_f1_threshold
      value: 72.91591167449951
    - type: cosine_precision
      value: 57.485875706214685
    - type: cosine_recall
      value: 85.95564941921859
    - type: dot_accuracy
      value: 60.47644829453167
    - type: dot_accuracy_threshold
      value: 36627.362060546875
    - type: dot_ap
      value: 63.696303449293204
    - type: dot_f1
      value: 68.3986041101202
    - type: dot_f1_threshold
      value: 30452.72216796875
    - type: dot_precision
      value: 54.04411764705882
    - type: dot_recall
      value: 93.13621964097149
    - type: euclidean_accuracy
      value: 63.02111532214402
    - type: euclidean_accuracy_threshold
      value: 1392.76762008667
    - type: euclidean_ap
      value: 66.65907089443218
    - type: euclidean_f1
      value: 69.05036524413688
    - type: euclidean_f1_threshold
      value: 1711.5310668945312
    - type: euclidean_precision
      value: 54.29262394195889
    - type: euclidean_recall
      value: 94.82576557550159
    - type: main_score
      value: 63.02111532214402
    - type: manhattan_accuracy
      value: 62.75040606388739
    - type: manhattan_accuracy_threshold
      value: 32475.347900390625
    - type: manhattan_ap
      value: 66.50943585125434
    - type: manhattan_f1
      value: 69.08382066276802
    - type: manhattan_f1_threshold
      value: 41238.470458984375
    - type: manhattan_precision
      value: 54.75896168108776
    - type: manhattan_recall
      value: 93.55860612460401
    - type: max_accuracy
      value: 63.02111532214402
    - type: max_ap
      value: 66.65907089443218
    - type: max_f1
      value: 69.08382066276802
    - type: max_precision
      value: 57.485875706214685
    - type: max_recall
      value: 94.82576557550159
    - type: similarity_accuracy
      value: 62.3714131023281
    - type: similarity_accuracy_threshold
      value: 79.70921993255615
    - type: similarity_ap
      value: 66.41380155495659
    - type: similarity_f1
      value: 68.89547185780786
    - type: similarity_f1_threshold
      value: 72.91591167449951
    - type: similarity_precision
      value: 57.485875706214685
    - type: similarity_recall
      value: 85.95564941921859
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB OnlineShopping (default)
      revision: e610f2ebd179a8fda30ae534c3878750a96db120
      split: test
      type: C-MTEB/OnlineShopping-classification
    metrics:
    - type: accuracy
      value: 91.88000000000001
    - type: ap
      value: 89.52463684448476
    - type: ap_weighted
      value: 89.52463684448476
    - type: f1
      value: 91.86313022306673
    - type: f1_weighted
      value: 91.87806318146912
    - type: main_score
      value: 91.88000000000001
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB OpusparcusPC (en)
      revision: 9e9b1f8ef51616073f47f306f7f47dd91663f86a
      split: test.full
      type: GEM/opusparcus
    metrics:
    - type: cosine_accuracy
      value: 92.65578635014838
    - type: cosine_accuracy_threshold
      value: 74.02530312538147
    - type: cosine_ap
      value: 98.3834226153613
    - type: cosine_f1
      value: 94.92567913890312
    - type: cosine_f1_threshold
      value: 74.02530312538147
    - type: cosine_precision
      value: 95.562435500516
    - type: cosine_recall
      value: 94.29735234215886
    - type: dot_accuracy
      value: 91.54302670623146
    - type: dot_accuracy_threshold
      value: 34452.29187011719
    - type: dot_ap
      value: 98.1237257754439
    - type: dot_f1
      value: 94.22400803616273
    - type: dot_f1_threshold
      value: 33670.41931152344
    - type: dot_precision
      value: 92.9633300297324
    - type: dot_recall
      value: 95.5193482688391
    - type: euclidean_accuracy
      value: 92.28486646884274
    - type: euclidean_accuracy_threshold
      value: 1602.8022766113281
    - type: euclidean_ap
      value: 98.3099021504706
    - type: euclidean_f1
      value: 94.75277497477296
    - type: euclidean_f1_threshold
      value: 1604.7462463378906
    - type: euclidean_precision
      value: 93.89999999999999
    - type: euclidean_recall
      value: 95.62118126272912
    - type: main_score
      value: 98.3834226153613
    - type: manhattan_accuracy
      value: 92.2106824925816
    - type: manhattan_accuracy_threshold
      value: 38872.90954589844
    - type: manhattan_ap
      value: 98.28694101230218
    - type: manhattan_f1
      value: 94.67815509376584
    - type: manhattan_f1_threshold
      value: 38872.90954589844
    - type: manhattan_precision
      value: 94.24823410696267
    - type: manhattan_recall
      value: 95.11201629327903
    - type: max_accuracy
      value: 92.65578635014838
    - type: max_ap
      value: 98.3834226153613
    - type: max_f1
      value: 94.92567913890312
    - type: max_precision
      value: 95.562435500516
    - type: max_recall
      value: 95.62118126272912
    - type: similarity_accuracy
      value: 92.65578635014838
    - type: similarity_accuracy_threshold
      value: 74.02530312538147
    - type: similarity_ap
      value: 98.3834226153613
    - type: similarity_f1
      value: 94.92567913890312
    - type: similarity_f1_threshold
      value: 74.02530312538147
    - type: similarity_precision
      value: 95.562435500516
    - type: similarity_recall
      value: 94.29735234215886
    task:
      type: PairClassification
  - dataset:
      config: de
      name: MTEB OpusparcusPC (de)
      revision: 9e9b1f8ef51616073f47f306f7f47dd91663f86a
      split: test.full
      type: GEM/opusparcus
    metrics:
    - type: cosine_accuracy
      value: 87.72178850248403
    - type: cosine_accuracy_threshold
      value: 73.33863377571106
    - type: cosine_ap
      value: 96.98901408834976
    - type: cosine_f1
      value: 91.89944134078212
    - type: cosine_f1_threshold
      value: 71.45810127258301
    - type: cosine_precision
      value: 89.64577656675749
    - type: cosine_recall
      value: 94.26934097421203
    - type: dot_accuracy
      value: 86.30234208658624
    - type: dot_accuracy_threshold
      value: 32027.130126953125
    - type: dot_ap
      value: 96.12260574893256
    - type: dot_f1
      value: 91.31602506714414
    - type: dot_f1_threshold
      value: 30804.376220703125
    - type: dot_precision
      value: 85.93091828138164
    - type: dot_recall
      value: 97.42120343839542
    - type: euclidean_accuracy
      value: 87.9347054648687
    - type: euclidean_accuracy_threshold
      value: 1609.6670150756836
    - type: euclidean_ap
      value: 97.00238860358252
    - type: euclidean_f1
      value: 92.1089063221043
    - type: euclidean_f1_threshold
      value: 1641.8487548828125
    - type: euclidean_precision
      value: 89.10714285714286
    - type: euclidean_recall
      value: 95.31996179560649
    - type: main_score
      value: 97.00238860358252
    - type: manhattan_accuracy
      value: 87.72178850248403
    - type: manhattan_accuracy_threshold
      value: 40137.060546875
    - type: manhattan_ap
      value: 96.98653728159941
    - type: manhattan_f1
      value: 92.03865623561896
    - type: manhattan_f1_threshold
      value: 40137.060546875
    - type: manhattan_precision
      value: 88.80994671403198
    - type: manhattan_recall
      value: 95.51098376313276
    - type: max_accuracy
      value: 87.9347054648687
    - type: max_ap
      value: 97.00238860358252
    - type: max_f1
      value: 92.1089063221043
    - type: max_precision
      value: 89.64577656675749
    - type: max_recall
      value: 97.42120343839542
    - type: similarity_accuracy
      value: 87.72178850248403
    - type: similarity_accuracy_threshold
      value: 73.33863377571106
    - type: similarity_ap
      value: 96.98901408834976
    - type: similarity_f1
      value: 91.89944134078212
    - type: similarity_f1_threshold
      value: 71.45810127258301
    - type: similarity_precision
      value: 89.64577656675749
    - type: similarity_recall
      value: 94.26934097421203
    task:
      type: PairClassification
  - dataset:
      config: fr
      name: MTEB OpusparcusPC (fr)
      revision: 9e9b1f8ef51616073f47f306f7f47dd91663f86a
      split: test.full
      type: GEM/opusparcus
    metrics:
    - type: cosine_accuracy
      value: 80.92643051771117
    - type: cosine_accuracy_threshold
      value: 76.68856382369995
    - type: cosine_ap
      value: 93.74622381534307
    - type: cosine_f1
      value: 87.12328767123287
    - type: cosine_f1_threshold
      value: 71.64022922515869
    - type: cosine_precision
      value: 80.64243448858834
    - type: cosine_recall
      value: 94.73684210526315
    - type: dot_accuracy
      value: 80.858310626703
    - type: dot_accuracy_threshold
      value: 34028.3935546875
    - type: dot_ap
      value: 91.18448457633308
    - type: dot_f1
      value: 86.82606657290202
    - type: dot_f1_threshold
      value: 34028.3935546875
    - type: dot_precision
      value: 82.2380106571936
    - type: dot_recall
      value: 91.9563058589871
    - type: euclidean_accuracy
      value: 80.858310626703
    - type: euclidean_accuracy_threshold
      value: 1595.7651138305664
    - type: euclidean_ap
      value: 93.8182717829648
    - type: euclidean_f1
      value: 87.04044117647058
    - type: euclidean_f1_threshold
      value: 1609.2475891113281
    - type: euclidean_precision
      value: 81.00940975192472
    - type: euclidean_recall
      value: 94.04170804369414
    - type: main_score
      value: 93.8182717829648
    - type: manhattan_accuracy
      value: 80.99455040871935
    - type: manhattan_accuracy_threshold
      value: 38092.132568359375
    - type: manhattan_ap
      value: 93.77563401151711
    - type: manhattan_f1
      value: 86.91983122362869
    - type: manhattan_f1_threshold
      value: 38092.132568359375
    - type: manhattan_precision
      value: 82.32682060390763
    - type: manhattan_recall
      value: 92.05561072492551
    - type: max_accuracy
      value: 80.99455040871935
    - type: max_ap
      value: 93.8182717829648
    - type: max_f1
      value: 87.12328767123287
    - type: max_precision
      value: 82.32682060390763
    - type: max_recall
      value: 94.73684210526315
    - type: similarity_accuracy
      value: 80.92643051771117
    - type: similarity_accuracy_threshold
      value: 76.68856382369995
    - type: similarity_ap
      value: 93.74622381534307
    - type: similarity_f1
      value: 87.12328767123287
    - type: similarity_f1_threshold
      value: 71.64022922515869
    - type: similarity_precision
      value: 80.64243448858834
    - type: similarity_recall
      value: 94.73684210526315
    task:
      type: PairClassification
  - dataset:
      config: ru
      name: MTEB OpusparcusPC (ru)
      revision: 9e9b1f8ef51616073f47f306f7f47dd91663f86a
      split: test.full
      type: GEM/opusparcus
    metrics:
    - type: cosine_accuracy
      value: 76.83823529411765
    - type: cosine_accuracy_threshold
      value: 72.70769476890564
    - type: cosine_ap
      value: 89.56692049908222
    - type: cosine_f1
      value: 83.99832003359934
    - type: cosine_f1_threshold
      value: 70.9052324295044
    - type: cosine_precision
      value: 76.16146230007617
    - type: cosine_recall
      value: 93.63295880149812
    - type: dot_accuracy
      value: 76.28676470588235
    - type: dot_accuracy_threshold
      value: 33740.68908691406
    - type: dot_ap
      value: 87.77185177141567
    - type: dot_f1
      value: 83.62251375370292
    - type: dot_f1_threshold
      value: 32726.611328125
    - type: dot_precision
      value: 76.29343629343629
    - type: dot_recall
      value: 92.50936329588015
    - type: euclidean_accuracy
      value: 77.32843137254902
    - type: euclidean_accuracy_threshold
      value: 1566.510009765625
    - type: euclidean_ap
      value: 89.60605626791111
    - type: euclidean_f1
      value: 84.06546080964686
    - type: euclidean_f1_threshold
      value: 1576.4202117919922
    - type: euclidean_precision
      value: 77.83094098883574
    - type: euclidean_recall
      value: 91.38576779026218
    - type: main_score
      value: 89.60605626791111
    - type: manhattan_accuracy
      value: 76.89950980392157
    - type: manhattan_accuracy_threshold
      value: 38202.215576171875
    - type: manhattan_ap
      value: 89.55766894104868
    - type: manhattan_f1
      value: 83.80462724935732
    - type: manhattan_f1_threshold
      value: 38934.375
    - type: manhattan_precision
      value: 77.25118483412322
    - type: manhattan_recall
      value: 91.57303370786516
    - type: max_accuracy
      value: 77.32843137254902
    - type: max_ap
      value: 89.60605626791111
    - type: max_f1
      value: 84.06546080964686
    - type: max_precision
      value: 77.83094098883574
    - type: max_recall
      value: 93.63295880149812
    - type: similarity_accuracy
      value: 76.83823529411765
    - type: similarity_accuracy_threshold
      value: 72.70769476890564
    - type: similarity_ap
      value: 89.56692049908222
    - type: similarity_f1
      value: 83.99832003359934
    - type: similarity_f1_threshold
      value: 70.9052324295044
    - type: similarity_precision
      value: 76.16146230007617
    - type: similarity_recall
      value: 93.63295880149812
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB PAC (default)
      revision: fc69d1c153a8ccdcf1eef52f4e2a27f88782f543
      split: test
      type: laugustyniak/abusive-clauses-pl
    metrics:
    - type: accuracy
      value: 68.39559803069794
    - type: ap
      value: 77.68074206719457
    - type: ap_weighted
      value: 77.68074206719457
    - type: f1
      value: 66.23485605467732
    - type: f1_weighted
      value: 69.03201442129347
    - type: main_score
      value: 68.39559803069794
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB PAWSX (default)
      revision: 9c6a90e430ac22b5779fb019a23e820b11a8b5e1
      split: test
      type: C-MTEB/PAWSX
    metrics:
    - type: cosine_pearson
      value: 13.161523266433587
    - type: cosine_spearman
      value: 15.557333873773386
    - type: euclidean_pearson
      value: 17.147508431907525
    - type: euclidean_spearman
      value: 15.664112857732146
    - type: main_score
      value: 15.557333873773386
    - type: manhattan_pearson
      value: 17.130875906264386
    - type: manhattan_spearman
      value: 15.624397342229637
    - type: pearson
      value: 13.161523266433587
    - type: spearman
      value: 15.557333873773386
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB PSC (default)
      revision: d05a294af9e1d3ff2bfb6b714e08a24a6cabc669
      split: test
      type: PL-MTEB/psc-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 97.86641929499072
    - type: cosine_accuracy_threshold
      value: 79.0391206741333
    - type: cosine_ap
      value: 99.19403807771533
    - type: cosine_f1
      value: 96.45608628659475
    - type: cosine_f1_threshold
      value: 79.0391206741333
    - type: cosine_precision
      value: 97.50778816199377
    - type: cosine_recall
      value: 95.42682926829268
    - type: dot_accuracy
      value: 98.14471243042672
    - type: dot_accuracy_threshold
      value: 29808.1787109375
    - type: dot_ap
      value: 99.331999859971
    - type: dot_f1
      value: 97.01492537313433
    - type: dot_f1_threshold
      value: 29808.1787109375
    - type: dot_precision
      value: 95.02923976608187
    - type: dot_recall
      value: 99.08536585365853
    - type: euclidean_accuracy
      value: 97.49536178107606
    - type: euclidean_accuracy_threshold
      value: 1276.227855682373
    - type: euclidean_ap
      value: 98.91056467717377
    - type: euclidean_f1
      value: 95.83975346687212
    - type: euclidean_f1_threshold
      value: 1276.227855682373
    - type: euclidean_precision
      value: 96.88473520249221
    - type: euclidean_recall
      value: 94.8170731707317
    - type: main_score
      value: 99.331999859971
    - type: manhattan_accuracy
      value: 97.49536178107606
    - type: manhattan_accuracy_threshold
      value: 31097.674560546875
    - type: manhattan_ap
      value: 98.95694691792707
    - type: manhattan_f1
      value: 95.83975346687212
    - type: manhattan_f1_threshold
      value: 31097.674560546875
    - type: manhattan_precision
      value: 96.88473520249221
    - type: manhattan_recall
      value: 94.8170731707317
    - type: max_accuracy
      value: 98.14471243042672
    - type: max_ap
      value: 99.331999859971
    - type: max_f1
      value: 97.01492537313433
    - type: max_precision
      value: 97.50778816199377
    - type: max_recall
      value: 99.08536585365853
    - type: similarity_accuracy
      value: 97.86641929499072
    - type: similarity_accuracy_threshold
      value: 79.0391206741333
    - type: similarity_ap
      value: 99.19403807771533
    - type: similarity_f1
      value: 96.45608628659475
    - type: similarity_f1_threshold
      value: 79.0391206741333
    - type: similarity_precision
      value: 97.50778816199377
    - type: similarity_recall
      value: 95.42682926829268
    task:
      type: PairClassification
  - dataset:
      config: en
      name: MTEB PawsXPairClassification (en)
      revision: 8a04d940a42cd40658986fdd8e3da561533a3646
      split: test
      type: google-research-datasets/paws-x
    metrics:
    - type: cosine_accuracy
      value: 61.8
    - type: cosine_accuracy_threshold
      value: 99.5664119720459
    - type: cosine_ap
      value: 60.679317786040585
    - type: cosine_f1
      value: 63.17354143441101
    - type: cosine_f1_threshold
      value: 97.22164869308472
    - type: cosine_precision
      value: 47.6457399103139
    - type: cosine_recall
      value: 93.71554575523705
    - type: dot_accuracy
      value: 55.7
    - type: dot_accuracy_threshold
      value: 48353.62548828125
    - type: dot_ap
      value: 48.53805970536875
    - type: dot_f1
      value: 62.42214532871972
    - type: dot_f1_threshold
      value: 38215.53955078125
    - type: dot_precision
      value: 45.48663640948058
    - type: dot_recall
      value: 99.44873208379272
    - type: euclidean_accuracy
      value: 61.75000000000001
    - type: euclidean_accuracy_threshold
      value: 189.0761137008667
    - type: euclidean_ap
      value: 60.55517418691518
    - type: euclidean_f1
      value: 63.07977736549165
    - type: euclidean_f1_threshold
      value: 504.3168067932129
    - type: euclidean_precision
      value: 47.53914988814318
    - type: euclidean_recall
      value: 93.71554575523705
    - type: main_score
      value: 60.679317786040585
    - type: manhattan_accuracy
      value: 61.9
    - type: manhattan_accuracy_threshold
      value: 4695.778274536133
    - type: manhattan_ap
      value: 60.48686620413608
    - type: manhattan_f1
      value: 62.92880855772778
    - type: manhattan_f1_threshold
      value: 12542.36831665039
    - type: manhattan_precision
      value: 47.28381374722838
    - type: manhattan_recall
      value: 94.04630650496141
    - type: max_accuracy
      value: 61.9
    - type: max_ap
      value: 60.679317786040585
    - type: max_f1
      value: 63.17354143441101
    - type: max_precision
      value: 47.6457399103139
    - type: max_recall
      value: 99.44873208379272
    - type: similarity_accuracy
      value: 61.8
    - type: similarity_accuracy_threshold
      value: 99.5664119720459
    - type: similarity_ap
      value: 60.679317786040585
    - type: similarity_f1
      value: 63.17354143441101
    - type: similarity_f1_threshold
      value: 97.22164869308472
    - type: similarity_precision
      value: 47.6457399103139
    - type: similarity_recall
      value: 93.71554575523705
    task:
      type: PairClassification
  - dataset:
      config: de
      name: MTEB PawsXPairClassification (de)
      revision: 8a04d940a42cd40658986fdd8e3da561533a3646
      split: test
      type: google-research-datasets/paws-x
    metrics:
    - type: cosine_accuracy
      value: 60.25
    - type: cosine_accuracy_threshold
      value: 99.54338073730469
    - type: cosine_ap
      value: 56.7863613689054
    - type: cosine_f1
      value: 62.23499820337766
    - type: cosine_f1_threshold
      value: 89.95014429092407
    - type: cosine_precision
      value: 45.86864406779661
    - type: cosine_recall
      value: 96.75977653631284
    - type: dot_accuracy
      value: 56.8
    - type: dot_accuracy_threshold
      value: 47349.78332519531
    - type: dot_ap
      value: 49.7857806061729
    - type: dot_f1
      value: 62.31225986727209
    - type: dot_f1_threshold
      value: 30143.206787109375
    - type: dot_precision
      value: 45.32520325203252
    - type: dot_recall
      value: 99.66480446927373
    - type: euclidean_accuracy
      value: 60.3
    - type: euclidean_accuracy_threshold
      value: 219.78106498718262
    - type: euclidean_ap
      value: 56.731544327179606
    - type: euclidean_f1
      value: 62.19895287958115
    - type: euclidean_f1_threshold
      value: 1792.1623229980469
    - type: euclidean_precision
      value: 45.22842639593909
    - type: euclidean_recall
      value: 99.55307262569832
    - type: main_score
      value: 56.7863613689054
    - type: manhattan_accuracy
      value: 60.150000000000006
    - type: manhattan_accuracy_threshold
      value: 5104.503631591797
    - type: manhattan_ap
      value: 56.70304479768734
    - type: manhattan_f1
      value: 62.22067039106145
    - type: manhattan_f1_threshold
      value: 42839.471435546875
    - type: manhattan_precision
      value: 45.2513966480447
    - type: manhattan_recall
      value: 99.55307262569832
    - type: max_accuracy
      value: 60.3
    - type: max_ap
      value: 56.7863613689054
    - type: max_f1
      value: 62.31225986727209
    - type: max_precision
      value: 45.86864406779661
    - type: max_recall
      value: 99.66480446927373
    - type: similarity_accuracy
      value: 60.25
    - type: similarity_accuracy_threshold
      value: 99.54338073730469
    - type: similarity_ap
      value: 56.7863613689054
    - type: similarity_f1
      value: 62.23499820337766
    - type: similarity_f1_threshold
      value: 89.95014429092407
    - type: similarity_precision
      value: 45.86864406779661
    - type: similarity_recall
      value: 96.75977653631284
    task:
      type: PairClassification
  - dataset:
      config: es
      name: MTEB PawsXPairClassification (es)
      revision: 8a04d940a42cd40658986fdd8e3da561533a3646
      split: test
      type: google-research-datasets/paws-x
    metrics:
    - type: cosine_accuracy
      value: 59.699999999999996
    - type: cosine_accuracy_threshold
      value: 99.55930709838867
    - type: cosine_ap
      value: 57.31662248806265
    - type: cosine_f1
      value: 62.444061962134256
    - type: cosine_f1_threshold
      value: 74.75898265838623
    - type: cosine_precision
      value: 45.3953953953954
    - type: cosine_recall
      value: 100.0
    - type: dot_accuracy
      value: 55.900000000000006
    - type: dot_accuracy_threshold
      value: 47512.90283203125
    - type: dot_ap
      value: 49.39339147787568
    - type: dot_f1
      value: 62.487082328625554
    - type: dot_f1_threshold
      value: 34989.03503417969
    - type: dot_precision
      value: 45.44088176352705
    - type: dot_recall
      value: 100.0
    - type: euclidean_accuracy
      value: 59.599999999999994
    - type: euclidean_accuracy_threshold
      value: 200.82547664642334
    - type: euclidean_ap
      value: 57.19737488445163
    - type: euclidean_f1
      value: 62.444061962134256
    - type: euclidean_f1_threshold
      value: 1538.8837814331055
    - type: euclidean_precision
      value: 45.3953953953954
    - type: euclidean_recall
      value: 100.0
    - type: main_score
      value: 57.31662248806265
    - type: manhattan_accuracy
      value: 59.550000000000004
    - type: manhattan_accuracy_threshold
      value: 5016.501617431641
    - type: manhattan_ap
      value: 57.089959907945065
    - type: manhattan_f1
      value: 62.444061962134256
    - type: manhattan_f1_threshold
      value: 37523.53515625
    - type: manhattan_precision
      value: 45.3953953953954
    - type: manhattan_recall
      value: 100.0
    - type: max_accuracy
      value: 59.699999999999996
    - type: max_ap
      value: 57.31662248806265
    - type: max_f1
      value: 62.487082328625554
    - type: max_precision
      value: 45.44088176352705
    - type: max_recall
      value: 100.0
    - type: similarity_accuracy
      value: 59.699999999999996
    - type: similarity_accuracy_threshold
      value: 99.55930709838867
    - type: similarity_ap
      value: 57.31662248806265
    - type: similarity_f1
      value: 62.444061962134256
    - type: similarity_f1_threshold
      value: 74.75898265838623
    - type: similarity_precision
      value: 45.3953953953954
    - type: similarity_recall
      value: 100.0
    task:
      type: PairClassification
  - dataset:
      config: fr
      name: MTEB PawsXPairClassification (fr)
      revision: 8a04d940a42cd40658986fdd8e3da561533a3646
      split: test
      type: google-research-datasets/paws-x
    metrics:
    - type: cosine_accuracy
      value: 61.150000000000006
    - type: cosine_accuracy_threshold
      value: 99.36153888702393
    - type: cosine_ap
      value: 59.43845317938599
    - type: cosine_f1
      value: 62.51298026998961
    - type: cosine_f1_threshold
      value: 76.77866220474243
    - type: cosine_precision
      value: 45.468277945619334
    - type: cosine_recall
      value: 100.0
    - type: dot_accuracy
      value: 55.75
    - type: dot_accuracy_threshold
      value: 48931.55212402344
    - type: dot_ap
      value: 50.15949290538757
    - type: dot_f1
      value: 62.53462603878117
    - type: dot_f1_threshold
      value: 34415.7958984375
    - type: dot_precision
      value: 45.4911838790932
    - type: dot_recall
      value: 100.0
    - type: euclidean_accuracy
      value: 61.050000000000004
    - type: euclidean_accuracy_threshold
      value: 240.8097267150879
    - type: euclidean_ap
      value: 59.367971294226216
    - type: euclidean_f1
      value: 62.51298026998961
    - type: euclidean_f1_threshold
      value: 1444.132423400879
    - type: euclidean_precision
      value: 45.468277945619334
    - type: euclidean_recall
      value: 100.0
    - type: main_score
      value: 59.43845317938599
    - type: manhattan_accuracy
      value: 60.95
    - type: manhattan_accuracy_threshold
      value: 5701.206207275391
    - type: manhattan_ap
      value: 59.30094096378774
    - type: manhattan_f1
      value: 62.53462603878117
    - type: manhattan_f1_threshold
      value: 33445.672607421875
    - type: manhattan_precision
      value: 45.4911838790932
    - type: manhattan_recall
      value: 100.0
    - type: max_accuracy
      value: 61.150000000000006
    - type: max_ap
      value: 59.43845317938599
    - type: max_f1
      value: 62.53462603878117
    - type: max_precision
      value: 45.4911838790932
    - type: max_recall
      value: 100.0
    - type: similarity_accuracy
      value: 61.150000000000006
    - type: similarity_accuracy_threshold
      value: 99.36153888702393
    - type: similarity_ap
      value: 59.43845317938599
    - type: similarity_f1
      value: 62.51298026998961
    - type: similarity_f1_threshold
      value: 76.77866220474243
    - type: similarity_precision
      value: 45.468277945619334
    - type: similarity_recall
      value: 100.0
    task:
      type: PairClassification
  - dataset:
      config: zh
      name: MTEB PawsXPairClassification (zh)
      revision: 8a04d940a42cd40658986fdd8e3da561533a3646
      split: test
      type: google-research-datasets/paws-x
    metrics:
    - type: cosine_accuracy
      value: 58.85
    - type: cosine_accuracy_threshold
      value: 99.73838329315186
    - type: cosine_ap
      value: 54.66913160570546
    - type: cosine_f1
      value: 62.32136632973162
    - type: cosine_f1_threshold
      value: 76.4499306678772
    - type: cosine_precision
      value: 45.265822784810126
    - type: cosine_recall
      value: 100.0
    - type: dot_accuracy
      value: 56.25
    - type: dot_accuracy_threshold
      value: 47351.9287109375
    - type: dot_ap
      value: 48.5266232989438
    - type: dot_f1
      value: 62.277951933124356
    - type: dot_f1_threshold
      value: 31325.28076171875
    - type: dot_precision
      value: 45.220030349013655
    - type: dot_recall
      value: 100.0
    - type: euclidean_accuracy
      value: 58.9
    - type: euclidean_accuracy_threshold
      value: 144.24468278884888
    - type: euclidean_ap
      value: 54.66981490353506
    - type: euclidean_f1
      value: 62.32136632973162
    - type: euclidean_f1_threshold
      value: 1484.908676147461
    - type: euclidean_precision
      value: 45.265822784810126
    - type: euclidean_recall
      value: 100.0
    - type: main_score
      value: 54.66981490353506
    - type: manhattan_accuracy
      value: 58.9
    - type: manhattan_accuracy_threshold
      value: 3586.785125732422
    - type: manhattan_ap
      value: 54.668355260247736
    - type: manhattan_f1
      value: 62.32136632973162
    - type: manhattan_f1_threshold
      value: 36031.22863769531
    - type: manhattan_precision
      value: 45.265822784810126
    - type: manhattan_recall
      value: 100.0
    - type: max_accuracy
      value: 58.9
    - type: max_ap
      value: 54.66981490353506
    - type: max_f1
      value: 62.32136632973162
    - type: max_precision
      value: 45.265822784810126
    - type: max_recall
      value: 100.0
    - type: similarity_accuracy
      value: 58.85
    - type: similarity_accuracy_threshold
      value: 99.73838329315186
    - type: similarity_ap
      value: 54.66913160570546
    - type: similarity_f1
      value: 62.32136632973162
    - type: similarity_f1_threshold
      value: 76.4499306678772
    - type: similarity_precision
      value: 45.265822784810126
    - type: similarity_recall
      value: 100.0
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB PolEmo2.0-IN (default)
      revision: d90724373c70959f17d2331ad51fb60c71176b03
      split: test
      type: PL-MTEB/polemo2_in
    metrics:
    - type: accuracy
      value: 83.75346260387812
    - type: f1
      value: 81.98304891214909
    - type: f1_weighted
      value: 84.29623200830078
    - type: main_score
      value: 83.75346260387812
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB PolEmo2.0-OUT (default)
      revision: 6a21ab8716e255ab1867265f8b396105e8aa63d4
      split: test
      type: PL-MTEB/polemo2_out
    metrics:
    - type: accuracy
      value: 66.53846153846153
    - type: f1
      value: 52.71826064368638
    - type: f1_weighted
      value: 69.10010124630334
    - type: main_score
      value: 66.53846153846153
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB PPC
      revision: None
      split: test
      type: PL-MTEB/ppc-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 81.8
    - type: cosine_accuracy_threshold
      value: 90.47793745994568
    - type: cosine_ap
      value: 91.42490266080884
    - type: cosine_f1
      value: 85.4632587859425
    - type: cosine_f1_threshold
      value: 90.47793745994568
    - type: cosine_precision
      value: 82.56172839506173
    - type: cosine_recall
      value: 88.57615894039735
    - type: dot_accuracy
      value: 74.6
    - type: dot_accuracy_threshold
      value: 42102.23693847656
    - type: dot_ap
      value: 86.20060009096979
    - type: dot_f1
      value: 80.02842928216063
    - type: dot_f1_threshold
      value: 38970.16906738281
    - type: dot_precision
      value: 70.1120797011208
    - type: dot_recall
      value: 93.21192052980133
    - type: euclidean_accuracy
      value: 81.5
    - type: euclidean_accuracy_threshold
      value: 880.433464050293
    - type: euclidean_ap
      value: 91.33143477982087
    - type: euclidean_f1
      value: 85.44600938967135
    - type: euclidean_f1_threshold
      value: 964.0384674072266
    - type: euclidean_precision
      value: 81.00890207715133
    - type: euclidean_recall
      value: 90.39735099337747
    - type: main_score
      value: 91.42490266080884
    - type: manhattan_accuracy
      value: 81.3
    - type: manhattan_accuracy_threshold
      value: 22100.830078125
    - type: manhattan_ap
      value: 91.25996158651282
    - type: manhattan_f1
      value: 85.38102643856921
    - type: manhattan_f1_threshold
      value: 24043.515014648438
    - type: manhattan_precision
      value: 80.49853372434018
    - type: manhattan_recall
      value: 90.89403973509934
    - type: max_accuracy
      value: 81.8
    - type: max_ap
      value: 91.42490266080884
    - type: max_f1
      value: 85.4632587859425
    - type: max_precision
      value: 82.56172839506173
    - type: max_recall
      value: 93.21192052980133
    - type: similarity_accuracy
      value: 81.8
    - type: similarity_accuracy_threshold
      value: 90.47793745994568
    - type: similarity_ap
      value: 91.42490266080884
    - type: similarity_f1
      value: 85.4632587859425
    - type: similarity_f1_threshold
      value: 90.47793745994568
    - type: similarity_precision
      value: 82.56172839506173
    - type: similarity_recall
      value: 88.57615894039735
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB QuoraRetrieval (default)
      revision: e4e08e0b7dbe3c8700f0daef558ff32256715259
      split: test
      type: mteb/quora
    metrics:
    - type: map_at_1
      value: 71.419
    - type: map_at_10
      value: 85.542
    - type: map_at_100
      value: 86.161
    - type: map_at_1000
      value: 86.175
    - type: map_at_20
      value: 85.949
    - type: map_at_3
      value: 82.623
    - type: map_at_5
      value: 84.5
    - type: mrr_at_1
      value: 82.27
    - type: mrr_at_10
      value: 88.21900000000001
    - type: mrr_at_100
      value: 88.313
    - type: mrr_at_1000
      value: 88.31400000000001
    - type: mrr_at_20
      value: 88.286
    - type: mrr_at_3
      value: 87.325
    - type: mrr_at_5
      value: 87.97500000000001
    - type: ndcg_at_1
      value: 82.3
    - type: ndcg_at_10
      value: 89.088
    - type: ndcg_at_100
      value: 90.217
    - type: ndcg_at_1000
      value: 90.29700000000001
    - type: ndcg_at_20
      value: 89.697
    - type: ndcg_at_3
      value: 86.435
    - type: ndcg_at_5
      value: 87.966
    - type: precision_at_1
      value: 82.3
    - type: precision_at_10
      value: 13.527000000000001
    - type: precision_at_100
      value: 1.537
    - type: precision_at_1000
      value: 0.157
    - type: precision_at_20
      value: 7.165000000000001
    - type: precision_at_3
      value: 37.92
    - type: precision_at_5
      value: 24.914
    - type: recall_at_1
      value: 71.419
    - type: recall_at_10
      value: 95.831
    - type: recall_at_100
      value: 99.64
    - type: recall_at_1000
      value: 99.988
    - type: recall_at_20
      value: 97.76599999999999
    - type: recall_at_3
      value: 88.081
    - type: recall_at_5
      value: 92.50500000000001
    - type: main_score
      value: 89.088
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB RUParaPhraserSTS (default)
      revision: 43265056790b8f7c59e0139acb4be0a8dad2c8f4
      split: test
      type: merionum/ru_paraphraser
    metrics:
    - type: cosine_pearson
      value: 67.91177744712421
    - type: cosine_spearman
      value: 76.77113726753656
    - type: euclidean_pearson
      value: 73.81454206068638
    - type: euclidean_spearman
      value: 76.92529493599028
    - type: main_score
      value: 76.77113726753656
    - type: manhattan_pearson
      value: 73.81690454439168
    - type: manhattan_spearman
      value: 76.87333776705002
    - type: pearson
      value: 67.91177744712421
    - type: spearman
      value: 76.77113726753656
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB RedditClustering (default)
      revision: 24640382cdbf8abc73003fb0fa6d111a705499eb
      split: test
      type: mteb/reddit-clustering
    metrics:
    - type: main_score
      value: 55.39924225216962
    - type: v_measure
      value: 55.39924225216962
    - type: v_measure_std
      value: 4.723802279292467
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB RedditClusteringP2P (default)
      revision: 385e3cb46b4cfa89021f56c4380204149d0efe33
      split: test
      type: mteb/reddit-clustering-p2p
    metrics:
    - type: main_score
      value: 62.87465161304012
    - type: v_measure
      value: 62.87465161304012
    - type: v_measure_std
      value: 12.082670914488473
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB RiaNewsRetrieval (default)
      revision: 82374b0bbacda6114f39ff9c5b925fa1512ca5d7
      split: test
      type: ai-forever/ria-news-retrieval
    metrics:
    - type: main_score
      value: 79.209
    - type: map_at_1
      value: 67.33
    - type: map_at_10
      value: 75.633
    - type: map_at_100
      value: 75.897
    - type: map_at_1000
      value: 75.907
    - type: map_at_20
      value: 75.804
    - type: map_at_3
      value: 74.2
    - type: map_at_5
      value: 75.13300000000001
    - type: mrr_at_1
      value: 67.31
    - type: mrr_at_10
      value: 75.62709126984095
    - type: mrr_at_100
      value: 75.89105697041113
    - type: mrr_at_1000
      value: 75.90115653883124
    - type: mrr_at_20
      value: 75.79802332308172
    - type: mrr_at_3
      value: 74.19499999999961
    - type: mrr_at_5
      value: 75.12849999999939
    - type: nauc_map_at_1000_diff1
      value: 74.30304869630591
    - type: nauc_map_at_1000_max
      value: 36.477146725784046
    - type: nauc_map_at_1000_std
      value: -20.862772498461723
    - type: nauc_map_at_100_diff1
      value: 74.29833058090355
    - type: nauc_map_at_100_max
      value: 36.483678619667884
    - type: nauc_map_at_100_std
      value: -20.856274849980135
    - type: nauc_map_at_10_diff1
      value: 74.20729220697967
    - type: nauc_map_at_10_max
      value: 36.56543146170092
    - type: nauc_map_at_10_std
      value: -20.991081015484728
    - type: nauc_map_at_1_diff1
      value: 77.38899022125185
    - type: nauc_map_at_1_max
      value: 32.45918619669731
    - type: nauc_map_at_1_std
      value: -22.149586336167324
    - type: nauc_map_at_20_diff1
      value: 74.2447573558587
    - type: nauc_map_at_20_max
      value: 36.50383130240387
    - type: nauc_map_at_20_std
      value: -20.87013743041831
    - type: nauc_map_at_3_diff1
      value: 74.3054577294586
    - type: nauc_map_at_3_max
      value: 36.484530586652724
    - type: nauc_map_at_3_std
      value: -21.90543024607988
    - type: nauc_map_at_5_diff1
      value: 74.21062368961503
    - type: nauc_map_at_5_max
      value: 36.55670532498779
    - type: nauc_map_at_5_std
      value: -21.488786900676942
    - type: nauc_mrr_at_1000_diff1
      value: 74.31619177956684
    - type: nauc_mrr_at_1000_max
      value: 36.53498918453189
    - type: nauc_mrr_at_1000_std
      value: -20.75986704931237
    - type: nauc_mrr_at_100_diff1
      value: 74.31146790382356
    - type: nauc_mrr_at_100_max
      value: 36.54149252857106
    - type: nauc_mrr_at_100_std
      value: -20.75341959250079
    - type: nauc_mrr_at_10_diff1
      value: 74.22027806145095
    - type: nauc_mrr_at_10_max
      value: 36.622542969971725
    - type: nauc_mrr_at_10_std
      value: -20.889417384064117
    - type: nauc_mrr_at_1_diff1
      value: 77.4306709551449
    - type: nauc_mrr_at_1_max
      value: 32.57259463438259
    - type: nauc_mrr_at_1_std
      value: -21.964402859613937
    - type: nauc_mrr_at_20_diff1
      value: 74.25784396230718
    - type: nauc_mrr_at_20_max
      value: 36.561412224507336
    - type: nauc_mrr_at_20_std
      value: -20.767665000065723
    - type: nauc_mrr_at_3_diff1
      value: 74.31423253547214
    - type: nauc_mrr_at_3_max
      value: 36.537745749488906
    - type: nauc_mrr_at_3_std
      value: -21.81259529019546
    - type: nauc_mrr_at_5_diff1
      value: 74.22404613312771
    - type: nauc_mrr_at_5_max
      value: 36.60743768455219
    - type: nauc_mrr_at_5_std
      value: -21.39479216331971
    - type: nauc_ndcg_at_1000_diff1
      value: 73.48182819705742
    - type: nauc_ndcg_at_1000_max
      value: 37.86991608461793
    - type: nauc_ndcg_at_1000_std
      value: -19.021499322688904
    - type: nauc_ndcg_at_100_diff1
      value: 73.34941250585759
    - type: nauc_ndcg_at_100_max
      value: 38.11150275625829
    - type: nauc_ndcg_at_100_std
      value: -18.70624087206104
    - type: nauc_ndcg_at_10_diff1
      value: 72.82520265115987
    - type: nauc_ndcg_at_10_max
      value: 38.43323357650525
    - type: nauc_ndcg_at_10_std
      value: -19.410953792830878
    - type: nauc_ndcg_at_1_diff1
      value: 77.38899022125185
    - type: nauc_ndcg_at_1_max
      value: 32.45918619669731
    - type: nauc_ndcg_at_1_std
      value: -22.149586336167324
    - type: nauc_ndcg_at_20_diff1
      value: 72.93309285256507
    - type: nauc_ndcg_at_20_max
      value: 38.217372819067755
    - type: nauc_ndcg_at_20_std
      value: -18.864113576359333
    - type: nauc_ndcg_at_3_diff1
      value: 73.18253776744112
    - type: nauc_ndcg_at_3_max
      value: 38.008109328364
    - type: nauc_ndcg_at_3_std
      value: -21.68785687594153
    - type: nauc_ndcg_at_5_diff1
      value: 72.90474739784793
    - type: nauc_ndcg_at_5_max
      value: 38.29483039202184
    - type: nauc_ndcg_at_5_std
      value: -20.833049811453474
    - type: nauc_precision_at_1000_diff1
      value: 59.306217613750334
    - type: nauc_precision_at_1000_max
      value: 72.20747948302262
    - type: nauc_precision_at_1000_std
      value: 45.58837180096227
    - type: nauc_precision_at_100_diff1
      value: 62.87286844562389
    - type: nauc_precision_at_100_max
      value: 61.33108214045868
    - type: nauc_precision_at_100_std
      value: 20.67481963545654
    - type: nauc_precision_at_10_diff1
      value: 64.11222984256685
    - type: nauc_precision_at_10_max
      value: 50.323697746037496
    - type: nauc_precision_at_10_std
      value: -7.9994544634332625
    - type: nauc_precision_at_1_diff1
      value: 77.38899022125185
    - type: nauc_precision_at_1_max
      value: 32.45918619669731
    - type: nauc_precision_at_1_std
      value: -22.149586336167324
    - type: nauc_precision_at_20_diff1
      value: 62.30228127286973
    - type: nauc_precision_at_20_max
      value: 52.02090746208407
    - type: nauc_precision_at_20_std
      value: 0.7629898806370331
    - type: nauc_precision_at_3_diff1
      value: 68.82856645994157
    - type: nauc_precision_at_3_max
      value: 43.94171571306625
    - type: nauc_precision_at_3_std
      value: -20.78595255410148
    - type: nauc_precision_at_5_diff1
      value: 66.62157622497887
    - type: nauc_precision_at_5_max
      value: 46.69398173603811
    - type: nauc_precision_at_5_std
      value: -17.412423571163057
    - type: nauc_recall_at_1000_diff1
      value: 59.30621761375148
    - type: nauc_recall_at_1000_max
      value: 72.20747948302191
    - type: nauc_recall_at_1000_std
      value: 45.588371800962655
    - type: nauc_recall_at_100_diff1
      value: 62.872868445623894
    - type: nauc_recall_at_100_max
      value: 61.33108214045813
    - type: nauc_recall_at_100_std
      value: 20.67481963545666
    - type: nauc_recall_at_10_diff1
      value: 64.11222984256698
    - type: nauc_recall_at_10_max
      value: 50.32369774603755
    - type: nauc_recall_at_10_std
      value: -7.999454463433321
    - type: nauc_recall_at_1_diff1
      value: 77.38899022125185
    - type: nauc_recall_at_1_max
      value: 32.45918619669731
    - type: nauc_recall_at_1_std
      value: -22.149586336167324
    - type: nauc_recall_at_20_diff1
      value: 62.3022812728695
    - type: nauc_recall_at_20_max
      value: 52.02090746208397
    - type: nauc_recall_at_20_std
      value: 0.7629898806369458
    - type: nauc_recall_at_3_diff1
      value: 68.82856645994157
    - type: nauc_recall_at_3_max
      value: 43.94171571306612
    - type: nauc_recall_at_3_std
      value: -20.78595255410157
    - type: nauc_recall_at_5_diff1
      value: 66.62157622497897
    - type: nauc_recall_at_5_max
      value: 46.693981736038246
    - type: nauc_recall_at_5_std
      value: -17.412423571162954
    - type: ndcg_at_1
      value: 67.33
    - type: ndcg_at_10
      value: 79.209
    - type: ndcg_at_100
      value: 80.463
    - type: ndcg_at_1000
      value: 80.74799999999999
    - type: ndcg_at_20
      value: 79.81899999999999
    - type: ndcg_at_3
      value: 76.335
    - type: ndcg_at_5
      value: 78.011
    - type: precision_at_1
      value: 67.33
    - type: precision_at_10
      value: 9.020999999999999
    - type: precision_at_100
      value: 0.96
    - type: precision_at_1000
      value: 0.098
    - type: precision_at_20
      value: 4.63
    - type: precision_at_3
      value: 27.493000000000002
    - type: precision_at_5
      value: 17.308
    - type: recall_at_1
      value: 67.33
    - type: recall_at_10
      value: 90.21000000000001
    - type: recall_at_100
      value: 96.00999999999999
    - type: recall_at_1000
      value: 98.29
    - type: recall_at_20
      value: 92.60000000000001
    - type: recall_at_3
      value: 82.48
    - type: recall_at_5
      value: 86.53999999999999
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB RuBQReranking (default)
      revision: 2e96b8f098fa4b0950fc58eacadeb31c0d0c7fa2
      split: test
      type: ai-forever/rubq-reranking
    metrics:
    - type: main_score
      value: 65.57453932493252
    - type: map
      value: 65.57453932493252
    - type: mrr
      value: 70.51408205663526
    - type: nAUC_map_diff1
      value: 26.69583260609023
    - type: nAUC_map_max
      value: 12.928262749610663
    - type: nAUC_map_std
      value: 11.702468857903128
    - type: nAUC_mrr_diff1
      value: 28.5206955462174
    - type: nAUC_mrr_max
      value: 14.207162454694227
    - type: nAUC_mrr_std
      value: 10.725721001555296
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB RuBQRetrieval (default)
      revision: e19b6ffa60b3bc248e0b41f4cc37c26a55c2a67b
      split: test
      type: ai-forever/rubq-retrieval
    metrics:
    - type: main_score
      value: 72.306
    - type: map_at_1
      value: 44.187
    - type: map_at_10
      value: 64.836
    - type: map_at_100
      value: 65.771
    - type: map_at_1000
      value: 65.8
    - type: map_at_20
      value: 65.497
    - type: map_at_3
      value: 59.692
    - type: map_at_5
      value: 63.105
    - type: mrr_at_1
      value: 62.23404255319149
    - type: mrr_at_10
      value: 73.40810161732159
    - type: mrr_at_100
      value: 73.67949305473395
    - type: mrr_at_1000
      value: 73.68707852294746
    - type: mrr_at_20
      value: 73.60429051697479
    - type: mrr_at_3
      value: 71.47360126083535
    - type: mrr_at_5
      value: 72.8447596532704
    - type: nauc_map_at_1000_diff1
      value: 39.838449035736886
    - type: nauc_map_at_1000_max
      value: 32.29962306877408
    - type: nauc_map_at_1000_std
      value: -6.324859592714388
    - type: nauc_map_at_100_diff1
      value: 39.824361938745426
    - type: nauc_map_at_100_max
      value: 32.32055222704763
    - type: nauc_map_at_100_std
      value: -6.301641111869559
    - type: nauc_map_at_10_diff1
      value: 39.50155328718487
    - type: nauc_map_at_10_max
      value: 31.745730244960672
    - type: nauc_map_at_10_std
      value: -6.867215137329693
    - type: nauc_map_at_1_diff1
      value: 47.66181128677822
    - type: nauc_map_at_1_max
      value: 21.75204233166764
    - type: nauc_map_at_1_std
      value: -8.06951079061697
    - type: nauc_map_at_20_diff1
      value: 39.78364637902108
    - type: nauc_map_at_20_max
      value: 32.39065528029405
    - type: nauc_map_at_20_std
      value: -6.368994332729006
    - type: nauc_map_at_3_diff1
      value: 39.51829474433183
    - type: nauc_map_at_3_max
      value: 28.633292697821673
    - type: nauc_map_at_3_std
      value: -7.2561170814963925
    - type: nauc_map_at_5_diff1
      value: 39.288433237676266
    - type: nauc_map_at_5_max
      value: 31.007702201615515
    - type: nauc_map_at_5_std
      value: -7.235131195162474
    - type: nauc_mrr_at_1000_diff1
      value: 49.599102391215226
    - type: nauc_mrr_at_1000_max
      value: 38.25521825911133
    - type: nauc_mrr_at_1000_std
      value: -10.448180939809435
    - type: nauc_mrr_at_100_diff1
      value: 49.5957067716212
    - type: nauc_mrr_at_100_max
      value: 38.26760703964535
    - type: nauc_mrr_at_100_std
      value: -10.438443051971081
    - type: nauc_mrr_at_10_diff1
      value: 49.35269710190271
    - type: nauc_mrr_at_10_max
      value: 38.43782589127069
    - type: nauc_mrr_at_10_std
      value: -10.404402063509815
    - type: nauc_mrr_at_1_diff1
      value: 53.32206103688421
    - type: nauc_mrr_at_1_max
      value: 33.52402390241035
    - type: nauc_mrr_at_1_std
      value: -12.73473393949936
    - type: nauc_mrr_at_20_diff1
      value: 49.550630850826636
    - type: nauc_mrr_at_20_max
      value: 38.35964703941151
    - type: nauc_mrr_at_20_std
      value: -10.444577766284766
    - type: nauc_mrr_at_3_diff1
      value: 49.12029127633829
    - type: nauc_mrr_at_3_max
      value: 38.01631275124067
    - type: nauc_mrr_at_3_std
      value: -10.523724301481309
    - type: nauc_mrr_at_5_diff1
      value: 49.04606949432458
    - type: nauc_mrr_at_5_max
      value: 38.33647550077891
    - type: nauc_mrr_at_5_std
      value: -10.47076409263114
    - type: nauc_ndcg_at_1000_diff1
      value: 41.342785916264226
    - type: nauc_ndcg_at_1000_max
      value: 35.75731064862711
    - type: nauc_ndcg_at_1000_std
      value: -5.45573422899229
    - type: nauc_ndcg_at_100_diff1
      value: 40.972974559636086
    - type: nauc_ndcg_at_100_max
      value: 36.32938573321036
    - type: nauc_ndcg_at_100_std
      value: -4.749631537590004
    - type: nauc_ndcg_at_10_diff1
      value: 39.67813474464166
    - type: nauc_ndcg_at_10_max
      value: 35.480200504848966
    - type: nauc_ndcg_at_10_std
      value: -6.318561293935512
    - type: nauc_ndcg_at_1_diff1
      value: 53.45970160222764
    - type: nauc_ndcg_at_1_max
      value: 33.14759013278075
    - type: nauc_ndcg_at_1_std
      value: -12.579833891774847
    - type: nauc_ndcg_at_20_diff1
      value: 40.67492861219249
    - type: nauc_ndcg_at_20_max
      value: 36.84960799838019
    - type: nauc_ndcg_at_20_std
      value: -5.202530835850179
    - type: nauc_ndcg_at_3_diff1
      value: 39.574906207408844
    - type: nauc_ndcg_at_3_max
      value: 31.76512164509258
    - type: nauc_ndcg_at_3_std
      value: -7.656143208565999
    - type: nauc_ndcg_at_5_diff1
      value: 39.096348529742095
    - type: nauc_ndcg_at_5_max
      value: 34.075926475544165
    - type: nauc_ndcg_at_5_std
      value: -7.238045445366631
    - type: nauc_precision_at_1000_diff1
      value: -14.283799754212609
    - type: nauc_precision_at_1000_max
      value: 6.449741756717101
    - type: nauc_precision_at_1000_std
      value: 4.862828679759048
    - type: nauc_precision_at_100_diff1
      value: -13.23173132700258
    - type: nauc_precision_at_100_max
      value: 11.058898534529195
    - type: nauc_precision_at_100_std
      value: 7.343683941814956
    - type: nauc_precision_at_10_diff1
      value: -7.202951643546464
    - type: nauc_precision_at_10_max
      value: 17.499446869433278
    - type: nauc_precision_at_10_std
      value: 2.8367985220406307
    - type: nauc_precision_at_1_diff1
      value: 53.45970160222764
    - type: nauc_precision_at_1_max
      value: 33.14759013278075
    - type: nauc_precision_at_1_std
      value: -12.579833891774847
    - type: nauc_precision_at_20_diff1
      value: -9.477122699154124
    - type: nauc_precision_at_20_max
      value: 16.80556031564312
    - type: nauc_precision_at_20_std
      value: 6.420218284416923
    - type: nauc_precision_at_3_diff1
      value: 5.5276143574150245
    - type: nauc_precision_at_3_max
      value: 23.65952688481666
    - type: nauc_precision_at_3_std
      value: -1.8730348729295785
    - type: nauc_precision_at_5_diff1
      value: -2.4537029093721308
    - type: nauc_precision_at_5_max
      value: 21.41469327545133
    - type: nauc_precision_at_5_std
      value: 0.1543890645722277
    - type: nauc_recall_at_1000_diff1
      value: -1.7474947956413491
    - type: nauc_recall_at_1000_max
      value: 46.22670991970479
    - type: nauc_recall_at_1000_std
      value: 62.582840705588794
    - type: nauc_recall_at_100_diff1
      value: 16.116089801097345
    - type: nauc_recall_at_100_max
      value: 52.54794580975103
    - type: nauc_recall_at_100_std
      value: 33.720245696003246
    - type: nauc_recall_at_10_diff1
      value: 23.134924318655482
    - type: nauc_recall_at_10_max
      value: 38.73754275649077
    - type: nauc_recall_at_10_std
      value: 0.6137471711639239
    - type: nauc_recall_at_1_diff1
      value: 47.66181128677822
    - type: nauc_recall_at_1_max
      value: 21.75204233166764
    - type: nauc_recall_at_1_std
      value: -8.06951079061697
    - type: nauc_recall_at_20_diff1
      value: 24.130616271355017
    - type: nauc_recall_at_20_max
      value: 48.306178640146136
    - type: nauc_recall_at_20_std
      value: 9.290819557000022
    - type: nauc_recall_at_3_diff1
      value: 29.767415016250226
    - type: nauc_recall_at_3_max
      value: 28.54289782140701
    - type: nauc_recall_at_3_std
      value: -5.1395675072005576
    - type: nauc_recall_at_5_diff1
      value: 25.410613126870174
    - type: nauc_recall_at_5_max
      value: 33.24658754857624
    - type: nauc_recall_at_5_std
      value: -4.211226036746632
    - type: ndcg_at_1
      value: 62.175000000000004
    - type: ndcg_at_10
      value: 72.306
    - type: ndcg_at_100
      value: 75.074
    - type: ndcg_at_1000
      value: 75.581
    - type: ndcg_at_20
      value: 73.875
    - type: ndcg_at_3
      value: 65.641
    - type: ndcg_at_5
      value: 69.48299999999999
    - type: precision_at_1
      value: 62.175000000000004
    - type: precision_at_10
      value: 13.907
    - type: precision_at_100
      value: 1.591
    - type: precision_at_1000
      value: 0.166
    - type: precision_at_20
      value: 7.446999999999999
    - type: precision_at_3
      value: 35.619
    - type: precision_at_5
      value: 24.917
    - type: recall_at_1
      value: 44.187
    - type: recall_at_10
      value: 85.10600000000001
    - type: recall_at_100
      value: 95.488
    - type: recall_at_1000
      value: 98.831
    - type: recall_at_20
      value: 90.22200000000001
    - type: recall_at_3
      value: 68.789
    - type: recall_at_5
      value: 77.85499999999999
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB RuReviewsClassification (default)
      revision: f6d2c31f4dc6b88f468552750bfec05b4b41b05a
      split: test
      type: ai-forever/ru-reviews-classification
    metrics:
    - type: accuracy
      value: 67.5830078125
    - type: f1
      value: 67.56931936632446
    - type: f1_weighted
      value: 67.57137733752779
    - type: main_score
      value: 67.5830078125
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB RuSTSBenchmarkSTS (default)
      revision: 7cf24f325c6da6195df55bef3d86b5e0616f3018
      split: test
      type: ai-forever/ru-stsbenchmark-sts
    metrics:
    - type: cosine_pearson
      value: 85.90493484626788
    - type: cosine_spearman
      value: 86.21965691667411
    - type: euclidean_pearson
      value: 86.07499842984909
    - type: euclidean_spearman
      value: 86.55506818735688
    - type: main_score
      value: 86.21965691667411
    - type: manhattan_pearson
      value: 85.95976420231729
    - type: manhattan_spearman
      value: 86.48604243661234
    - type: pearson
      value: 85.90493484626788
    - type: spearman
      value: 86.21965691667411
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB RuSciBenchGRNTIClassification (default)
      revision: 673a610d6d3dd91a547a0d57ae1b56f37ebbf6a1
      split: test
      type: ai-forever/ru-scibench-grnti-classification
    metrics:
    - type: accuracy
      value: 59.1943359375
    - type: f1
      value: 58.894480861440414
    - type: f1_weighted
      value: 58.903615560240866
    - type: main_score
      value: 59.1943359375
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB RuSciBenchGRNTIClusteringP2P (default)
      revision: 673a610d6d3dd91a547a0d57ae1b56f37ebbf6a1
      split: test
      type: ai-forever/ru-scibench-grnti-classification
    metrics:
    - type: main_score
      value: 57.99209448663228
    - type: v_measure
      value: 57.99209448663228
    - type: v_measure_std
      value: 1.0381163861993816
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB RuSciBenchOECDClassification (default)
      revision: 26c88e99dcaba32bb45d0e1bfc21902337f6d471
      split: test
      type: ai-forever/ru-scibench-oecd-classification
    metrics:
    - type: accuracy
      value: 45.556640625
    - type: f1
      value: 45.159163104085906
    - type: f1_weighted
      value: 45.16098316398626
    - type: main_score
      value: 45.556640625
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB RuSciBenchOECDClusteringP2P (default)
      revision: 26c88e99dcaba32bb45d0e1bfc21902337f6d471
      split: test
      type: ai-forever/ru-scibench-oecd-classification
    metrics:
    - type: main_score
      value: 50.787548070488974
    - type: v_measure
      value: 50.787548070488974
    - type: v_measure_std
      value: 0.8569958168946827
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB SCIDOCS (default)
      revision: f8c2fcf00f625baaa80f62ec5bd9e1fff3b8ae88
      split: test
      type: mteb/scidocs
    metrics:
    - type: map_at_1
      value: 4.843
    - type: map_at_10
      value: 11.752
    - type: map_at_100
      value: 13.919
    - type: map_at_1000
      value: 14.198
    - type: map_at_20
      value: 12.898000000000001
    - type: map_at_3
      value: 8.603
    - type: map_at_5
      value: 10.069
    - type: mrr_at_1
      value: 23.799999999999997
    - type: mrr_at_10
      value: 34.449999999999996
    - type: mrr_at_100
      value: 35.64
    - type: mrr_at_1000
      value: 35.691
    - type: mrr_at_20
      value: 35.213
    - type: mrr_at_3
      value: 31.383
    - type: mrr_at_5
      value: 33.062999999999995
    - type: ndcg_at_1
      value: 23.799999999999997
    - type: ndcg_at_10
      value: 19.811
    - type: ndcg_at_100
      value: 28.108
    - type: ndcg_at_1000
      value: 33.1
    - type: ndcg_at_20
      value: 22.980999999999998
    - type: ndcg_at_3
      value: 19.153000000000002
    - type: ndcg_at_5
      value: 16.408
    - type: precision_at_1
      value: 23.799999999999997
    - type: precision_at_10
      value: 10.16
    - type: precision_at_100
      value: 2.1999999999999997
    - type: precision_at_1000
      value: 0.34099999999999997
    - type: precision_at_20
      value: 6.915
    - type: precision_at_3
      value: 17.8
    - type: precision_at_5
      value: 14.14
    - type: recall_at_1
      value: 4.843
    - type: recall_at_10
      value: 20.595
    - type: recall_at_100
      value: 44.66
    - type: recall_at_1000
      value: 69.152
    - type: recall_at_20
      value: 28.04
    - type: recall_at_3
      value: 10.833
    - type: recall_at_5
      value: 14.346999999999998
    - type: main_score
      value: 19.811
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB SICK-E-PL (default)
      revision: 71bba34b0ece6c56dfcf46d9758a27f7a90f17e9
      split: test
      type: PL-MTEB/sicke-pl-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 80.90093762739502
    - type: cosine_accuracy_threshold
      value: 94.40930485725403
    - type: cosine_ap
      value: 71.15400909912427
    - type: cosine_f1
      value: 66.8213457076566
    - type: cosine_f1_threshold
      value: 91.53673648834229
    - type: cosine_precision
      value: 62.4922504649721
    - type: cosine_recall
      value: 71.7948717948718
    - type: dot_accuracy
      value: 78.41418671015083
    - type: dot_accuracy_threshold
      value: 42924.45068359375
    - type: dot_ap
      value: 63.34003025365763
    - type: dot_f1
      value: 62.518258837277244
    - type: dot_f1_threshold
      value: 40900.738525390625
    - type: dot_precision
      value: 52.99653293709758
    - type: dot_recall
      value: 76.21082621082621
    - type: euclidean_accuracy
      value: 80.67672238075826
    - type: euclidean_accuracy_threshold
      value: 696.0524559020996
    - type: euclidean_ap
      value: 70.88762835990224
    - type: euclidean_f1
      value: 66.711051930759
    - type: euclidean_f1_threshold
      value: 878.5581588745117
    - type: euclidean_precision
      value: 62.625
    - type: euclidean_recall
      value: 71.36752136752136
    - type: main_score
      value: 71.15400909912427
    - type: manhattan_accuracy
      value: 80.65633917651854
    - type: manhattan_accuracy_threshold
      value: 17277.72674560547
    - type: manhattan_ap
      value: 70.67105336611716
    - type: manhattan_f1
      value: 66.51346027577151
    - type: manhattan_f1_threshold
      value: 21687.957763671875
    - type: manhattan_precision
      value: 61.69305724725944
    - type: manhattan_recall
      value: 72.15099715099716
    - type: max_accuracy
      value: 80.90093762739502
    - type: max_ap
      value: 71.15400909912427
    - type: max_f1
      value: 66.8213457076566
    - type: max_precision
      value: 62.625
    - type: max_recall
      value: 76.21082621082621
    - type: similarity_accuracy
      value: 80.90093762739502
    - type: similarity_accuracy_threshold
      value: 94.40930485725403
    - type: similarity_ap
      value: 71.15400909912427
    - type: similarity_f1
      value: 66.8213457076566
    - type: similarity_f1_threshold
      value: 91.53673648834229
    - type: similarity_precision
      value: 62.4922504649721
    - type: similarity_recall
      value: 71.7948717948718
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB SICK-R (default)
      revision: 20a6d6f312dd54037fe07a32d58e5e168867909d
      split: test
      type: mteb/sickr-sts
    metrics:
    - type: cosine_pearson
      value: 92.3339946866199
    - type: cosine_spearman
      value: 89.61697355115497
    - type: euclidean_pearson
      value: 90.3264916449669
    - type: euclidean_spearman
      value: 89.36270451308866
    - type: main_score
      value: 89.61697355115497
    - type: manhattan_pearson
      value: 90.18909339052534
    - type: manhattan_spearman
      value: 89.28337093097377
    - type: pearson
      value: 92.3339946866199
    - type: spearman
      value: 89.61697355115497
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB SICK-R-PL (default)
      revision: fd5c2441b7eeff8676768036142af4cfa42c1339
      split: test
      type: PL-MTEB/sickr-pl-sts
    metrics:
    - type: cosine_pearson
      value: 85.27883048457821
    - type: cosine_spearman
      value: 80.53204892678619
    - type: euclidean_pearson
      value: 82.78520705216168
    - type: euclidean_spearman
      value: 80.27848359873212
    - type: main_score
      value: 80.53204892678619
    - type: manhattan_pearson
      value: 82.63270640583454
    - type: manhattan_spearman
      value: 80.21507977473146
    - type: pearson
      value: 85.27883048457821
    - type: spearman
      value: 80.53204892678619
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB SICKFr (default)
      revision: e077ab4cf4774a1e36d86d593b150422fafd8e8a
      split: test
      type: Lajavaness/SICK-fr
    metrics:
    - type: cosine_pearson
      value: 88.77029361817212
    - type: cosine_spearman
      value: 83.9453600346894
    - type: euclidean_pearson
      value: 85.85331086208573
    - type: euclidean_spearman
      value: 83.70852031985308
    - type: main_score
      value: 83.9453600346894
    - type: manhattan_pearson
      value: 85.66222265885914
    - type: manhattan_spearman
      value: 83.60833111525962
    - type: pearson
      value: 88.77029361817212
    - type: spearman
      value: 83.9453600346894
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STS12 (default)
      revision: a0d554a64d88156834ff5ae9920b964011b16384
      split: test
      type: mteb/sts12-sts
    metrics:
    - type: cosine_pearson
      value: 88.76435859522375
    - type: cosine_spearman
      value: 82.43768167804375
    - type: euclidean_pearson
      value: 87.43566183874832
    - type: euclidean_spearman
      value: 82.82166873757507
    - type: main_score
      value: 82.43768167804375
    - type: manhattan_pearson
      value: 87.39450871380951
    - type: manhattan_spearman
      value: 82.89253043430163
    - type: pearson
      value: 88.76435859522375
    - type: spearman
      value: 82.43768167804375
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STS13 (default)
      revision: 7e90230a92c190f1bf69ae9002b8cea547a64cca
      split: test
      type: mteb/sts13-sts
    metrics:
    - type: cosine_pearson
      value: 88.86627241652141
    - type: cosine_spearman
      value: 89.49011599120688
    - type: euclidean_pearson
      value: 89.3314120073772
    - type: euclidean_spearman
      value: 89.8226502776963
    - type: main_score
      value: 89.49011599120688
    - type: manhattan_pearson
      value: 89.2252179076963
    - type: manhattan_spearman
      value: 89.74573844021225
    - type: pearson
      value: 88.86627241652141
    - type: spearman
      value: 89.49011599120688
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STS14 (default)
      revision: 6031580fec1f6af667f0bd2da0a551cf4f0b2375
      split: test
      type: mteb/sts14-sts
    metrics:
    - type: cosine_pearson
      value: 87.22891405215968
    - type: cosine_spearman
      value: 84.9467188157614
    - type: euclidean_pearson
      value: 87.20330004726237
    - type: euclidean_spearman
      value: 85.34806059461808
    - type: main_score
      value: 84.9467188157614
    - type: manhattan_pearson
      value: 87.15224666107623
    - type: manhattan_spearman
      value: 85.34596898699708
    - type: pearson
      value: 87.22891405215968
    - type: spearman
      value: 84.9467188157614
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STS15 (default)
      revision: ae752c7c21bf194d8b67fd573edf7ae58183cbe3
      split: test
      type: mteb/sts15-sts
    metrics:
    - type: cosine_pearson
      value: 88.14066430111033
    - type: cosine_spearman
      value: 89.31337445552545
    - type: euclidean_pearson
      value: 89.08039335366983
    - type: euclidean_spearman
      value: 89.6658762856415
    - type: main_score
      value: 89.31337445552545
    - type: manhattan_pearson
      value: 89.08057438154486
    - type: manhattan_spearman
      value: 89.68673984203022
    - type: pearson
      value: 88.14066430111033
    - type: spearman
      value: 89.31337445552545
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STS16 (default)
      revision: 4d8694f8f0e0100860b497b999b3dbed754a0513
      split: test
      type: mteb/sts16-sts
    metrics:
    - type: cosine_pearson
      value: 85.14908856657084
    - type: cosine_spearman
      value: 86.84648320786727
    - type: euclidean_pearson
      value: 86.11454713131947
    - type: euclidean_spearman
      value: 86.77738862047961
    - type: main_score
      value: 86.84648320786727
    - type: manhattan_pearson
      value: 86.07804821916372
    - type: manhattan_spearman
      value: 86.78676064310474
    - type: pearson
      value: 85.14908856657084
    - type: spearman
      value: 86.84648320786727
    task:
      type: STS
  - dataset:
      config: en-en
      name: MTEB STS17 (en-en)
      revision: faeb762787bd10488a50c8b5be4a3b82e411949c
      split: test
      type: mteb/sts17-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 89.61633502468356
    - type: cosine_spearman
      value: 89.99772663224805
    - type: euclidean_pearson
      value: 90.14056501501044
    - type: euclidean_spearman
      value: 90.04496896837503
    - type: main_score
      value: 89.99772663224805
    - type: manhattan_pearson
      value: 90.08964860311801
    - type: manhattan_spearman
      value: 90.00091712362196
    - type: pearson
      value: 89.61633502468356
    - type: spearman
      value: 89.99772663224805
    task:
      type: STS
  - dataset:
      config: es-en
      name: MTEB STS17 (es-en)
      revision: faeb762787bd10488a50c8b5be4a3b82e411949c
      split: test
      type: mteb/sts17-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 86.44548026840202
    - type: cosine_spearman
      value: 87.26263108768539
    - type: euclidean_pearson
      value: 86.42844593583838
    - type: euclidean_spearman
      value: 86.89388428664364
    - type: main_score
      value: 87.26263108768539
    - type: manhattan_pearson
      value: 86.47186940800881
    - type: manhattan_spearman
      value: 87.02163091089946
    - type: pearson
      value: 86.44548026840202
    - type: spearman
      value: 87.26263108768539
    task:
      type: STS
  - dataset:
      config: en-de
      name: MTEB STS17 (en-de)
      revision: faeb762787bd10488a50c8b5be4a3b82e411949c
      split: test
      type: mteb/sts17-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 87.89345132532758
    - type: cosine_spearman
      value: 87.96246221327699
    - type: euclidean_pearson
      value: 88.49013032701419
    - type: euclidean_spearman
      value: 87.81981265317344
    - type: main_score
      value: 87.96246221327699
    - type: manhattan_pearson
      value: 88.31360914178538
    - type: manhattan_spearman
      value: 87.62734530005075
    - type: pearson
      value: 87.89345132532758
    - type: spearman
      value: 87.96246221327699
    task:
      type: STS
  - dataset:
      config: es-es
      name: MTEB STS17 (es-es)
      revision: faeb762787bd10488a50c8b5be4a3b82e411949c
      split: test
      type: mteb/sts17-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 88.4084678497171
    - type: cosine_spearman
      value: 88.77640638748285
    - type: euclidean_pearson
      value: 89.60124312475843
    - type: euclidean_spearman
      value: 88.4321442688528
    - type: main_score
      value: 88.77640638748285
    - type: manhattan_pearson
      value: 89.62375118021299
    - type: manhattan_spearman
      value: 88.46998118661577
    - type: pearson
      value: 88.4084678497171
    - type: spearman
      value: 88.77640638748285
    task:
      type: STS
  - dataset:
      config: fr-en
      name: MTEB STS17 (fr-en)
      revision: faeb762787bd10488a50c8b5be4a3b82e411949c
      split: test
      type: mteb/sts17-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 87.30688801326498
    - type: cosine_spearman
      value: 87.55684697258378
    - type: euclidean_pearson
      value: 87.89672951056794
    - type: euclidean_spearman
      value: 87.28050429201674
    - type: main_score
      value: 87.55684697258378
    - type: manhattan_pearson
      value: 87.74292745320572
    - type: manhattan_spearman
      value: 87.16383993876582
    - type: pearson
      value: 87.30688801326498
    - type: spearman
      value: 87.55684697258378
    task:
      type: STS
  - dataset:
      config: zh-en
      name: MTEB STS22 (zh-en)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 73.46180375170147
    - type: cosine_spearman
      value: 73.39559590127081
    - type: euclidean_pearson
      value: 73.72613901293681
    - type: euclidean_spearman
      value: 71.85465165176795
    - type: main_score
      value: 73.39559590127081
    - type: manhattan_pearson
      value: 73.07859140869076
    - type: manhattan_spearman
      value: 71.22047343718893
    - type: pearson
      value: 73.46180375170147
    - type: spearman
      value: 73.39559590127081
    task:
      type: STS
  - dataset:
      config: zh
      name: MTEB STS22 (zh)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 62.47531620842637
    - type: cosine_spearman
      value: 66.22504667157702
    - type: euclidean_pearson
      value: 66.76201254783692
    - type: euclidean_spearman
      value: 66.86115760269463
    - type: main_score
      value: 66.22504667157702
    - type: manhattan_pearson
      value: 66.73847836793489
    - type: manhattan_spearman
      value: 66.7677116377695
    - type: pearson
      value: 62.47531620842637
    - type: spearman
      value: 66.22504667157702
    task:
      type: STS
  - dataset:
      config: es
      name: MTEB STS22 (es)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 69.89707002436481
    - type: cosine_spearman
      value: 72.2054865735116
    - type: euclidean_pearson
      value: 71.81856615570756
    - type: euclidean_spearman
      value: 72.72593304629407
    - type: main_score
      value: 72.2054865735116
    - type: manhattan_pearson
      value: 72.00362684700072
    - type: manhattan_spearman
      value: 72.62783534769964
    - type: pearson
      value: 69.89707002436481
    - type: spearman
      value: 72.2054865735116
    task:
      type: STS
  - dataset:
      config: fr
      name: MTEB STS22 (fr)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 81.59623734395916
    - type: cosine_spearman
      value: 83.28946105111358
    - type: euclidean_pearson
      value: 79.377330171466
    - type: euclidean_spearman
      value: 81.81029781662205
    - type: main_score
      value: 83.28946105111358
    - type: manhattan_pearson
      value: 78.96970881689698
    - type: manhattan_spearman
      value: 81.91773236079703
    - type: pearson
      value: 81.59623734395916
    - type: spearman
      value: 83.28946105111358
    task:
      type: STS
  - dataset:
      config: de-fr
      name: MTEB STS22 (de-fr)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 55.03825643126142
    - type: cosine_spearman
      value: 58.25792501780429
    - type: euclidean_pearson
      value: 50.38007603973409
    - type: euclidean_spearman
      value: 59.39961789383097
    - type: main_score
      value: 58.25792501780429
    - type: manhattan_pearson
      value: 50.518568927999155
    - type: manhattan_spearman
      value: 59.84185466003894
    - type: pearson
      value: 55.03825643126142
    - type: spearman
      value: 58.25792501780429
    task:
      type: STS
  - dataset:
      config: pl-en
      name: MTEB STS22 (pl-en)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 77.77233721490776
    - type: cosine_spearman
      value: 76.17596588017625
    - type: euclidean_pearson
      value: 74.47600468156611
    - type: euclidean_spearman
      value: 72.61278728057012
    - type: main_score
      value: 76.17596588017625
    - type: manhattan_pearson
      value: 74.48118910099699
    - type: manhattan_spearman
      value: 73.33167419101696
    - type: pearson
      value: 77.77233721490776
    - type: spearman
      value: 76.17596588017625
    task:
      type: STS
  - dataset:
      config: pl
      name: MTEB STS22 (pl)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 42.87453608131507
    - type: cosine_spearman
      value: 45.137849894401185
    - type: euclidean_pearson
      value: 31.66964197694796
    - type: euclidean_spearman
      value: 44.1014900837869
    - type: main_score
      value: 45.137849894401185
    - type: manhattan_pearson
      value: 31.007199259384745
    - type: manhattan_spearman
      value: 43.48181523288926
    - type: pearson
      value: 42.87453608131507
    - type: spearman
      value: 45.137849894401185
    task:
      type: STS
  - dataset:
      config: en
      name: MTEB STS22 (en)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 66.87400150638176
    - type: cosine_spearman
      value: 67.27861354834066
    - type: euclidean_pearson
      value: 66.81789582140216
    - type: euclidean_spearman
      value: 66.44220479858708
    - type: main_score
      value: 67.27861354834066
    - type: manhattan_pearson
      value: 66.92509859033235
    - type: manhattan_spearman
      value: 66.46841124185076
    - type: pearson
      value: 66.87400150638176
    - type: spearman
      value: 67.27861354834066
    task:
      type: STS
  - dataset:
      config: ru
      name: MTEB STS22 (ru)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 61.819804551576084
    - type: cosine_spearman
      value: 65.0864146772135
    - type: euclidean_pearson
      value: 62.518151090361876
    - type: euclidean_spearman
      value: 65.13608138548017
    - type: main_score
      value: 65.0864146772135
    - type: manhattan_pearson
      value: 62.51413246915267
    - type: manhattan_spearman
      value: 65.19077543064323
    - type: pearson
      value: 61.819804551576084
    - type: spearman
      value: 65.0864146772135
    task:
      type: STS
  - dataset:
      config: de
      name: MTEB STS22 (de)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 54.85728696035389
    - type: cosine_spearman
      value: 61.60906359227576
    - type: euclidean_pearson
      value: 52.57582587901851
    - type: euclidean_spearman
      value: 61.41823097598308
    - type: main_score
      value: 61.60906359227576
    - type: manhattan_pearson
      value: 52.500978361080506
    - type: manhattan_spearman
      value: 61.30365596659758
    - type: pearson
      value: 54.85728696035389
    - type: spearman
      value: 61.60906359227576
    task:
      type: STS
  - dataset:
      config: fr-pl
      name: MTEB STS22 (fr-pl)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 67.68016005631422
    - type: cosine_spearman
      value: 84.51542547285167
    - type: euclidean_pearson
      value: 66.19871164667245
    - type: euclidean_spearman
      value: 73.24670207647144
    - type: main_score
      value: 84.51542547285167
    - type: manhattan_pearson
      value: 67.0443525268974
    - type: manhattan_spearman
      value: 73.24670207647144
    - type: pearson
      value: 67.68016005631422
    - type: spearman
      value: 84.51542547285167
    task:
      type: STS
  - dataset:
      config: de-pl
      name: MTEB STS22 (de-pl)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 47.49467414030747
    - type: cosine_spearman
      value: 56.81512095681289
    - type: euclidean_pearson
      value: 48.42860221765214
    - type: euclidean_spearman
      value: 58.63197306329092
    - type: main_score
      value: 56.81512095681289
    - type: manhattan_pearson
      value: 48.39594959260441
    - type: manhattan_spearman
      value: 58.63197306329092
    - type: pearson
      value: 47.49467414030747
    - type: spearman
      value: 56.81512095681289
    task:
      type: STS
  - dataset:
      config: es-en
      name: MTEB STS22 (es-en)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 76.8364678896155
    - type: cosine_spearman
      value: 78.45516413087114
    - type: euclidean_pearson
      value: 78.62779318576634
    - type: euclidean_spearman
      value: 78.88760695649488
    - type: main_score
      value: 78.45516413087114
    - type: manhattan_pearson
      value: 78.62131335760031
    - type: manhattan_spearman
      value: 78.81861844200388
    - type: pearson
      value: 76.8364678896155
    - type: spearman
      value: 78.45516413087114
    task:
      type: STS
  - dataset:
      config: de-en
      name: MTEB STS22 (de-en)
      revision: de9d86b3b84231dc21f76c7b7af1f28e2f57f6e3
      split: test
      type: mteb/sts22-crosslingual-sts
    metrics:
    - type: cosine_pearson
      value: 65.16640313911604
    - type: cosine_spearman
      value: 60.887608967403914
    - type: euclidean_pearson
      value: 67.49902244990913
    - type: euclidean_spearman
      value: 59.2458787136538
    - type: main_score
      value: 60.887608967403914
    - type: manhattan_pearson
      value: 67.34313506388378
    - type: manhattan_spearman
      value: 59.05283429200166
    - type: pearson
      value: 65.16640313911604
    - type: spearman
      value: 60.887608967403914
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB QBQTC (default)
      revision: 790b0510dc52b1553e8c49f3d2afb48c0e5c48b7
      split: test
      type: C-MTEB/QBQTC
    metrics:
      - type: cosine_pearson
        value: 34.20049144526891
      - type: cosine_spearman
        value: 36.41802814113771
      - type: euclidean_pearson
        value: 34.569942139590626
      - type: euclidean_spearman
        value: 36.06141660786936
      - type: main_score
        value: 36.41802814113771
      - type: manhattan_pearson
        value: 34.537041543916003
      - type: manhattan_spearman
        value: 36.033418927773825
      - type: pearson
        value: 34.20049144526891
      - type: spearman
        value: 36.41802814113771
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STSB (default)
      revision: 0cde68302b3541bb8b3c340dc0644b0b745b3dc0
      split: test
      type: C-MTEB/STSB
    metrics:
    - type: cosine_pearson
      value: 81.5092853013241
    - type: cosine_spearman
      value: 83.54005474244292
    - type: euclidean_pearson
      value: 83.7246578378554
    - type: euclidean_spearman
      value: 84.46767551087716
    - type: main_score
      value: 83.54005474244292
    - type: manhattan_pearson
      value: 83.65922665594636
    - type: manhattan_spearman
      value: 84.42431449101848
    - type: pearson
      value: 81.5092853013241
    - type: spearman
      value: 83.54005474244292
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB STSBenchmark (default)
      revision: b0fddb56ed78048fa8b90373c8a3cfc37b684831
      split: test
      type: mteb/stsbenchmark-sts
    metrics:
    - type: cosine_pearson
      value: 87.70246866744966
    - type: cosine_spearman
      value: 89.44070045346106
    - type: euclidean_pearson
      value: 89.56956519641007
    - type: euclidean_spearman
      value: 89.95830112784283
    - type: main_score
      value: 89.44070045346106
    - type: manhattan_pearson
      value: 89.48264471425145
    - type: manhattan_spearman
      value: 89.87900732483114
    - type: pearson
      value: 87.70246866744966
    - type: spearman
      value: 89.44070045346106
    task:
      type: STS
  - dataset:
      config: de
      name: MTEB STSBenchmarkMultilingualSTS (de)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 86.83701990805217
    - type: cosine_spearman
      value: 87.80280785492258
    - type: euclidean_pearson
      value: 87.77325330043514
    - type: euclidean_spearman
      value: 88.3564607283144
    - type: main_score
      value: 87.80280785492258
    - type: manhattan_pearson
      value: 87.6745449945946
    - type: manhattan_spearman
      value: 88.30660465978795
    - type: pearson
      value: 86.83701990805217
    - type: spearman
      value: 87.80280785492258
    task:
      type: STS
  - dataset:
      config: zh
      name: MTEB STSBenchmarkMultilingualSTS (zh)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 84.27751020600267
    - type: cosine_spearman
      value: 85.63500407412486
    - type: euclidean_pearson
      value: 85.21829891649696
    - type: euclidean_spearman
      value: 85.9384575715382
    - type: main_score
      value: 85.63500407412486
    - type: manhattan_pearson
      value: 85.10797194089801
    - type: manhattan_spearman
      value: 85.8770162042784
    - type: pearson
      value: 84.27751020600267
    - type: spearman
      value: 85.63500407412486
    task:
      type: STS
  - dataset:
      config: fr
      name: MTEB STSBenchmarkMultilingualSTS (fr)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 86.56833656723254
    - type: cosine_spearman
      value: 87.4393978501382
    - type: euclidean_pearson
      value: 87.45171512751267
    - type: euclidean_spearman
      value: 88.13106516566947
    - type: main_score
      value: 87.4393978501382
    - type: manhattan_pearson
      value: 87.33010961793333
    - type: manhattan_spearman
      value: 88.06707425102182
    - type: pearson
      value: 86.56833656723254
    - type: spearman
      value: 87.4393978501382
    task:
      type: STS
  - dataset:
      config: pl
      name: MTEB STSBenchmarkMultilingualSTS (pl)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 85.45065540325523
    - type: cosine_spearman
      value: 85.47881076789359
    - type: euclidean_pearson
      value: 85.1999493863155
    - type: euclidean_spearman
      value: 85.7874947669187
    - type: main_score
      value: 85.47881076789359
    - type: manhattan_pearson
      value: 85.06075305990376
    - type: manhattan_spearman
      value: 85.71563015639558
    - type: pearson
      value: 85.45065540325523
    - type: spearman
      value: 85.47881076789359
    task:
      type: STS
  - dataset:
      config: es
      name: MTEB STSBenchmarkMultilingualSTS (es)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 87.11952824079832
    - type: cosine_spearman
      value: 87.9643473573153
    - type: euclidean_pearson
      value: 88.11750364639971
    - type: euclidean_spearman
      value: 88.63695109016498
    - type: main_score
      value: 87.9643473573153
    - type: manhattan_pearson
      value: 88.00294453126699
    - type: manhattan_spearman
      value: 88.53750241758391
    - type: pearson
      value: 87.11952824079832
    - type: spearman
      value: 87.9643473573153
    task:
      type: STS
  - dataset:
      config: ru
      name: MTEB STSBenchmarkMultilingualSTS (ru)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 85.99804354414991
    - type: cosine_spearman
      value: 86.30252111551002
    - type: euclidean_pearson
      value: 86.1880652037762
    - type: euclidean_spearman
      value: 86.69556223944502
    - type: main_score
      value: 86.30252111551002
    - type: manhattan_pearson
      value: 86.0736400320898
    - type: manhattan_spearman
      value: 86.61747927593393
    - type: pearson
      value: 85.99804354414991
    - type: spearman
      value: 86.30252111551002
    task:
      type: STS
  - dataset:
      config: en
      name: MTEB STSBenchmarkMultilingualSTS (en)
      revision: 29afa2569dcedaaa2fe6a3dcfebab33d28b82e8c
      split: test
      type: mteb/stsb_multi_mt
    metrics:
    - type: cosine_pearson
      value: 87.70246861738103
    - type: cosine_spearman
      value: 89.44070045346106
    - type: euclidean_pearson
      value: 89.56956518833663
    - type: euclidean_spearman
      value: 89.95830112784283
    - type: main_score
      value: 89.44070045346106
    - type: manhattan_pearson
      value: 89.48264470792915
    - type: manhattan_spearman
      value: 89.87900732483114
    - type: pearson
      value: 87.70246861738103
    - type: spearman
      value: 89.44070045346106
    task:
      type: STS
  - dataset:
      config: default
      name: MTEB SciDocsRR (default)
      revision: d3c5e1fc0b855ab6097bf1cda04dd73947d7caab
      split: test
      type: mteb/scidocs-reranking
    metrics:
    - type: map
      value: 84.88064122814694
    - type: mrr
      value: 95.84832651009123
    - type: main_score
      value: 84.88064122814694
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB SciFact (default)
      revision: 0228b52cf27578f30900b9e5271d331663a030d7
      split: test
      type: mteb/scifact
    metrics:
    - type: map_at_1
      value: 57.289
    - type: map_at_10
      value: 67.88499999999999
    - type: map_at_100
      value: 68.477
    - type: map_at_1000
      value: 68.50500000000001
    - type: map_at_20
      value: 68.33500000000001
    - type: map_at_3
      value: 65.08
    - type: map_at_5
      value: 67.001
    - type: mrr_at_1
      value: 59.667
    - type: mrr_at_10
      value: 68.626
    - type: mrr_at_100
      value: 69.082
    - type: mrr_at_1000
      value: 69.108
    - type: mrr_at_20
      value: 68.958
    - type: mrr_at_3
      value: 66.667
    - type: mrr_at_5
      value: 67.983
    - type: ndcg_at_1
      value: 59.667
    - type: ndcg_at_10
      value: 72.309
    - type: ndcg_at_100
      value: 74.58399999999999
    - type: ndcg_at_1000
      value: 75.25500000000001
    - type: ndcg_at_20
      value: 73.656
    - type: ndcg_at_3
      value: 67.791
    - type: ndcg_at_5
      value: 70.45
    - type: precision_at_1
      value: 59.667
    - type: precision_at_10
      value: 9.567
    - type: precision_at_100
      value: 1.073
    - type: precision_at_1000
      value: 0.11299999999999999
    - type: precision_at_20
      value: 5.083
    - type: precision_at_3
      value: 26.333000000000002
    - type: precision_at_5
      value: 17.666999999999998
    - type: recall_at_1
      value: 57.289
    - type: recall_at_10
      value: 84.756
    - type: recall_at_100
      value: 94.5
    - type: recall_at_1000
      value: 99.667
    - type: recall_at_20
      value: 89.7
    - type: recall_at_3
      value: 73.22800000000001
    - type: recall_at_5
      value: 79.444
    - type: main_score
      value: 72.309
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB SpanishNewsClusteringP2P (default)
      revision: bf8ca8ddc5b7da4f7004720ddf99bbe0483480e6
      split: test
      type: jinaai/spanish_news_clustering
    metrics:
    - type: main_score
      value: 45.04477709795154
    - type: v_measure
      value: 45.04477709795154
    - type: v_measure_std
      value: 0.0
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB SpanishPassageRetrievalS2S (default)
      revision: 9cddf2ce5209ade52c2115ccfa00eb22c6d3a837
      split: test
      type: jinaai/spanish_passage_retrieval
    metrics:
    - type: main_score
      value: 69.83
    - type: map_at_1
      value: 15.736
    - type: map_at_10
      value: 52.027
    - type: map_at_100
      value: 65.08800000000001
    - type: map_at_1000
      value: 65.08800000000001
    - type: map_at_20
      value: 60.79900000000001
    - type: map_at_3
      value: 32.869
    - type: map_at_5
      value: 41.436
    - type: mrr_at_1
      value: 75.44910179640718
    - type: mrr_at_10
      value: 84.43446440452426
    - type: mrr_at_100
      value: 84.48052612723271
    - type: mrr_at_1000
      value: 84.48052612723271
    - type: mrr_at_20
      value: 84.48052612723271
    - type: mrr_at_3
      value: 83.13373253493013
    - type: mrr_at_5
      value: 84.3013972055888
    - type: nauc_map_at_1000_diff1
      value: 50.611540149694356
    - type: nauc_map_at_1000_max
      value: 2.1102430434260238
    - type: nauc_map_at_1000_std
      value: -18.88993521335793
    - type: nauc_map_at_100_diff1
      value: 50.611540149694356
    - type: nauc_map_at_100_max
      value: 2.1102430434260238
    - type: nauc_map_at_100_std
      value: -18.88993521335793
    - type: nauc_map_at_10_diff1
      value: 59.13518981755268
    - type: nauc_map_at_10_max
      value: -9.810386627392807
    - type: nauc_map_at_10_std
      value: -38.31810152345078
    - type: nauc_map_at_1_diff1
      value: 74.96782567287174
    - type: nauc_map_at_1_max
      value: -29.648279252607875
    - type: nauc_map_at_1_std
      value: -54.017459339141595
    - type: nauc_map_at_20_diff1
      value: 55.26694458629849
    - type: nauc_map_at_20_max
      value: -1.9490244535020729
    - type: nauc_map_at_20_std
      value: -25.22211659104076
    - type: nauc_map_at_3_diff1
      value: 71.67607885031732
    - type: nauc_map_at_3_max
      value: -25.078101661694507
    - type: nauc_map_at_3_std
      value: -50.55408861920259
    - type: nauc_map_at_5_diff1
      value: 61.50111515417668
    - type: nauc_map_at_5_max
      value: -16.4114670513168
    - type: nauc_map_at_5_std
      value: -44.391416134859135
    - type: nauc_mrr_at_1000_diff1
      value: 74.18848063283234
    - type: nauc_mrr_at_1000_max
      value: 21.929205946778005
    - type: nauc_mrr_at_1000_std
      value: -36.27399268489433
    - type: nauc_mrr_at_100_diff1
      value: 74.18848063283234
    - type: nauc_mrr_at_100_max
      value: 21.929205946778005
    - type: nauc_mrr_at_100_std
      value: -36.27399268489433
    - type: nauc_mrr_at_10_diff1
      value: 74.27231582268745
    - type: nauc_mrr_at_10_max
      value: 21.481133301135337
    - type: nauc_mrr_at_10_std
      value: -36.72070854872902
    - type: nauc_mrr_at_1_diff1
      value: 76.54855950439561
    - type: nauc_mrr_at_1_max
      value: 26.99938321212366
    - type: nauc_mrr_at_1_std
      value: -33.098742603429635
    - type: nauc_mrr_at_20_diff1
      value: 74.18848063283234
    - type: nauc_mrr_at_20_max
      value: 21.929205946778005
    - type: nauc_mrr_at_20_std
      value: -36.27399268489433
    - type: nauc_mrr_at_3_diff1
      value: 72.05379526740143
    - type: nauc_mrr_at_3_max
      value: 18.875831185752528
    - type: nauc_mrr_at_3_std
      value: -37.27302006456391
    - type: nauc_mrr_at_5_diff1
      value: 74.25342356682029
    - type: nauc_mrr_at_5_max
      value: 20.756340085088738
    - type: nauc_mrr_at_5_std
      value: -37.99507208540703
    - type: nauc_ndcg_at_1000_diff1
      value: 53.259363764380275
    - type: nauc_ndcg_at_1000_max
      value: 12.936954959423218
    - type: nauc_ndcg_at_1000_std
      value: -16.953898675672153
    - type: nauc_ndcg_at_100_diff1
      value: 53.259363764380275
    - type: nauc_ndcg_at_100_max
      value: 12.936954959423218
    - type: nauc_ndcg_at_100_std
      value: -16.953898675672153
    - type: nauc_ndcg_at_10_diff1
      value: 53.70942345413554
    - type: nauc_ndcg_at_10_max
      value: -3.8465093347016186
    - type: nauc_ndcg_at_10_std
      value: -31.208127919994755
    - type: nauc_ndcg_at_1_diff1
      value: 75.30551289259554
    - type: nauc_ndcg_at_1_max
      value: 25.53292054129834
    - type: nauc_ndcg_at_1_std
      value: -33.285498788395145
    - type: nauc_ndcg_at_20_diff1
      value: 57.62409278278133
    - type: nauc_ndcg_at_20_max
      value: 2.8040586426056233
    - type: nauc_ndcg_at_20_std
      value: -26.270875776221704
    - type: nauc_ndcg_at_3_diff1
      value: 48.42294834754225
    - type: nauc_ndcg_at_3_max
      value: 16.912467881065822
    - type: nauc_ndcg_at_3_std
      value: -13.324841189277873
    - type: nauc_ndcg_at_5_diff1
      value: 47.512819802794596
    - type: nauc_ndcg_at_5_max
      value: 14.645518203506594
    - type: nauc_ndcg_at_5_std
      value: -17.641450435599275
    - type: nauc_precision_at_1000_diff1
      value: -34.43320975829637
    - type: nauc_precision_at_1000_max
      value: 29.08585622578186
    - type: nauc_precision_at_1000_std
      value: 46.55117940162061
    - type: nauc_precision_at_100_diff1
      value: -34.433209758296364
    - type: nauc_precision_at_100_max
      value: 29.085856225781885
    - type: nauc_precision_at_100_std
      value: 46.55117940162065
    - type: nauc_precision_at_10_diff1
      value: -21.895306304096902
    - type: nauc_precision_at_10_max
      value: 33.190476527593745
    - type: nauc_precision_at_10_std
      value: 37.64916268614298
    - type: nauc_precision_at_1_diff1
      value: 75.30551289259554
    - type: nauc_precision_at_1_max
      value: 25.53292054129834
    - type: nauc_precision_at_1_std
      value: -33.285498788395145
    - type: nauc_precision_at_20_diff1
      value: -27.63076748060466
    - type: nauc_precision_at_20_max
      value: 30.689810416086154
    - type: nauc_precision_at_20_std
      value: 46.164191636131626
    - type: nauc_precision_at_3_diff1
      value: 20.547345067837288
    - type: nauc_precision_at_3_max
      value: 26.177050942827528
    - type: nauc_precision_at_3_std
      value: 5.960466052973099
    - type: nauc_precision_at_5_diff1
      value: -8.928755534002669
    - type: nauc_precision_at_5_max
      value: 40.83262650073459
    - type: nauc_precision_at_5_std
      value: 26.158537031161494
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: .nan
    - type: nauc_recall_at_100_max
      value: .nan
    - type: nauc_recall_at_100_std
      value: .nan
    - type: nauc_recall_at_10_diff1
      value: 53.08654386169444
    - type: nauc_recall_at_10_max
      value: -23.276269379519356
    - type: nauc_recall_at_10_std
      value: -50.80707792706157
    - type: nauc_recall_at_1_diff1
      value: 74.96782567287174
    - type: nauc_recall_at_1_max
      value: -29.648279252607875
    - type: nauc_recall_at_1_std
      value: -54.017459339141595
    - type: nauc_recall_at_20_diff1
      value: 51.60121897059633
    - type: nauc_recall_at_20_max
      value: -14.241779530735387
    - type: nauc_recall_at_20_std
      value: -37.877451525215456
    - type: nauc_recall_at_3_diff1
      value: 66.99474984329694
    - type: nauc_recall_at_3_max
      value: -30.802787353187966
    - type: nauc_recall_at_3_std
      value: -53.58737792129713
    - type: nauc_recall_at_5_diff1
      value: 54.64214444958567
    - type: nauc_recall_at_5_max
      value: -23.341309362104703
    - type: nauc_recall_at_5_std
      value: -51.381363923145265
    - type: ndcg_at_1
      value: 76.048
    - type: ndcg_at_10
      value: 69.83
    - type: ndcg_at_100
      value: 82.11500000000001
    - type: ndcg_at_1000
      value: 82.11500000000001
    - type: ndcg_at_20
      value: 75.995
    - type: ndcg_at_3
      value: 69.587
    - type: ndcg_at_5
      value: 69.062
    - type: precision_at_1
      value: 76.048
    - type: precision_at_10
      value: 43.653
    - type: precision_at_100
      value: 7.718999999999999
    - type: precision_at_1000
      value: 0.772
    - type: precision_at_20
      value: 31.108000000000004
    - type: precision_at_3
      value: 63.87199999999999
    - type: precision_at_5
      value: 56.407
    - type: recall_at_1
      value: 15.736
    - type: recall_at_10
      value: 66.873
    - type: recall_at_100
      value: 100.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 85.01100000000001
    - type: recall_at_3
      value: 36.441
    - type: recall_at_5
      value: 49.109
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB SprintDuplicateQuestions (default)
      revision: d66bd1f72af766a5cc4b0ca5e00c162f89e8cc46
      split: test
      type: mteb/sprintduplicatequestions-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 99.87326732673267
    - type: cosine_accuracy_threshold
      value: 86.0752820968628
    - type: cosine_ap
      value: 96.98758090713252
    - type: cosine_f1
      value: 93.52881698685542
    - type: cosine_f1_threshold
      value: 86.0752820968628
    - type: cosine_precision
      value: 94.58077709611452
    - type: cosine_recall
      value: 92.5
    - type: dot_accuracy
      value: 99.82574257425742
    - type: dot_accuracy_threshold
      value: 40484.73815917969
    - type: dot_ap
      value: 95.68959907254845
    - type: dot_f1
      value: 91.31293188548865
    - type: dot_f1_threshold
      value: 40336.810302734375
    - type: dot_precision
      value: 90.15594541910332
    - type: dot_recall
      value: 92.5
    - type: euclidean_accuracy
      value: 99.87128712871286
    - type: euclidean_accuracy_threshold
      value: 1162.5749588012695
    - type: euclidean_ap
      value: 96.92640435656577
    - type: euclidean_f1
      value: 93.4475806451613
    - type: euclidean_f1_threshold
      value: 1162.5749588012695
    - type: euclidean_precision
      value: 94.20731707317073
    - type: euclidean_recall
      value: 92.7
    - type: main_score
      value: 96.98758090713252
    - type: manhattan_accuracy
      value: 99.86930693069307
    - type: manhattan_accuracy_threshold
      value: 28348.71826171875
    - type: manhattan_ap
      value: 96.93832673967925
    - type: manhattan_f1
      value: 93.33333333333333
    - type: manhattan_f1_threshold
      value: 28348.71826171875
    - type: manhattan_precision
      value: 94.28571428571428
    - type: manhattan_recall
      value: 92.4
    - type: max_accuracy
      value: 99.87326732673267
    - type: max_ap
      value: 96.98758090713252
    - type: max_f1
      value: 93.52881698685542
    - type: max_precision
      value: 94.58077709611452
    - type: max_recall
      value: 92.7
    - type: similarity_accuracy
      value: 99.87326732673267
    - type: similarity_accuracy_threshold
      value: 86.0752820968628
    - type: similarity_ap
      value: 96.98758090713252
    - type: similarity_f1
      value: 93.52881698685542
    - type: similarity_f1_threshold
      value: 86.0752820968628
    - type: similarity_precision
      value: 94.58077709611452
    - type: similarity_recall
      value: 92.5
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB StackExchangeClustering (default)
      revision: 6cbc1f7b2bc0622f2e39d2c77fa502909748c259
      split: test
      type: mteb/stackexchange-clustering
    metrics:
    - type: main_score
      value: 65.6560129719848
    - type: v_measure
      value: 65.6560129719848
    - type: v_measure_std
      value: 4.781229811487539
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB StackExchangeClusteringP2P (default)
      revision: 815ca46b2622cec33ccafc3735d572c266efdb44
      split: test
      type: mteb/stackexchange-clustering-p2p
    metrics:
    - type: main_score
      value: 35.07546243853692
    - type: v_measure
      value: 35.07546243853692
    - type: v_measure_std
      value: 1.1978740356240998
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB StackOverflowDupQuestions (default)
      revision: e185fbe320c72810689fc5848eb6114e1ef5ec69
      split: test
      type: mteb/stackoverflowdupquestions-reranking
    metrics:
    - type: map
      value: 51.771005199508835
    - type: mrr
      value: 52.65443298531534
    - type: main_score
      value: 51.771005199508835
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB SummEval (default)
      revision: cda12ad7615edc362dbf25a00fdd61d3b1eaf93c
      split: test
      type: mteb/summeval
    metrics:
    - type: cosine_pearson
      value: 29.48686238342228
    - type: cosine_spearman
      value: 29.706543509170054
    - type: dot_pearson
      value: 27.95853155597859
    - type: dot_spearman
      value: 27.604287986935162
    - type: main_score
      value: 29.706543509170054
    - type: pearson
      value: 29.48686238342228
    - type: spearman
      value: 29.706543509170054
    task:
      type: Summarization
  - dataset:
      config: default
      name: MTEB SummEvalFr (default)
      revision: b385812de6a9577b6f4d0f88c6a6e35395a94054
      split: test
      type: lyon-nlp/summarization-summeval-fr-p2p
    metrics:
    - type: cosine_pearson
      value: 31.551301434917868
    - type: cosine_spearman
      value: 30.709049789175186
    - type: dot_pearson
      value: 27.77050901756549
    - type: dot_spearman
      value: 26.715505953561795
    - type: main_score
      value: 30.709049789175186
    - type: pearson
      value: 31.551301434917868
    - type: spearman
      value: 30.709049789175186
    task:
      type: Summarization
  - dataset:
      config: default
      name: MTEB SyntecReranking (default)
      revision: b205c5084a0934ce8af14338bf03feb19499c84d
      split: test
      type: lyon-nlp/mteb-fr-reranking-syntec-s2p
    metrics:
    - type: map
      value: 73.31666666666666
    - type: mrr
      value: 73.31666666666666
    - type: main_score
      value: 73.31666666666666
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB SyntecRetrieval (default)
      revision: 19661ccdca4dfc2d15122d776b61685f48c68ca9
      split: test
      type: lyon-nlp/mteb-fr-retrieval-syntec-s2p
    metrics:
    - type: main_score
      value: 83.851
    - type: map_at_1
      value: 68.0
    - type: map_at_10
      value: 79.187
    - type: map_at_100
      value: 79.32900000000001
    - type: map_at_1000
      value: 79.32900000000001
    - type: map_at_20
      value: 79.32900000000001
    - type: map_at_3
      value: 77.333
    - type: map_at_5
      value: 78.93299999999999
    - type: mrr_at_1
      value: 68.0
    - type: mrr_at_10
      value: 79.18730158730159
    - type: mrr_at_100
      value: 79.32945845004669
    - type: mrr_at_1000
      value: 79.32945845004669
    - type: mrr_at_20
      value: 79.32945845004669
    - type: mrr_at_3
      value: 77.33333333333333
    - type: mrr_at_5
      value: 78.93333333333332
    - type: nauc_map_at_1000_diff1
      value: 63.31103256935259
    - type: nauc_map_at_1000_max
      value: 11.073749121365623
    - type: nauc_map_at_1000_std
      value: 7.4973309839738
    - type: nauc_map_at_100_diff1
      value: 63.31103256935259
    - type: nauc_map_at_100_max
      value: 11.073749121365623
    - type: nauc_map_at_100_std
      value: 7.4973309839738
    - type: nauc_map_at_10_diff1
      value: 62.91585737195978
    - type: nauc_map_at_10_max
      value: 11.770664508983133
    - type: nauc_map_at_10_std
      value: 8.179883948527962
    - type: nauc_map_at_1_diff1
      value: 66.1236265634718
    - type: nauc_map_at_1_max
      value: 7.000207311173955
    - type: nauc_map_at_1_std
      value: 6.54412272821497
    - type: nauc_map_at_20_diff1
      value: 63.31103256935259
    - type: nauc_map_at_20_max
      value: 11.073749121365623
    - type: nauc_map_at_20_std
      value: 7.4973309839738
    - type: nauc_map_at_3_diff1
      value: 62.14039574010254
    - type: nauc_map_at_3_max
      value: 11.06996398110187
    - type: nauc_map_at_3_std
      value: 7.288759297085769
    - type: nauc_map_at_5_diff1
      value: 63.0401271126211
    - type: nauc_map_at_5_max
      value: 10.779317801858609
    - type: nauc_map_at_5_std
      value: 6.476660484760681
    - type: nauc_mrr_at_1000_diff1
      value: 63.31103256935259
    - type: nauc_mrr_at_1000_max
      value: 11.073749121365623
    - type: nauc_mrr_at_1000_std
      value: 7.4973309839738
    - type: nauc_mrr_at_100_diff1
      value: 63.31103256935259
    - type: nauc_mrr_at_100_max
      value: 11.073749121365623
    - type: nauc_mrr_at_100_std
      value: 7.4973309839738
    - type: nauc_mrr_at_10_diff1
      value: 62.91585737195978
    - type: nauc_mrr_at_10_max
      value: 11.770664508983133
    - type: nauc_mrr_at_10_std
      value: 8.179883948527962
    - type: nauc_mrr_at_1_diff1
      value: 66.1236265634718
    - type: nauc_mrr_at_1_max
      value: 7.000207311173955
    - type: nauc_mrr_at_1_std
      value: 6.54412272821497
    - type: nauc_mrr_at_20_diff1
      value: 63.31103256935259
    - type: nauc_mrr_at_20_max
      value: 11.073749121365623
    - type: nauc_mrr_at_20_std
      value: 7.4973309839738
    - type: nauc_mrr_at_3_diff1
      value: 62.14039574010254
    - type: nauc_mrr_at_3_max
      value: 11.06996398110187
    - type: nauc_mrr_at_3_std
      value: 7.288759297085769
    - type: nauc_mrr_at_5_diff1
      value: 63.0401271126211
    - type: nauc_mrr_at_5_max
      value: 10.779317801858609
    - type: nauc_mrr_at_5_std
      value: 6.476660484760681
    - type: nauc_ndcg_at_1000_diff1
      value: 62.9544299483241
    - type: nauc_ndcg_at_1000_max
      value: 11.577079766964538
    - type: nauc_ndcg_at_1000_std
      value: 7.703856790100716
    - type: nauc_ndcg_at_100_diff1
      value: 62.9544299483241
    - type: nauc_ndcg_at_100_max
      value: 11.577079766964538
    - type: nauc_ndcg_at_100_std
      value: 7.703856790100716
    - type: nauc_ndcg_at_10_diff1
      value: 61.29907952217381
    - type: nauc_ndcg_at_10_max
      value: 14.760627422715425
    - type: nauc_ndcg_at_10_std
      value: 10.805573898143368
    - type: nauc_ndcg_at_1_diff1
      value: 66.1236265634718
    - type: nauc_ndcg_at_1_max
      value: 7.000207311173955
    - type: nauc_ndcg_at_1_std
      value: 6.54412272821497
    - type: nauc_ndcg_at_20_diff1
      value: 62.9544299483241
    - type: nauc_ndcg_at_20_max
      value: 11.577079766964538
    - type: nauc_ndcg_at_20_std
      value: 7.703856790100716
    - type: nauc_ndcg_at_3_diff1
      value: 60.25643527856101
    - type: nauc_ndcg_at_3_max
      value: 12.236302709487546
    - type: nauc_ndcg_at_3_std
      value: 7.36883189112067
    - type: nauc_ndcg_at_5_diff1
      value: 61.65220590318238
    - type: nauc_ndcg_at_5_max
      value: 11.39969101913945
    - type: nauc_ndcg_at_5_std
      value: 5.406207922379402
    - type: nauc_precision_at_1000_diff1
      value: .nan
    - type: nauc_precision_at_1000_max
      value: .nan
    - type: nauc_precision_at_1000_std
      value: .nan
    - type: nauc_precision_at_100_diff1
      value: .nan
    - type: nauc_precision_at_100_max
      value: .nan
    - type: nauc_precision_at_100_std
      value: .nan
    - type: nauc_precision_at_10_diff1
      value: 19.14098972922579
    - type: nauc_precision_at_10_max
      value: 100.0
    - type: nauc_precision_at_10_std
      value: 93.46405228758135
    - type: nauc_precision_at_1_diff1
      value: 66.1236265634718
    - type: nauc_precision_at_1_max
      value: 7.000207311173955
    - type: nauc_precision_at_1_std
      value: 6.54412272821497
    - type: nauc_precision_at_20_diff1
      value: 100.0
    - type: nauc_precision_at_20_max
      value: 100.0
    - type: nauc_precision_at_20_std
      value: 100.0
    - type: nauc_precision_at_3_diff1
      value: 50.29636629155561
    - type: nauc_precision_at_3_max
      value: 18.00532600292076
    - type: nauc_precision_at_3_std
      value: 7.649686453053768
    - type: nauc_precision_at_5_diff1
      value: 43.522408963585356
    - type: nauc_precision_at_5_max
      value: 16.923436041082983
    - type: nauc_precision_at_5_std
      value: -10.854341736694092
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: .nan
    - type: nauc_recall_at_100_max
      value: .nan
    - type: nauc_recall_at_100_std
      value: .nan
    - type: nauc_recall_at_10_diff1
      value: 19.1409897292252
    - type: nauc_recall_at_10_max
      value: 100.0
    - type: nauc_recall_at_10_std
      value: 93.46405228758134
    - type: nauc_recall_at_1_diff1
      value: 66.1236265634718
    - type: nauc_recall_at_1_max
      value: 7.000207311173955
    - type: nauc_recall_at_1_std
      value: 6.54412272821497
    - type: nauc_recall_at_20_diff1
      value: .nan
    - type: nauc_recall_at_20_max
      value: .nan
    - type: nauc_recall_at_20_std
      value: .nan
    - type: nauc_recall_at_3_diff1
      value: 50.29636629155569
    - type: nauc_recall_at_3_max
      value: 18.005326002920754
    - type: nauc_recall_at_3_std
      value: 7.649686453053851
    - type: nauc_recall_at_5_diff1
      value: 43.5224089635856
    - type: nauc_recall_at_5_max
      value: 16.92343604108335
    - type: nauc_recall_at_5_std
      value: -10.854341736694499
    - type: ndcg_at_1
      value: 68.0
    - type: ndcg_at_10
      value: 83.851
    - type: ndcg_at_100
      value: 84.36099999999999
    - type: ndcg_at_1000
      value: 84.36099999999999
    - type: ndcg_at_20
      value: 84.36099999999999
    - type: ndcg_at_3
      value: 80.333
    - type: ndcg_at_5
      value: 83.21600000000001
    - type: precision_at_1
      value: 68.0
    - type: precision_at_10
      value: 9.8
    - type: precision_at_100
      value: 1.0
    - type: precision_at_1000
      value: 0.1
    - type: precision_at_20
      value: 5.0
    - type: precision_at_3
      value: 29.666999999999998
    - type: precision_at_5
      value: 19.2
    - type: recall_at_1
      value: 68.0
    - type: recall_at_10
      value: 98.0
    - type: recall_at_100
      value: 100.0
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 100.0
    - type: recall_at_3
      value: 89.0
    - type: recall_at_5
      value: 96.0
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB T2Reranking (default)
      revision: 76631901a18387f85eaa53e5450019b87ad58ef9
      split: dev
      type: C-MTEB/T2Reranking
    metrics:
    - type: map
      value: 65.3088203970324
    - type: mrr
      value: 74.79505862376546
    - type: main_score
      value: 65.3088203970324
    task:
      type: Reranking
  - dataset:
      config: default
      name: MTEB T2Retrieval (default)
      revision: 8731a845f1bf500a4f111cf1070785c793d10e64
      split: dev
      type: C-MTEB/T2Retrieval
    metrics:
    - type: main_score
      value: 83.163
    - type: map_at_1
      value: 26.875
    - type: map_at_10
      value: 75.454
    - type: map_at_100
      value: 79.036
    - type: map_at_1000
      value: 79.111
    - type: map_at_20
      value: 78.145
    - type: map_at_3
      value: 53.181
    - type: map_at_5
      value: 65.362
    - type: mrr_at_1
      value: 88.90057864281957
    - type: mrr_at_10
      value: 91.53186397301344
    - type: mrr_at_100
      value: 91.62809075510003
    - type: mrr_at_1000
      value: 91.63198173030787
    - type: mrr_at_20
      value: 91.59414668799909
    - type: mrr_at_3
      value: 91.0792565316499
    - type: mrr_at_5
      value: 91.35718043135199
    - type: nauc_map_at_1000_diff1
      value: 12.364843957982409
    - type: nauc_map_at_1000_max
      value: 52.07043464458799
    - type: nauc_map_at_1000_std
      value: 16.040095055100494
    - type: nauc_map_at_100_diff1
      value: 12.370621073823022
    - type: nauc_map_at_100_max
      value: 51.960738727635636
    - type: nauc_map_at_100_std
      value: 15.935832440430747
    - type: nauc_map_at_10_diff1
      value: 16.852819486606585
    - type: nauc_map_at_10_max
      value: 40.11184760756059
    - type: nauc_map_at_10_std
      value: 0.9306648364102376
    - type: nauc_map_at_1_diff1
      value: 52.87356542654683
    - type: nauc_map_at_1_max
      value: -22.210039746171255
    - type: nauc_map_at_1_std
      value: -38.11345358035342
    - type: nauc_map_at_20_diff1
      value: 13.045089059562837
    - type: nauc_map_at_20_max
      value: 49.591383082160036
    - type: nauc_map_at_20_std
      value: 12.54330050352008
    - type: nauc_map_at_3_diff1
      value: 38.08172234377615
    - type: nauc_map_at_3_max
      value: -6.868621684867697
    - type: nauc_map_at_3_std
      value: -35.4712388845996
    - type: nauc_map_at_5_diff1
      value: 29.665551705577474
    - type: nauc_map_at_5_max
      value: 10.958628576519045
    - type: nauc_map_at_5_std
      value: -25.113120842097057
    - type: nauc_mrr_at_1000_diff1
      value: 47.39372999496945
    - type: nauc_mrr_at_1000_max
      value: 83.11274997493808
    - type: nauc_mrr_at_1000_std
      value: 39.74195374546631
    - type: nauc_mrr_at_100_diff1
      value: 47.396678946057676
    - type: nauc_mrr_at_100_max
      value: 83.1192584274415
    - type: nauc_mrr_at_100_std
      value: 39.75840860374685
    - type: nauc_mrr_at_10_diff1
      value: 47.35365644138715
    - type: nauc_mrr_at_10_max
      value: 83.189165639531
    - type: nauc_mrr_at_10_std
      value: 39.83653157887758
    - type: nauc_mrr_at_1_diff1
      value: 47.98740362820094
    - type: nauc_mrr_at_1_max
      value: 80.32340034580369
    - type: nauc_mrr_at_1_std
      value: 34.57857131423388
    - type: nauc_mrr_at_20_diff1
      value: 47.399132055537194
    - type: nauc_mrr_at_20_max
      value: 83.16329919869686
    - type: nauc_mrr_at_20_std
      value: 39.84204692042734
    - type: nauc_mrr_at_3_diff1
      value: 47.09295580511751
    - type: nauc_mrr_at_3_max
      value: 82.95831045602642
    - type: nauc_mrr_at_3_std
      value: 38.98036804692351
    - type: nauc_mrr_at_5_diff1
      value: 47.20100268549764
    - type: nauc_mrr_at_5_max
      value: 83.16652480381642
    - type: nauc_mrr_at_5_std
      value: 39.55690491560902
    - type: nauc_ndcg_at_1000_diff1
      value: 17.201962509184547
    - type: nauc_ndcg_at_1000_max
      value: 63.75820559259539
    - type: nauc_ndcg_at_1000_std
      value: 29.28676096486067
    - type: nauc_ndcg_at_100_diff1
      value: 16.76847216096811
    - type: nauc_ndcg_at_100_max
      value: 62.646517934470744
    - type: nauc_ndcg_at_100_std
      value: 28.7441617667637
    - type: nauc_ndcg_at_10_diff1
      value: 16.559511980751886
    - type: nauc_ndcg_at_10_max
      value: 54.35027464277944
    - type: nauc_ndcg_at_10_std
      value: 16.98089333577716
    - type: nauc_ndcg_at_1_diff1
      value: 47.98740362820094
    - type: nauc_ndcg_at_1_max
      value: 80.32340034580369
    - type: nauc_ndcg_at_1_std
      value: 34.57857131423388
    - type: nauc_ndcg_at_20_diff1
      value: 16.721525245428243
    - type: nauc_ndcg_at_20_max
      value: 57.683661870555724
    - type: nauc_ndcg_at_20_std
      value: 21.736044200026853
    - type: nauc_ndcg_at_3_diff1
      value: 12.488009696556192
    - type: nauc_ndcg_at_3_max
      value: 69.2365575305502
    - type: nauc_ndcg_at_3_std
      value: 30.622418945055323
    - type: nauc_ndcg_at_5_diff1
      value: 12.364114556230609
    - type: nauc_ndcg_at_5_max
      value: 62.33360746285387
    - type: nauc_ndcg_at_5_std
      value: 24.898000803570227
    - type: nauc_precision_at_1000_diff1
      value: -35.14745130154524
    - type: nauc_precision_at_1000_max
      value: 48.811507982849065
    - type: nauc_precision_at_1000_std
      value: 62.43036496029399
    - type: nauc_precision_at_100_diff1
      value: -35.15276411320076
    - type: nauc_precision_at_100_max
      value: 50.87010333741109
    - type: nauc_precision_at_100_std
      value: 63.418221030407175
    - type: nauc_precision_at_10_diff1
      value: -34.84255710936113
    - type: nauc_precision_at_10_max
      value: 56.588401051428825
    - type: nauc_precision_at_10_std
      value: 57.4763370653757
    - type: nauc_precision_at_1_diff1
      value: 47.98740362820094
    - type: nauc_precision_at_1_max
      value: 80.32340034580369
    - type: nauc_precision_at_1_std
      value: 34.57857131423388
    - type: nauc_precision_at_20_diff1
      value: -35.165762365233505
    - type: nauc_precision_at_20_max
      value: 54.148762449660424
    - type: nauc_precision_at_20_std
      value: 61.569719669368716
    - type: nauc_precision_at_3_diff1
      value: -28.63023175340299
    - type: nauc_precision_at_3_max
      value: 68.69825987618499
    - type: nauc_precision_at_3_std
      value: 48.15479495755423
    - type: nauc_precision_at_5_diff1
      value: -34.13811355456687
    - type: nauc_precision_at_5_max
      value: 62.369363941490604
    - type: nauc_precision_at_5_std
      value: 52.282904411187914
    - type: nauc_recall_at_1000_diff1
      value: 8.686444579162663
    - type: nauc_recall_at_1000_max
      value: 59.58864478011338
    - type: nauc_recall_at_1000_std
      value: 56.692774954297455
    - type: nauc_recall_at_100_diff1
      value: 8.820596225758342
    - type: nauc_recall_at_100_max
      value: 53.15048885657892
    - type: nauc_recall_at_100_std
      value: 39.78931159236714
    - type: nauc_recall_at_10_diff1
      value: 16.022301106315027
    - type: nauc_recall_at_10_max
      value: 29.83242342459543
    - type: nauc_recall_at_10_std
      value: -4.805965555875844
    - type: nauc_recall_at_1_diff1
      value: 52.87356542654683
    - type: nauc_recall_at_1_max
      value: -22.210039746171255
    - type: nauc_recall_at_1_std
      value: -38.11345358035342
    - type: nauc_recall_at_20_diff1
      value: 10.35772828627265
    - type: nauc_recall_at_20_max
      value: 43.06420839754062
    - type: nauc_recall_at_20_std
      value: 15.040522218235692
    - type: nauc_recall_at_3_diff1
      value: 36.23953684770224
    - type: nauc_recall_at_3_max
      value: -11.709269151700374
    - type: nauc_recall_at_3_std
      value: -38.13943178150384
    - type: nauc_recall_at_5_diff1
      value: 28.644872415763384
    - type: nauc_recall_at_5_max
      value: 2.062151266111129
    - type: nauc_recall_at_5_std
      value: -30.81114034774277
    - type: ndcg_at_1
      value: 88.901
    - type: ndcg_at_10
      value: 83.163
    - type: ndcg_at_100
      value: 86.854
    - type: ndcg_at_1000
      value: 87.602
    - type: ndcg_at_20
      value: 84.908
    - type: ndcg_at_3
      value: 84.848
    - type: ndcg_at_5
      value: 83.372
    - type: precision_at_1
      value: 88.901
    - type: precision_at_10
      value: 41.343
    - type: precision_at_100
      value: 4.957000000000001
    - type: precision_at_1000
      value: 0.513
    - type: precision_at_20
      value: 22.955000000000002
    - type: precision_at_3
      value: 74.29599999999999
    - type: precision_at_5
      value: 62.251999999999995
    - type: recall_at_1
      value: 26.875
    - type: recall_at_10
      value: 81.902
    - type: recall_at_100
      value: 93.988
    - type: recall_at_1000
      value: 97.801
    - type: recall_at_20
      value: 87.809
    - type: recall_at_3
      value: 54.869
    - type: recall_at_5
      value: 68.728
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB TERRa (default)
      revision: 7b58f24536063837d644aab9a023c62199b2a612
      split: dev
      type: ai-forever/terra-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 60.586319218241044
    - type: cosine_accuracy_threshold
      value: 82.49806761741638
    - type: cosine_ap
      value: 58.73198048427448
    - type: cosine_f1
      value: 67.37967914438502
    - type: cosine_f1_threshold
      value: 77.46461033821106
    - type: cosine_precision
      value: 57.01357466063348
    - type: cosine_recall
      value: 82.35294117647058
    - type: dot_accuracy
      value: 60.26058631921825
    - type: dot_accuracy_threshold
      value: 35627.020263671875
    - type: dot_ap
      value: 57.418783612898224
    - type: dot_f1
      value: 66.51982378854623
    - type: dot_f1_threshold
      value: 27620.843505859375
    - type: dot_precision
      value: 50.16611295681063
    - type: dot_recall
      value: 98.69281045751634
    - type: euclidean_accuracy
      value: 60.26058631921825
    - type: euclidean_accuracy_threshold
      value: 1255.4466247558594
    - type: euclidean_ap
      value: 58.748656145387955
    - type: euclidean_f1
      value: 66.99029126213591
    - type: euclidean_f1_threshold
      value: 1565.1330947875977
    - type: euclidean_precision
      value: 53.28185328185329
    - type: euclidean_recall
      value: 90.19607843137256
    - type: main_score
      value: 58.8479126365766
    - type: manhattan_accuracy
      value: 59.934853420195445
    - type: manhattan_accuracy_threshold
      value: 29897.271728515625
    - type: manhattan_ap
      value: 58.8479126365766
    - type: manhattan_f1
      value: 66.81318681318683
    - type: manhattan_f1_threshold
      value: 46291.802978515625
    - type: manhattan_precision
      value: 50.331125827814574
    - type: manhattan_recall
      value: 99.34640522875817
    - type: max_accuracy
      value: 60.586319218241044
    - type: max_ap
      value: 58.8479126365766
    - type: max_f1
      value: 67.37967914438502
    - type: max_precision
      value: 57.01357466063348
    - type: max_recall
      value: 99.34640522875817
    - type: similarity_accuracy
      value: 60.586319218241044
    - type: similarity_accuracy_threshold
      value: 82.49806761741638
    - type: similarity_ap
      value: 58.73198048427448
    - type: similarity_f1
      value: 67.37967914438502
    - type: similarity_f1_threshold
      value: 77.46461033821106
    - type: similarity_precision
      value: 57.01357466063348
    - type: similarity_recall
      value: 82.35294117647058
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB TNews (default)
      revision: 317f262bf1e6126357bbe89e875451e4b0938fe4
      split: validation
      type: C-MTEB/TNews-classification
    metrics:
    - type: accuracy
      value: 45.967999999999996
    - type: f1
      value: 44.699306100915706
    - type: f1_weighted
      value: 46.03730319014832
    - type: main_score
      value: 45.967999999999996
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB TRECCOVID (default)
      revision: bb9466bac8153a0349341eb1b22e06409e78ef4e
      split: test
      type: mteb/trec-covid
    metrics:
    - type: map_at_1
      value: 0.251
    - type: map_at_10
      value: 1.9480000000000002
    - type: map_at_100
      value: 11.082
    - type: map_at_1000
      value: 26.700000000000003
    - type: map_at_20
      value: 3.3529999999999998
    - type: map_at_3
      value: 0.679
    - type: map_at_5
      value: 1.079
    - type: mrr_at_1
      value: 94.0
    - type: mrr_at_10
      value: 95.786
    - type: mrr_at_100
      value: 95.786
    - type: mrr_at_1000
      value: 95.786
    - type: mrr_at_20
      value: 95.786
    - type: mrr_at_3
      value: 95.0
    - type: mrr_at_5
      value: 95.5
    - type: ndcg_at_1
      value: 91.0
    - type: ndcg_at_10
      value: 77.71900000000001
    - type: ndcg_at_100
      value: 57.726
    - type: ndcg_at_1000
      value: 52.737
    - type: ndcg_at_20
      value: 72.54
    - type: ndcg_at_3
      value: 83.397
    - type: ndcg_at_5
      value: 80.806
    - type: precision_at_1
      value: 94.0
    - type: precision_at_10
      value: 81.0
    - type: precision_at_100
      value: 59.199999999999996
    - type: precision_at_1000
      value: 23.244
    - type: precision_at_20
      value: 75.2
    - type: precision_at_3
      value: 88.0
    - type: precision_at_5
      value: 84.8
    - type: recall_at_1
      value: 0.251
    - type: recall_at_10
      value: 2.1229999999999998
    - type: recall_at_100
      value: 14.496999999999998
    - type: recall_at_1000
      value: 50.09
    - type: recall_at_20
      value: 3.8309999999999995
    - type: recall_at_3
      value: 0.696
    - type: recall_at_5
      value: 1.1400000000000001
    - type: main_score
      value: 77.71900000000001
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB TenKGnadClusteringP2P (default)
      revision: 5c59e41555244b7e45c9a6be2d720ab4bafae558
      split: test
      type: slvnwhrl/tenkgnad-clustering-p2p
    metrics:
    - type: main_score
      value: 43.763609722295215
    - type: v_measure
      value: 43.763609722295215
    - type: v_measure_std
      value: 2.8751199473862457
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB TenKGnadClusteringS2S (default)
      revision: 6cddbe003f12b9b140aec477b583ac4191f01786
      split: test
      type: slvnwhrl/tenkgnad-clustering-s2s
    metrics:
    - type: main_score
      value: 39.762424448504355
    - type: v_measure
      value: 39.762424448504355
    - type: v_measure_std
      value: 3.30146124979502
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB ThuNewsClusteringP2P (default)
      revision: 5798586b105c0434e4f0fe5e767abe619442cf93
      split: test
      type: C-MTEB/ThuNewsClusteringP2P
    metrics:
    - type: main_score
      value: 63.133819258289456
    - type: v_measure
      value: 63.133819258289456
    - type: v_measure_std
      value: 1.8854253356479695
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB ThuNewsClusteringS2S (default)
      revision: 8a8b2caeda43f39e13c4bc5bea0f8a667896e10d
      split: test
      type: C-MTEB/ThuNewsClusteringS2S
    metrics:
    - type: main_score
      value: 58.98195851785808
    - type: v_measure
      value: 58.98195851785808
    - type: v_measure_std
      value: 1.6237600076393737
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB Touche2020 (default)
      revision: a34f9a33db75fa0cbb21bb5cfc3dae8dc8bec93f
      split: test
      type: mteb/touche2020
    metrics:
    - type: map_at_1
      value: 3.3550000000000004
    - type: map_at_10
      value: 10.08
    - type: map_at_100
      value: 16.136
    - type: map_at_1000
      value: 17.605
    - type: map_at_20
      value: 12.561
    - type: map_at_3
      value: 5.641
    - type: map_at_5
      value: 7.3260000000000005
    - type: mrr_at_1
      value: 46.939
    - type: mrr_at_10
      value: 58.152
    - type: mrr_at_100
      value: 58.594
    - type: mrr_at_1000
      value: 58.601000000000006
    - type: mrr_at_20
      value: 58.279
    - type: mrr_at_3
      value: 55.102
    - type: mrr_at_5
      value: 56.531
    - type: ndcg_at_1
      value: 44.897999999999996
    - type: ndcg_at_10
      value: 26.298
    - type: ndcg_at_100
      value: 37.596000000000004
    - type: ndcg_at_1000
      value: 49.424
    - type: ndcg_at_20
      value: 27.066000000000003
    - type: ndcg_at_3
      value: 31.528
    - type: ndcg_at_5
      value: 28.219
    - type: precision_at_1
      value: 46.939
    - type: precision_at_10
      value: 22.245
    - type: precision_at_100
      value: 7.531000000000001
    - type: precision_at_1000
      value: 1.5350000000000001
    - type: precision_at_20
      value: 17.041
    - type: precision_at_3
      value: 30.612000000000002
    - type: precision_at_5
      value: 26.122
    - type: recall_at_1
      value: 3.3550000000000004
    - type: recall_at_10
      value: 16.41
    - type: recall_at_100
      value: 47.272
    - type: recall_at_1000
      value: 83.584
    - type: recall_at_20
      value: 24.091
    - type: recall_at_3
      value: 6.8180000000000005
    - type: recall_at_5
      value: 9.677
    - type: main_score
      value: 26.298
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB ToxicConversationsClassification (default)
      revision: edfaf9da55d3dd50d43143d90c1ac476895ae6de
      split: test
      type: mteb/toxic_conversations_50k
    metrics:
    - type: accuracy
      value: 91.2890625
    - type: ap
      value: 33.95547153875715
    - type: ap_weighted
      value: 33.95547153875715
    - type: f1
      value: 75.10768597556462
    - type: f1_weighted
      value: 92.00161208992606
    - type: main_score
      value: 91.2890625
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB TweetSentimentExtractionClassification (default)
      revision: d604517c81ca91fe16a244d1248fc021f9ecee7a
      split: test
      type: mteb/tweet_sentiment_extraction
    metrics:
    - type: accuracy
      value: 71.3978494623656
    - type: f1
      value: 71.7194818511814
    - type: f1_weighted
      value: 71.13860187349744
    - type: main_score
      value: 71.3978494623656
    task:
      type: Classification
  - dataset:
      config: default
      name: MTEB TwentyNewsgroupsClustering (default)
      revision: 6125ec4e24fa026cec8a478383ee943acfbd5449
      split: test
      type: mteb/twentynewsgroups-clustering
    metrics:
    - type: main_score
      value: 52.4921688720602
    - type: v_measure
      value: 52.4921688720602
    - type: v_measure_std
      value: 0.992768152658908
    task:
      type: Clustering
  - dataset:
      config: default
      name: MTEB TwitterSemEval2015 (default)
      revision: 70970daeab8776df92f5ea462b6173c0b46fd2d1
      split: test
      type: mteb/twittersemeval2015-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 85.11652858079513
    - type: cosine_accuracy_threshold
      value: 87.90839910507202
    - type: cosine_ap
      value: 70.90459908851724
    - type: cosine_f1
      value: 65.66581227877457
    - type: cosine_f1_threshold
      value: 85.13308763504028
    - type: cosine_precision
      value: 61.094708153531684
    - type: cosine_recall
      value: 70.97625329815304
    - type: dot_accuracy
      value: 83.41181379269239
    - type: dot_accuracy_threshold
      value: 43110.113525390625
    - type: dot_ap
      value: 65.64869491143095
    - type: dot_f1
      value: 62.05308447460914
    - type: dot_f1_threshold
      value: 41412.542724609375
    - type: dot_precision
      value: 57.38623626989464
    - type: dot_recall
      value: 67.54617414248021
    - type: euclidean_accuracy
      value: 85.15229182809799
    - type: euclidean_accuracy_threshold
      value: 1043.08500289917
    - type: euclidean_ap
      value: 70.71204383269375
    - type: euclidean_f1
      value: 65.20304568527919
    - type: euclidean_f1_threshold
      value: 1179.2595863342285
    - type: euclidean_precision
      value: 62.81173594132029
    - type: euclidean_recall
      value: 67.78364116094987
    - type: main_score
      value: 70.90459908851724
    - type: manhattan_accuracy
      value: 85.1820945341837
    - type: manhattan_accuracy_threshold
      value: 26115.0390625
    - type: manhattan_ap
      value: 70.66113937117431
    - type: manhattan_f1
      value: 65.33383628819313
    - type: manhattan_f1_threshold
      value: 29105.181884765625
    - type: manhattan_precision
      value: 62.40691808791736
    - type: manhattan_recall
      value: 68.54881266490766
    - type: max_accuracy
      value: 85.1820945341837
    - type: max_ap
      value: 70.90459908851724
    - type: max_f1
      value: 65.66581227877457
    - type: max_precision
      value: 62.81173594132029
    - type: max_recall
      value: 70.97625329815304
    - type: similarity_accuracy
      value: 85.11652858079513
    - type: similarity_accuracy_threshold
      value: 87.90839910507202
    - type: similarity_ap
      value: 70.90459908851724
    - type: similarity_f1
      value: 65.66581227877457
    - type: similarity_f1_threshold
      value: 85.13308763504028
    - type: similarity_precision
      value: 61.094708153531684
    - type: similarity_recall
      value: 70.97625329815304
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB TwitterURLCorpus (default)
      revision: 8b6510b0b1fa4e4c4f879467980e9be563ec1cdf
      split: test
      type: mteb/twitterurlcorpus-pairclassification
    metrics:
    - type: cosine_accuracy
      value: 88.10299996119068
    - type: cosine_accuracy_threshold
      value: 84.34982895851135
    - type: cosine_ap
      value: 84.13755787769226
    - type: cosine_f1
      value: 76.0967548076923
    - type: cosine_f1_threshold
      value: 82.8936219215393
    - type: cosine_precision
      value: 74.28864769727193
    - type: cosine_recall
      value: 77.99507237449954
    - type: dot_accuracy
      value: 86.64182869561843
    - type: dot_accuracy_threshold
      value: 38794.677734375
    - type: dot_ap
      value: 80.20301567411457
    - type: dot_f1
      value: 73.50650291634967
    - type: dot_f1_threshold
      value: 37447.23205566406
    - type: dot_precision
      value: 69.41498460485802
    - type: dot_recall
      value: 78.11056359716662
    - type: euclidean_accuracy
      value: 87.9361198432103
    - type: euclidean_accuracy_threshold
      value: 1184.421157836914
    - type: euclidean_ap
      value: 83.79582690117218
    - type: euclidean_f1
      value: 75.81431709042175
    - type: euclidean_f1_threshold
      value: 1258.2727432250977
    - type: euclidean_precision
      value: 73.39099099099099
    - type: euclidean_recall
      value: 78.40314136125654
    - type: main_score
      value: 84.13755787769226
    - type: manhattan_accuracy
      value: 87.96134590755618
    - type: manhattan_accuracy_threshold
      value: 29077.291870117188
    - type: manhattan_ap
      value: 83.79487172269923
    - type: manhattan_f1
      value: 75.82421603424935
    - type: manhattan_f1_threshold
      value: 31224.124145507812
    - type: manhattan_precision
      value: 72.24740255212329
    - type: manhattan_recall
      value: 79.77363720357253
    - type: max_accuracy
      value: 88.10299996119068
    - type: max_ap
      value: 84.13755787769226
    - type: max_f1
      value: 76.0967548076923
    - type: max_precision
      value: 74.28864769727193
    - type: max_recall
      value: 79.77363720357253
    - type: similarity_accuracy
      value: 88.10299996119068
    - type: similarity_accuracy_threshold
      value: 84.34982895851135
    - type: similarity_ap
      value: 84.13755787769226
    - type: similarity_f1
      value: 76.0967548076923
    - type: similarity_f1_threshold
      value: 82.8936219215393
    - type: similarity_precision
      value: 74.28864769727193
    - type: similarity_recall
      value: 77.99507237449954
    task:
      type: PairClassification
  - dataset:
      config: default
      name: MTEB VideoRetrieval (default)
      revision: 58c2597a5943a2ba48f4668c3b90d796283c5639
      split: dev
      type: C-MTEB/VideoRetrieval
    metrics:
    - type: main_score
      value: 70.433
    - type: map_at_1
      value: 55.7
    - type: map_at_10
      value: 66.013
    - type: map_at_100
      value: 66.534
    - type: map_at_1000
      value: 66.547
    - type: map_at_20
      value: 66.334
    - type: map_at_3
      value: 64.2
    - type: map_at_5
      value: 65.445
    - type: mrr_at_1
      value: 55.7
    - type: mrr_at_10
      value: 66.01329365079364
    - type: mrr_at_100
      value: 66.53350061744233
    - type: mrr_at_1000
      value: 66.54744831962995
    - type: mrr_at_20
      value: 66.3335147364675
    - type: mrr_at_3
      value: 64.2
    - type: mrr_at_5
      value: 65.44500000000002
    - type: nauc_map_at_1000_diff1
      value: 76.26428836976245
    - type: nauc_map_at_1000_max
      value: 35.41847367373575
    - type: nauc_map_at_1000_std
      value: -33.04639860831992
    - type: nauc_map_at_100_diff1
      value: 76.25793229023193
    - type: nauc_map_at_100_max
      value: 35.43663260110076
    - type: nauc_map_at_100_std
      value: -33.04238139882945
    - type: nauc_map_at_10_diff1
      value: 76.2108281297711
    - type: nauc_map_at_10_max
      value: 35.59442419423183
    - type: nauc_map_at_10_std
      value: -33.32346518997277
    - type: nauc_map_at_1_diff1
      value: 79.17728405262736
    - type: nauc_map_at_1_max
      value: 31.880738163589527
    - type: nauc_map_at_1_std
      value: -30.891888718004584
    - type: nauc_map_at_20_diff1
      value: 76.2181333410193
    - type: nauc_map_at_20_max
      value: 35.43448818430876
    - type: nauc_map_at_20_std
      value: -33.35682442863193
    - type: nauc_map_at_3_diff1
      value: 76.10046541433466
    - type: nauc_map_at_3_max
      value: 34.6831278555291
    - type: nauc_map_at_3_std
      value: -34.030826044831116
    - type: nauc_map_at_5_diff1
      value: 75.96513023582064
    - type: nauc_map_at_5_max
      value: 34.66920832438069
    - type: nauc_map_at_5_std
      value: -33.79799777830796
    - type: nauc_mrr_at_1000_diff1
      value: 76.26428836976245
    - type: nauc_mrr_at_1000_max
      value: 35.41847367373575
    - type: nauc_mrr_at_1000_std
      value: -33.04639860831992
    - type: nauc_mrr_at_100_diff1
      value: 76.25793229023193
    - type: nauc_mrr_at_100_max
      value: 35.43663260110076
    - type: nauc_mrr_at_100_std
      value: -33.04238139882945
    - type: nauc_mrr_at_10_diff1
      value: 76.2108281297711
    - type: nauc_mrr_at_10_max
      value: 35.59442419423183
    - type: nauc_mrr_at_10_std
      value: -33.32346518997277
    - type: nauc_mrr_at_1_diff1
      value: 79.17728405262736
    - type: nauc_mrr_at_1_max
      value: 31.880738163589527
    - type: nauc_mrr_at_1_std
      value: -30.891888718004584
    - type: nauc_mrr_at_20_diff1
      value: 76.2181333410193
    - type: nauc_mrr_at_20_max
      value: 35.43448818430876
    - type: nauc_mrr_at_20_std
      value: -33.35682442863193
    - type: nauc_mrr_at_3_diff1
      value: 76.10046541433466
    - type: nauc_mrr_at_3_max
      value: 34.6831278555291
    - type: nauc_mrr_at_3_std
      value: -34.030826044831116
    - type: nauc_mrr_at_5_diff1
      value: 75.96513023582064
    - type: nauc_mrr_at_5_max
      value: 34.66920832438069
    - type: nauc_mrr_at_5_std
      value: -33.79799777830796
    - type: nauc_ndcg_at_1000_diff1
      value: 75.68118206798317
    - type: nauc_ndcg_at_1000_max
      value: 37.12252980787349
    - type: nauc_ndcg_at_1000_std
      value: -31.457578337430505
    - type: nauc_ndcg_at_100_diff1
      value: 75.46730761564156
    - type: nauc_ndcg_at_100_max
      value: 37.549890025544265
    - type: nauc_ndcg_at_100_std
      value: -31.35066985945112
    - type: nauc_ndcg_at_10_diff1
      value: 75.09890404887037
    - type: nauc_ndcg_at_10_max
      value: 38.024147790014204
    - type: nauc_ndcg_at_10_std
      value: -33.67408368593356
    - type: nauc_ndcg_at_1_diff1
      value: 79.17728405262736
    - type: nauc_ndcg_at_1_max
      value: 31.880738163589527
    - type: nauc_ndcg_at_1_std
      value: -30.891888718004584
    - type: nauc_ndcg_at_20_diff1
      value: 75.12977548171354
    - type: nauc_ndcg_at_20_max
      value: 37.524926748917956
    - type: nauc_ndcg_at_20_std
      value: -33.771344674947485
    - type: nauc_ndcg_at_3_diff1
      value: 74.94037476984154
    - type: nauc_ndcg_at_3_max
      value: 35.60345554050552
    - type: nauc_ndcg_at_3_std
      value: -35.256991346321854
    - type: nauc_ndcg_at_5_diff1
      value: 74.54265907753783
    - type: nauc_ndcg_at_5_max
      value: 35.57662819978585
    - type: nauc_ndcg_at_5_std
      value: -34.879794448418465
    - type: nauc_precision_at_1000_diff1
      value: 74.52277207179142
    - type: nauc_precision_at_1000_max
      value: 94.25510945118707
    - type: nauc_precision_at_1000_std
      value: 91.6874157070222
    - type: nauc_precision_at_100_diff1
      value: 65.98346655735419
    - type: nauc_precision_at_100_max
      value: 78.81168727653687
    - type: nauc_precision_at_100_std
      value: 27.241465691967708
    - type: nauc_precision_at_10_diff1
      value: 69.55050319096688
    - type: nauc_precision_at_10_max
      value: 51.827749140893374
    - type: nauc_precision_at_10_std
      value: -34.60818605792837
    - type: nauc_precision_at_1_diff1
      value: 79.17728405262736
    - type: nauc_precision_at_1_max
      value: 31.880738163589527
    - type: nauc_precision_at_1_std
      value: -30.891888718004584
    - type: nauc_precision_at_20_diff1
      value: 68.08078305042736
    - type: nauc_precision_at_20_max
      value: 52.83318878288501
    - type: nauc_precision_at_20_std
      value: -35.46070292817927
    - type: nauc_precision_at_3_diff1
      value: 70.76249609881901
    - type: nauc_precision_at_3_max
      value: 38.86561868624655
    - type: nauc_precision_at_3_std
      value: -39.68917853446992
    - type: nauc_precision_at_5_diff1
      value: 68.39110629013278
    - type: nauc_precision_at_5_max
      value: 39.28677163904683
    - type: nauc_precision_at_5_std
      value: -39.39101423819562
    - type: nauc_recall_at_1000_diff1
      value: 74.52277207179175
    - type: nauc_recall_at_1000_max
      value: 94.25510945118776
    - type: nauc_recall_at_1000_std
      value: 91.68741570702382
    - type: nauc_recall_at_100_diff1
      value: 65.9834665573548
    - type: nauc_recall_at_100_max
      value: 78.81168727653679
    - type: nauc_recall_at_100_std
      value: 27.241465691967598
    - type: nauc_recall_at_10_diff1
      value: 69.55050319096708
    - type: nauc_recall_at_10_max
      value: 51.82774914089347
    - type: nauc_recall_at_10_std
      value: -34.6081860579283
    - type: nauc_recall_at_1_diff1
      value: 79.17728405262736
    - type: nauc_recall_at_1_max
      value: 31.880738163589527
    - type: nauc_recall_at_1_std
      value: -30.891888718004584
    - type: nauc_recall_at_20_diff1
      value: 68.08078305042746
    - type: nauc_recall_at_20_max
      value: 52.833188782885244
    - type: nauc_recall_at_20_std
      value: -35.46070292817895
    - type: nauc_recall_at_3_diff1
      value: 70.76249609881896
    - type: nauc_recall_at_3_max
      value: 38.865618686246464
    - type: nauc_recall_at_3_std
      value: -39.68917853446999
    - type: nauc_recall_at_5_diff1
      value: 68.39110629013274
    - type: nauc_recall_at_5_max
      value: 39.28677163904688
    - type: nauc_recall_at_5_std
      value: -39.39101423819562
    - type: ndcg_at_1
      value: 55.7
    - type: ndcg_at_10
      value: 70.433
    - type: ndcg_at_100
      value: 72.975
    - type: ndcg_at_1000
      value: 73.283
    - type: ndcg_at_20
      value: 71.58
    - type: ndcg_at_3
      value: 66.83099999999999
    - type: ndcg_at_5
      value: 69.085
    - type: precision_at_1
      value: 55.7
    - type: precision_at_10
      value: 8.4
    - type: precision_at_100
      value: 0.959
    - type: precision_at_1000
      value: 0.098
    - type: precision_at_20
      value: 4.425
    - type: precision_at_3
      value: 24.8
    - type: precision_at_5
      value: 15.98
    - type: recall_at_1
      value: 55.7
    - type: recall_at_10
      value: 84.0
    - type: recall_at_100
      value: 95.89999999999999
    - type: recall_at_1000
      value: 98.2
    - type: recall_at_20
      value: 88.5
    - type: recall_at_3
      value: 74.4
    - type: recall_at_5
      value: 79.9
    task:
      type: Retrieval
  - dataset:
      config: default
      name: MTEB Waimai (default)
      revision: 339287def212450dcaa9df8c22bf93e9980c7023
      split: test
      type: C-MTEB/waimai-classification
    metrics:
    - type: accuracy
      value: 86.58999999999999
    - type: ap
      value: 70.02619249927523
    - type: ap_weighted
      value: 70.02619249927523
    - type: f1
      value: 84.97572770889423
    - type: f1_weighted
      value: 86.6865713531272
    - type: main_score
      value: 86.58999999999999
    task:
      type: Classification
  - dataset:
      config: en
      name: MTEB XMarket (en)
      revision: dfe57acff5b62c23732a7b7d3e3fb84ff501708b
      split: test
      type: jinaai/xmarket_ml
    metrics:
    - type: main_score
      value: 34.772999999999996
    - type: map_at_1
      value: 7.2620000000000005
    - type: map_at_10
      value: 17.98
    - type: map_at_100
      value: 24.828
    - type: map_at_1000
      value: 26.633000000000003
    - type: map_at_20
      value: 20.699
    - type: map_at_3
      value: 12.383
    - type: map_at_5
      value: 14.871
    - type: mrr_at_1
      value: 34.718100890207715
    - type: mrr_at_10
      value: 43.9336827525092
    - type: mrr_at_100
      value: 44.66474011066837
    - type: mrr_at_1000
      value: 44.7075592197356
    - type: mrr_at_20
      value: 44.35984436569346
    - type: mrr_at_3
      value: 41.73901893981052
    - type: mrr_at_5
      value: 43.025973550207134
    - type: nauc_map_at_1000_diff1
      value: 13.899869081196364
    - type: nauc_map_at_1000_max
      value: 46.60452816386231
    - type: nauc_map_at_1000_std
      value: 24.87925799401773
    - type: nauc_map_at_100_diff1
      value: 16.164805650871084
    - type: nauc_map_at_100_max
      value: 44.720912958558095
    - type: nauc_map_at_100_std
      value: 20.236734536210477
    - type: nauc_map_at_10_diff1
      value: 23.58580520913581
    - type: nauc_map_at_10_max
      value: 31.276151869914216
    - type: nauc_map_at_10_std
      value: -0.1833326246041355
    - type: nauc_map_at_1_diff1
      value: 37.02663305598722
    - type: nauc_map_at_1_max
      value: 14.931071531116528
    - type: nauc_map_at_1_std
      value: -12.478790028708453
    - type: nauc_map_at_20_diff1
      value: 20.718297881540593
    - type: nauc_map_at_20_max
      value: 36.62264094841859
    - type: nauc_map_at_20_std
      value: 6.658514770057742
    - type: nauc_map_at_3_diff1
      value: 29.379034581120006
    - type: nauc_map_at_3_max
      value: 21.387214269548803
    - type: nauc_map_at_3_std
      value: -9.3404121914247
    - type: nauc_map_at_5_diff1
      value: 26.627169792839485
    - type: nauc_map_at_5_max
      value: 25.393331109666388
    - type: nauc_map_at_5_std
      value: -6.023485287246353
    - type: nauc_mrr_at_1000_diff1
      value: 12.047232036652295
    - type: nauc_mrr_at_1000_max
      value: 46.611862580860645
    - type: nauc_mrr_at_1000_std
      value: 27.89146066442305
    - type: nauc_mrr_at_100_diff1
      value: 12.05261747449997
    - type: nauc_mrr_at_100_max
      value: 46.61328535381203
    - type: nauc_mrr_at_100_std
      value: 27.886145596874535
    - type: nauc_mrr_at_10_diff1
      value: 12.006935553036941
    - type: nauc_mrr_at_10_max
      value: 46.53351686240496
    - type: nauc_mrr_at_10_std
      value: 27.708742470257462
    - type: nauc_mrr_at_1_diff1
      value: 13.323408127738782
    - type: nauc_mrr_at_1_max
      value: 43.78884661002012
    - type: nauc_mrr_at_1_std
      value: 25.164417588165673
    - type: nauc_mrr_at_20_diff1
      value: 12.036022973968011
    - type: nauc_mrr_at_20_max
      value: 46.56537838037131
    - type: nauc_mrr_at_20_std
      value: 27.78189157249635
    - type: nauc_mrr_at_3_diff1
      value: 11.943896700976381
    - type: nauc_mrr_at_3_max
      value: 46.33644663073225
    - type: nauc_mrr_at_3_std
      value: 27.523915405053845
    - type: nauc_mrr_at_5_diff1
      value: 12.03108009033769
    - type: nauc_mrr_at_5_max
      value: 46.49103616896692
    - type: nauc_mrr_at_5_std
      value: 27.630879129863366
    - type: nauc_ndcg_at_1000_diff1
      value: 9.766823796017324
    - type: nauc_ndcg_at_1000_max
      value: 52.85844801910602
    - type: nauc_ndcg_at_1000_std
      value: 36.43271437761207
    - type: nauc_ndcg_at_100_diff1
      value: 12.035059298282036
    - type: nauc_ndcg_at_100_max
      value: 50.05520240705682
    - type: nauc_ndcg_at_100_std
      value: 29.87678724506636
    - type: nauc_ndcg_at_10_diff1
      value: 10.281893031139424
    - type: nauc_ndcg_at_10_max
      value: 47.02153679426017
    - type: nauc_ndcg_at_10_std
      value: 26.624948330369126
    - type: nauc_ndcg_at_1_diff1
      value: 13.323408127738782
    - type: nauc_ndcg_at_1_max
      value: 43.78884661002012
    - type: nauc_ndcg_at_1_std
      value: 25.164417588165673
    - type: nauc_ndcg_at_20_diff1
      value: 11.463524849646598
    - type: nauc_ndcg_at_20_max
      value: 47.415073186019704
    - type: nauc_ndcg_at_20_std
      value: 26.359019620164307
    - type: nauc_ndcg_at_3_diff1
      value: 9.689199913805394
    - type: nauc_ndcg_at_3_max
      value: 45.68151849572808
    - type: nauc_ndcg_at_3_std
      value: 26.559193219799486
    - type: nauc_ndcg_at_5_diff1
      value: 9.448823370356575
    - type: nauc_ndcg_at_5_max
      value: 46.19999662690141
    - type: nauc_ndcg_at_5_std
      value: 26.8411706726069
    - type: nauc_precision_at_1000_diff1
      value: -20.379065598727024
    - type: nauc_precision_at_1000_max
      value: 13.162562437268427
    - type: nauc_precision_at_1000_std
      value: 22.658226157785812
    - type: nauc_precision_at_100_diff1
      value: -16.458155977309282
    - type: nauc_precision_at_100_max
      value: 35.97956789169889
    - type: nauc_precision_at_100_std
      value: 48.878375009979194
    - type: nauc_precision_at_10_diff1
      value: -7.810992317607771
    - type: nauc_precision_at_10_max
      value: 49.307339277444754
    - type: nauc_precision_at_10_std
      value: 42.82533951854582
    - type: nauc_precision_at_1_diff1
      value: 13.323408127738782
    - type: nauc_precision_at_1_max
      value: 43.78884661002012
    - type: nauc_precision_at_1_std
      value: 25.164417588165673
    - type: nauc_precision_at_20_diff1
      value: -11.43933465149542
    - type: nauc_precision_at_20_max
      value: 46.93722753460038
    - type: nauc_precision_at_20_std
      value: 47.36223769029678
    - type: nauc_precision_at_3_diff1
      value: 1.3230178593599737
    - type: nauc_precision_at_3_max
      value: 48.49039534395576
    - type: nauc_precision_at_3_std
      value: 33.161384183129194
    - type: nauc_precision_at_5_diff1
      value: -3.185516457926519
    - type: nauc_precision_at_5_max
      value: 49.5814309394308
    - type: nauc_precision_at_5_std
      value: 37.57637865900281
    - type: nauc_recall_at_1000_diff1
      value: 7.839499443984168
    - type: nauc_recall_at_1000_max
      value: 52.67165467640894
    - type: nauc_recall_at_1000_std
      value: 48.85318316702583
    - type: nauc_recall_at_100_diff1
      value: 14.117557049589418
    - type: nauc_recall_at_100_max
      value: 40.59046301348715
    - type: nauc_recall_at_100_std
      value: 24.379680901739505
    - type: nauc_recall_at_10_diff1
      value: 20.04536052614054
    - type: nauc_recall_at_10_max
      value: 25.54148839721574
    - type: nauc_recall_at_10_std
      value: -1.938182527562211
    - type: nauc_recall_at_1_diff1
      value: 37.02663305598722
    - type: nauc_recall_at_1_max
      value: 14.931071531116528
    - type: nauc_recall_at_1_std
      value: -12.478790028708453
    - type: nauc_recall_at_20_diff1
      value: 17.959977483235566
    - type: nauc_recall_at_20_max
      value: 29.88502687870809
    - type: nauc_recall_at_20_std
      value: 4.26527395196852
    - type: nauc_recall_at_3_diff1
      value: 26.297810954500456
    - type: nauc_recall_at_3_max
      value: 18.819406079307402
    - type: nauc_recall_at_3_std
      value: -10.002237229729081
    - type: nauc_recall_at_5_diff1
      value: 22.739080899568485
    - type: nauc_recall_at_5_max
      value: 21.0322968243985
    - type: nauc_recall_at_5_std
      value: -6.927749435306422
    - type: ndcg_at_1
      value: 34.717999999999996
    - type: ndcg_at_10
      value: 34.772999999999996
    - type: ndcg_at_100
      value: 39.407
    - type: ndcg_at_1000
      value: 44.830999999999996
    - type: ndcg_at_20
      value: 35.667
    - type: ndcg_at_3
      value: 34.332
    - type: ndcg_at_5
      value: 34.408
    - type: precision_at_1
      value: 34.717999999999996
    - type: precision_at_10
      value: 23.430999999999997
    - type: precision_at_100
      value: 9.31
    - type: precision_at_1000
      value: 2.259
    - type: precision_at_20
      value: 18.826999999999998
    - type: precision_at_3
      value: 30.553
    - type: precision_at_5
      value: 27.792
    - type: recall_at_1
      value: 7.2620000000000005
    - type: recall_at_10
      value: 26.384
    - type: recall_at_100
      value: 52.506
    - type: recall_at_1000
      value: 73.38
    - type: recall_at_20
      value: 34.032000000000004
    - type: recall_at_3
      value: 14.821000000000002
    - type: recall_at_5
      value: 19.481
    task:
      type: Retrieval
  - dataset:
      config: de
      name: MTEB XMarket (de)
      revision: dfe57acff5b62c23732a7b7d3e3fb84ff501708b
      split: test
      type: jinaai/xmarket_ml
    metrics:
    - type: main_score
      value: 28.316000000000003
    - type: map_at_1
      value: 8.667
    - type: map_at_10
      value: 17.351
    - type: map_at_100
      value: 21.02
    - type: map_at_1000
      value: 21.951
    - type: map_at_20
      value: 18.994
    - type: map_at_3
      value: 13.23
    - type: map_at_5
      value: 15.17
    - type: mrr_at_1
      value: 27.27272727272727
    - type: mrr_at_10
      value: 36.10858487561485
    - type: mrr_at_100
      value: 36.92033814316568
    - type: mrr_at_1000
      value: 36.972226653870365
    - type: mrr_at_20
      value: 36.58914906427944
    - type: mrr_at_3
      value: 33.642969201552305
    - type: mrr_at_5
      value: 35.13417554289494
    - type: nauc_map_at_1000_diff1
      value: 23.345116790998063
    - type: nauc_map_at_1000_max
      value: 44.447240670835725
    - type: nauc_map_at_1000_std
      value: 18.34636500680144
    - type: nauc_map_at_100_diff1
      value: 24.458120909292347
    - type: nauc_map_at_100_max
      value: 43.31851431140378
    - type: nauc_map_at_100_std
      value: 15.654778355549965
    - type: nauc_map_at_10_diff1
      value: 29.376508937265044
    - type: nauc_map_at_10_max
      value: 36.650196725140795
    - type: nauc_map_at_10_std
      value: 4.682465435374843
    - type: nauc_map_at_1_diff1
      value: 40.382365672683214
    - type: nauc_map_at_1_max
      value: 22.894341150096785
    - type: nauc_map_at_1_std
      value: -5.610725673968323
    - type: nauc_map_at_20_diff1
      value: 27.197033425732908
    - type: nauc_map_at_20_max
      value: 39.71672400647207
    - type: nauc_map_at_20_std
      value: 8.944436813309933
    - type: nauc_map_at_3_diff1
      value: 34.49739294661502
    - type: nauc_map_at_3_max
      value: 29.006972420735284
    - type: nauc_map_at_3_std
      value: -3.0372650571243986
    - type: nauc_map_at_5_diff1
      value: 32.764901537277105
    - type: nauc_map_at_5_max
      value: 32.658533295918154
    - type: nauc_map_at_5_std
      value: 0.029626452286996906
    - type: nauc_mrr_at_1000_diff1
      value: 19.521229956280603
    - type: nauc_mrr_at_1000_max
      value: 44.39409866211472
    - type: nauc_mrr_at_1000_std
      value: 23.580697307036058
    - type: nauc_mrr_at_100_diff1
      value: 19.51312676591073
    - type: nauc_mrr_at_100_max
      value: 44.39559153963895
    - type: nauc_mrr_at_100_std
      value: 23.57913711397437
    - type: nauc_mrr_at_10_diff1
      value: 19.584635617935145
    - type: nauc_mrr_at_10_max
      value: 44.44842226236198
    - type: nauc_mrr_at_10_std
      value: 23.382684909390434
    - type: nauc_mrr_at_1_diff1
      value: 20.92594790923806
    - type: nauc_mrr_at_1_max
      value: 40.593939625252816
    - type: nauc_mrr_at_1_std
      value: 20.37467598073644
    - type: nauc_mrr_at_20_diff1
      value: 19.590641822115725
    - type: nauc_mrr_at_20_max
      value: 44.42512299604718
    - type: nauc_mrr_at_20_std
      value: 23.45564260800024
    - type: nauc_mrr_at_3_diff1
      value: 20.005307129527232
    - type: nauc_mrr_at_3_max
      value: 43.68300366192776
    - type: nauc_mrr_at_3_std
      value: 22.297190480842005
    - type: nauc_mrr_at_5_diff1
      value: 19.852896386271716
    - type: nauc_mrr_at_5_max
      value: 44.20641808920062
    - type: nauc_mrr_at_5_std
      value: 22.966517330852895
    - type: nauc_ndcg_at_1000_diff1
      value: 17.800116251376103
    - type: nauc_ndcg_at_1000_max
      value: 50.98332718061365
    - type: nauc_ndcg_at_1000_std
      value: 31.464484658102577
    - type: nauc_ndcg_at_100_diff1
      value: 19.555159680541088
    - type: nauc_ndcg_at_100_max
      value: 48.56377130899141
    - type: nauc_ndcg_at_100_std
      value: 25.77572748714817
    - type: nauc_ndcg_at_10_diff1
      value: 20.003008726679415
    - type: nauc_ndcg_at_10_max
      value: 45.1293725480628
    - type: nauc_ndcg_at_10_std
      value: 21.149213260765872
    - type: nauc_ndcg_at_1_diff1
      value: 21.00986278773023
    - type: nauc_ndcg_at_1_max
      value: 40.524637076774894
    - type: nauc_ndcg_at_1_std
      value: 20.29682194006685
    - type: nauc_ndcg_at_20_diff1
      value: 20.659734137312284
    - type: nauc_ndcg_at_20_max
      value: 45.73108736599869
    - type: nauc_ndcg_at_20_std
      value: 21.200736170346133
    - type: nauc_ndcg_at_3_diff1
      value: 19.200120542882544
    - type: nauc_ndcg_at_3_max
      value: 42.89772612963168
    - type: nauc_ndcg_at_3_std
      value: 20.713292754978983
    - type: nauc_ndcg_at_5_diff1
      value: 19.96329647992544
    - type: nauc_ndcg_at_5_max
      value: 44.296627037787324
    - type: nauc_ndcg_at_5_std
      value: 21.200135784971973
    - type: nauc_precision_at_1000_diff1
      value: -11.543221249009427
    - type: nauc_precision_at_1000_max
      value: 9.132801614448221
    - type: nauc_precision_at_1000_std
      value: 21.203720655381055
    - type: nauc_precision_at_100_diff1
      value: -12.510945425786039
    - type: nauc_precision_at_100_max
      value: 31.42530963666252
    - type: nauc_precision_at_100_std
      value: 44.99672783467617
    - type: nauc_precision_at_10_diff1
      value: -4.025802651746804
    - type: nauc_precision_at_10_max
      value: 47.50967924227793
    - type: nauc_precision_at_10_std
      value: 41.1558559268985
    - type: nauc_precision_at_1_diff1
      value: 21.00986278773023
    - type: nauc_precision_at_1_max
      value: 40.524637076774894
    - type: nauc_precision_at_1_std
      value: 20.29682194006685
    - type: nauc_precision_at_20_diff1
      value: -8.059482951110002
    - type: nauc_precision_at_20_max
      value: 44.28832115946278
    - type: nauc_precision_at_20_std
      value: 45.2005585353651
    - type: nauc_precision_at_3_diff1
      value: 8.53530005716248
    - type: nauc_precision_at_3_max
      value: 46.48353678905102
    - type: nauc_precision_at_3_std
      value: 28.868791323881972
    - type: nauc_precision_at_5_diff1
      value: 3.093619954821814
    - type: nauc_precision_at_5_max
      value: 48.43294475817019
    - type: nauc_precision_at_5_std
      value: 34.83430452745434
    - type: nauc_recall_at_1000_diff1
      value: 9.93680206699751
    - type: nauc_recall_at_1000_max
      value: 52.97840222394363
    - type: nauc_recall_at_1000_std
      value: 46.370023604436255
    - type: nauc_recall_at_100_diff1
      value: 14.100542445524972
    - type: nauc_recall_at_100_max
      value: 42.853775131475224
    - type: nauc_recall_at_100_std
      value: 26.93029971231028
    - type: nauc_recall_at_10_diff1
      value: 22.774547475714716
    - type: nauc_recall_at_10_max
      value: 33.984586405015044
    - type: nauc_recall_at_10_std
      value: 5.332325172373655
    - type: nauc_recall_at_1_diff1
      value: 40.382365672683214
    - type: nauc_recall_at_1_max
      value: 22.894341150096785
    - type: nauc_recall_at_1_std
      value: -5.610725673968323
    - type: nauc_recall_at_20_diff1
      value: 19.751060483835936
    - type: nauc_recall_at_20_max
      value: 36.18774034635102
    - type: nauc_recall_at_20_std
      value: 10.362242090308577
    - type: nauc_recall_at_3_diff1
      value: 30.29462372902671
    - type: nauc_recall_at_3_max
      value: 27.377175450099635
    - type: nauc_recall_at_3_std
      value: -3.015752705993425
    - type: nauc_recall_at_5_diff1
      value: 28.096893312615723
    - type: nauc_recall_at_5_max
      value: 30.485075571512425
    - type: nauc_recall_at_5_std
      value: 0.09106417003502826
    - type: ndcg_at_1
      value: 27.248
    - type: ndcg_at_10
      value: 28.316000000000003
    - type: ndcg_at_100
      value: 33.419
    - type: ndcg_at_1000
      value: 38.134
    - type: ndcg_at_20
      value: 29.707
    - type: ndcg_at_3
      value: 26.93
    - type: ndcg_at_5
      value: 27.363
    - type: precision_at_1
      value: 27.248
    - type: precision_at_10
      value: 15.073
    - type: precision_at_100
      value: 5.061
    - type: precision_at_1000
      value: 1.325
    - type: precision_at_20
      value: 11.407
    - type: precision_at_3
      value: 21.823
    - type: precision_at_5
      value: 18.984
    - type: recall_at_1
      value: 8.667
    - type: recall_at_10
      value: 26.984
    - type: recall_at_100
      value: 49.753
    - type: recall_at_1000
      value: 70.354
    - type: recall_at_20
      value: 33.955999999999996
    - type: recall_at_3
      value: 16.086
    - type: recall_at_5
      value: 20.544999999999998
    task:
      type: Retrieval
  - dataset:
      config: es
      name: MTEB XMarket (es)
      revision: dfe57acff5b62c23732a7b7d3e3fb84ff501708b
      split: test
      type: jinaai/xmarket_ml
    metrics:
    - type: main_score
      value: 26.592
    - type: map_at_1
      value: 8.081000000000001
    - type: map_at_10
      value: 16.486
    - type: map_at_100
      value: 19.996
    - type: map_at_1000
      value: 20.889
    - type: map_at_20
      value: 18.088
    - type: map_at_3
      value: 12.864
    - type: map_at_5
      value: 14.515
    - type: mrr_at_1
      value: 24.643356643356643
    - type: mrr_at_10
      value: 33.755599955599926
    - type: mrr_at_100
      value: 34.55914769326114
    - type: mrr_at_1000
      value: 34.614384237219745
    - type: mrr_at_20
      value: 34.228909650276194
    - type: mrr_at_3
      value: 31.445221445221456
    - type: mrr_at_5
      value: 32.71375291375297
    - type: nauc_map_at_1000_diff1
      value: 19.17751654240679
    - type: nauc_map_at_1000_max
      value: 43.493743561136434
    - type: nauc_map_at_1000_std
      value: 21.14477911550252
    - type: nauc_map_at_100_diff1
      value: 20.259227234415395
    - type: nauc_map_at_100_max
      value: 42.510860292169106
    - type: nauc_map_at_100_std
      value: 18.63085160442346
    - type: nauc_map_at_10_diff1
      value: 24.12419385640694
    - type: nauc_map_at_10_max
      value: 35.99892932069915
    - type: nauc_map_at_10_std
      value: 8.488520124325058
    - type: nauc_map_at_1_diff1
      value: 35.09239143996649
    - type: nauc_map_at_1_max
      value: 23.72498533914286
    - type: nauc_map_at_1_std
      value: -4.164387883546102
    - type: nauc_map_at_20_diff1
      value: 22.411418237320817
    - type: nauc_map_at_20_max
      value: 39.12496266094892
    - type: nauc_map_at_20_std
      value: 12.371656353894227
    - type: nauc_map_at_3_diff1
      value: 28.106972376813506
    - type: nauc_map_at_3_max
      value: 29.57824316865409
    - type: nauc_map_at_3_std
      value: 1.8928791254813127
    - type: nauc_map_at_5_diff1
      value: 26.4958239149419
    - type: nauc_map_at_5_max
      value: 32.45906016649239
    - type: nauc_map_at_5_std
      value: 4.612735963224018
    - type: nauc_mrr_at_1000_diff1
      value: 17.614812607094446
    - type: nauc_mrr_at_1000_max
      value: 41.13031556228715
    - type: nauc_mrr_at_1000_std
      value: 22.564112871230318
    - type: nauc_mrr_at_100_diff1
      value: 17.614044568011085
    - type: nauc_mrr_at_100_max
      value: 41.129436273086796
    - type: nauc_mrr_at_100_std
      value: 22.566763500658766
    - type: nauc_mrr_at_10_diff1
      value: 17.61869494452089
    - type: nauc_mrr_at_10_max
      value: 41.091542329381426
    - type: nauc_mrr_at_10_std
      value: 22.370473458633594
    - type: nauc_mrr_at_1_diff1
      value: 20.321421442201913
    - type: nauc_mrr_at_1_max
      value: 38.36531448180009
    - type: nauc_mrr_at_1_std
      value: 18.422203207777688
    - type: nauc_mrr_at_20_diff1
      value: 17.614767736091625
    - type: nauc_mrr_at_20_max
      value: 41.11221420736687
    - type: nauc_mrr_at_20_std
      value: 22.44271891522012
    - type: nauc_mrr_at_3_diff1
      value: 17.98184651584625
    - type: nauc_mrr_at_3_max
      value: 40.424293610470144
    - type: nauc_mrr_at_3_std
      value: 21.554750947206706
    - type: nauc_mrr_at_5_diff1
      value: 17.72088314927416
    - type: nauc_mrr_at_5_max
      value: 40.662724739072694
    - type: nauc_mrr_at_5_std
      value: 21.822957528431928
    - type: nauc_ndcg_at_1000_diff1
      value: 15.310699428328398
    - type: nauc_ndcg_at_1000_max
      value: 48.83921393349997
    - type: nauc_ndcg_at_1000_std
      value: 32.22600294110774
    - type: nauc_ndcg_at_100_diff1
      value: 16.62672763977423
    - type: nauc_ndcg_at_100_max
      value: 47.36060653537392
    - type: nauc_ndcg_at_100_std
      value: 27.879865162871575
    - type: nauc_ndcg_at_10_diff1
      value: 16.436684176028116
    - type: nauc_ndcg_at_10_max
      value: 43.00026520872974
    - type: nauc_ndcg_at_10_std
      value: 22.507354939162806
    - type: nauc_ndcg_at_1_diff1
      value: 20.321421442201913
    - type: nauc_ndcg_at_1_max
      value: 38.36531448180009
    - type: nauc_ndcg_at_1_std
      value: 18.422203207777688
    - type: nauc_ndcg_at_20_diff1
      value: 17.127747123248835
    - type: nauc_ndcg_at_20_max
      value: 44.57322943752733
    - type: nauc_ndcg_at_20_std
      value: 23.146541187377036
    - type: nauc_ndcg_at_3_diff1
      value: 16.372742984728514
    - type: nauc_ndcg_at_3_max
      value: 40.91938017883993
    - type: nauc_ndcg_at_3_std
      value: 21.50917089194154
    - type: nauc_ndcg_at_5_diff1
      value: 16.40486505525073
    - type: nauc_ndcg_at_5_max
      value: 41.94597203181329
    - type: nauc_ndcg_at_5_std
      value: 22.068260809047562
    - type: nauc_precision_at_1000_diff1
      value: -15.9415313729527
    - type: nauc_precision_at_1000_max
      value: 12.653329948983643
    - type: nauc_precision_at_1000_std
      value: 26.371820703256173
    - type: nauc_precision_at_100_diff1
      value: -11.851070166675289
    - type: nauc_precision_at_100_max
      value: 32.164365923950115
    - type: nauc_precision_at_100_std
      value: 45.930226426725426
    - type: nauc_precision_at_10_diff1
      value: -3.1352660378259163
    - type: nauc_precision_at_10_max
      value: 45.48359878733272
    - type: nauc_precision_at_10_std
      value: 40.2917038044196
    - type: nauc_precision_at_1_diff1
      value: 20.321421442201913
    - type: nauc_precision_at_1_max
      value: 38.36531448180009
    - type: nauc_precision_at_1_std
      value: 18.422203207777688
    - type: nauc_precision_at_20_diff1
      value: -7.087513342144751
    - type: nauc_precision_at_20_max
      value: 43.66272019058357
    - type: nauc_precision_at_20_std
      value: 44.22863351071686
    - type: nauc_precision_at_3_diff1
      value: 7.836185032609045
    - type: nauc_precision_at_3_max
      value: 44.85412904097269
    - type: nauc_precision_at_3_std
      value: 30.209139149500057
    - type: nauc_precision_at_5_diff1
      value: 3.028150537253791
    - type: nauc_precision_at_5_max
      value: 45.73661708882973
    - type: nauc_precision_at_5_std
      value: 34.65500311185052
    - type: nauc_recall_at_1000_diff1
      value: 9.526124668370704
    - type: nauc_recall_at_1000_max
      value: 51.4190208452196
    - type: nauc_recall_at_1000_std
      value: 45.694891695646426
    - type: nauc_recall_at_100_diff1
      value: 12.68466215400009
    - type: nauc_recall_at_100_max
      value: 42.79112054268112
    - type: nauc_recall_at_100_std
      value: 28.61954251400998
    - type: nauc_recall_at_10_diff1
      value: 17.95124413416829
    - type: nauc_recall_at_10_max
      value: 33.1192036755167
    - type: nauc_recall_at_10_std
      value: 9.3588175959525
    - type: nauc_recall_at_1_diff1
      value: 35.09239143996649
    - type: nauc_recall_at_1_max
      value: 23.72498533914286
    - type: nauc_recall_at_1_std
      value: -4.164387883546102
    - type: nauc_recall_at_20_diff1
      value: 16.24916980445646
    - type: nauc_recall_at_20_max
      value: 36.51316122236076
    - type: nauc_recall_at_20_std
      value: 13.641588062425736
    - type: nauc_recall_at_3_diff1
      value: 23.263199724138786
    - type: nauc_recall_at_3_max
      value: 27.67354561610614
    - type: nauc_recall_at_3_std
      value: 3.103127242654415
    - type: nauc_recall_at_5_diff1
      value: 20.719704839229635
    - type: nauc_recall_at_5_max
      value: 29.66480839111333
    - type: nauc_recall_at_5_std
      value: 5.514884455797986
    - type: ndcg_at_1
      value: 24.643
    - type: ndcg_at_10
      value: 26.592
    - type: ndcg_at_100
      value: 31.887
    - type: ndcg_at_1000
      value: 36.695
    - type: ndcg_at_20
      value: 28.166000000000004
    - type: ndcg_at_3
      value: 25.238
    - type: ndcg_at_5
      value: 25.545
    - type: precision_at_1
      value: 24.643
    - type: precision_at_10
      value: 13.730999999999998
    - type: precision_at_100
      value: 4.744000000000001
    - type: precision_at_1000
      value: 1.167
    - type: precision_at_20
      value: 10.562000000000001
    - type: precision_at_3
      value: 20.288999999999998
    - type: precision_at_5
      value: 17.337
    - type: recall_at_1
      value: 8.081000000000001
    - type: recall_at_10
      value: 25.911
    - type: recall_at_100
      value: 48.176
    - type: recall_at_1000
      value: 69.655
    - type: recall_at_20
      value: 32.924
    - type: recall_at_3
      value: 16.125
    - type: recall_at_5
      value: 19.988
    task:
      type: Retrieval
  - dataset:
      config: deu-deu
      name: MTEB XPQARetrieval (deu-deu)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 84.552
    - type: map_at_1
      value: 59.023
    - type: map_at_10
      value: 81.051
    - type: map_at_100
      value: 81.539
    - type: map_at_1000
      value: 81.54299999999999
    - type: map_at_20
      value: 81.401
    - type: map_at_3
      value: 76.969
    - type: map_at_5
      value: 80.07600000000001
    - type: mrr_at_1
      value: 77.67624020887729
    - type: mrr_at_10
      value: 83.30509967259314
    - type: mrr_at_100
      value: 83.58599391639456
    - type: mrr_at_1000
      value: 83.58970114722587
    - type: mrr_at_20
      value: 83.50275980440317
    - type: mrr_at_3
      value: 82.07136640557006
    - type: mrr_at_5
      value: 82.94604003481287
    - type: nauc_map_at_1000_diff1
      value: 63.12885104269942
    - type: nauc_map_at_1000_max
      value: 57.7017996674959
    - type: nauc_map_at_1000_std
      value: -24.951068985070513
    - type: nauc_map_at_100_diff1
      value: 63.12866509393162
    - type: nauc_map_at_100_max
      value: 57.70176426013332
    - type: nauc_map_at_100_std
      value: -24.96012290790273
    - type: nauc_map_at_10_diff1
      value: 62.847709436211204
    - type: nauc_map_at_10_max
      value: 57.408873624779524
    - type: nauc_map_at_10_std
      value: -25.635130363219062
    - type: nauc_map_at_1_diff1
      value: 71.89683981857102
    - type: nauc_map_at_1_max
      value: 20.204460967432645
    - type: nauc_map_at_1_std
      value: -23.07894656629493
    - type: nauc_map_at_20_diff1
      value: 63.00504457011043
    - type: nauc_map_at_20_max
      value: 57.66009512514262
    - type: nauc_map_at_20_std
      value: -25.100138593754885
    - type: nauc_map_at_3_diff1
      value: 63.199874607788274
    - type: nauc_map_at_3_max
      value: 47.54482033763308
    - type: nauc_map_at_3_std
      value: -27.714557098916963
    - type: nauc_map_at_5_diff1
      value: 63.01006523518669
    - type: nauc_map_at_5_max
      value: 56.501965964288495
    - type: nauc_map_at_5_std
      value: -25.367825762790925
    - type: nauc_mrr_at_1000_diff1
      value: 66.24988063948112
    - type: nauc_mrr_at_1000_max
      value: 63.56921667744273
    - type: nauc_mrr_at_1000_std
      value: -22.073973768031863
    - type: nauc_mrr_at_100_diff1
      value: 66.24919554296275
    - type: nauc_mrr_at_100_max
      value: 63.57382447608361
    - type: nauc_mrr_at_100_std
      value: -22.084627248538187
    - type: nauc_mrr_at_10_diff1
      value: 66.0143885124066
    - type: nauc_mrr_at_10_max
      value: 63.51277586011898
    - type: nauc_mrr_at_10_std
      value: -22.477523960705454
    - type: nauc_mrr_at_1_diff1
      value: 68.25415199323474
    - type: nauc_mrr_at_1_max
      value: 63.069019003272416
    - type: nauc_mrr_at_1_std
      value: -18.77085924093244
    - type: nauc_mrr_at_20_diff1
      value: 66.16203167351055
    - type: nauc_mrr_at_20_max
      value: 63.607477776215845
    - type: nauc_mrr_at_20_std
      value: -22.15083176017266
    - type: nauc_mrr_at_3_diff1
      value: 66.39368842782302
    - type: nauc_mrr_at_3_max
      value: 63.11411066585295
    - type: nauc_mrr_at_3_std
      value: -22.63174342814071
    - type: nauc_mrr_at_5_diff1
      value: 66.17932562332354
    - type: nauc_mrr_at_5_max
      value: 63.70434825329594
    - type: nauc_mrr_at_5_std
      value: -21.704012812430438
    - type: nauc_ndcg_at_1000_diff1
      value: 63.958010361549356
    - type: nauc_ndcg_at_1000_max
      value: 60.516445000134624
    - type: nauc_ndcg_at_1000_std
      value: -24.264672248289923
    - type: nauc_ndcg_at_100_diff1
      value: 63.97654644758022
    - type: nauc_ndcg_at_100_max
      value: 60.62187552803407
    - type: nauc_ndcg_at_100_std
      value: -24.317149225778312
    - type: nauc_ndcg_at_10_diff1
      value: 62.505321221321566
    - type: nauc_ndcg_at_10_max
      value: 59.77891112351258
    - type: nauc_ndcg_at_10_std
      value: -26.90910005589911
    - type: nauc_ndcg_at_1_diff1
      value: 68.25415199323474
    - type: nauc_ndcg_at_1_max
      value: 63.069019003272416
    - type: nauc_ndcg_at_1_std
      value: -18.77085924093244
    - type: nauc_ndcg_at_20_diff1
      value: 63.04281805056225
    - type: nauc_ndcg_at_20_max
      value: 60.600957307444226
    - type: nauc_ndcg_at_20_std
      value: -24.954862079889203
    - type: nauc_ndcg_at_3_diff1
      value: 62.970441139740316
    - type: nauc_ndcg_at_3_max
      value: 57.543715669055295
    - type: nauc_ndcg_at_3_std
      value: -25.659388431714703
    - type: nauc_ndcg_at_5_diff1
      value: 62.82652127664541
    - type: nauc_ndcg_at_5_max
      value: 58.6970443258532
    - type: nauc_ndcg_at_5_std
      value: -25.66329354851023
    - type: nauc_precision_at_1000_diff1
      value: -33.38530947486223
    - type: nauc_precision_at_1000_max
      value: 25.972468024345414
    - type: nauc_precision_at_1000_std
      value: 17.460222955117978
    - type: nauc_precision_at_100_diff1
      value: -32.45175999251703
    - type: nauc_precision_at_100_max
      value: 26.367996120487337
    - type: nauc_precision_at_100_std
      value: 17.097957946391208
    - type: nauc_precision_at_10_diff1
      value: -26.97411235289487
    - type: nauc_precision_at_10_max
      value: 31.504961687240762
    - type: nauc_precision_at_10_std
      value: 11.125341183874687
    - type: nauc_precision_at_1_diff1
      value: 68.25415199323474
    - type: nauc_precision_at_1_max
      value: 63.069019003272416
    - type: nauc_precision_at_1_std
      value: -18.77085924093244
    - type: nauc_precision_at_20_diff1
      value: -29.8678078736273
    - type: nauc_precision_at_20_max
      value: 29.031222186584504
    - type: nauc_precision_at_20_std
      value: 14.943600563087928
    - type: nauc_precision_at_3_diff1
      value: -15.92947221299854
    - type: nauc_precision_at_3_max
      value: 37.73833494235097
    - type: nauc_precision_at_3_std
      value: 3.1573228443500847
    - type: nauc_precision_at_5_diff1
      value: -22.269156821101642
    - type: nauc_precision_at_5_max
      value: 35.65821838116355
    - type: nauc_precision_at_5_std
      value: 9.265930386198972
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: 66.17058859539249
    - type: nauc_recall_at_100_max
      value: 78.066942935192
    - type: nauc_recall_at_100_std
      value: -22.213377762074686
    - type: nauc_recall_at_10_diff1
      value: 50.82149700700275
    - type: nauc_recall_at_10_max
      value: 56.68053325008221
    - type: nauc_recall_at_10_std
      value: -41.81657941433277
    - type: nauc_recall_at_1_diff1
      value: 71.89683981857102
    - type: nauc_recall_at_1_max
      value: 20.204460967432645
    - type: nauc_recall_at_1_std
      value: -23.07894656629493
    - type: nauc_recall_at_20_diff1
      value: 48.28076011857885
    - type: nauc_recall_at_20_max
      value: 63.29641555519295
    - type: nauc_recall_at_20_std
      value: -32.953559708819405
    - type: nauc_recall_at_3_diff1
      value: 58.15516956312558
    - type: nauc_recall_at_3_max
      value: 42.66315890283056
    - type: nauc_recall_at_3_std
      value: -32.16572530544806
    - type: nauc_recall_at_5_diff1
      value: 55.900844052439766
    - type: nauc_recall_at_5_max
      value: 55.23702018862884
    - type: nauc_recall_at_5_std
      value: -30.105929528165
    - type: ndcg_at_1
      value: 77.676
    - type: ndcg_at_10
      value: 84.552
    - type: ndcg_at_100
      value: 86.232
    - type: ndcg_at_1000
      value: 86.33800000000001
    - type: ndcg_at_20
      value: 85.515
    - type: ndcg_at_3
      value: 81.112
    - type: ndcg_at_5
      value: 82.943
    - type: precision_at_1
      value: 77.676
    - type: precision_at_10
      value: 15.17
    - type: precision_at_100
      value: 1.6230000000000002
    - type: precision_at_1000
      value: 0.163
    - type: precision_at_20
      value: 7.858999999999999
    - type: precision_at_3
      value: 42.994
    - type: precision_at_5
      value: 28.747
    - type: recall_at_1
      value: 59.023
    - type: recall_at_10
      value: 92.465
    - type: recall_at_100
      value: 99.18400000000001
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 95.844
    - type: recall_at_3
      value: 81.826
    - type: recall_at_5
      value: 88.22
    task:
      type: Retrieval
  - dataset:
      config: deu-eng
      name: MTEB XPQARetrieval (deu-eng)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 82.149
    - type: map_at_1
      value: 56.277
    - type: map_at_10
      value: 78.36999999999999
    - type: map_at_100
      value: 78.94
    - type: map_at_1000
      value: 78.95
    - type: map_at_20
      value: 78.818
    - type: map_at_3
      value: 74.25
    - type: map_at_5
      value: 77.11099999999999
    - type: mrr_at_1
      value: 74.28198433420366
    - type: mrr_at_10
      value: 80.57487877657589
    - type: mrr_at_100
      value: 80.94025764149008
    - type: mrr_at_1000
      value: 80.94608738871234
    - type: mrr_at_20
      value: 80.86240675885023
    - type: mrr_at_3
      value: 79.4604003481288
    - type: mrr_at_5
      value: 80.10008703220191
    - type: nauc_map_at_1000_diff1
      value: 60.44369249057189
    - type: nauc_map_at_1000_max
      value: 49.822240441830246
    - type: nauc_map_at_1000_std
      value: -27.34026380762817
    - type: nauc_map_at_100_diff1
      value: 60.44635668050401
    - type: nauc_map_at_100_max
      value: 49.838675926660684
    - type: nauc_map_at_100_std
      value: -27.310365556055583
    - type: nauc_map_at_10_diff1
      value: 60.18546951726522
    - type: nauc_map_at_10_max
      value: 49.72075398096832
    - type: nauc_map_at_10_std
      value: -27.86056102461558
    - type: nauc_map_at_1_diff1
      value: 71.2906657099758
    - type: nauc_map_at_1_max
      value: 18.970399251589
    - type: nauc_map_at_1_std
      value: -27.260776614286602
    - type: nauc_map_at_20_diff1
      value: 60.3525975566164
    - type: nauc_map_at_20_max
      value: 49.852487866710646
    - type: nauc_map_at_20_std
      value: -27.305173830170332
    - type: nauc_map_at_3_diff1
      value: 60.66803500571236
    - type: nauc_map_at_3_max
      value: 41.18191941521972
    - type: nauc_map_at_3_std
      value: -28.71383593401732
    - type: nauc_map_at_5_diff1
      value: 60.57216514504887
    - type: nauc_map_at_5_max
      value: 47.99837400446299
    - type: nauc_map_at_5_std
      value: -28.756183015949986
    - type: nauc_mrr_at_1000_diff1
      value: 63.77031955602516
    - type: nauc_mrr_at_1000_max
      value: 54.26907383811417
    - type: nauc_mrr_at_1000_std
      value: -26.227442087164714
    - type: nauc_mrr_at_100_diff1
      value: 63.77196650108669
    - type: nauc_mrr_at_100_max
      value: 54.281801457913126
    - type: nauc_mrr_at_100_std
      value: -26.216077891830793
    - type: nauc_mrr_at_10_diff1
      value: 63.50095284903051
    - type: nauc_mrr_at_10_max
      value: 54.3186301730016
    - type: nauc_mrr_at_10_std
      value: -26.29570241722173
    - type: nauc_mrr_at_1_diff1
      value: 65.15855770999057
    - type: nauc_mrr_at_1_max
      value: 53.213286738515066
    - type: nauc_mrr_at_1_std
      value: -24.683178252901943
    - type: nauc_mrr_at_20_diff1
      value: 63.74936550280859
    - type: nauc_mrr_at_20_max
      value: 54.355343751439065
    - type: nauc_mrr_at_20_std
      value: -26.197316900009817
    - type: nauc_mrr_at_3_diff1
      value: 63.912612979082695
    - type: nauc_mrr_at_3_max
      value: 53.75399024225975
    - type: nauc_mrr_at_3_std
      value: -27.194143264554675
    - type: nauc_mrr_at_5_diff1
      value: 63.72491059053639
    - type: nauc_mrr_at_5_max
      value: 53.66107604019352
    - type: nauc_mrr_at_5_std
      value: -26.92281560584754
    - type: nauc_ndcg_at_1000_diff1
      value: 61.304218998714354
    - type: nauc_ndcg_at_1000_max
      value: 52.409135743660386
    - type: nauc_ndcg_at_1000_std
      value: -26.539796489464056
    - type: nauc_ndcg_at_100_diff1
      value: 61.40355045085304
    - type: nauc_ndcg_at_100_max
      value: 52.79402259608008
    - type: nauc_ndcg_at_100_std
      value: -25.927273456979965
    - type: nauc_ndcg_at_10_diff1
      value: 59.93675608684116
    - type: nauc_ndcg_at_10_max
      value: 52.617848197542706
    - type: nauc_ndcg_at_10_std
      value: -27.314820020095887
    - type: nauc_ndcg_at_1_diff1
      value: 65.15855770999057
    - type: nauc_ndcg_at_1_max
      value: 53.213286738515066
    - type: nauc_ndcg_at_1_std
      value: -24.683178252901943
    - type: nauc_ndcg_at_20_diff1
      value: 60.85093704358376
    - type: nauc_ndcg_at_20_max
      value: 53.14529242671602
    - type: nauc_ndcg_at_20_std
      value: -25.93187916231906
    - type: nauc_ndcg_at_3_diff1
      value: 60.42301123518882
    - type: nauc_ndcg_at_3_max
      value: 49.59021992975956
    - type: nauc_ndcg_at_3_std
      value: -27.397117967810363
    - type: nauc_ndcg_at_5_diff1
      value: 60.78655153154219
    - type: nauc_ndcg_at_5_max
      value: 49.54194799556953
    - type: nauc_ndcg_at_5_std
      value: -29.467910172913413
    - type: nauc_precision_at_1000_diff1
      value: -34.35027108027456
    - type: nauc_precision_at_1000_max
      value: 23.762671066858815
    - type: nauc_precision_at_1000_std
      value: 16.1704780298982
    - type: nauc_precision_at_100_diff1
      value: -32.66610016754961
    - type: nauc_precision_at_100_max
      value: 25.504044603109588
    - type: nauc_precision_at_100_std
      value: 16.932402988816786
    - type: nauc_precision_at_10_diff1
      value: -25.720903145017342
    - type: nauc_precision_at_10_max
      value: 30.37029690599926
    - type: nauc_precision_at_10_std
      value: 10.560753160200314
    - type: nauc_precision_at_1_diff1
      value: 65.15855770999057
    - type: nauc_precision_at_1_max
      value: 53.213286738515066
    - type: nauc_precision_at_1_std
      value: -24.683178252901943
    - type: nauc_precision_at_20_diff1
      value: -29.577582332619084
    - type: nauc_precision_at_20_max
      value: 27.984145595920417
    - type: nauc_precision_at_20_std
      value: 15.083711704044727
    - type: nauc_precision_at_3_diff1
      value: -14.736267532892697
    - type: nauc_precision_at_3_max
      value: 36.12211021824307
    - type: nauc_precision_at_3_std
      value: 3.068643876519412
    - type: nauc_precision_at_5_diff1
      value: -19.846707283120825
    - type: nauc_precision_at_5_max
      value: 33.573804532177896
    - type: nauc_precision_at_5_std
      value: 5.700545622744924
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: 68.24749796604452
    - type: nauc_recall_at_100_max
      value: 83.30024864929815
    - type: nauc_recall_at_100_std
      value: 21.23763053711522
    - type: nauc_recall_at_10_diff1
      value: 50.704049683241436
    - type: nauc_recall_at_10_max
      value: 57.64578984555556
    - type: nauc_recall_at_10_std
      value: -26.632759037746073
    - type: nauc_recall_at_1_diff1
      value: 71.2906657099758
    - type: nauc_recall_at_1_max
      value: 18.970399251589
    - type: nauc_recall_at_1_std
      value: -27.260776614286602
    - type: nauc_recall_at_20_diff1
      value: 54.124480837579505
    - type: nauc_recall_at_20_max
      value: 66.4641515433479
    - type: nauc_recall_at_20_std
      value: -14.615911455379393
    - type: nauc_recall_at_3_diff1
      value: 56.54358788321059
    - type: nauc_recall_at_3_max
      value: 37.765735322465744
    - type: nauc_recall_at_3_std
      value: -30.824147408598574
    - type: nauc_recall_at_5_diff1
      value: 56.392894535029214
    - type: nauc_recall_at_5_max
      value: 45.959268387521554
    - type: nauc_recall_at_5_std
      value: -33.58175576925282
    - type: ndcg_at_1
      value: 74.28200000000001
    - type: ndcg_at_10
      value: 82.149
    - type: ndcg_at_100
      value: 84.129
    - type: ndcg_at_1000
      value: 84.307
    - type: ndcg_at_20
      value: 83.39999999999999
    - type: ndcg_at_3
      value: 78.583
    - type: ndcg_at_5
      value: 80.13900000000001
    - type: precision_at_1
      value: 74.28200000000001
    - type: precision_at_10
      value: 14.960999999999999
    - type: precision_at_100
      value: 1.6119999999999999
    - type: precision_at_1000
      value: 0.163
    - type: precision_at_20
      value: 7.813000000000001
    - type: precision_at_3
      value: 41.819
    - type: precision_at_5
      value: 27.911
    - type: recall_at_1
      value: 56.277
    - type: recall_at_10
      value: 90.729
    - type: recall_at_100
      value: 98.792
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 95.148
    - type: recall_at_3
      value: 79.989
    - type: recall_at_5
      value: 85.603
    task:
      type: Retrieval
  - dataset:
      config: eng-deu
      name: MTEB XPQARetrieval (eng-deu)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 60.428000000000004
    - type: map_at_1
      value: 33.453
    - type: map_at_10
      value: 54.217000000000006
    - type: map_at_100
      value: 55.832
    - type: map_at_1000
      value: 55.884
    - type: map_at_20
      value: 55.236
    - type: map_at_3
      value: 48.302
    - type: map_at_5
      value: 51.902
    - type: mrr_at_1
      value: 53.916449086161876
    - type: mrr_at_10
      value: 61.4685647975465
    - type: mrr_at_100
      value: 62.13718159287348
    - type: mrr_at_1000
      value: 62.15799113826325
    - type: mrr_at_20
      value: 61.885388764243544
    - type: mrr_at_3
      value: 59.44299390774582
    - type: mrr_at_5
      value: 60.26544821583981
    - type: nauc_map_at_1000_diff1
      value: 39.824412602121804
    - type: nauc_map_at_1000_max
      value: 39.49332709959374
    - type: nauc_map_at_1000_std
      value: -17.27462623749702
    - type: nauc_map_at_100_diff1
      value: 39.80528910003463
    - type: nauc_map_at_100_max
      value: 39.51471609156093
    - type: nauc_map_at_100_std
      value: -17.275536933094937
    - type: nauc_map_at_10_diff1
      value: 39.28558292349772
    - type: nauc_map_at_10_max
      value: 38.13220294838968
    - type: nauc_map_at_10_std
      value: -18.235985574392863
    - type: nauc_map_at_1_diff1
      value: 43.68892397816937
    - type: nauc_map_at_1_max
      value: 14.478978190224353
    - type: nauc_map_at_1_std
      value: -18.435031919225477
    - type: nauc_map_at_20_diff1
      value: 39.8733530971344
    - type: nauc_map_at_20_max
      value: 39.30513202591992
    - type: nauc_map_at_20_std
      value: -17.62362848144766
    - type: nauc_map_at_3_diff1
      value: 40.31116611188815
    - type: nauc_map_at_3_max
      value: 31.107314675202165
    - type: nauc_map_at_3_std
      value: -19.52930881946966
    - type: nauc_map_at_5_diff1
      value: 39.1241499095765
    - type: nauc_map_at_5_max
      value: 37.330543901034055
    - type: nauc_map_at_5_std
      value: -17.893862772447548
    - type: nauc_mrr_at_1000_diff1
      value: 43.07490530140024
    - type: nauc_mrr_at_1000_max
      value: 42.28469195779226
    - type: nauc_mrr_at_1000_std
      value: -15.583217110180737
    - type: nauc_mrr_at_100_diff1
      value: 43.068836494603886
    - type: nauc_mrr_at_100_max
      value: 42.29612450479168
    - type: nauc_mrr_at_100_std
      value: -15.57218089438229
    - type: nauc_mrr_at_10_diff1
      value: 42.88685919151777
    - type: nauc_mrr_at_10_max
      value: 41.89944452003811
    - type: nauc_mrr_at_10_std
      value: -15.909673572763165
    - type: nauc_mrr_at_1_diff1
      value: 45.67646898532131
    - type: nauc_mrr_at_1_max
      value: 43.0541870425035
    - type: nauc_mrr_at_1_std
      value: -15.597124291613563
    - type: nauc_mrr_at_20_diff1
      value: 43.14141873150977
    - type: nauc_mrr_at_20_max
      value: 42.33063543184022
    - type: nauc_mrr_at_20_std
      value: -15.607612016107304
    - type: nauc_mrr_at_3_diff1
      value: 43.18370928261982
    - type: nauc_mrr_at_3_max
      value: 42.18529980773961
    - type: nauc_mrr_at_3_std
      value: -15.900151400673629
    - type: nauc_mrr_at_5_diff1
      value: 42.43443044877765
    - type: nauc_mrr_at_5_max
      value: 42.05818605278972
    - type: nauc_mrr_at_5_std
      value: -15.436502733299893
    - type: nauc_ndcg_at_1000_diff1
      value: 40.60606676178781
    - type: nauc_ndcg_at_1000_max
      value: 41.71923393878376
    - type: nauc_ndcg_at_1000_std
      value: -15.694740326899556
    - type: nauc_ndcg_at_100_diff1
      value: 40.15270376312309
    - type: nauc_ndcg_at_100_max
      value: 42.234126305709225
    - type: nauc_ndcg_at_100_std
      value: -15.436051984708952
    - type: nauc_ndcg_at_10_diff1
      value: 39.142259831299455
    - type: nauc_ndcg_at_10_max
      value: 38.61470104273746
    - type: nauc_ndcg_at_10_std
      value: -18.577452829132742
    - type: nauc_ndcg_at_1_diff1
      value: 45.67646898532131
    - type: nauc_ndcg_at_1_max
      value: 43.0541870425035
    - type: nauc_ndcg_at_1_std
      value: -15.597124291613563
    - type: nauc_ndcg_at_20_diff1
      value: 40.805159395901306
    - type: nauc_ndcg_at_20_max
      value: 41.58685629374952
    - type: nauc_ndcg_at_20_std
      value: -16.862408156222592
    - type: nauc_ndcg_at_3_diff1
      value: 39.12028215488432
    - type: nauc_ndcg_at_3_max
      value: 39.70580596343164
    - type: nauc_ndcg_at_3_std
      value: -16.705546903936213
    - type: nauc_ndcg_at_5_diff1
      value: 38.42075404927361
    - type: nauc_ndcg_at_5_max
      value: 38.064219879504385
    - type: nauc_ndcg_at_5_std
      value: -17.20282111665876
    - type: nauc_precision_at_1000_diff1
      value: -4.419224540552891
    - type: nauc_precision_at_1000_max
      value: 35.686022591225246
    - type: nauc_precision_at_1000_std
      value: 15.023520191032972
    - type: nauc_precision_at_100_diff1
      value: -2.9027602601603895
    - type: nauc_precision_at_100_max
      value: 39.99864013028808
    - type: nauc_precision_at_100_std
      value: 13.863497117255525
    - type: nauc_precision_at_10_diff1
      value: 5.539104839809501
    - type: nauc_precision_at_10_max
      value: 42.41625740557432
    - type: nauc_precision_at_10_std
      value: 1.0894693748662556
    - type: nauc_precision_at_1_diff1
      value: 45.67646898532131
    - type: nauc_precision_at_1_max
      value: 43.0541870425035
    - type: nauc_precision_at_1_std
      value: -15.597124291613563
    - type: nauc_precision_at_20_diff1
      value: 4.734562571681868
    - type: nauc_precision_at_20_max
      value: 44.35081213316202
    - type: nauc_precision_at_20_std
      value: 6.642891478284595
    - type: nauc_precision_at_3_diff1
      value: 13.936559341472101
    - type: nauc_precision_at_3_max
      value: 45.426668552497524
    - type: nauc_precision_at_3_std
      value: -5.219785419247125
    - type: nauc_precision_at_5_diff1
      value: 8.366706789546015
    - type: nauc_precision_at_5_max
      value: 46.161942989326896
    - type: nauc_precision_at_5_std
      value: -0.193140343545876
    - type: nauc_recall_at_1000_diff1
      value: 45.61785312444842
    - type: nauc_recall_at_1000_max
      value: 75.68258976531774
    - type: nauc_recall_at_1000_std
      value: 37.469059422121575
    - type: nauc_recall_at_100_diff1
      value: 26.798748531805096
    - type: nauc_recall_at_100_max
      value: 54.72134095197765
    - type: nauc_recall_at_100_std
      value: -1.5967608233799417
    - type: nauc_recall_at_10_diff1
      value: 32.13211696200521
    - type: nauc_recall_at_10_max
      value: 31.13866254975895
    - type: nauc_recall_at_10_std
      value: -22.31404161136118
    - type: nauc_recall_at_1_diff1
      value: 43.68892397816937
    - type: nauc_recall_at_1_max
      value: 14.478978190224353
    - type: nauc_recall_at_1_std
      value: -18.435031919225477
    - type: nauc_recall_at_20_diff1
      value: 38.597996930461385
    - type: nauc_recall_at_20_max
      value: 42.49849027366794
    - type: nauc_recall_at_20_std
      value: -16.536471900752154
    - type: nauc_recall_at_3_diff1
      value: 35.343730012759266
    - type: nauc_recall_at_3_max
      value: 26.898722085043392
    - type: nauc_recall_at_3_std
      value: -19.4459792273884
    - type: nauc_recall_at_5_diff1
      value: 31.8310298012186
    - type: nauc_recall_at_5_max
      value: 32.67800489655844
    - type: nauc_recall_at_5_std
      value: -16.800929103347283
    - type: ndcg_at_1
      value: 53.916
    - type: ndcg_at_10
      value: 60.428000000000004
    - type: ndcg_at_100
      value: 65.95
    - type: ndcg_at_1000
      value: 66.88
    - type: ndcg_at_20
      value: 62.989
    - type: ndcg_at_3
      value: 55.204
    - type: ndcg_at_5
      value: 56.42700000000001
    - type: precision_at_1
      value: 53.916
    - type: precision_at_10
      value: 14.346999999999998
    - type: precision_at_100
      value: 1.849
    - type: precision_at_1000
      value: 0.196
    - type: precision_at_20
      value: 8.022
    - type: precision_at_3
      value: 34.552
    - type: precision_at_5
      value: 24.569
    - type: recall_at_1
      value: 33.453
    - type: recall_at_10
      value: 71.07900000000001
    - type: recall_at_100
      value: 93.207
    - type: recall_at_1000
      value: 99.60799999999999
    - type: recall_at_20
      value: 79.482
    - type: recall_at_3
      value: 53.98
    - type: recall_at_5
      value: 60.781
    task:
      type: Retrieval
  - dataset:
      config: eng-pol
      name: MTEB XPQARetrieval (eng-pol)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 34.042
    - type: map_at_1
      value: 13.236
    - type: map_at_10
      value: 27.839999999999996
    - type: map_at_100
      value: 30.171999999999997
    - type: map_at_1000
      value: 30.349999999999998
    - type: map_at_20
      value: 29.044999999999998
    - type: map_at_3
      value: 22.58
    - type: map_at_5
      value: 25.83
    - type: mrr_at_1
      value: 30.318471337579616
    - type: mrr_at_10
      value: 37.4983823678091
    - type: mrr_at_100
      value: 38.5784523175009
    - type: mrr_at_1000
      value: 38.63608698968148
    - type: mrr_at_20
      value: 38.02996157871825
    - type: mrr_at_3
      value: 34.798301486199584
    - type: mrr_at_5
      value: 36.39702760084925
    - type: nauc_map_at_1000_diff1
      value: 21.07199789609177
    - type: nauc_map_at_1000_max
      value: 25.959233507893277
    - type: nauc_map_at_1000_std
      value: -28.011925372852826
    - type: nauc_map_at_100_diff1
      value: 21.086788412737548
    - type: nauc_map_at_100_max
      value: 25.8611620203686
    - type: nauc_map_at_100_std
      value: -28.179239912057515
    - type: nauc_map_at_10_diff1
      value: 21.23841745922078
    - type: nauc_map_at_10_max
      value: 25.44290342378288
    - type: nauc_map_at_10_std
      value: -28.75578689110275
    - type: nauc_map_at_1_diff1
      value: 28.87454015638211
    - type: nauc_map_at_1_max
      value: 17.50681123879997
    - type: nauc_map_at_1_std
      value: -30.382831850562432
    - type: nauc_map_at_20_diff1
      value: 21.076559713540455
    - type: nauc_map_at_20_max
      value: 25.538154202494535
    - type: nauc_map_at_20_std
      value: -28.518764617658555
    - type: nauc_map_at_3_diff1
      value: 22.159185358766468
    - type: nauc_map_at_3_max
      value: 23.01652660927249
    - type: nauc_map_at_3_std
      value: -29.567722713221862
    - type: nauc_map_at_5_diff1
      value: 21.35578810370897
    - type: nauc_map_at_5_max
      value: 25.550550437767395
    - type: nauc_map_at_5_std
      value: -28.7889035461355
    - type: nauc_mrr_at_1000_diff1
      value: 22.28633009221923
    - type: nauc_mrr_at_1000_max
      value: 26.920205393136392
    - type: nauc_mrr_at_1000_std
      value: -25.887791634977642
    - type: nauc_mrr_at_100_diff1
      value: 22.2754975739755
    - type: nauc_mrr_at_100_max
      value: 26.90235716615346
    - type: nauc_mrr_at_100_std
      value: -25.891596020584345
    - type: nauc_mrr_at_10_diff1
      value: 22.415076305593534
    - type: nauc_mrr_at_10_max
      value: 26.504643796222222
    - type: nauc_mrr_at_10_std
      value: -26.6046081215833
    - type: nauc_mrr_at_1_diff1
      value: 23.406748619244368
    - type: nauc_mrr_at_1_max
      value: 29.058228240823553
    - type: nauc_mrr_at_1_std
      value: -26.450169820901078
    - type: nauc_mrr_at_20_diff1
      value: 22.29233141817678
    - type: nauc_mrr_at_20_max
      value: 26.69021351064081
    - type: nauc_mrr_at_20_std
      value: -26.086596227376656
    - type: nauc_mrr_at_3_diff1
      value: 22.20746187500145
    - type: nauc_mrr_at_3_max
      value: 27.143725946169457
    - type: nauc_mrr_at_3_std
      value: -26.7017708594376
    - type: nauc_mrr_at_5_diff1
      value: 22.71898965233195
    - type: nauc_mrr_at_5_max
      value: 26.932386658571662
    - type: nauc_mrr_at_5_std
      value: -26.725541058780234
    - type: nauc_ndcg_at_1000_diff1
      value: 20.541734305148466
    - type: nauc_ndcg_at_1000_max
      value: 27.180534238090758
    - type: nauc_ndcg_at_1000_std
      value: -23.74197745177845
    - type: nauc_ndcg_at_100_diff1
      value: 20.570052839937468
    - type: nauc_ndcg_at_100_max
      value: 26.21605034405486
    - type: nauc_ndcg_at_100_std
      value: -25.359817188805028
    - type: nauc_ndcg_at_10_diff1
      value: 21.241423075073467
    - type: nauc_ndcg_at_10_max
      value: 24.599199195239475
    - type: nauc_ndcg_at_10_std
      value: -28.404540333309008
    - type: nauc_ndcg_at_1_diff1
      value: 23.406748619244368
    - type: nauc_ndcg_at_1_max
      value: 29.058228240823553
    - type: nauc_ndcg_at_1_std
      value: -26.450169820901078
    - type: nauc_ndcg_at_20_diff1
      value: 20.740460046196873
    - type: nauc_ndcg_at_20_max
      value: 24.82380195169634
    - type: nauc_ndcg_at_20_std
      value: -27.376298834244313
    - type: nauc_ndcg_at_3_diff1
      value: 19.994948682426504
    - type: nauc_ndcg_at_3_max
      value: 26.153790759405105
    - type: nauc_ndcg_at_3_std
      value: -27.194548404540885
    - type: nauc_ndcg_at_5_diff1
      value: 21.48414272096384
    - type: nauc_ndcg_at_5_max
      value: 25.239652015076373
    - type: nauc_ndcg_at_5_std
      value: -28.2620160957961
    - type: nauc_precision_at_1000_diff1
      value: -0.7557639926687744
    - type: nauc_precision_at_1000_max
      value: 24.265591636994436
    - type: nauc_precision_at_1000_std
      value: 16.833104654292654
    - type: nauc_precision_at_100_diff1
      value: 4.647847665941115
    - type: nauc_precision_at_100_max
      value: 24.42192644844434
    - type: nauc_precision_at_100_std
      value: 0.2718848568876648
    - type: nauc_precision_at_10_diff1
      value: 9.465969286722654
    - type: nauc_precision_at_10_max
      value: 27.448993150448043
    - type: nauc_precision_at_10_std
      value: -16.519099596502212
    - type: nauc_precision_at_1_diff1
      value: 23.406748619244368
    - type: nauc_precision_at_1_max
      value: 29.058228240823553
    - type: nauc_precision_at_1_std
      value: -26.450169820901078
    - type: nauc_precision_at_20_diff1
      value: 8.021421615668114
    - type: nauc_precision_at_20_max
      value: 26.18556481398635
    - type: nauc_precision_at_20_std
      value: -12.207152108668367
    - type: nauc_precision_at_3_diff1
      value: 11.783572803634241
    - type: nauc_precision_at_3_max
      value: 29.259715774978893
    - type: nauc_precision_at_3_std
      value: -20.407524967717425
    - type: nauc_precision_at_5_diff1
      value: 10.371728615220821
    - type: nauc_precision_at_5_max
      value: 30.270642833482864
    - type: nauc_precision_at_5_std
      value: -18.407334880575494
    - type: nauc_recall_at_1000_diff1
      value: 6.008969959111555
    - type: nauc_recall_at_1000_max
      value: 39.79691734058127
    - type: nauc_recall_at_1000_std
      value: 32.43591825510109
    - type: nauc_recall_at_100_diff1
      value: 15.2374566058917
    - type: nauc_recall_at_100_max
      value: 23.058785539503717
    - type: nauc_recall_at_100_std
      value: -15.962888794058165
    - type: nauc_recall_at_10_diff1
      value: 19.46184821807753
    - type: nauc_recall_at_10_max
      value: 19.001003513986866
    - type: nauc_recall_at_10_std
      value: -27.753332786663876
    - type: nauc_recall_at_1_diff1
      value: 28.87454015638211
    - type: nauc_recall_at_1_max
      value: 17.50681123879997
    - type: nauc_recall_at_1_std
      value: -30.382831850562432
    - type: nauc_recall_at_20_diff1
      value: 17.237090858517405
    - type: nauc_recall_at_20_max
      value: 18.42118474134871
    - type: nauc_recall_at_20_std
      value: -24.862787724031957
    - type: nauc_recall_at_3_diff1
      value: 18.813019521758577
    - type: nauc_recall_at_3_max
      value: 19.198572333053544
    - type: nauc_recall_at_3_std
      value: -28.5644958605618
    - type: nauc_recall_at_5_diff1
      value: 20.247501986329482
    - type: nauc_recall_at_5_max
      value: 21.121526202170358
    - type: nauc_recall_at_5_std
      value: -27.220378617864853
    - type: ndcg_at_1
      value: 30.318
    - type: ndcg_at_10
      value: 34.042
    - type: ndcg_at_100
      value: 42.733
    - type: ndcg_at_1000
      value: 46.015
    - type: ndcg_at_20
      value: 37.053999999999995
    - type: ndcg_at_3
      value: 29.254
    - type: ndcg_at_5
      value: 30.514000000000003
    - type: precision_at_1
      value: 30.318
    - type: precision_at_10
      value: 10.981
    - type: precision_at_100
      value: 1.889
    - type: precision_at_1000
      value: 0.234
    - type: precision_at_20
      value: 6.643000000000001
    - type: precision_at_3
      value: 22.166
    - type: precision_at_5
      value: 17.477999999999998
    - type: recall_at_1
      value: 13.236
    - type: recall_at_10
      value: 41.461
    - type: recall_at_100
      value: 75.008
    - type: recall_at_1000
      value: 96.775
    - type: recall_at_20
      value: 50.754
    - type: recall_at_3
      value: 26.081
    - type: recall_at_5
      value: 33.168
    task:
      type: Retrieval
  - dataset:
      config: eng-cmn
      name: MTEB XPQARetrieval (eng-cmn)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 37.504
    - type: map_at_1
      value: 16.019
    - type: map_at_10
      value: 30.794
    - type: map_at_100
      value: 33.157
    - type: map_at_1000
      value: 33.324999999999996
    - type: map_at_20
      value: 32.161
    - type: map_at_3
      value: 25.372
    - type: map_at_5
      value: 28.246
    - type: mrr_at_1
      value: 30.461165048543688
    - type: mrr_at_10
      value: 39.393107566651224
    - type: mrr_at_100
      value: 40.570039540602295
    - type: mrr_at_1000
      value: 40.6306116407744
    - type: mrr_at_20
      value: 40.09428159978876
    - type: mrr_at_3
      value: 37.176375404530745
    - type: mrr_at_5
      value: 38.09870550161812
    - type: nauc_map_at_1000_diff1
      value: 30.82306881892873
    - type: nauc_map_at_1000_max
      value: 5.877636000666466
    - type: nauc_map_at_1000_std
      value: -30.7140513386797
    - type: nauc_map_at_100_diff1
      value: 30.85192449151961
    - type: nauc_map_at_100_max
      value: 5.809195131550909
    - type: nauc_map_at_100_std
      value: -30.838556702972063
    - type: nauc_map_at_10_diff1
      value: 30.50359163635058
    - type: nauc_map_at_10_max
      value: 6.373491595869303
    - type: nauc_map_at_10_std
      value: -29.89368007827676
    - type: nauc_map_at_1_diff1
      value: 38.60240510083884
    - type: nauc_map_at_1_max
      value: 10.407392664609139
    - type: nauc_map_at_1_std
      value: -17.76327278732833
    - type: nauc_map_at_20_diff1
      value: 30.897489125753598
    - type: nauc_map_at_20_max
      value: 5.9303381898248
    - type: nauc_map_at_20_std
      value: -30.863345188760515
    - type: nauc_map_at_3_diff1
      value: 32.8150951852729
    - type: nauc_map_at_3_max
      value: 7.671931402215177
    - type: nauc_map_at_3_std
      value: -25.654809758216533
    - type: nauc_map_at_5_diff1
      value: 31.19558194781019
    - type: nauc_map_at_5_max
      value: 6.426885613116939
    - type: nauc_map_at_5_std
      value: -28.609027858850016
    - type: nauc_mrr_at_1000_diff1
      value: 30.7596332048733
    - type: nauc_mrr_at_1000_max
      value: 1.1970748115580212
    - type: nauc_mrr_at_1000_std
      value: -34.647570668150216
    - type: nauc_mrr_at_100_diff1
      value: 30.74693370788581
    - type: nauc_mrr_at_100_max
      value: 1.1673272262754841
    - type: nauc_mrr_at_100_std
      value: -34.67761028542745
    - type: nauc_mrr_at_10_diff1
      value: 30.537820575183076
    - type: nauc_mrr_at_10_max
      value: 1.0261868725502707
    - type: nauc_mrr_at_10_std
      value: -34.999990560631204
    - type: nauc_mrr_at_1_diff1
      value: 35.51868580113285
    - type: nauc_mrr_at_1_max
      value: 5.117103773147307
    - type: nauc_mrr_at_1_std
      value: -30.633913466736956
    - type: nauc_mrr_at_20_diff1
      value: 30.67318175430903
    - type: nauc_mrr_at_20_max
      value: 1.0979983974981327
    - type: nauc_mrr_at_20_std
      value: -34.8388339739997
    - type: nauc_mrr_at_3_diff1
      value: 30.884642006045702
    - type: nauc_mrr_at_3_max
      value: 1.7970996544095983
    - type: nauc_mrr_at_3_std
      value: -34.290172894906085
    - type: nauc_mrr_at_5_diff1
      value: 30.89687518368571
    - type: nauc_mrr_at_5_max
      value: 1.2123714988495347
    - type: nauc_mrr_at_5_std
      value: -35.01704580471926
    - type: nauc_ndcg_at_1000_diff1
      value: 29.214476799077342
    - type: nauc_ndcg_at_1000_max
      value: 3.6379035546112872
    - type: nauc_ndcg_at_1000_std
      value: -32.35757522049194
    - type: nauc_ndcg_at_100_diff1
      value: 29.130004541376298
    - type: nauc_ndcg_at_100_max
      value: 2.9580589185293045
    - type: nauc_ndcg_at_100_std
      value: -33.26884643871724
    - type: nauc_ndcg_at_10_diff1
      value: 28.521001084366393
    - type: nauc_ndcg_at_10_max
      value: 3.630223957267483
    - type: nauc_ndcg_at_10_std
      value: -33.14524140940815
    - type: nauc_ndcg_at_1_diff1
      value: 35.51868580113285
    - type: nauc_ndcg_at_1_max
      value: 5.117103773147307
    - type: nauc_ndcg_at_1_std
      value: -30.633913466736956
    - type: nauc_ndcg_at_20_diff1
      value: 29.194462756848782
    - type: nauc_ndcg_at_20_max
      value: 2.61162903136461
    - type: nauc_ndcg_at_20_std
      value: -34.59161403211834
    - type: nauc_ndcg_at_3_diff1
      value: 30.183555327135203
    - type: nauc_ndcg_at_3_max
      value: 5.61949040917093
    - type: nauc_ndcg_at_3_std
      value: -30.350117794058175
    - type: nauc_ndcg_at_5_diff1
      value: 29.74420394139971
    - type: nauc_ndcg_at_5_max
      value: 3.952183813937688
    - type: nauc_ndcg_at_5_std
      value: -31.807833795302038
    - type: nauc_precision_at_1000_diff1
      value: -5.467049121617333
    - type: nauc_precision_at_1000_max
      value: -3.993986884198271
    - type: nauc_precision_at_1000_std
      value: -13.703967324212224
    - type: nauc_precision_at_100_diff1
      value: 1.5585428307943647
    - type: nauc_precision_at_100_max
      value: -4.250455723613214
    - type: nauc_precision_at_100_std
      value: -22.294689856776493
    - type: nauc_precision_at_10_diff1
      value: 11.076036917255259
    - type: nauc_precision_at_10_max
      value: -1.5859394644365377
    - type: nauc_precision_at_10_std
      value: -34.94912594413202
    - type: nauc_precision_at_1_diff1
      value: 35.51868580113285
    - type: nauc_precision_at_1_max
      value: 5.117103773147307
    - type: nauc_precision_at_1_std
      value: -30.633913466736956
    - type: nauc_precision_at_20_diff1
      value: 9.311484455773828
    - type: nauc_precision_at_20_max
      value: -3.678383428592432
    - type: nauc_precision_at_20_std
      value: -33.700002761401635
    - type: nauc_precision_at_3_diff1
      value: 19.2787260874381
    - type: nauc_precision_at_3_max
      value: 0.18292109396940018
    - type: nauc_precision_at_3_std
      value: -35.23939824276542
    - type: nauc_precision_at_5_diff1
      value: 14.97930592298584
    - type: nauc_precision_at_5_max
      value: -1.63540635880963
    - type: nauc_precision_at_5_std
      value: -35.908283558321315
    - type: nauc_recall_at_1000_diff1
      value: 26.63056473607804
    - type: nauc_recall_at_1000_max
      value: 62.7304558520689
    - type: nauc_recall_at_1000_std
      value: 58.12421701377561
    - type: nauc_recall_at_100_diff1
      value: 21.42127379898579
    - type: nauc_recall_at_100_max
      value: 1.4748203516921914
    - type: nauc_recall_at_100_std
      value: -27.56467339041136
    - type: nauc_recall_at_10_diff1
      value: 21.20479652609812
    - type: nauc_recall_at_10_max
      value: 1.7394881489709888
    - type: nauc_recall_at_10_std
      value: -32.15116902585072
    - type: nauc_recall_at_1_diff1
      value: 38.60240510083884
    - type: nauc_recall_at_1_max
      value: 10.407392664609139
    - type: nauc_recall_at_1_std
      value: -17.76327278732833
    - type: nauc_recall_at_20_diff1
      value: 23.049652721582632
    - type: nauc_recall_at_20_max
      value: -1.7715787106286838
    - type: nauc_recall_at_20_std
      value: -36.14203686002867
    - type: nauc_recall_at_3_diff1
      value: 26.522179829461873
    - type: nauc_recall_at_3_max
      value: 6.078208732431124
    - type: nauc_recall_at_3_std
      value: -25.02625711226274
    - type: nauc_recall_at_5_diff1
      value: 24.19538553561693
    - type: nauc_recall_at_5_max
      value: 2.4963810785503524
    - type: nauc_recall_at_5_std
      value: -30.449635496921257
    - type: ndcg_at_1
      value: 30.461
    - type: ndcg_at_10
      value: 37.504
    - type: ndcg_at_100
      value: 46.156000000000006
    - type: ndcg_at_1000
      value: 48.985
    - type: ndcg_at_20
      value: 41.025
    - type: ndcg_at_3
      value: 32.165
    - type: ndcg_at_5
      value: 33.072
    - type: precision_at_1
      value: 30.461
    - type: precision_at_10
      value: 11.032
    - type: precision_at_100
      value: 1.8870000000000002
    - type: precision_at_1000
      value: 0.22499999999999998
    - type: precision_at_20
      value: 6.833
    - type: precision_at_3
      value: 22.532
    - type: precision_at_5
      value: 16.966
    - type: recall_at_1
      value: 16.019
    - type: recall_at_10
      value: 47.557
    - type: recall_at_100
      value: 80.376
    - type: recall_at_1000
      value: 98.904
    - type: recall_at_20
      value: 58.48100000000001
    - type: recall_at_3
      value: 30.682
    - type: recall_at_5
      value: 36.714999999999996
    task:
      type: Retrieval
  - dataset:
      config: eng-spa
      name: MTEB XPQARetrieval (eng-spa)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 53.359
    - type: map_at_1
      value: 22.892000000000003
    - type: map_at_10
      value: 45.773
    - type: map_at_100
      value: 47.778999999999996
    - type: map_at_1000
      value: 47.882999999999996
    - type: map_at_20
      value: 46.869
    - type: map_at_3
      value: 37.643
    - type: map_at_5
      value: 43.120999999999995
    - type: mrr_at_1
      value: 47.28877679697352
    - type: mrr_at_10
      value: 56.95890630316857
    - type: mrr_at_100
      value: 57.71103367009639
    - type: mrr_at_1000
      value: 57.73661441948852
    - type: mrr_at_20
      value: 57.37701091311334
    - type: mrr_at_3
      value: 54.74989491382929
    - type: mrr_at_5
      value: 56.08659100462372
    - type: nauc_map_at_1000_diff1
      value: 27.8347129954991
    - type: nauc_map_at_1000_max
      value: 38.04300600762859
    - type: nauc_map_at_1000_std
      value: -18.294653328262868
    - type: nauc_map_at_100_diff1
      value: 27.818449297770858
    - type: nauc_map_at_100_max
      value: 38.03533462156633
    - type: nauc_map_at_100_std
      value: -18.332989980880644
    - type: nauc_map_at_10_diff1
      value: 27.520664180018358
    - type: nauc_map_at_10_max
      value: 37.67109855753314
    - type: nauc_map_at_10_std
      value: -18.496721673888683
    - type: nauc_map_at_1_diff1
      value: 37.56020148060502
    - type: nauc_map_at_1_max
      value: 10.298394230150745
    - type: nauc_map_at_1_std
      value: -20.41359936101547
    - type: nauc_map_at_20_diff1
      value: 27.615023038189722
    - type: nauc_map_at_20_max
      value: 37.808525116320254
    - type: nauc_map_at_20_std
      value: -18.49235775420803
    - type: nauc_map_at_3_diff1
      value: 30.797347567428424
    - type: nauc_map_at_3_max
      value: 29.374407828869497
    - type: nauc_map_at_3_std
      value: -19.75905772914969
    - type: nauc_map_at_5_diff1
      value: 28.431802888884803
    - type: nauc_map_at_5_max
      value: 35.57723911610521
    - type: nauc_map_at_5_std
      value: -19.093588845366824
    - type: nauc_mrr_at_1000_diff1
      value: 33.263611009054586
    - type: nauc_mrr_at_1000_max
      value: 40.620639901613664
    - type: nauc_mrr_at_1000_std
      value: -17.083016011032036
    - type: nauc_mrr_at_100_diff1
      value: 33.25375012559163
    - type: nauc_mrr_at_100_max
      value: 40.62376205172005
    - type: nauc_mrr_at_100_std
      value: -17.091930575226684
    - type: nauc_mrr_at_10_diff1
      value: 33.05787202690095
    - type: nauc_mrr_at_10_max
      value: 40.4516362611674
    - type: nauc_mrr_at_10_std
      value: -17.088910666499892
    - type: nauc_mrr_at_1_diff1
      value: 36.424151087824555
    - type: nauc_mrr_at_1_max
      value: 40.955715626650445
    - type: nauc_mrr_at_1_std
      value: -16.56636409111209
    - type: nauc_mrr_at_20_diff1
      value: 33.12029456858138
    - type: nauc_mrr_at_20_max
      value: 40.56409347292635
    - type: nauc_mrr_at_20_std
      value: -17.102034817242068
    - type: nauc_mrr_at_3_diff1
      value: 33.52377926814156
    - type: nauc_mrr_at_3_max
      value: 40.824911575046876
    - type: nauc_mrr_at_3_std
      value: -16.855935748811092
    - type: nauc_mrr_at_5_diff1
      value: 33.08646471768442
    - type: nauc_mrr_at_5_max
      value: 40.59323589955881
    - type: nauc_mrr_at_5_std
      value: -16.77829710500156
    - type: nauc_ndcg_at_1000_diff1
      value: 28.741186244590207
    - type: nauc_ndcg_at_1000_max
      value: 40.0113825410539
    - type: nauc_ndcg_at_1000_std
      value: -17.15655081742458
    - type: nauc_ndcg_at_100_diff1
      value: 28.680521359782972
    - type: nauc_ndcg_at_100_max
      value: 39.94751899984445
    - type: nauc_ndcg_at_100_std
      value: -17.82813814043932
    - type: nauc_ndcg_at_10_diff1
      value: 27.22858072673168
    - type: nauc_ndcg_at_10_max
      value: 38.600188968554725
    - type: nauc_ndcg_at_10_std
      value: -18.517203924893614
    - type: nauc_ndcg_at_1_diff1
      value: 36.424151087824555
    - type: nauc_ndcg_at_1_max
      value: 40.955715626650445
    - type: nauc_ndcg_at_1_std
      value: -16.56636409111209
    - type: nauc_ndcg_at_20_diff1
      value: 27.56875900623774
    - type: nauc_ndcg_at_20_max
      value: 38.95264310199067
    - type: nauc_ndcg_at_20_std
      value: -18.709973965688445
    - type: nauc_ndcg_at_3_diff1
      value: 28.682842749851574
    - type: nauc_ndcg_at_3_max
      value: 38.361215408395964
    - type: nauc_ndcg_at_3_std
      value: -16.800291231827515
    - type: nauc_ndcg_at_5_diff1
      value: 28.178239259093484
    - type: nauc_ndcg_at_5_max
      value: 36.77096292606479
    - type: nauc_ndcg_at_5_std
      value: -18.718861696641145
    - type: nauc_precision_at_1000_diff1
      value: -7.3686253252869305
    - type: nauc_precision_at_1000_max
      value: 31.98896996987639
    - type: nauc_precision_at_1000_std
      value: 13.125659676392267
    - type: nauc_precision_at_100_diff1
      value: -2.8239113056969156
    - type: nauc_precision_at_100_max
      value: 36.95062472971812
    - type: nauc_precision_at_100_std
      value: 7.230228733647562
    - type: nauc_precision_at_10_diff1
      value: 2.5515545798843555
    - type: nauc_precision_at_10_max
      value: 45.46146019314904
    - type: nauc_precision_at_10_std
      value: -1.3249340536211553
    - type: nauc_precision_at_1_diff1
      value: 36.424151087824555
    - type: nauc_precision_at_1_max
      value: 40.955715626650445
    - type: nauc_precision_at_1_std
      value: -16.56636409111209
    - type: nauc_precision_at_20_diff1
      value: 0.7202861770489576
    - type: nauc_precision_at_20_max
      value: 41.9937596214609
    - type: nauc_precision_at_20_std
      value: 0.2756400069730064
    - type: nauc_precision_at_3_diff1
      value: 12.89221206929447
    - type: nauc_precision_at_3_max
      value: 48.57775126381142
    - type: nauc_precision_at_3_std
      value: -8.042242254131068
    - type: nauc_precision_at_5_diff1
      value: 7.063616193387763
    - type: nauc_precision_at_5_max
      value: 47.26496887331675
    - type: nauc_precision_at_5_std
      value: -4.735805200913049
    - type: nauc_recall_at_1000_diff1
      value: 2.6650052980682224
    - type: nauc_recall_at_1000_max
      value: 81.94826279951472
    - type: nauc_recall_at_1000_std
      value: 48.46012388224573
    - type: nauc_recall_at_100_diff1
      value: 24.516371948375827
    - type: nauc_recall_at_100_max
      value: 39.17639620389552
    - type: nauc_recall_at_100_std
      value: -17.884197602579533
    - type: nauc_recall_at_10_diff1
      value: 19.93892097640112
    - type: nauc_recall_at_10_max
      value: 33.079079440022106
    - type: nauc_recall_at_10_std
      value: -20.22227622801884
    - type: nauc_recall_at_1_diff1
      value: 37.56020148060502
    - type: nauc_recall_at_1_max
      value: 10.298394230150745
    - type: nauc_recall_at_1_std
      value: -20.41359936101547
    - type: nauc_recall_at_20_diff1
      value: 20.363784035670633
    - type: nauc_recall_at_20_max
      value: 33.39352971625336
    - type: nauc_recall_at_20_std
      value: -21.712050932168875
    - type: nauc_recall_at_3_diff1
      value: 26.220072121604655
    - type: nauc_recall_at_3_max
      value: 25.853218030218507
    - type: nauc_recall_at_3_std
      value: -17.830613372910907
    - type: nauc_recall_at_5_diff1
      value: 22.25850162680252
    - type: nauc_recall_at_5_max
      value: 30.89620539042785
    - type: nauc_recall_at_5_std
      value: -19.16786434439169
    - type: ndcg_at_1
      value: 47.288999999999994
    - type: ndcg_at_10
      value: 53.359
    - type: ndcg_at_100
      value: 60.25899999999999
    - type: ndcg_at_1000
      value: 61.902
    - type: ndcg_at_20
      value: 56.025000000000006
    - type: ndcg_at_3
      value: 47.221999999999994
    - type: ndcg_at_5
      value: 49.333
    - type: precision_at_1
      value: 47.288999999999994
    - type: precision_at_10
      value: 16.003
    - type: precision_at_100
      value: 2.221
    - type: precision_at_1000
      value: 0.246
    - type: precision_at_20
      value: 8.985
    - type: precision_at_3
      value: 34.510000000000005
    - type: precision_at_5
      value: 26.961000000000002
    - type: recall_at_1
      value: 22.892000000000003
    - type: recall_at_10
      value: 62.928
    - type: recall_at_100
      value: 89.105
    - type: recall_at_1000
      value: 99.319
    - type: recall_at_20
      value: 71.387
    - type: recall_at_3
      value: 43.492999999999995
    - type: recall_at_5
      value: 53.529
    task:
      type: Retrieval
  - dataset:
      config: eng-fra
      name: MTEB XPQARetrieval (eng-fra)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 54.888000000000005
    - type: map_at_1
      value: 26.079
    - type: map_at_10
      value: 47.434
    - type: map_at_100
      value: 49.376
    - type: map_at_1000
      value: 49.461
    - type: map_at_20
      value: 48.634
    - type: map_at_3
      value: 40.409
    - type: map_at_5
      value: 44.531
    - type: mrr_at_1
      value: 46.86248331108144
    - type: mrr_at_10
      value: 56.45506177548896
    - type: mrr_at_100
      value: 57.20360629445577
    - type: mrr_at_1000
      value: 57.227004696897986
    - type: mrr_at_20
      value: 56.905302765737865
    - type: mrr_at_3
      value: 54.09434801958164
    - type: mrr_at_5
      value: 55.40943480195811
    - type: nauc_map_at_1000_diff1
      value: 37.739936045535885
    - type: nauc_map_at_1000_max
      value: 35.92625003516368
    - type: nauc_map_at_1000_std
      value: -15.825119611638398
    - type: nauc_map_at_100_diff1
      value: 37.71697833661983
    - type: nauc_map_at_100_max
      value: 35.91174068136317
    - type: nauc_map_at_100_std
      value: -15.838841891589006
    - type: nauc_map_at_10_diff1
      value: 37.52309268219689
    - type: nauc_map_at_10_max
      value: 35.4887130483351
    - type: nauc_map_at_10_std
      value: -16.61132378136234
    - type: nauc_map_at_1_diff1
      value: 42.705087329207984
    - type: nauc_map_at_1_max
      value: 12.047671550242974
    - type: nauc_map_at_1_std
      value: -17.156030827065834
    - type: nauc_map_at_20_diff1
      value: 37.59446680137666
    - type: nauc_map_at_20_max
      value: 35.80559546695052
    - type: nauc_map_at_20_std
      value: -16.158338316249786
    - type: nauc_map_at_3_diff1
      value: 38.618415267131816
    - type: nauc_map_at_3_max
      value: 27.030227996183925
    - type: nauc_map_at_3_std
      value: -18.962500694157857
    - type: nauc_map_at_5_diff1
      value: 37.980845601534256
    - type: nauc_map_at_5_max
      value: 32.82374761283266
    - type: nauc_map_at_5_std
      value: -17.856875825229565
    - type: nauc_mrr_at_1000_diff1
      value: 40.26059509279346
    - type: nauc_mrr_at_1000_max
      value: 39.28453752990871
    - type: nauc_mrr_at_1000_std
      value: -13.306217279524212
    - type: nauc_mrr_at_100_diff1
      value: 40.23390833398881
    - type: nauc_mrr_at_100_max
      value: 39.26041461025653
    - type: nauc_mrr_at_100_std
      value: -13.317700798873153
    - type: nauc_mrr_at_10_diff1
      value: 40.163737640180145
    - type: nauc_mrr_at_10_max
      value: 39.27138538165913
    - type: nauc_mrr_at_10_std
      value: -13.472971360323038
    - type: nauc_mrr_at_1_diff1
      value: 42.95339241383707
    - type: nauc_mrr_at_1_max
      value: 40.62982307619158
    - type: nauc_mrr_at_1_std
      value: -10.429597045942748
    - type: nauc_mrr_at_20_diff1
      value: 40.23703505923782
    - type: nauc_mrr_at_20_max
      value: 39.27051308063652
    - type: nauc_mrr_at_20_std
      value: -13.390197643922038
    - type: nauc_mrr_at_3_diff1
      value: 40.5721313555661
    - type: nauc_mrr_at_3_max
      value: 39.254774354468594
    - type: nauc_mrr_at_3_std
      value: -13.773803807863827
    - type: nauc_mrr_at_5_diff1
      value: 40.41081287079734
    - type: nauc_mrr_at_5_max
      value: 39.515241132077335
    - type: nauc_mrr_at_5_std
      value: -13.306544090087336
    - type: nauc_ndcg_at_1000_diff1
      value: 38.04772268296103
    - type: nauc_ndcg_at_1000_max
      value: 38.03364565521176
    - type: nauc_ndcg_at_1000_std
      value: -14.203182726102263
    - type: nauc_ndcg_at_100_diff1
      value: 37.51752795463643
    - type: nauc_ndcg_at_100_max
      value: 37.809671511710604
    - type: nauc_ndcg_at_100_std
      value: -13.880578225081408
    - type: nauc_ndcg_at_10_diff1
      value: 36.78438984005559
    - type: nauc_ndcg_at_10_max
      value: 36.98105155993232
    - type: nauc_ndcg_at_10_std
      value: -16.886308645939113
    - type: nauc_ndcg_at_1_diff1
      value: 42.95339241383707
    - type: nauc_ndcg_at_1_max
      value: 40.62982307619158
    - type: nauc_ndcg_at_1_std
      value: -10.429597045942748
    - type: nauc_ndcg_at_20_diff1
      value: 36.94164323893683
    - type: nauc_ndcg_at_20_max
      value: 37.333583379288285
    - type: nauc_ndcg_at_20_std
      value: -15.853318071434716
    - type: nauc_ndcg_at_3_diff1
      value: 36.905604845477384
    - type: nauc_ndcg_at_3_max
      value: 35.10252586688781
    - type: nauc_ndcg_at_3_std
      value: -17.128435988977742
    - type: nauc_ndcg_at_5_diff1
      value: 37.96742463612705
    - type: nauc_ndcg_at_5_max
      value: 34.65945109443365
    - type: nauc_ndcg_at_5_std
      value: -17.916428667861183
    - type: nauc_precision_at_1000_diff1
      value: -3.740861894117653
    - type: nauc_precision_at_1000_max
      value: 31.993854396874177
    - type: nauc_precision_at_1000_std
      value: 17.445629474196448
    - type: nauc_precision_at_100_diff1
      value: -0.4825948747911606
    - type: nauc_precision_at_100_max
      value: 35.834638448782954
    - type: nauc_precision_at_100_std
      value: 16.82718796079511
    - type: nauc_precision_at_10_diff1
      value: 8.285949866268147
    - type: nauc_precision_at_10_max
      value: 45.3292519726866
    - type: nauc_precision_at_10_std
      value: 4.5574850748441555
    - type: nauc_precision_at_1_diff1
      value: 42.95339241383707
    - type: nauc_precision_at_1_max
      value: 40.62982307619158
    - type: nauc_precision_at_1_std
      value: -10.429597045942748
    - type: nauc_precision_at_20_diff1
      value: 4.890590733611442
    - type: nauc_precision_at_20_max
      value: 41.83051757078859
    - type: nauc_precision_at_20_std
      value: 9.197347125630467
    - type: nauc_precision_at_3_diff1
      value: 17.79940075411976
    - type: nauc_precision_at_3_max
      value: 45.224103632426946
    - type: nauc_precision_at_3_std
      value: -5.017203435609909
    - type: nauc_precision_at_5_diff1
      value: 13.548063145911929
    - type: nauc_precision_at_5_max
      value: 46.84837547409909
    - type: nauc_precision_at_5_std
      value: -0.8925939386354484
    - type: nauc_recall_at_1000_diff1
      value: 74.48441717138078
    - type: nauc_recall_at_1000_max
      value: 74.66717137705027
    - type: nauc_recall_at_1000_std
      value: 0.24030117471512125
    - type: nauc_recall_at_100_diff1
      value: 22.553777341988656
    - type: nauc_recall_at_100_max
      value: 31.67861029246527
    - type: nauc_recall_at_100_std
      value: 0.2707450517253687
    - type: nauc_recall_at_10_diff1
      value: 28.490866614443235
    - type: nauc_recall_at_10_max
      value: 31.722970141434352
    - type: nauc_recall_at_10_std
      value: -21.97893365028007
    - type: nauc_recall_at_1_diff1
      value: 42.705087329207984
    - type: nauc_recall_at_1_max
      value: 12.047671550242974
    - type: nauc_recall_at_1_std
      value: -17.156030827065834
    - type: nauc_recall_at_20_diff1
      value: 27.44043454173112
    - type: nauc_recall_at_20_max
      value: 31.454281772040716
    - type: nauc_recall_at_20_std
      value: -20.1735695305415
    - type: nauc_recall_at_3_diff1
      value: 34.08447534706394
    - type: nauc_recall_at_3_max
      value: 21.793973773840865
    - type: nauc_recall_at_3_std
      value: -22.753978372378906
    - type: nauc_recall_at_5_diff1
      value: 33.59686526199479
    - type: nauc_recall_at_5_max
      value: 29.188889073761302
    - type: nauc_recall_at_5_std
      value: -21.96156333744562
    - type: ndcg_at_1
      value: 46.861999999999995
    - type: ndcg_at_10
      value: 54.888000000000005
    - type: ndcg_at_100
      value: 61.477000000000004
    - type: ndcg_at_1000
      value: 62.768
    - type: ndcg_at_20
      value: 57.812
    - type: ndcg_at_3
      value: 48.721
    - type: ndcg_at_5
      value: 50.282000000000004
    - type: precision_at_1
      value: 46.861999999999995
    - type: precision_at_10
      value: 15.167
    - type: precision_at_100
      value: 2.072
    - type: precision_at_1000
      value: 0.22499999999999998
    - type: precision_at_20
      value: 8.672
    - type: precision_at_3
      value: 33.066
    - type: precision_at_5
      value: 24.726
    - type: recall_at_1
      value: 26.079
    - type: recall_at_10
      value: 66.095
    - type: recall_at_100
      value: 91.65299999999999
    - type: recall_at_1000
      value: 99.83999999999999
    - type: recall_at_20
      value: 75.28
    - type: recall_at_3
      value: 46.874
    - type: recall_at_5
      value: 55.062
    task:
      type: Retrieval
  - dataset:
      config: pol-eng
      name: MTEB XPQARetrieval (pol-eng)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 50.831
    - type: map_at_1
      value: 25.549
    - type: map_at_10
      value: 44.432
    - type: map_at_100
      value: 46.431
    - type: map_at_1000
      value: 46.525
    - type: map_at_20
      value: 45.595
    - type: map_at_3
      value: 38.574000000000005
    - type: map_at_5
      value: 42.266999999999996
    - type: mrr_at_1
      value: 43.5006435006435
    - type: mrr_at_10
      value: 51.561255132683684
    - type: mrr_at_100
      value: 52.59912482635216
    - type: mrr_at_1000
      value: 52.631337587043056
    - type: mrr_at_20
      value: 52.23234440063273
    - type: mrr_at_3
      value: 48.97039897039895
    - type: mrr_at_5
      value: 50.31531531531527
    - type: nauc_map_at_1000_diff1
      value: 35.907901295900174
    - type: nauc_map_at_1000_max
      value: 24.573763602041687
    - type: nauc_map_at_1000_std
      value: -29.524077960309313
    - type: nauc_map_at_100_diff1
      value: 35.86869121827827
    - type: nauc_map_at_100_max
      value: 24.532343818487494
    - type: nauc_map_at_100_std
      value: -29.613979124488864
    - type: nauc_map_at_10_diff1
      value: 35.90171794022391
    - type: nauc_map_at_10_max
      value: 23.90914892943268
    - type: nauc_map_at_10_std
      value: -30.43698820061533
    - type: nauc_map_at_1_diff1
      value: 50.80313333312038
    - type: nauc_map_at_1_max
      value: 16.649890421888156
    - type: nauc_map_at_1_std
      value: -22.323989416471683
    - type: nauc_map_at_20_diff1
      value: 35.77755470212964
    - type: nauc_map_at_20_max
      value: 24.199895270297034
    - type: nauc_map_at_20_std
      value: -30.223411960170647
    - type: nauc_map_at_3_diff1
      value: 38.964124882315936
    - type: nauc_map_at_3_max
      value: 21.187432510177167
    - type: nauc_map_at_3_std
      value: -28.976663506389887
    - type: nauc_map_at_5_diff1
      value: 36.04644236616672
    - type: nauc_map_at_5_max
      value: 23.501186429317094
    - type: nauc_map_at_5_std
      value: -30.068144596060748
    - type: nauc_mrr_at_1000_diff1
      value: 41.36555452105447
    - type: nauc_mrr_at_1000_max
      value: 26.376799280402867
    - type: nauc_mrr_at_1000_std
      value: -30.008603028757424
    - type: nauc_mrr_at_100_diff1
      value: 41.35523965220727
    - type: nauc_mrr_at_100_max
      value: 26.402612115967706
    - type: nauc_mrr_at_100_std
      value: -29.991754627128024
    - type: nauc_mrr_at_10_diff1
      value: 41.001395127259315
    - type: nauc_mrr_at_10_max
      value: 26.104860505051384
    - type: nauc_mrr_at_10_std
      value: -30.38420449487516
    - type: nauc_mrr_at_1_diff1
      value: 44.882846373248206
    - type: nauc_mrr_at_1_max
      value: 26.61905322890808
    - type: nauc_mrr_at_1_std
      value: -28.724565662206153
    - type: nauc_mrr_at_20_diff1
      value: 41.278009142648834
    - type: nauc_mrr_at_20_max
      value: 26.284565529087295
    - type: nauc_mrr_at_20_std
      value: -30.19549140549242
    - type: nauc_mrr_at_3_diff1
      value: 41.74663893951077
    - type: nauc_mrr_at_3_max
      value: 26.263048464325884
    - type: nauc_mrr_at_3_std
      value: -30.676733442965688
    - type: nauc_mrr_at_5_diff1
      value: 41.11461477846568
    - type: nauc_mrr_at_5_max
      value: 25.94713927964926
    - type: nauc_mrr_at_5_std
      value: -30.317066480767817
    - type: nauc_ndcg_at_1000_diff1
      value: 36.34161052445199
    - type: nauc_ndcg_at_1000_max
      value: 26.321036033696206
    - type: nauc_ndcg_at_1000_std
      value: -27.59146917115399
    - type: nauc_ndcg_at_100_diff1
      value: 35.66557800007035
    - type: nauc_ndcg_at_100_max
      value: 26.282211208336136
    - type: nauc_ndcg_at_100_std
      value: -27.905634124461333
    - type: nauc_ndcg_at_10_diff1
      value: 35.34872687407275
    - type: nauc_ndcg_at_10_max
      value: 24.018561915792272
    - type: nauc_ndcg_at_10_std
      value: -31.57712772869015
    - type: nauc_ndcg_at_1_diff1
      value: 44.882846373248206
    - type: nauc_ndcg_at_1_max
      value: 26.865602442152554
    - type: nauc_ndcg_at_1_std
      value: -28.509295454329152
    - type: nauc_ndcg_at_20_diff1
      value: 35.46177768045546
    - type: nauc_ndcg_at_20_max
      value: 24.921273675141542
    - type: nauc_ndcg_at_20_std
      value: -30.84348812979793
    - type: nauc_ndcg_at_3_diff1
      value: 36.84688489063923
    - type: nauc_ndcg_at_3_max
      value: 24.088513229463736
    - type: nauc_ndcg_at_3_std
      value: -30.05640995379297
    - type: nauc_ndcg_at_5_diff1
      value: 35.623143276796185
    - type: nauc_ndcg_at_5_max
      value: 23.76654250474061
    - type: nauc_ndcg_at_5_std
      value: -30.87847710074466
    - type: nauc_precision_at_1000_diff1
      value: -16.270532533886932
    - type: nauc_precision_at_1000_max
      value: 17.37365042394671
    - type: nauc_precision_at_1000_std
      value: 16.27166715693082
    - type: nauc_precision_at_100_diff1
      value: -13.175264889436313
    - type: nauc_precision_at_100_max
      value: 19.488571046893963
    - type: nauc_precision_at_100_std
      value: 9.055429698007798
    - type: nauc_precision_at_10_diff1
      value: 0.6806938753592942
    - type: nauc_precision_at_10_max
      value: 21.933083960522616
    - type: nauc_precision_at_10_std
      value: -18.2147036942157
    - type: nauc_precision_at_1_diff1
      value: 44.882846373248206
    - type: nauc_precision_at_1_max
      value: 26.865602442152554
    - type: nauc_precision_at_1_std
      value: -28.509295454329152
    - type: nauc_precision_at_20_diff1
      value: -4.318119150162302
    - type: nauc_precision_at_20_max
      value: 21.089702301041687
    - type: nauc_precision_at_20_std
      value: -10.333077681479546
    - type: nauc_precision_at_3_diff1
      value: 11.496076462671107
    - type: nauc_precision_at_3_max
      value: 23.018301549827008
    - type: nauc_precision_at_3_std
      value: -23.98652995416454
    - type: nauc_precision_at_5_diff1
      value: 4.271050668117355
    - type: nauc_precision_at_5_max
      value: 23.61051327966779
    - type: nauc_precision_at_5_std
      value: -21.557618503107847
    - type: nauc_recall_at_1000_diff1
      value: 62.23955911850697
    - type: nauc_recall_at_1000_max
      value: 83.20491723365542
    - type: nauc_recall_at_1000_std
      value: 66.5173462601958
    - type: nauc_recall_at_100_diff1
      value: 20.503778602988177
    - type: nauc_recall_at_100_max
      value: 29.379026288767506
    - type: nauc_recall_at_100_std
      value: -16.139120874540573
    - type: nauc_recall_at_10_diff1
      value: 27.659110249896557
    - type: nauc_recall_at_10_max
      value: 19.69557968026332
    - type: nauc_recall_at_10_std
      value: -33.95657132767551
    - type: nauc_recall_at_1_diff1
      value: 50.80313333312038
    - type: nauc_recall_at_1_max
      value: 16.649890421888156
    - type: nauc_recall_at_1_std
      value: -22.323989416471683
    - type: nauc_recall_at_20_diff1
      value: 27.084453724565176
    - type: nauc_recall_at_20_max
      value: 21.40080632474994
    - type: nauc_recall_at_20_std
      value: -32.83683639340239
    - type: nauc_recall_at_3_diff1
      value: 34.32950941333572
    - type: nauc_recall_at_3_max
      value: 18.55616615958199
    - type: nauc_recall_at_3_std
      value: -30.375983327454076
    - type: nauc_recall_at_5_diff1
      value: 29.44516734974564
    - type: nauc_recall_at_5_max
      value: 20.630543534300312
    - type: nauc_recall_at_5_std
      value: -31.30763062499127
    - type: ndcg_at_1
      value: 43.501
    - type: ndcg_at_10
      value: 50.831
    - type: ndcg_at_100
      value: 58.17099999999999
    - type: ndcg_at_1000
      value: 59.705
    - type: ndcg_at_20
      value: 54.047999999999995
    - type: ndcg_at_3
      value: 44.549
    - type: ndcg_at_5
      value: 46.861000000000004
    - type: precision_at_1
      value: 43.501
    - type: precision_at_10
      value: 12.895999999999999
    - type: precision_at_100
      value: 1.9
    - type: precision_at_1000
      value: 0.21
    - type: precision_at_20
      value: 7.593
    - type: precision_at_3
      value: 29.215000000000003
    - type: precision_at_5
      value: 21.57
    - type: recall_at_1
      value: 25.549
    - type: recall_at_10
      value: 61.795
    - type: recall_at_100
      value: 90.019
    - type: recall_at_1000
      value: 99.807
    - type: recall_at_20
      value: 72.096
    - type: recall_at_3
      value: 43.836999999999996
    - type: recall_at_5
      value: 51.714000000000006
    task:
      type: Retrieval
  - dataset:
      config: pol-pol
      name: MTEB XPQARetrieval (pol-pol)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 53.70399999999999
    - type: map_at_1
      value: 27.739000000000004
    - type: map_at_10
      value: 47.469
    - type: map_at_100
      value: 49.392
    - type: map_at_1000
      value: 49.483
    - type: map_at_20
      value: 48.646
    - type: map_at_3
      value: 41.467
    - type: map_at_5
      value: 45.467
    - type: mrr_at_1
      value: 47.00636942675159
    - type: mrr_at_10
      value: 54.63699322616519
    - type: mrr_at_100
      value: 55.54525182833755
    - type: mrr_at_1000
      value: 55.581331515356155
    - type: mrr_at_20
      value: 55.22918377451415
    - type: mrr_at_3
      value: 52.03821656050952
    - type: mrr_at_5
      value: 53.38216560509549
    - type: nauc_map_at_1000_diff1
      value: 45.03530825034854
    - type: nauc_map_at_1000_max
      value: 34.22740272603397
    - type: nauc_map_at_1000_std
      value: -30.428880484199244
    - type: nauc_map_at_100_diff1
      value: 44.978704455592805
    - type: nauc_map_at_100_max
      value: 34.20908357964765
    - type: nauc_map_at_100_std
      value: -30.47325365059666
    - type: nauc_map_at_10_diff1
      value: 44.9560579177672
    - type: nauc_map_at_10_max
      value: 33.70097588985278
    - type: nauc_map_at_10_std
      value: -31.205563222357885
    - type: nauc_map_at_1_diff1
      value: 57.94711780881773
    - type: nauc_map_at_1_max
      value: 21.60278071836319
    - type: nauc_map_at_1_std
      value: -23.273741268035923
    - type: nauc_map_at_20_diff1
      value: 44.97859054699532
    - type: nauc_map_at_20_max
      value: 34.153729150181846
    - type: nauc_map_at_20_std
      value: -30.97482545902907
    - type: nauc_map_at_3_diff1
      value: 47.52016138686765
    - type: nauc_map_at_3_max
      value: 30.176197065298417
    - type: nauc_map_at_3_std
      value: -29.90628984041898
    - type: nauc_map_at_5_diff1
      value: 45.36581638257985
    - type: nauc_map_at_5_max
      value: 33.697200263698036
    - type: nauc_map_at_5_std
      value: -31.165331120088453
    - type: nauc_mrr_at_1000_diff1
      value: 53.32889526818364
    - type: nauc_mrr_at_1000_max
      value: 36.104118340589736
    - type: nauc_mrr_at_1000_std
      value: -31.321132494516984
    - type: nauc_mrr_at_100_diff1
      value: 53.30695875258367
    - type: nauc_mrr_at_100_max
      value: 36.114890079024455
    - type: nauc_mrr_at_100_std
      value: -31.291749322117447
    - type: nauc_mrr_at_10_diff1
      value: 53.189084772141435
    - type: nauc_mrr_at_10_max
      value: 35.939061062282484
    - type: nauc_mrr_at_10_std
      value: -31.502185884653645
    - type: nauc_mrr_at_1_diff1
      value: 56.89368291041337
    - type: nauc_mrr_at_1_max
      value: 36.07581125496313
    - type: nauc_mrr_at_1_std
      value: -29.703764232519475
    - type: nauc_mrr_at_20_diff1
      value: 53.23955737199497
    - type: nauc_mrr_at_20_max
      value: 36.068824838215676
    - type: nauc_mrr_at_20_std
      value: -31.420039428197594
    - type: nauc_mrr_at_3_diff1
      value: 53.74385074861207
    - type: nauc_mrr_at_3_max
      value: 35.57054587735015
    - type: nauc_mrr_at_3_std
      value: -32.356894834537684
    - type: nauc_mrr_at_5_diff1
      value: 53.66669556981826
    - type: nauc_mrr_at_5_max
      value: 36.02102289605049
    - type: nauc_mrr_at_5_std
      value: -32.030437067359124
    - type: nauc_ndcg_at_1000_diff1
      value: 46.34900536768847
    - type: nauc_ndcg_at_1000_max
      value: 35.6314995837715
    - type: nauc_ndcg_at_1000_std
      value: -28.965103958822624
    - type: nauc_ndcg_at_100_diff1
      value: 45.1587893788861
    - type: nauc_ndcg_at_100_max
      value: 35.62430753595297
    - type: nauc_ndcg_at_100_std
      value: -28.77303405812772
    - type: nauc_ndcg_at_10_diff1
      value: 44.928781590765965
    - type: nauc_ndcg_at_10_max
      value: 34.315200006430366
    - type: nauc_ndcg_at_10_std
      value: -32.05164097076614
    - type: nauc_ndcg_at_1_diff1
      value: 57.228262350455125
    - type: nauc_ndcg_at_1_max
      value: 35.645285703387366
    - type: nauc_ndcg_at_1_std
      value: -29.893553821348718
    - type: nauc_ndcg_at_20_diff1
      value: 44.959903633039865
    - type: nauc_ndcg_at_20_max
      value: 35.493022926282755
    - type: nauc_ndcg_at_20_std
      value: -31.54989291850644
    - type: nauc_ndcg_at_3_diff1
      value: 46.65266185996905
    - type: nauc_ndcg_at_3_max
      value: 33.74458119579594
    - type: nauc_ndcg_at_3_std
      value: -31.493683304534176
    - type: nauc_ndcg_at_5_diff1
      value: 46.08707037187612
    - type: nauc_ndcg_at_5_max
      value: 34.7401426055243
    - type: nauc_ndcg_at_5_std
      value: -32.44390676345172
    - type: nauc_precision_at_1000_diff1
      value: -12.11355300492561
    - type: nauc_precision_at_1000_max
      value: 14.490738062121233
    - type: nauc_precision_at_1000_std
      value: 14.448811005059097
    - type: nauc_precision_at_100_diff1
      value: -9.742085657181239
    - type: nauc_precision_at_100_max
      value: 18.030305489251223
    - type: nauc_precision_at_100_std
      value: 8.213089709529765
    - type: nauc_precision_at_10_diff1
      value: 5.153466672774969
    - type: nauc_precision_at_10_max
      value: 27.29412644661678
    - type: nauc_precision_at_10_std
      value: -15.505053884112355
    - type: nauc_precision_at_1_diff1
      value: 57.228262350455125
    - type: nauc_precision_at_1_max
      value: 35.645285703387366
    - type: nauc_precision_at_1_std
      value: -29.893553821348718
    - type: nauc_precision_at_20_diff1
      value: -0.6812430761066635
    - type: nauc_precision_at_20_max
      value: 25.81911286466295
    - type: nauc_precision_at_20_std
      value: -8.388506222482595
    - type: nauc_precision_at_3_diff1
      value: 18.263873866510576
    - type: nauc_precision_at_3_max
      value: 30.879576105862345
    - type: nauc_precision_at_3_std
      value: -24.0342929870108
    - type: nauc_precision_at_5_diff1
      value: 10.9905804265327
    - type: nauc_precision_at_5_max
      value: 30.88468087429045
    - type: nauc_precision_at_5_std
      value: -20.458684056213507
    - type: nauc_recall_at_1000_diff1
      value: -64.887668417171
    - type: nauc_recall_at_1000_max
      value: 52.25501730358092
    - type: nauc_recall_at_1000_std
      value: 85.13647916200132
    - type: nauc_recall_at_100_diff1
      value: 18.956777346127655
    - type: nauc_recall_at_100_max
      value: 36.10473493564588
    - type: nauc_recall_at_100_std
      value: -10.007474558899949
    - type: nauc_recall_at_10_diff1
      value: 33.810344497568046
    - type: nauc_recall_at_10_max
      value: 31.395430183214245
    - type: nauc_recall_at_10_std
      value: -33.12920524433795
    - type: nauc_recall_at_1_diff1
      value: 57.94711780881773
    - type: nauc_recall_at_1_max
      value: 21.60278071836319
    - type: nauc_recall_at_1_std
      value: -23.273741268035923
    - type: nauc_recall_at_20_diff1
      value: 31.449657437065397
    - type: nauc_recall_at_20_max
      value: 34.519574934321945
    - type: nauc_recall_at_20_std
      value: -33.43406862055647
    - type: nauc_recall_at_3_diff1
      value: 42.07841848382365
    - type: nauc_recall_at_3_max
      value: 28.7648772833266
    - type: nauc_recall_at_3_std
      value: -31.56367736320086
    - type: nauc_recall_at_5_diff1
      value: 39.21392858246301
    - type: nauc_recall_at_5_max
      value: 34.28338202081927
    - type: nauc_recall_at_5_std
      value: -33.725680523721906
    - type: ndcg_at_1
      value: 46.879
    - type: ndcg_at_10
      value: 53.70399999999999
    - type: ndcg_at_100
      value: 60.532
    - type: ndcg_at_1000
      value: 61.997
    - type: ndcg_at_20
      value: 56.818999999999996
    - type: ndcg_at_3
      value: 47.441
    - type: ndcg_at_5
      value: 49.936
    - type: precision_at_1
      value: 46.879
    - type: precision_at_10
      value: 13.376
    - type: precision_at_100
      value: 1.8980000000000001
    - type: precision_at_1000
      value: 0.208
    - type: precision_at_20
      value: 7.771
    - type: precision_at_3
      value: 30.658
    - type: precision_at_5
      value: 22.828
    - type: recall_at_1
      value: 27.739000000000004
    - type: recall_at_10
      value: 64.197
    - type: recall_at_100
      value: 90.54100000000001
    - type: recall_at_1000
      value: 99.90400000000001
    - type: recall_at_20
      value: 74.178
    - type: recall_at_3
      value: 46.312
    - type: recall_at_5
      value: 54.581999999999994
    task:
      type: Retrieval
  - dataset:
      config: cmn-eng
      name: MTEB XPQARetrieval (cmn-eng)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 64.64
    - type: map_at_1
      value: 35.858000000000004
    - type: map_at_10
      value: 58.547000000000004
    - type: map_at_100
      value: 60.108
    - type: map_at_1000
      value: 60.153999999999996
    - type: map_at_20
      value: 59.528000000000006
    - type: map_at_3
      value: 51.578
    - type: map_at_5
      value: 56.206999999999994
    - type: mrr_at_1
      value: 56.95121951219512
    - type: mrr_at_10
      value: 64.93975029036001
    - type: mrr_at_100
      value: 65.63357055718294
    - type: mrr_at_1000
      value: 65.64844109026834
    - type: mrr_at_20
      value: 65.41280668715439
    - type: mrr_at_3
      value: 62.68292682926826
    - type: mrr_at_5
      value: 64.1585365853658
    - type: nauc_map_at_1000_diff1
      value: 45.82740870907091
    - type: nauc_map_at_1000_max
      value: 21.9696540066807
    - type: nauc_map_at_1000_std
      value: -32.028262356639495
    - type: nauc_map_at_100_diff1
      value: 45.802053117616396
    - type: nauc_map_at_100_max
      value: 21.946002070290966
    - type: nauc_map_at_100_std
      value: -32.06190418866229
    - type: nauc_map_at_10_diff1
      value: 46.017774155748945
    - type: nauc_map_at_10_max
      value: 21.876909086095544
    - type: nauc_map_at_10_std
      value: -32.13913568843985
    - type: nauc_map_at_1_diff1
      value: 56.34671160956164
    - type: nauc_map_at_1_max
      value: 17.6796949796236
    - type: nauc_map_at_1_std
      value: -13.741140688066045
    - type: nauc_map_at_20_diff1
      value: 46.027469176858716
    - type: nauc_map_at_20_max
      value: 21.80738432042703
    - type: nauc_map_at_20_std
      value: -32.430379634015395
    - type: nauc_map_at_3_diff1
      value: 48.40096725254027
    - type: nauc_map_at_3_max
      value: 21.15442803574233
    - type: nauc_map_at_3_std
      value: -26.205850292181417
    - type: nauc_map_at_5_diff1
      value: 45.77800041356389
    - type: nauc_map_at_5_max
      value: 22.11718771798752
    - type: nauc_map_at_5_std
      value: -30.32876338031471
    - type: nauc_mrr_at_1000_diff1
      value: 49.748274798877944
    - type: nauc_mrr_at_1000_max
      value: 24.547774167219906
    - type: nauc_mrr_at_1000_std
      value: -32.728447209433504
    - type: nauc_mrr_at_100_diff1
      value: 49.734549290377856
    - type: nauc_mrr_at_100_max
      value: 24.536933315055222
    - type: nauc_mrr_at_100_std
      value: -32.74076335880697
    - type: nauc_mrr_at_10_diff1
      value: 49.82827711456392
    - type: nauc_mrr_at_10_max
      value: 24.536773657485075
    - type: nauc_mrr_at_10_std
      value: -33.05707547166962
    - type: nauc_mrr_at_1_diff1
      value: 51.954289992321044
    - type: nauc_mrr_at_1_max
      value: 26.336255074856886
    - type: nauc_mrr_at_1_std
      value: -29.042962019692446
    - type: nauc_mrr_at_20_diff1
      value: 49.70938465628863
    - type: nauc_mrr_at_20_max
      value: 24.433219849576947
    - type: nauc_mrr_at_20_std
      value: -32.94123791846049
    - type: nauc_mrr_at_3_diff1
      value: 50.289486880347134
    - type: nauc_mrr_at_3_max
      value: 24.978796972860142
    - type: nauc_mrr_at_3_std
      value: -32.11305594784892
    - type: nauc_mrr_at_5_diff1
      value: 49.95013396316144
    - type: nauc_mrr_at_5_max
      value: 24.514452761198303
    - type: nauc_mrr_at_5_std
      value: -32.865859962984146
    - type: nauc_ndcg_at_1000_diff1
      value: 45.73806489233998
    - type: nauc_ndcg_at_1000_max
      value: 22.404941391043867
    - type: nauc_ndcg_at_1000_std
      value: -33.063445720849685
    - type: nauc_ndcg_at_100_diff1
      value: 45.1046206923062
    - type: nauc_ndcg_at_100_max
      value: 22.081133719684658
    - type: nauc_ndcg_at_100_std
      value: -33.299291459450146
    - type: nauc_ndcg_at_10_diff1
      value: 46.140608688357496
    - type: nauc_ndcg_at_10_max
      value: 21.442489279388916
    - type: nauc_ndcg_at_10_std
      value: -35.115870342856006
    - type: nauc_ndcg_at_1_diff1
      value: 51.954289992321044
    - type: nauc_ndcg_at_1_max
      value: 26.336255074856886
    - type: nauc_ndcg_at_1_std
      value: -29.042962019692446
    - type: nauc_ndcg_at_20_diff1
      value: 45.966784725457046
    - type: nauc_ndcg_at_20_max
      value: 21.166632858613145
    - type: nauc_ndcg_at_20_std
      value: -35.65112890375392
    - type: nauc_ndcg_at_3_diff1
      value: 46.7404863978999
    - type: nauc_ndcg_at_3_max
      value: 22.701743709129456
    - type: nauc_ndcg_at_3_std
      value: -30.907633466983192
    - type: nauc_ndcg_at_5_diff1
      value: 45.86487199083486
    - type: nauc_ndcg_at_5_max
      value: 22.088804840002513
    - type: nauc_ndcg_at_5_std
      value: -32.3853481632832
    - type: nauc_precision_at_1000_diff1
      value: -25.69710612774455
    - type: nauc_precision_at_1000_max
      value: 1.3964400247388091
    - type: nauc_precision_at_1000_std
      value: -8.873947511634814
    - type: nauc_precision_at_100_diff1
      value: -24.013497191077978
    - type: nauc_precision_at_100_max
      value: 2.0197725715909343
    - type: nauc_precision_at_100_std
      value: -11.387423148770633
    - type: nauc_precision_at_10_diff1
      value: -6.47728645242781
    - type: nauc_precision_at_10_max
      value: 6.815261443768304
    - type: nauc_precision_at_10_std
      value: -26.825062292855943
    - type: nauc_precision_at_1_diff1
      value: 51.954289992321044
    - type: nauc_precision_at_1_max
      value: 26.336255074856886
    - type: nauc_precision_at_1_std
      value: -29.042962019692446
    - type: nauc_precision_at_20_diff1
      value: -12.355232044747511
    - type: nauc_precision_at_20_max
      value: 4.022126850949725
    - type: nauc_precision_at_20_std
      value: -23.688935769326772
    - type: nauc_precision_at_3_diff1
      value: 7.662671665835864
    - type: nauc_precision_at_3_max
      value: 14.372394760986248
    - type: nauc_precision_at_3_std
      value: -28.635125665532453
    - type: nauc_precision_at_5_diff1
      value: -1.4592476425511611
    - type: nauc_precision_at_5_max
      value: 11.124310161474174
    - type: nauc_precision_at_5_std
      value: -27.89526669318053
    - type: nauc_recall_at_1000_diff1
      value: -19.58450046684932
    - type: nauc_recall_at_1000_max
      value: 70.71661998133165
    - type: nauc_recall_at_1000_std
      value: 93.05555555556315
    - type: nauc_recall_at_100_diff1
      value: 15.06356457571853
    - type: nauc_recall_at_100_max
      value: 14.051414749344806
    - type: nauc_recall_at_100_std
      value: -29.461874235153008
    - type: nauc_recall_at_10_diff1
      value: 41.29842726117901
    - type: nauc_recall_at_10_max
      value: 15.768699673830898
    - type: nauc_recall_at_10_std
      value: -42.11585661287712
    - type: nauc_recall_at_1_diff1
      value: 56.34671160956164
    - type: nauc_recall_at_1_max
      value: 17.6796949796236
    - type: nauc_recall_at_1_std
      value: -13.741140688066045
    - type: nauc_recall_at_20_diff1
      value: 38.8078283585263
    - type: nauc_recall_at_20_max
      value: 12.06816084005326
    - type: nauc_recall_at_20_std
      value: -48.20956170056591
    - type: nauc_recall_at_3_diff1
      value: 44.71028758038993
    - type: nauc_recall_at_3_max
      value: 19.1059093689162
    - type: nauc_recall_at_3_std
      value: -26.795164453784253
    - type: nauc_recall_at_5_diff1
      value: 41.06320797773054
    - type: nauc_recall_at_5_max
      value: 19.117028272530998
    - type: nauc_recall_at_5_std
      value: -33.985747504612156
    - type: ndcg_at_1
      value: 56.95099999999999
    - type: ndcg_at_10
      value: 64.64
    - type: ndcg_at_100
      value: 70.017
    - type: ndcg_at_1000
      value: 70.662
    - type: ndcg_at_20
      value: 67.256
    - type: ndcg_at_3
      value: 58.269000000000005
    - type: ndcg_at_5
      value: 60.94199999999999
    - type: precision_at_1
      value: 56.95099999999999
    - type: precision_at_10
      value: 15.671
    - type: precision_at_100
      value: 2.002
    - type: precision_at_1000
      value: 0.208
    - type: precision_at_20
      value: 8.689
    - type: precision_at_3
      value: 36.341
    - type: precision_at_5
      value: 26.854
    - type: recall_at_1
      value: 35.858000000000004
    - type: recall_at_10
      value: 75.02
    - type: recall_at_100
      value: 95.76
    - type: recall_at_1000
      value: 99.837
    - type: recall_at_20
      value: 83.732
    - type: recall_at_3
      value: 57.093
    - type: recall_at_5
      value: 66.193
    task:
      type: Retrieval
  - dataset:
      config: cmn-cmn
      name: MTEB XPQARetrieval (cmn-cmn)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 69.446
    - type: map_at_1
      value: 39.995999999999995
    - type: map_at_10
      value: 64.033
    - type: map_at_100
      value: 65.51599999999999
    - type: map_at_1000
      value: 65.545
    - type: map_at_20
      value: 64.958
    - type: map_at_3
      value: 57.767
    - type: map_at_5
      value: 61.998
    - type: mrr_at_1
      value: 63.3495145631068
    - type: mrr_at_10
      value: 70.21146363075978
    - type: mrr_at_100
      value: 70.82810974202124
    - type: mrr_at_1000
      value: 70.83816803303915
    - type: mrr_at_20
      value: 70.60140248428802
    - type: mrr_at_3
      value: 68.66909385113267
    - type: mrr_at_5
      value: 69.56108414239482
    - type: nauc_map_at_1000_diff1
      value: 51.649897072831465
    - type: nauc_map_at_1000_max
      value: 38.25222728655331
    - type: nauc_map_at_1000_std
      value: -39.10327919949334
    - type: nauc_map_at_100_diff1
      value: 51.644205886401465
    - type: nauc_map_at_100_max
      value: 38.23611154355255
    - type: nauc_map_at_100_std
      value: -39.1677073977285
    - type: nauc_map_at_10_diff1
      value: 51.81444145636039
    - type: nauc_map_at_10_max
      value: 38.03382104326485
    - type: nauc_map_at_10_std
      value: -38.999395639812015
    - type: nauc_map_at_1_diff1
      value: 59.785298201044704
    - type: nauc_map_at_1_max
      value: 23.273537759937785
    - type: nauc_map_at_1_std
      value: -17.838712689290194
    - type: nauc_map_at_20_diff1
      value: 51.680208795601004
    - type: nauc_map_at_20_max
      value: 38.23334583518634
    - type: nauc_map_at_20_std
      value: -39.24344495939061
    - type: nauc_map_at_3_diff1
      value: 52.180913298194056
    - type: nauc_map_at_3_max
      value: 33.45482478000481
    - type: nauc_map_at_3_std
      value: -31.682911030586297
    - type: nauc_map_at_5_diff1
      value: 50.804900676175436
    - type: nauc_map_at_5_max
      value: 37.68924816012326
    - type: nauc_map_at_5_std
      value: -36.85016896616712
    - type: nauc_mrr_at_1000_diff1
      value: 56.371477471577535
    - type: nauc_mrr_at_1000_max
      value: 42.773877962050086
    - type: nauc_mrr_at_1000_std
      value: -40.41765081873682
    - type: nauc_mrr_at_100_diff1
      value: 56.3619751528192
    - type: nauc_mrr_at_100_max
      value: 42.76298794859916
    - type: nauc_mrr_at_100_std
      value: -40.44070582448831
    - type: nauc_mrr_at_10_diff1
      value: 56.33810523477712
    - type: nauc_mrr_at_10_max
      value: 42.76591937795783
    - type: nauc_mrr_at_10_std
      value: -40.69339583030244
    - type: nauc_mrr_at_1_diff1
      value: 58.90399906884378
    - type: nauc_mrr_at_1_max
      value: 43.38806571165292
    - type: nauc_mrr_at_1_std
      value: -38.224015285584
    - type: nauc_mrr_at_20_diff1
      value: 56.32629070537032
    - type: nauc_mrr_at_20_max
      value: 42.79615263472604
    - type: nauc_mrr_at_20_std
      value: -40.496777397603076
    - type: nauc_mrr_at_3_diff1
      value: 55.96989454480743
    - type: nauc_mrr_at_3_max
      value: 42.49832220744744
    - type: nauc_mrr_at_3_std
      value: -39.883799467132384
    - type: nauc_mrr_at_5_diff1
      value: 56.003080766475755
    - type: nauc_mrr_at_5_max
      value: 42.73308051011805
    - type: nauc_mrr_at_5_std
      value: -39.87179511166683
    - type: nauc_ndcg_at_1000_diff1
      value: 52.49054229225255
    - type: nauc_ndcg_at_1000_max
      value: 39.61644750719859
    - type: nauc_ndcg_at_1000_std
      value: -40.89845763194674
    - type: nauc_ndcg_at_100_diff1
      value: 52.33511250864434
    - type: nauc_ndcg_at_100_max
      value: 39.25530146124452
    - type: nauc_ndcg_at_100_std
      value: -41.92444498004374
    - type: nauc_ndcg_at_10_diff1
      value: 52.62031505931842
    - type: nauc_ndcg_at_10_max
      value: 38.667195545396766
    - type: nauc_ndcg_at_10_std
      value: -42.59503924641507
    - type: nauc_ndcg_at_1_diff1
      value: 58.90399906884378
    - type: nauc_ndcg_at_1_max
      value: 43.38806571165292
    - type: nauc_ndcg_at_1_std
      value: -38.224015285584
    - type: nauc_ndcg_at_20_diff1
      value: 52.15061629809436
    - type: nauc_ndcg_at_20_max
      value: 39.09332400054708
    - type: nauc_ndcg_at_20_std
      value: -42.80018671618001
    - type: nauc_ndcg_at_3_diff1
      value: 51.04210728138207
    - type: nauc_ndcg_at_3_max
      value: 38.19034802567046
    - type: nauc_ndcg_at_3_std
      value: -38.179821090765216
    - type: nauc_ndcg_at_5_diff1
      value: 51.04399574045204
    - type: nauc_ndcg_at_5_max
      value: 38.42492210204548
    - type: nauc_ndcg_at_5_std
      value: -38.868073241617715
    - type: nauc_precision_at_1000_diff1
      value: -25.151369907213734
    - type: nauc_precision_at_1000_max
      value: 9.012549147054989
    - type: nauc_precision_at_1000_std
      value: -9.319786589947698
    - type: nauc_precision_at_100_diff1
      value: -23.20945211843088
    - type: nauc_precision_at_100_max
      value: 9.860701593969862
    - type: nauc_precision_at_100_std
      value: -13.073877818347231
    - type: nauc_precision_at_10_diff1
      value: -6.970781124246847
    - type: nauc_precision_at_10_max
      value: 19.392675322254487
    - type: nauc_precision_at_10_std
      value: -26.74943490717657
    - type: nauc_precision_at_1_diff1
      value: 58.90399906884378
    - type: nauc_precision_at_1_max
      value: 43.38806571165292
    - type: nauc_precision_at_1_std
      value: -38.224015285584
    - type: nauc_precision_at_20_diff1
      value: -13.046456108081102
    - type: nauc_precision_at_20_max
      value: 15.69439950383875
    - type: nauc_precision_at_20_std
      value: -23.836004512018093
    - type: nauc_precision_at_3_diff1
      value: 3.5444232965528846
    - type: nauc_precision_at_3_max
      value: 27.08858445453865
    - type: nauc_precision_at_3_std
      value: -29.12757283665593
    - type: nauc_precision_at_5_diff1
      value: -3.6853986353320267
    - type: nauc_precision_at_5_max
      value: 24.32059689571271
    - type: nauc_precision_at_5_std
      value: -27.46188072134163
    - type: nauc_recall_at_1000_diff1
      value: 86.93515141907919
    - type: nauc_recall_at_1000_max
      value: 100.0
    - type: nauc_recall_at_1000_std
      value: 100.0
    - type: nauc_recall_at_100_diff1
      value: 39.7052887613879
    - type: nauc_recall_at_100_max
      value: 18.40943977796887
    - type: nauc_recall_at_100_std
      value: -88.74014854144974
    - type: nauc_recall_at_10_diff1
      value: 48.85342500870892
    - type: nauc_recall_at_10_max
      value: 32.69617204234419
    - type: nauc_recall_at_10_std
      value: -51.9937231860804
    - type: nauc_recall_at_1_diff1
      value: 59.785298201044704
    - type: nauc_recall_at_1_max
      value: 23.273537759937785
    - type: nauc_recall_at_1_std
      value: -17.838712689290194
    - type: nauc_recall_at_20_diff1
      value: 45.40839773314378
    - type: nauc_recall_at_20_max
      value: 33.02458321493215
    - type: nauc_recall_at_20_std
      value: -55.97800739448166
    - type: nauc_recall_at_3_diff1
      value: 47.05565693416531
    - type: nauc_recall_at_3_max
      value: 28.743850400344297
    - type: nauc_recall_at_3_std
      value: -32.436470486397475
    - type: nauc_recall_at_5_diff1
      value: 45.30223758669577
    - type: nauc_recall_at_5_max
      value: 33.6567274747059
    - type: nauc_recall_at_5_std
      value: -39.946712017948514
    - type: ndcg_at_1
      value: 63.349999999999994
    - type: ndcg_at_10
      value: 69.446
    - type: ndcg_at_100
      value: 74.439
    - type: ndcg_at_1000
      value: 74.834
    - type: ndcg_at_20
      value: 71.763
    - type: ndcg_at_3
      value: 64.752
    - type: ndcg_at_5
      value: 66.316
    - type: precision_at_1
      value: 63.349999999999994
    - type: precision_at_10
      value: 16.286
    - type: precision_at_100
      value: 2.024
    - type: precision_at_1000
      value: 0.207
    - type: precision_at_20
      value: 8.908000000000001
    - type: precision_at_3
      value: 40.655
    - type: precision_at_5
      value: 28.859
    - type: recall_at_1
      value: 39.995999999999995
    - type: recall_at_10
      value: 78.107
    - type: recall_at_100
      value: 97.538
    - type: recall_at_1000
      value: 99.96000000000001
    - type: recall_at_20
      value: 85.72
    - type: recall_at_3
      value: 63.291
    - type: recall_at_5
      value: 70.625
    task:
      type: Retrieval
  - dataset:
      config: spa-eng
      name: MTEB XPQARetrieval (spa-eng)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 68.258
    - type: map_at_1
      value: 33.06
    - type: map_at_10
      value: 61.590999999999994
    - type: map_at_100
      value: 63.341
    - type: map_at_1000
      value: 63.385999999999996
    - type: map_at_20
      value: 62.77700000000001
    - type: map_at_3
      value: 52.547999999999995
    - type: map_at_5
      value: 58.824
    - type: mrr_at_1
      value: 63.80832282471627
    - type: mrr_at_10
      value: 70.76848015372607
    - type: mrr_at_100
      value: 71.33996704518061
    - type: mrr_at_1000
      value: 71.35368444388072
    - type: mrr_at_20
      value: 71.18191741103522
    - type: mrr_at_3
      value: 68.83144178226142
    - type: mrr_at_5
      value: 69.88440521227405
    - type: nauc_map_at_1000_diff1
      value: 41.59255746310511
    - type: nauc_map_at_1000_max
      value: 42.064075373358065
    - type: nauc_map_at_1000_std
      value: -25.130730194381723
    - type: nauc_map_at_100_diff1
      value: 41.56447648820406
    - type: nauc_map_at_100_max
      value: 42.06711634651607
    - type: nauc_map_at_100_std
      value: -25.14871585556968
    - type: nauc_map_at_10_diff1
      value: 41.28968387107058
    - type: nauc_map_at_10_max
      value: 41.511538272139774
    - type: nauc_map_at_10_std
      value: -25.99906440164276
    - type: nauc_map_at_1_diff1
      value: 51.09859596320021
    - type: nauc_map_at_1_max
      value: 12.406789321338222
    - type: nauc_map_at_1_std
      value: -18.227486548655076
    - type: nauc_map_at_20_diff1
      value: 41.39469672947315
    - type: nauc_map_at_20_max
      value: 41.98309315808902
    - type: nauc_map_at_20_std
      value: -25.44704720985219
    - type: nauc_map_at_3_diff1
      value: 43.16164995512842
    - type: nauc_map_at_3_max
      value: 30.935400935562818
    - type: nauc_map_at_3_std
      value: -23.53095555148866
    - type: nauc_map_at_5_diff1
      value: 41.23474352142375
    - type: nauc_map_at_5_max
      value: 39.03088859147947
    - type: nauc_map_at_5_std
      value: -26.046526443708366
    - type: nauc_mrr_at_1000_diff1
      value: 51.79649678213789
    - type: nauc_mrr_at_1000_max
      value: 50.50340748045259
    - type: nauc_mrr_at_1000_std
      value: -24.777183703493407
    - type: nauc_mrr_at_100_diff1
      value: 51.78609028166551
    - type: nauc_mrr_at_100_max
      value: 50.51732896833555
    - type: nauc_mrr_at_100_std
      value: -24.760054686874717
    - type: nauc_mrr_at_10_diff1
      value: 51.705268395036995
    - type: nauc_mrr_at_10_max
      value: 50.35818415293149
    - type: nauc_mrr_at_10_std
      value: -25.170367120250404
    - type: nauc_mrr_at_1_diff1
      value: 53.91475115581825
    - type: nauc_mrr_at_1_max
      value: 49.122529616282016
    - type: nauc_mrr_at_1_std
      value: -22.377647552937155
    - type: nauc_mrr_at_20_diff1
      value: 51.778984221197774
    - type: nauc_mrr_at_20_max
      value: 50.5070957827813
    - type: nauc_mrr_at_20_std
      value: -24.908935023607285
    - type: nauc_mrr_at_3_diff1
      value: 51.82683773090423
    - type: nauc_mrr_at_3_max
      value: 50.77993196421369
    - type: nauc_mrr_at_3_std
      value: -24.3925832021831
    - type: nauc_mrr_at_5_diff1
      value: 51.722232683543034
    - type: nauc_mrr_at_5_max
      value: 50.334865493961864
    - type: nauc_mrr_at_5_std
      value: -25.513593495703297
    - type: nauc_ndcg_at_1000_diff1
      value: 44.21851582991263
    - type: nauc_ndcg_at_1000_max
      value: 45.73539068637836
    - type: nauc_ndcg_at_1000_std
      value: -24.716522467580397
    - type: nauc_ndcg_at_100_diff1
      value: 43.8002401615357
    - type: nauc_ndcg_at_100_max
      value: 45.801409410061915
    - type: nauc_ndcg_at_100_std
      value: -24.73171742499903
    - type: nauc_ndcg_at_10_diff1
      value: 42.540922778755885
    - type: nauc_ndcg_at_10_max
      value: 44.348836943874595
    - type: nauc_ndcg_at_10_std
      value: -28.05403666494785
    - type: nauc_ndcg_at_1_diff1
      value: 53.91475115581825
    - type: nauc_ndcg_at_1_max
      value: 49.122529616282016
    - type: nauc_ndcg_at_1_std
      value: -22.377647552937155
    - type: nauc_ndcg_at_20_diff1
      value: 43.10347921163421
    - type: nauc_ndcg_at_20_max
      value: 45.53253270265022
    - type: nauc_ndcg_at_20_std
      value: -26.63902791862846
    - type: nauc_ndcg_at_3_diff1
      value: 42.41720274782384
    - type: nauc_ndcg_at_3_max
      value: 42.91778219334943
    - type: nauc_ndcg_at_3_std
      value: -24.793252033594076
    - type: nauc_ndcg_at_5_diff1
      value: 42.51515034945093
    - type: nauc_ndcg_at_5_max
      value: 41.62080576508792
    - type: nauc_ndcg_at_5_std
      value: -28.209669314955065
    - type: nauc_precision_at_1000_diff1
      value: -14.89794075433148
    - type: nauc_precision_at_1000_max
      value: 27.85387929356412
    - type: nauc_precision_at_1000_std
      value: 10.728618597190849
    - type: nauc_precision_at_100_diff1
      value: -13.075270046295856
    - type: nauc_precision_at_100_max
      value: 29.77208946756632
    - type: nauc_precision_at_100_std
      value: 8.491662697326039
    - type: nauc_precision_at_10_diff1
      value: -4.0826025188781205
    - type: nauc_precision_at_10_max
      value: 39.04278085180075
    - type: nauc_precision_at_10_std
      value: -5.925408651372333
    - type: nauc_precision_at_1_diff1
      value: 53.91475115581825
    - type: nauc_precision_at_1_max
      value: 49.122529616282016
    - type: nauc_precision_at_1_std
      value: -22.377647552937155
    - type: nauc_precision_at_20_diff1
      value: -7.93186440645135
    - type: nauc_precision_at_20_max
      value: 35.81281308891365
    - type: nauc_precision_at_20_std
      value: 0.1241277857515697
    - type: nauc_precision_at_3_diff1
      value: 7.563562511484409
    - type: nauc_precision_at_3_max
      value: 43.43738862378524
    - type: nauc_precision_at_3_std
      value: -11.958059731912615
    - type: nauc_precision_at_5_diff1
      value: -0.1801152449011624
    - type: nauc_precision_at_5_max
      value: 41.32486715619513
    - type: nauc_precision_at_5_std
      value: -10.088699021919552
    - type: nauc_recall_at_1000_diff1
      value: 86.93359696819986
    - type: nauc_recall_at_1000_max
      value: 100.0
    - type: nauc_recall_at_1000_std
      value: 72.21843645604022
    - type: nauc_recall_at_100_diff1
      value: 29.86050842714198
    - type: nauc_recall_at_100_max
      value: 48.106658251136245
    - type: nauc_recall_at_100_std
      value: -14.981886214880035
    - type: nauc_recall_at_10_diff1
      value: 33.67119240737528
    - type: nauc_recall_at_10_max
      value: 39.271984859561414
    - type: nauc_recall_at_10_std
      value: -35.6434883839217
    - type: nauc_recall_at_1_diff1
      value: 51.09859596320021
    - type: nauc_recall_at_1_max
      value: 12.406789321338222
    - type: nauc_recall_at_1_std
      value: -18.227486548655076
    - type: nauc_recall_at_20_diff1
      value: 33.211979983240724
    - type: nauc_recall_at_20_max
      value: 43.47676074743184
    - type: nauc_recall_at_20_std
      value: -33.88107138395349
    - type: nauc_recall_at_3_diff1
      value: 39.22513750146998
    - type: nauc_recall_at_3_max
      value: 27.066674083840166
    - type: nauc_recall_at_3_std
      value: -26.963282529629893
    - type: nauc_recall_at_5_diff1
      value: 36.53718917129459
    - type: nauc_recall_at_5_max
      value: 35.40550013169686
    - type: nauc_recall_at_5_std
      value: -34.209159379410806
    - type: ndcg_at_1
      value: 63.808
    - type: ndcg_at_10
      value: 68.258
    - type: ndcg_at_100
      value: 73.38799999999999
    - type: ndcg_at_1000
      value: 74.03
    - type: ndcg_at_20
      value: 70.968
    - type: ndcg_at_3
      value: 62.33
    - type: ndcg_at_5
      value: 64.096
    - type: precision_at_1
      value: 63.808
    - type: precision_at_10
      value: 19.243
    - type: precision_at_100
      value: 2.367
    - type: precision_at_1000
      value: 0.245
    - type: precision_at_20
      value: 10.599
    - type: precision_at_3
      value: 44.515
    - type: precision_at_5
      value: 33.467999999999996
    - type: recall_at_1
      value: 33.06
    - type: recall_at_10
      value: 77.423
    - type: recall_at_100
      value: 95.923
    - type: recall_at_1000
      value: 99.874
    - type: recall_at_20
      value: 85.782
    - type: recall_at_3
      value: 57.098000000000006
    - type: recall_at_5
      value: 67.472
    task:
      type: Retrieval
  - dataset:
      config: spa-spa
      name: MTEB XPQARetrieval (spa-spa)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 72.004
    - type: map_at_1
      value: 36.248000000000005
    - type: map_at_10
      value: 65.679
    - type: map_at_100
      value: 67.22399999999999
    - type: map_at_1000
      value: 67.264
    - type: map_at_20
      value: 66.705
    - type: map_at_3
      value: 56.455
    - type: map_at_5
      value: 62.997
    - type: mrr_at_1
      value: 67.71752837326608
    - type: mrr_at_10
      value: 74.59782021257429
    - type: mrr_at_100
      value: 75.0640960767943
    - type: mrr_at_1000
      value: 75.07324799466076
    - type: mrr_at_20
      value: 74.9323963386884
    - type: mrr_at_3
      value: 72.95081967213115
    - type: mrr_at_5
      value: 73.82723833543506
    - type: nauc_map_at_1000_diff1
      value: 43.111810717567714
    - type: nauc_map_at_1000_max
      value: 44.835247208972476
    - type: nauc_map_at_1000_std
      value: -32.798405973931985
    - type: nauc_map_at_100_diff1
      value: 43.090223482932764
    - type: nauc_map_at_100_max
      value: 44.83392441557943
    - type: nauc_map_at_100_std
      value: -32.81149166676563
    - type: nauc_map_at_10_diff1
      value: 42.87841934951979
    - type: nauc_map_at_10_max
      value: 43.9838653389494
    - type: nauc_map_at_10_std
      value: -33.588084643627084
    - type: nauc_map_at_1_diff1
      value: 54.509245848379095
    - type: nauc_map_at_1_max
      value: 10.05921648322742
    - type: nauc_map_at_1_std
      value: -24.652326014826762
    - type: nauc_map_at_20_diff1
      value: 43.07468612984794
    - type: nauc_map_at_20_max
      value: 44.75663122615032
    - type: nauc_map_at_20_std
      value: -33.11788887878321
    - type: nauc_map_at_3_diff1
      value: 44.63272828938906
    - type: nauc_map_at_3_max
      value: 32.1584369869227
    - type: nauc_map_at_3_std
      value: -30.761662210142944
    - type: nauc_map_at_5_diff1
      value: 42.77296997803048
    - type: nauc_map_at_5_max
      value: 41.78894616737652
    - type: nauc_map_at_5_std
      value: -33.56459774477362
    - type: nauc_mrr_at_1000_diff1
      value: 53.097544131833494
    - type: nauc_mrr_at_1000_max
      value: 50.61134979184588
    - type: nauc_mrr_at_1000_std
      value: -35.6221191487669
    - type: nauc_mrr_at_100_diff1
      value: 53.096609856182106
    - type: nauc_mrr_at_100_max
      value: 50.61951585642645
    - type: nauc_mrr_at_100_std
      value: -35.62396157508327
    - type: nauc_mrr_at_10_diff1
      value: 52.771534471912304
    - type: nauc_mrr_at_10_max
      value: 50.430863224435726
    - type: nauc_mrr_at_10_std
      value: -36.027992076620365
    - type: nauc_mrr_at_1_diff1
      value: 55.05316238884337
    - type: nauc_mrr_at_1_max
      value: 49.461858515275196
    - type: nauc_mrr_at_1_std
      value: -31.87492636319712
    - type: nauc_mrr_at_20_diff1
      value: 53.083253469629746
    - type: nauc_mrr_at_20_max
      value: 50.62156424256193
    - type: nauc_mrr_at_20_std
      value: -35.879153692447154
    - type: nauc_mrr_at_3_diff1
      value: 52.98283109188415
    - type: nauc_mrr_at_3_max
      value: 50.83561260429378
    - type: nauc_mrr_at_3_std
      value: -35.30839538038797
    - type: nauc_mrr_at_5_diff1
      value: 52.93270510879709
    - type: nauc_mrr_at_5_max
      value: 50.54595596761199
    - type: nauc_mrr_at_5_std
      value: -35.84059376434395
    - type: nauc_ndcg_at_1000_diff1
      value: 45.343685089209416
    - type: nauc_ndcg_at_1000_max
      value: 47.801141576669465
    - type: nauc_ndcg_at_1000_std
      value: -33.512958862879195
    - type: nauc_ndcg_at_100_diff1
      value: 45.255590461515894
    - type: nauc_ndcg_at_100_max
      value: 47.99240031881967
    - type: nauc_ndcg_at_100_std
      value: -33.614465006695205
    - type: nauc_ndcg_at_10_diff1
      value: 43.93472511731019
    - type: nauc_ndcg_at_10_max
      value: 45.92599752897053
    - type: nauc_ndcg_at_10_std
      value: -36.43629114491574
    - type: nauc_ndcg_at_1_diff1
      value: 55.05316238884337
    - type: nauc_ndcg_at_1_max
      value: 49.461858515275196
    - type: nauc_ndcg_at_1_std
      value: -31.87492636319712
    - type: nauc_ndcg_at_20_diff1
      value: 44.93534591273201
    - type: nauc_ndcg_at_20_max
      value: 47.55153940713458
    - type: nauc_ndcg_at_20_std
      value: -35.56392448745206
    - type: nauc_ndcg_at_3_diff1
      value: 43.17916122133396
    - type: nauc_ndcg_at_3_max
      value: 45.603634205103276
    - type: nauc_ndcg_at_3_std
      value: -32.473227507181214
    - type: nauc_ndcg_at_5_diff1
      value: 44.10242961669216
    - type: nauc_ndcg_at_5_max
      value: 43.61666669031808
    - type: nauc_ndcg_at_5_std
      value: -35.98808321497782
    - type: nauc_precision_at_1000_diff1
      value: -23.264714449991146
    - type: nauc_precision_at_1000_max
      value: 28.505729576735465
    - type: nauc_precision_at_1000_std
      value: 11.987379232920926
    - type: nauc_precision_at_100_diff1
      value: -21.156119174614627
    - type: nauc_precision_at_100_max
      value: 30.711646221646255
    - type: nauc_precision_at_100_std
      value: 9.650486536340322
    - type: nauc_precision_at_10_diff1
      value: -10.98001328477502
    - type: nauc_precision_at_10_max
      value: 39.25638073760597
    - type: nauc_precision_at_10_std
      value: -4.3456859257488
    - type: nauc_precision_at_1_diff1
      value: 55.05316238884337
    - type: nauc_precision_at_1_max
      value: 49.461858515275196
    - type: nauc_precision_at_1_std
      value: -31.87492636319712
    - type: nauc_precision_at_20_diff1
      value: -14.97565390664424
    - type: nauc_precision_at_20_max
      value: 36.383835295942355
    - type: nauc_precision_at_20_std
      value: 1.525158880381114
    - type: nauc_precision_at_3_diff1
      value: 1.0448345623903483
    - type: nauc_precision_at_3_max
      value: 45.69772060667404
    - type: nauc_precision_at_3_std
      value: -13.002685018948293
    - type: nauc_precision_at_5_diff1
      value: -5.434185597628904
    - type: nauc_precision_at_5_max
      value: 42.99162431099203
    - type: nauc_precision_at_5_std
      value: -9.789308817624534
    - type: nauc_recall_at_1000_diff1
      value: 12.309303236094845
    - type: nauc_recall_at_1000_max
      value: 100.0
    - type: nauc_recall_at_1000_std
      value: 86.93359696819986
    - type: nauc_recall_at_100_diff1
      value: 39.093544920901415
    - type: nauc_recall_at_100_max
      value: 55.62814395062938
    - type: nauc_recall_at_100_std
      value: -22.6919033301514
    - type: nauc_recall_at_10_diff1
      value: 35.50100141633622
    - type: nauc_recall_at_10_max
      value: 39.25750019586647
    - type: nauc_recall_at_10_std
      value: -43.01273078031791
    - type: nauc_recall_at_1_diff1
      value: 54.509245848379095
    - type: nauc_recall_at_1_max
      value: 10.05921648322742
    - type: nauc_recall_at_1_std
      value: -24.652326014826762
    - type: nauc_recall_at_20_diff1
      value: 38.1281707132327
    - type: nauc_recall_at_20_max
      value: 43.97950642900301
    - type: nauc_recall_at_20_std
      value: -44.049952771307574
    - type: nauc_recall_at_3_diff1
      value: 40.01986938242728
    - type: nauc_recall_at_3_max
      value: 27.517114421061173
    - type: nauc_recall_at_3_std
      value: -32.99056780232045
    - type: nauc_recall_at_5_diff1
      value: 38.52035606499483
    - type: nauc_recall_at_5_max
      value: 37.05834604678859
    - type: nauc_recall_at_5_std
      value: -39.86196378897912
    - type: ndcg_at_1
      value: 67.718
    - type: ndcg_at_10
      value: 72.004
    - type: ndcg_at_100
      value: 76.554
    - type: ndcg_at_1000
      value: 77.07300000000001
    - type: ndcg_at_20
      value: 74.37899999999999
    - type: ndcg_at_3
      value: 66.379
    - type: ndcg_at_5
      value: 68.082
    - type: precision_at_1
      value: 67.718
    - type: precision_at_10
      value: 19.849
    - type: precision_at_100
      value: 2.3800000000000003
    - type: precision_at_1000
      value: 0.245
    - type: precision_at_20
      value: 10.813
    - type: precision_at_3
      value: 46.574
    - type: precision_at_5
      value: 34.83
    - type: recall_at_1
      value: 36.248000000000005
    - type: recall_at_10
      value: 80.252
    - type: recall_at_100
      value: 96.73
    - type: recall_at_1000
      value: 99.874
    - type: recall_at_20
      value: 87.703
    - type: recall_at_3
      value: 60.815
    - type: recall_at_5
      value: 71.16
    task:
      type: Retrieval
  - dataset:
      config: fra-eng
      name: MTEB XPQARetrieval (fra-eng)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 73.729
    - type: map_at_1
      value: 43.964999999999996
    - type: map_at_10
      value: 67.803
    - type: map_at_100
      value: 69.188
    - type: map_at_1000
      value: 69.21000000000001
    - type: map_at_20
      value: 68.747
    - type: map_at_3
      value: 60.972
    - type: map_at_5
      value: 65.39399999999999
    - type: mrr_at_1
      value: 68.4913217623498
    - type: mrr_at_10
      value: 75.2600822260368
    - type: mrr_at_100
      value: 75.6599169808848
    - type: mrr_at_1000
      value: 75.66720883727534
    - type: mrr_at_20
      value: 75.52375865860405
    - type: mrr_at_3
      value: 73.54250111259452
    - type: mrr_at_5
      value: 74.51713395638626
    - type: nauc_map_at_1000_diff1
      value: 46.81533703002097
    - type: nauc_map_at_1000_max
      value: 46.30794757084772
    - type: nauc_map_at_1000_std
      value: -14.953470500312335
    - type: nauc_map_at_100_diff1
      value: 46.82464740277745
    - type: nauc_map_at_100_max
      value: 46.32852879948254
    - type: nauc_map_at_100_std
      value: -14.950035098066172
    - type: nauc_map_at_10_diff1
      value: 46.31406143369831
    - type: nauc_map_at_10_max
      value: 45.337593270786634
    - type: nauc_map_at_10_std
      value: -16.011789445907876
    - type: nauc_map_at_1_diff1
      value: 57.097134715065835
    - type: nauc_map_at_1_max
      value: 21.93931500350721
    - type: nauc_map_at_1_std
      value: -15.134457251301637
    - type: nauc_map_at_20_diff1
      value: 46.47030891134173
    - type: nauc_map_at_20_max
      value: 46.29169960276292
    - type: nauc_map_at_20_std
      value: -15.14241106541829
    - type: nauc_map_at_3_diff1
      value: 50.27064228648596
    - type: nauc_map_at_3_max
      value: 39.43058773971639
    - type: nauc_map_at_3_std
      value: -16.16545993089126
    - type: nauc_map_at_5_diff1
      value: 46.974867679747426
    - type: nauc_map_at_5_max
      value: 44.31091104855002
    - type: nauc_map_at_5_std
      value: -16.50175337658926
    - type: nauc_mrr_at_1000_diff1
      value: 55.20294005110399
    - type: nauc_mrr_at_1000_max
      value: 51.947725719119966
    - type: nauc_mrr_at_1000_std
      value: -14.586112939597232
    - type: nauc_mrr_at_100_diff1
      value: 55.20426251109304
    - type: nauc_mrr_at_100_max
      value: 51.95648725402534
    - type: nauc_mrr_at_100_std
      value: -14.579769236539143
    - type: nauc_mrr_at_10_diff1
      value: 54.93870506205835
    - type: nauc_mrr_at_10_max
      value: 51.89312772900638
    - type: nauc_mrr_at_10_std
      value: -14.692635010092939
    - type: nauc_mrr_at_1_diff1
      value: 56.54945935175171
    - type: nauc_mrr_at_1_max
      value: 51.28134504197991
    - type: nauc_mrr_at_1_std
      value: -12.909042186563061
    - type: nauc_mrr_at_20_diff1
      value: 55.10667018041461
    - type: nauc_mrr_at_20_max
      value: 51.98236870783707
    - type: nauc_mrr_at_20_std
      value: -14.599377575198025
    - type: nauc_mrr_at_3_diff1
      value: 55.67124311746892
    - type: nauc_mrr_at_3_max
      value: 51.77903236246767
    - type: nauc_mrr_at_3_std
      value: -14.94452633860763
    - type: nauc_mrr_at_5_diff1
      value: 55.42849172366371
    - type: nauc_mrr_at_5_max
      value: 51.76902965753959
    - type: nauc_mrr_at_5_std
      value: -15.357993534727072
    - type: nauc_ndcg_at_1000_diff1
      value: 48.736844959280326
    - type: nauc_ndcg_at_1000_max
      value: 48.92891159935398
    - type: nauc_ndcg_at_1000_std
      value: -13.983968675611056
    - type: nauc_ndcg_at_100_diff1
      value: 48.73859328503975
    - type: nauc_ndcg_at_100_max
      value: 49.31867149556439
    - type: nauc_ndcg_at_100_std
      value: -13.72387564912742
    - type: nauc_ndcg_at_10_diff1
      value: 46.50313862975287
    - type: nauc_ndcg_at_10_max
      value: 47.13599793554596
    - type: nauc_ndcg_at_10_std
      value: -16.317919977400113
    - type: nauc_ndcg_at_1_diff1
      value: 56.54945935175171
    - type: nauc_ndcg_at_1_max
      value: 51.28134504197991
    - type: nauc_ndcg_at_1_std
      value: -12.909042186563061
    - type: nauc_ndcg_at_20_diff1
      value: 47.01727117133912
    - type: nauc_ndcg_at_20_max
      value: 49.121366036709105
    - type: nauc_ndcg_at_20_std
      value: -14.411078677638775
    - type: nauc_ndcg_at_3_diff1
      value: 49.229581145458276
    - type: nauc_ndcg_at_3_max
      value: 47.427609717032
    - type: nauc_ndcg_at_3_std
      value: -16.52066627289908
    - type: nauc_ndcg_at_5_diff1
      value: 48.0152514127505
    - type: nauc_ndcg_at_5_max
      value: 46.12152407850816
    - type: nauc_ndcg_at_5_std
      value: -17.613295491954656
    - type: nauc_precision_at_1000_diff1
      value: -25.959006032642463
    - type: nauc_precision_at_1000_max
      value: 12.81002362947137
    - type: nauc_precision_at_1000_std
      value: 12.575312826061513
    - type: nauc_precision_at_100_diff1
      value: -24.35413527283394
    - type: nauc_precision_at_100_max
      value: 14.878359236477303
    - type: nauc_precision_at_100_std
      value: 12.384426050018428
    - type: nauc_precision_at_10_diff1
      value: -17.93220761770618
    - type: nauc_precision_at_10_max
      value: 23.523485811847294
    - type: nauc_precision_at_10_std
      value: 4.424456968716939
    - type: nauc_precision_at_1_diff1
      value: 56.54945935175171
    - type: nauc_precision_at_1_max
      value: 51.28134504197991
    - type: nauc_precision_at_1_std
      value: -12.909042186563061
    - type: nauc_precision_at_20_diff1
      value: -21.776871398686936
    - type: nauc_precision_at_20_max
      value: 21.18436338264366
    - type: nauc_precision_at_20_std
      value: 9.937274986573321
    - type: nauc_precision_at_3_diff1
      value: -1.2411845580934435
    - type: nauc_precision_at_3_max
      value: 34.962281941875
    - type: nauc_precision_at_3_std
      value: -2.447892908501237
    - type: nauc_precision_at_5_diff1
      value: -11.134164534114085
    - type: nauc_precision_at_5_max
      value: 30.22079740070525
    - type: nauc_precision_at_5_std
      value: -0.24232594421765946
    - type: nauc_recall_at_1000_diff1
      value: .nan
    - type: nauc_recall_at_1000_max
      value: .nan
    - type: nauc_recall_at_1000_std
      value: .nan
    - type: nauc_recall_at_100_diff1
      value: 43.3647412452869
    - type: nauc_recall_at_100_max
      value: 63.50094950500327
    - type: nauc_recall_at_100_std
      value: 2.3911909633714044
    - type: nauc_recall_at_10_diff1
      value: 33.993445071666855
    - type: nauc_recall_at_10_max
      value: 41.38694129134144
    - type: nauc_recall_at_10_std
      value: -19.308698266099096
    - type: nauc_recall_at_1_diff1
      value: 57.097134715065835
    - type: nauc_recall_at_1_max
      value: 21.93931500350721
    - type: nauc_recall_at_1_std
      value: -15.134457251301637
    - type: nauc_recall_at_20_diff1
      value: 32.03888531880772
    - type: nauc_recall_at_20_max
      value: 49.660787482562085
    - type: nauc_recall_at_20_std
      value: -12.641456758778382
    - type: nauc_recall_at_3_diff1
      value: 47.94527082900579
    - type: nauc_recall_at_3_max
      value: 36.51733131437679
    - type: nauc_recall_at_3_std
      value: -18.65511713247495
    - type: nauc_recall_at_5_diff1
      value: 42.04545772092305
    - type: nauc_recall_at_5_max
      value: 41.21440912972303
    - type: nauc_recall_at_5_std
      value: -21.47386527081128
    - type: ndcg_at_1
      value: 68.491
    - type: ndcg_at_10
      value: 73.729
    - type: ndcg_at_100
      value: 77.684
    - type: ndcg_at_1000
      value: 78.084
    - type: ndcg_at_20
      value: 75.795
    - type: ndcg_at_3
      value: 68.568
    - type: ndcg_at_5
      value: 70.128
    - type: precision_at_1
      value: 68.491
    - type: precision_at_10
      value: 16.996
    - type: precision_at_100
      value: 2.023
    - type: precision_at_1000
      value: 0.207
    - type: precision_at_20
      value: 9.246
    - type: precision_at_3
      value: 41.923
    - type: precision_at_5
      value: 29.826000000000004
    - type: recall_at_1
      value: 43.964999999999996
    - type: recall_at_10
      value: 82.777
    - type: recall_at_100
      value: 97.287
    - type: recall_at_1000
      value: 100.0
    - type: recall_at_20
      value: 89.183
    - type: recall_at_3
      value: 65.803
    - type: recall_at_5
      value: 74.119
    task:
      type: Retrieval
  - dataset:
      config: fra-fra
      name: MTEB XPQARetrieval (fr)
      revision: c99d599f0a6ab9b85b065da6f9d94f9cf731679f
      split: test
      type: jinaai/xpqa
    metrics:
    - type: main_score
      value: 77.581
    - type: map_at_1
      value: 46.444
    - type: map_at_10
      value: 72.084
    - type: map_at_100
      value: 73.175
    - type: map_at_1000
      value: 73.193
    - type: map_at_20
      value: 72.77799999999999
    - type: map_at_3
      value: 65.242
    - type: map_at_5
      value: 69.926
    - type: mrr_at_1
      value: 71.82910547396529
    - type: mrr_at_10
      value: 78.66594612923046
    - type: mrr_at_100
      value: 78.97334934049613
    - type: mrr_at_1000
      value: 78.97687021803557
    - type: mrr_at_20
      value: 78.85701141744282
    - type: mrr_at_3
      value: 76.96929238985311
    - type: mrr_at_5
      value: 77.99732977303067
    - type: nauc_map_at_1000_diff1
      value: 49.090956807097804
    - type: nauc_map_at_1000_max
      value: 52.01095354889508
    - type: nauc_map_at_1000_std
      value: -12.182870421711026
    - type: nauc_map_at_100_diff1
      value: 49.091664766684566
    - type: nauc_map_at_100_max
      value: 52.017499797253755
    - type: nauc_map_at_100_std
      value: -12.188342487271528
    - type: nauc_map_at_10_diff1
      value: 48.6619338205362
    - type: nauc_map_at_10_max
      value: 50.93591260329888
    - type: nauc_map_at_10_std
      value: -12.899399261673365
    - type: nauc_map_at_1_diff1
      value: 61.89699552471587
    - type: nauc_map_at_1_max
      value: 22.387748207421946
    - type: nauc_map_at_1_std
      value: -17.139518194308437
    - type: nauc_map_at_20_diff1
      value: 48.72828404686453
    - type: nauc_map_at_20_max
      value: 51.781074586075434
    - type: nauc_map_at_20_std
      value: -12.174270605093136
    - type: nauc_map_at_3_diff1
      value: 53.11509580126934
    - type: nauc_map_at_3_max
      value: 42.1768380145106
    - type: nauc_map_at_3_std
      value: -14.98340833032363
    - type: nauc_map_at_5_diff1
      value: 49.60521390803235
    - type: nauc_map_at_5_max
      value: 49.80360562029127
    - type: nauc_map_at_5_std
      value: -13.900652140457618
    - type: nauc_mrr_at_1000_diff1
      value: 58.10782478654255
    - type: nauc_mrr_at_1000_max
      value: 61.31083013535486
    - type: nauc_mrr_at_1000_std
      value: -9.624904298545921
    - type: nauc_mrr_at_100_diff1
      value: 58.11041683306092
    - type: nauc_mrr_at_100_max
      value: 61.31590199755797
    - type: nauc_mrr_at_100_std
      value: -9.625991053580865
    - type: nauc_mrr_at_10_diff1
      value: 57.883701815695375
    - type: nauc_mrr_at_10_max
      value: 61.36276126424689
    - type: nauc_mrr_at_10_std
      value: -9.495072468420386
    - type: nauc_mrr_at_1_diff1
      value: 60.18176977079093
    - type: nauc_mrr_at_1_max
      value: 59.697615236642555
    - type: nauc_mrr_at_1_std
      value: -9.396133077966779
    - type: nauc_mrr_at_20_diff1
      value: 57.964817434006754
    - type: nauc_mrr_at_20_max
      value: 61.34073539502932
    - type: nauc_mrr_at_20_std
      value: -9.602378876645131
    - type: nauc_mrr_at_3_diff1
      value: 58.44338049427257
    - type: nauc_mrr_at_3_max
      value: 60.92272989411293
    - type: nauc_mrr_at_3_std
      value: -9.928970439416162
    - type: nauc_mrr_at_5_diff1
      value: 58.01513016866578
    - type: nauc_mrr_at_5_max
      value: 61.46805302986586
    - type: nauc_mrr_at_5_std
      value: -9.842227002440984
    - type: nauc_ndcg_at_1000_diff1
      value: 50.99293152828167
    - type: nauc_ndcg_at_1000_max
      value: 56.14232784664811
    - type: nauc_ndcg_at_1000_std
      value: -10.529213072410288
    - type: nauc_ndcg_at_100_diff1
      value: 50.99385944312529
    - type: nauc_ndcg_at_100_max
      value: 56.34825518954588
    - type: nauc_ndcg_at_100_std
      value: -10.398943874846047
    - type: nauc_ndcg_at_10_diff1
      value: 48.51273364357823
    - type: nauc_ndcg_at_10_max
      value: 53.77871849486298
    - type: nauc_ndcg_at_10_std
      value: -11.82105972112472
    - type: nauc_ndcg_at_1_diff1
      value: 60.18176977079093
    - type: nauc_ndcg_at_1_max
      value: 59.697615236642555
    - type: nauc_ndcg_at_1_std
      value: -9.396133077966779
    - type: nauc_ndcg_at_20_diff1
      value: 49.04268319033412
    - type: nauc_ndcg_at_20_max
      value: 55.47011381097071
    - type: nauc_ndcg_at_20_std
      value: -10.486452945493042
    - type: nauc_ndcg_at_3_diff1
      value: 50.95112745400584
    - type: nauc_ndcg_at_3_max
      value: 53.45473828705577
    - type: nauc_ndcg_at_3_std
      value: -13.420699384045728
    - type: nauc_ndcg_at_5_diff1
      value: 50.313156212000074
    - type: nauc_ndcg_at_5_max
      value: 52.78539129309866
    - type: nauc_ndcg_at_5_std
      value: -13.586274096509122
    - type: nauc_precision_at_1000_diff1
      value: -31.13772049254778
    - type: nauc_precision_at_1000_max
      value: 17.2847598361294
    - type: nauc_precision_at_1000_std
      value: 15.497531773816887
    - type: nauc_precision_at_100_diff1
      value: -29.98812263553739
    - type: nauc_precision_at_100_max
      value: 19.048620003227654
    - type: nauc_precision_at_100_std
      value: 15.38499952171958
    - type: nauc_precision_at_10_diff1
      value: -25.33028097412579
    - type: nauc_precision_at_10_max
      value: 26.077919168306853
    - type: nauc_precision_at_10_std
      value: 11.35352933466097
    - type: nauc_precision_at_1_diff1
      value: 60.18176977079093
    - type: nauc_precision_at_1_max
      value: 59.697615236642555
    - type: nauc_precision_at_1_std
      value: -9.396133077966779
    - type: nauc_precision_at_20_diff1
      value: -28.417606311068905
    - type: nauc_precision_at_20_max
      value: 23.958679828637692
    - type: nauc_precision_at_20_std
      value: 14.442021499194205
    - type: nauc_precision_at_3_diff1
      value: -8.127396049790482
    - type: nauc_precision_at_3_max
      value: 37.348067982957076
    - type: nauc_precision_at_3_std
      value: 4.747913619596849
    - type: nauc_precision_at_5_diff1
      value: -16.902418446058395
    - type: nauc_precision_at_5_max
      value: 32.73583852552014
    - type: nauc_precision_at_5_std
      value: 7.031446423850052
    - type: nauc_recall_at_1000_diff1
      value: -14.485978369112514
    - type: nauc_recall_at_1000_max
      value: 78.59123887333172
    - type: nauc_recall_at_1000_std
      value: 90.7384575424963
    - type: nauc_recall_at_100_diff1
      value: 41.47842281590715
    - type: nauc_recall_at_100_max
      value: 67.47271545727422
    - type: nauc_recall_at_100_std
      value: 14.555561992253999
    - type: nauc_recall_at_10_diff1
      value: 33.05308907973924
    - type: nauc_recall_at_10_max
      value: 45.49878918493155
    - type: nauc_recall_at_10_std
      value: -11.560069806810926
    - type: nauc_recall_at_1_diff1
      value: 61.89699552471587
    - type: nauc_recall_at_1_max
      value: 22.387748207421946
    - type: nauc_recall_at_1_std
      value: -17.139518194308437
    - type: nauc_recall_at_20_diff1
      value: 31.305721376453754
    - type: nauc_recall_at_20_max
      value: 51.24817763724019
    - type: nauc_recall_at_20_std
      value: -5.0809908162023145
    - type: nauc_recall_at_3_diff1
      value: 49.27109038342917
    - type: nauc_recall_at_3_max
      value: 37.69188317998447
    - type: nauc_recall_at_3_std
      value: -17.119900758664336
    - type: nauc_recall_at_5_diff1
      value: 42.74501803377967
    - type: nauc_recall_at_5_max
      value: 46.877008503354844
    - type: nauc_recall_at_5_std
      value: -15.704892082115975
    - type: ndcg_at_1
      value: 71.829
    - type: ndcg_at_10
      value: 77.581
    - type: ndcg_at_100
      value: 80.75
    - type: ndcg_at_1000
      value: 81.026
    - type: ndcg_at_20
      value: 79.092
    - type: ndcg_at_3
      value: 72.81
    - type: ndcg_at_5
      value: 74.22999999999999
    - type: precision_at_1
      value: 71.829
    - type: precision_at_10
      value: 17.717
    - type: precision_at_100
      value: 2.031
    - type: precision_at_1000
      value: 0.207
    - type: precision_at_20
      value: 9.399000000000001
    - type: precision_at_3
      value: 44.458999999999996
    - type: precision_at_5
      value: 31.535000000000004
    - type: recall_at_1
      value: 46.444
    - type: recall_at_10
      value: 86.275
    - type: recall_at_100
      value: 98.017
    - type: recall_at_1000
      value: 99.8
    - type: recall_at_20
      value: 90.935
    - type: recall_at_3
      value: 70.167
    - type: recall_at_5
      value: 78.2
    task:
      type: Retrieval
---

<br><br>

<p align="center">
<img src="https://huggingface.co/datasets/jinaai/documentation-images/resolve/main/logo.webp" alt="Jina AI: Your Search Foundation, Supercharged!" width="150px">
</p>


<p align="center">
<b>The embedding model trained by <a href="https://jina.ai/"><b>Jina AI</b></a>.</b>
</p>

<p align="center">
<b>jina-embeddings-v3: Multilingual Embeddings With Task LoRA</b>
</p>

## Quick Start

[Blog](https://jina.ai/news/jina-embeddings-v3-a-frontier-multilingual-embedding-model/#parameter-dimensions) | [Azure](https://azuremarketplace.microsoft.com/en-us/marketplace/apps/jinaai.jina-embeddings-v3-vm) | [AWS SageMaker](https://aws.amazon.com/marketplace/pp/prodview-kdi3xkt62lo32) | [API](https://jina.ai/embeddings)


## Intended Usage & Model Info


`jina-embeddings-v3` is a **multilingual multi-task text embedding model** designed for a variety of NLP applications.
Based on the [Jina-XLM-RoBERTa architecture](https://huggingface.co/jinaai/xlm-roberta-flash-implementation), 
this model supports Rotary Position Embeddings to handle long input sequences up to **8192 tokens**.
Additionally, it features 5 LoRA adapters to generate task-specific embeddings efficiently.

### Key Features:
- **Extended Sequence Length:** Supports up to 8192 tokens with RoPE.
- **Task-Specific Embedding:** Customize embeddings through the `task` argument with the following options:
    - `retrieval.query`: Used for query embeddings in asymmetric retrieval tasks
    - `retrieval.passage`: Used for passage embeddings in asymmetric retrieval tasks
    - `separation`: Used for embeddings in clustering and re-ranking applications
    - `classification`: Used for embeddings in classification tasks
    - `text-matching`: Used for embeddings in tasks that quantify similarity between two texts, such as STS or symmetric retrieval tasks
- **Matryoshka Embeddings**: Supports flexible embedding sizes (`32, 64, 128, 256, 512, 768, 1024`), allowing for truncating embeddings to fit your application.

### Supported Languages:
While the foundation model supports 100 languages, we've focused our tuning efforts on the following 30 languages: 
**Arabic, Bengali, Chinese, Danish, Dutch, English, Finnish, French, Georgian, German, Greek, 
Hindi, Indonesian, Italian, Japanese, Korean, Latvian, Norwegian, Polish, Portuguese, Romanian, 
Russian, Slovak, Spanish, Swedish, Thai, Turkish, Ukrainian, Urdu,** and **Vietnamese.**


> **⚠️ Important Notice:**  
> We fixed a bug in the `encode` function [#60](https://huggingface.co/jinaai/jina-embeddings-v3/discussions/60) where **Matryoshka embedding truncation** occurred *after normalization*, leading to non-normalized truncated embeddings. This issue has been resolved in the latest code revision.  
>  
> If you have encoded data using the previous version and wish to maintain consistency, please use the specific code revision when loading the model: `AutoModel.from_pretrained('jinaai/jina-embeddings-v3', code_revision='da863dd04a4e5dce6814c6625adfba87b83838aa', ...)`


## Usage

**<details><summary>Apply mean pooling when integrating the model.</summary>**
<p>

### Why Use Mean Pooling?

Mean pooling takes all token embeddings from the model's output and averages them at the sentence or paragraph level. 
This approach has been shown to produce high-quality sentence embeddings.

We provide an `encode` function that handles this for you automatically.

However, if you're working with the model directly, outside of the `encode` function, 
you'll need to apply mean pooling manually. Here's how you can do it:


```python
import torch
import torch.nn.functional as F
from transformers import AutoTokenizer, AutoModel


def mean_pooling(model_output, attention_mask):
    token_embeddings = model_output[0]
    input_mask_expanded = (
        attention_mask.unsqueeze(-1).expand(token_embeddings.size()).float()
    )
    return torch.sum(token_embeddings * input_mask_expanded, 1) / torch.clamp(
        input_mask_expanded.sum(1), min=1e-9
    )


sentences = ["How is the weather today?", "What is the current weather like today?"]

tokenizer = AutoTokenizer.from_pretrained("jinaai/jina-embeddings-v3")
model = AutoModel.from_pretrained("jinaai/jina-embeddings-v3", trust_remote_code=True)

encoded_input = tokenizer(sentences, padding=True, truncation=True, return_tensors="pt")
task = 'retrieval.query'
task_id = model._adaptation_map[task]
adapter_mask = torch.full((len(sentences),), task_id, dtype=torch.int32)
with torch.no_grad():
    model_output = model(**encoded_input, adapter_mask=adapter_mask)

embeddings = mean_pooling(model_output, encoded_input["attention_mask"])
embeddings = F.normalize(embeddings, p=2, dim=1)

```

</p>
</details>

The easiest way to start using `jina-embeddings-v3` is with the [Jina Embedding API](https://jina.ai/embeddings/).

Alternatively, you can use `jina-embeddings-v3` directly via Transformers package:
```bash
!pip install transformers torch einops
!pip install 'numpy<2'
```
If you run it on a GPU that support [FlashAttention-2](https://github.com/Dao-AILab/flash-attention). By 2024.9.12, it supports Ampere, Ada, or Hopper GPUs (e.g., A100, RTX 3090, RTX 4090, H100),

```bash
!pip install flash-attn --no-build-isolation
```

```python
from transformers import AutoModel

# Initialize the model
model = AutoModel.from_pretrained("jinaai/jina-embeddings-v3", trust_remote_code=True)

texts = [
    "Follow the white rabbit.",  # English
    "Sigue al conejo blanco.",  # Spanish
    "Suis le lapin blanc.",  # French
    "跟着白兔走。",  # Chinese
    "اتبع الأرنب الأبيض.",  # Arabic
    "Folge dem weißen Kaninchen.",  # German
]

# When calling the `encode` function, you can choose a `task` based on the use case:
# 'retrieval.query', 'retrieval.passage', 'separation', 'classification', 'text-matching'
# Alternatively, you can choose not to pass a `task`, and no specific LoRA adapter will be used.
embeddings = model.encode(texts, task="text-matching")

# Compute similarities
print(embeddings[0] @ embeddings[1].T)
```

By default, the model supports a maximum sequence length of 8192 tokens. 
However, if you want to truncate your input texts to a shorter length, you can pass the `max_length` parameter to the `encode` function:
```python
embeddings = model.encode(["Very long ... document"], max_length=2048)

```

In case you want to use **Matryoshka embeddings** and switch to a different dimension, 
you can adjust it by passing the `truncate_dim` parameter to the `encode` function:
```python
embeddings = model.encode(['Sample text'], truncate_dim=256)
```


The latest version (3.1.0) of [SentenceTransformers](https://github.com/UKPLab/sentence-transformers) also supports `jina-embeddings-v3`:

```bash
!pip install -U sentence-transformers
```

```python
from sentence_transformers import SentenceTransformer

model = SentenceTransformer("jinaai/jina-embeddings-v3", trust_remote_code=True)

task = "retrieval.query"
embeddings = model.encode(
    ["What is the weather like in Berlin today?"],
    task=task,
    prompt_name=task,
)
```

You can fine-tune `jina-embeddings-v3` using [SentenceTransformerTrainer](https://sbert.net/docs/package_reference/sentence_transformer/trainer.html). 
To fine-tune for a specific task, you should set the task before passing the model to the ST Trainer, either during initialization:
```python
model = SentenceTransformer("jinaai/jina-embeddings-v3", trust_remote_code=True, model_kwargs={'default_task': 'classification'})
```
Or afterwards:
```python
model = SentenceTransformer("jinaai/jina-embeddings-v3", trust_remote_code=True)
model[0].default_task = 'classification'
```
This way you can fine-tune the LoRA adapter for the chosen task.

However, If you want to fine-tune the entire model, make sure the main parameters are set as trainable when loading the model:
```python
model = SentenceTransformer("jinaai/jina-embeddings-v3", trust_remote_code=True, model_kwargs={'lora_main_params_trainable': True})
```
This will allow fine-tuning the whole model instead of just the LoRA adapters.


**<details><summary>ONNX Inference.</summary>**
<p>

You can use ONNX for efficient inference with `jina-embeddings-v3`:
```python
import onnxruntime
import numpy as np
from transformers import AutoTokenizer, PretrainedConfig

# Mean pool function
def mean_pooling(model_output: np.ndarray, attention_mask: np.ndarray):
    token_embeddings = model_output
    input_mask_expanded = np.expand_dims(attention_mask, axis=-1)
    input_mask_expanded = np.broadcast_to(input_mask_expanded, token_embeddings.shape)
    sum_embeddings = np.sum(token_embeddings * input_mask_expanded, axis=1)
    sum_mask = np.clip(np.sum(input_mask_expanded, axis=1), a_min=1e-9, a_max=None)
    return sum_embeddings / sum_mask

# Load tokenizer and model config
tokenizer = AutoTokenizer.from_pretrained('jinaai/jina-embeddings-v3')
config = PretrainedConfig.from_pretrained('jinaai/jina-embeddings-v3')

# Tokenize input
input_text = tokenizer('sample text', return_tensors='np')

# ONNX session
model_path = 'jina-embeddings-v3/onnx/model.onnx'
session = onnxruntime.InferenceSession(model_path)

# Prepare inputs for ONNX model
task_type = 'text-matching'
task_id = np.array(config.lora_adaptations.index(task_type), dtype=np.int64)
inputs = {
    'input_ids': input_text['input_ids'],
    'attention_mask': input_text['attention_mask'],
    'task_id': task_id
}

# Run model
outputs = session.run(None, inputs)[0]

# Apply mean pooling and normalization to the model outputs
embeddings = mean_pooling(outputs, input_text["attention_mask"])
embeddings = embeddings / np.linalg.norm(embeddings, ord=2, axis=1, keepdims=True)
```

</p>
</details>


## Contact

Join our [Discord community](https://discord.jina.ai) and chat with other community members about ideas.

## License

`jina-embeddings-v3` is listed on AWS & Azure. If you need to use it beyond those platforms or on-premises within your company, note that the models is licensed under CC BY-NC 4.0. For commercial usage inquiries, feel free to [contact us](https://jina.ai/contact-sales/).

## Citation

If you find `jina-embeddings-v3` useful in your research, please cite the following paper:

```bibtex
@misc{sturua2024jinaembeddingsv3multilingualembeddingstask,
      title={jina-embeddings-v3: Multilingual Embeddings With Task LoRA}, 
      author={Saba Sturua and Isabelle Mohr and Mohammad Kalim Akram and Michael Günther and Bo Wang and Markus Krimmel and Feng Wang and Georgios Mastrapas and Andreas Koukounas and Andreas Koukounas and Nan Wang and Han Xiao},
      year={2024},
      eprint={2409.10173},
      archivePrefix={arXiv},
      primaryClass={cs.CL},
      url={https://arxiv.org/abs/2409.10173}, 
}

```
