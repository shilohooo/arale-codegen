CREATE TABLE t_pgsql_generate_test (
    id_col SERIAL PRIMARY KEY NOT NULL,
    small_int_col SMALLINT NOT NULL,
    small_serial_col SMALLSERIAL NOT NULL ,
    integer_type_col INTEGER NOT NULL,
    big_int_col BIGINT NOT NULL,
    real_num_col REAL NOT NULL,
    double_num_col DOUBLE PRECISION,
    decimal_num_col DECIMAL(10, 2),
    numeric_type_col NUMERIC(15, 5),
    bool_type_col BOOLEAN,
    char_type_col CHAR(5) NOT NULL,
    varchar_type_col VARCHAR(50),
    text_type_col TEXT,
    date_type_col DATE ,
    time_type_col TIME,
    timestamp_type_col TIMESTAMP,
    timestamptz_type_col TIMESTAMP WITH TIME ZONE,
    interval_type_col INTERVAL,
    bytea_type_col BYTEA ,
    inet_type_col INET,
    cidr_type_col CIDR,
    macaddr_type_col MACADDR,
    uuid_type_col UUID,
    json_type_col JSON,
    jsonb_type_col JSONB,
    int_array_col INTEGER[],
    text_array_col TEXT[],
    point_type_col POINT,
    line_type_col LINE,
    lseg_type_col LSEG,
    box_type_col BOX,
    path_type_col PATH,
    polygon_type_col POLYGON,
    circle_type_col CIRCLE,
    money_type_col MONEY,
    bit_type_col BIT(8),
    varbit_type_col BIT(1),
    tsvector_type_col TSVECTOR,
    tsquery_type_col TSQUERY
);