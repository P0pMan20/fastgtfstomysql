# A fast GTFS-Static to MySQL importer

## Key Optimisations

- Multithreading
- InnoDB does a COMMIT for each INSERT as soon as it is received by [default](https://dev.mysql.com/doc/refman/5.7/en/optimizing-innodb-bulk-data-loading.html), so by wrapping the set of inserts in a TRANSACTION the overhead of doing a log flush to disk for every insert is avoided.

The Carins GTFS schedule [found here](https://translink.com.au/about-translink/open-data/gtfs-rt) is used for testing.