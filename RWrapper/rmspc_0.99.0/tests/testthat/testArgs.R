test_that("We check if GRanges argument type is correct ", {
  expect_error(!is.logical(GRanges))
})
test_that("We check if the type of the directoryGRangesInput argument is correct ", {
    expect_error(!dir.exists(directoryGRangesInput))
})
test_that("We check if the keep argument is correct ", {
    expect_error(!is.logical(keep))
})
